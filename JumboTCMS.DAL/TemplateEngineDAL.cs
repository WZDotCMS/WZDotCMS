﻿/*
 * 程序中文名称: 将博内容管理系统通用版
 * 
 * 程序英文名称: JumboTCMS
 * 
 * 程序版本: 5.2.X
 * 
 * 程序编写: 随风缘 (定制开发请联系：jumbot114#126.com,不接受免费的技术答疑,请见谅)
 * 
 * 官方网站: http://www.jumbotcms.net/
 * 
 * 商业服务: http://www.jumbotcms.net/about/service.html
 * 
 */

using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using System.Web;
using JumboTCMS.Utils;
using JumboTCMS.Entity;
using JumboTCMS.DBUtility;

namespace JumboTCMS.DAL
{
    /// <summary>
    /// 生成html主文件
    /// </summary>
    public class TemplateEngineDAL : Common
    {
        public TemplateEngineDAL()
        {
            base.SetupSystemDate();
            this.MainChannel = new JumboTCMS.DAL.Normal_ChannelDAL().GetEntity("0");
        }
        public TemplateEngineDAL(string _channelid)
        {
            base.SetupSystemDate();
            if (_channelid == string.Empty)
                _channelid = "0";
            this.MainChannel = new JumboTCMS.DAL.Normal_ChannelDAL().GetEntity(_channelid);
            //_Channel = Channel;

        }
        public JumboTCMS.Entity.Normal_Channel MainChannel;//页面频道实体
        public JumboTCMS.Entity.Normal_Channel ThisChannel;//模块频道实体
        private string _pagetitle, _pagekeywords, _pagedescription, _pagenav;
        private bool m_isHtml;
        /// <summary>
        /// 页面默认标题
        /// </summary>
        public string PageTitle
        {
            get { return this._pagetitle; }
            set { this._pagetitle = value; }
        }
        /// <summary>
        /// 页面默认关键字
        /// </summary>
        public string PageKeywords
        {
            get { return this._pagekeywords; }
            set { this._pagekeywords = value; }
        }
        /// <summary>
        /// 页面默认简介
        /// </summary>
        public string PageDescription
        {
            get { return this._pagedescription; }
            set { this._pagedescription = value; }
        }
        /// <summary>
        /// 页面链接导航
        /// </summary>
        public string PageNav
        {
            get { return this._pagenav; }
            set { this._pagenav = value; }
        }
        /// <summary>
        /// 是否缓存页面
        /// </summary>
        public bool IsHtml
        {
            get { return this.m_isHtml; }
            set { this.m_isHtml = value; }
        }

        /// <summary>
        /// 判断最终页面是否静态(频道ID只能从外部传入，不支持跨频道)
        /// </summary>
        /// <returns></returns>
        public bool PageIsHtml()
        {
            if (this.MainChannel.Id == "0")//没指定频道
                return (site.IsHtml);
            else
                return (site.IsHtml && this.MainChannel.IsHtml);
        }
        private string p__getNeightor(bool isHtml, string channelType, string channelId, string classId, string contentId)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                StringBuilder sb = new StringBuilder();
                _doh.Reset();
                _doh.SqlCmd = "SELECT TOP 1 [Id],[Title],[FirstPage] FROM [jcms_module_" + channelType + "] WHERE [ChannelId] = " + channelId + " And [IsPass]=1 AND [Id]<" + contentId + " order By [Id] DESC";
                DataTable dtContent = _doh.GetDataTable();
                if (dtContent.Rows.Count > 0)
                    sb.Append("<div class=\"l\"><span title=\"往前\">&laquo;</span> <a title=\"" + dtContent.Rows[0]["Title"].ToString() + "\" href=\"" + dtContent.Rows[0]["FirstPage"].ToString() + "\">" + dtContent.Rows[0]["Title"].ToString() + "</a></div>\r\n");
                else
                    sb.Append("<div class=\"l\"><span title=\"返回列表\">&laquo;</span> <a title=\"返回列表\" href=\"" + Go2Class(1, isHtml, channelId, classId, false) + "\">返回列表</a></div>\r\n");

                _doh.Reset();
                _doh.SqlCmd = "SELECT TOP 1 [Id],[Title],[FirstPage] FROM [jcms_module_" + channelType + "] WHERE [ChannelId] = " + channelId + " AND [IsPass]=1 AND [Id]>" + contentId + " order By [Id] ASC";
                dtContent = _doh.GetDataTable();
                if (dtContent.Rows.Count > 0)
                    sb.Append("<div class=\"r\"><span title=\"往后\">&raquo;</span> <a title=\"" + dtContent.Rows[0]["Title"].ToString() + "\" href=\"" + dtContent.Rows[0]["FirstPage"].ToString() + "\">" + dtContent.Rows[0]["Title"].ToString() + "</a></div>\r\n");
                else
                    sb.Append("<div class=\"r\"><span title=\"返回列表\">&raquo;</span> <a title=\"返回列表\" href=\"" + Go2Class(1, isHtml, channelId, classId, false) + "\">返回列表</a></div>\r\n");
                dtContent.Clear();
                dtContent.Dispose();
                return sb.ToString();
            }

        }

        /// <summary>
        /// 替换专题标签
        /// </summary>
        /// <param name="_pagestr"></param>
        public void ReplaceSpecialTag(ref string _pagestr, string _SpecialId)
        {
            new JumboTCMS.DAL.Normal_SpecialDAL().ExecuteTags(ref _pagestr, _SpecialId);
        }
        /// <summary>
        /// 替换频道标签
        /// </summary>
        /// <param name="_pagestr"></param>
        public void ReplaceChannelTag(ref string _pagestr, string _ChannelId)
        {
            new JumboTCMS.DAL.Normal_ChannelDAL().ExecuteTags(ref _pagestr, _ChannelId);
        }
        /// <summary>
        /// 判断内容阅读权限(频道ID只能从外部传入，不支持跨频道)
        /// 假设内容ID和栏目ID都已经正确
        /// </summary>
        /// <param name="_contentid"></param>
        /// <param name="_classid"></param>
        /// <returns></returns>
        public bool CanReadContent(string _contentid, string _classid)
        {
            if (Cookie.GetValue(site.CookiePrev + "admin") != null)//管理员直接可以看
                return true;
            int _usergroup = 0;
            if (Cookie.GetValue(site.CookiePrev + "user") != null)
                _usergroup = Str2Int(Cookie.GetValue(site.CookiePrev + "user", "groupid"));
            int _ContentReadGroup, _ClassReadGroup;
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "id=" + _contentid;
                _ContentReadGroup = Str2Int(_doh.GetField("jcms_module_" + this.MainChannel.Type, "ReadGroup").ToString());
                _doh.Reset();
                _doh.ConditionExpress = "id=" + _classid;
                _ClassReadGroup = Str2Int(_doh.GetField("jcms_normal_class", "ReadGroup").ToString());
            }
            if (_ContentReadGroup > -1)//说明不是继承栏目
            {
                if (_ContentReadGroup > _usergroup)
                    return false;
                else
                    return true;
            }
            else
            {
                if (_ClassReadGroup > _usergroup)
                    return false;
                else
                    return true;
            }
        }
        /// <summary>
        /// 替换栏目标签
        /// </summary>
        /// <param name="_pagestr"></param>
        public void ReplaceClassTag(ref string _pagestr, string _ClassId)
        {
            executeTag_Class(ref _pagestr, _ClassId);

        }
        /// <summary>
        /// 替换单页内容标签(频道ID从外部传入)
        /// </summary>
        /// <param name="_pagestr"></param>
        public void ReplaceContentTag(ref string _pagestr, string _ContentId)
        {
            executeTag_Content(ref _pagestr, _ContentId);
        }
        /// <summary>
        /// 解析栏目循环标签(不支持跨频道)
        /// </summary>
        /// <param name="_pagestr">原始内容</param>
        /// <returns></returns>
        public void ReplaceChannelClassLoopTag(ref string _pagestr)
        {
            replaceTag_ChannelClassLoop(ref _pagestr);
            replaceTag_ChannelClass2Loop(ref _pagestr);
            replaceTag_ClassTree(ref _pagestr);//2012-02-24新增标签
        }
        /// <summary>
        /// 解析内容循环标签
        /// </summary>
        /// <param name="_pagestr">原始内容</param>
        /// <returns></returns>
        public void ReplaceContentLoopTag(ref string _pagestr)
        {
            replaceTag_ContentLoop(ref _pagestr);
        }
        /// <summary>
        /// 解析shtml标签
        /// </summary>
        /// <param name="_pagestr"></param>
        public void ReplaceShtmlTag(ref string _pagestr)
        {
            replaceTag_Shtml(ref _pagestr);
        }
        /// <summary>
        /// 解析站点信息
        /// </summary>
        /// <param name="_pagestr">原始内容</param>
        /// <returns></returns>
        public void ReplaceSiteTags(ref string _pagestr)
        {
            replaceTag_Include(ref _pagestr);
            replaceTag_SiteConfig(ref _pagestr);
            replaceTag_GetRemoteWeb(ref _pagestr);
        }
        /// <summary>
        /// 解析公共标签
        /// </summary>
        /// <param name="_pagestr">原始内容</param>
        /// <returns></returns>
        public void ReplacePublicTag(ref string _pagestr)
        {
            replaceTag_Include(ref _pagestr);
            replaceTag_SiteConfig(ref _pagestr);
            replaceTag_GetRemoteWeb(ref _pagestr);
            replaceTag_ChannelLoop(ref _pagestr);
        }
        /// <summary>
        /// 替换html包含标签(解析次序：2)
        /// </summary>
        /// <param name="_pagestr"></param>
        private void replaceTag_Include(ref string _pagestr)
        {
            string RegexString = "<jcms:include (?<tagcontent>.*?) />";
            string[] _tagcontent = JumboTCMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tagcontent", false);
            if (_tagcontent.Length > 0)//标签存在
            {
                string _loopbody = string.Empty;
                string _replacestr = string.Empty;
                string _viewstr = string.Empty;
                string _tagfile = string.Empty;
                for (int i = 0; i < _tagcontent.Length; i++)
                {
                    _loopbody = "<jcms:include " + _tagcontent[i] + " />";
                    _tagfile = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "file");
                    if (!_tagfile.StartsWith("/") && !_tagfile.StartsWith("~/"))
                        _tagfile = site.Dir + "_data/html/" + _tagfile;
                    if (JumboTCMS.Utils.DirFile.FileExists(_tagfile))
                        _replacestr = JumboTCMS.Utils.DirFile.ReadFile(_tagfile);
                    else
                        _replacestr = "";
                    _pagestr = _pagestr.Replace(_loopbody, _replacestr);
                }
            }
        }
        /// <summary>
        /// 替换shtml包含标签
        /// </summary>
        /// <param name="_pagestr"></param>
        private void replaceTag_Shtml(ref string _pagestr)
        {
            string RegexString = "<!--#include (?<tagcontent>.*?) -->";
            string[] _tagcontent = JumboTCMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tagcontent", false);
            if (_tagcontent.Length > 0)//标签存在
            {
                string _loopbody = string.Empty;
                string _replacestr = string.Empty;
                string _viewstr = string.Empty;
                string _tagfile = string.Empty;
                for (int i = 0; i < _tagcontent.Length; i++)
                {
                    _loopbody = "<!--#include " + _tagcontent[i] + " -->";
                    _tagfile = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "virtual");
                    if (JumboTCMS.Utils.DirFile.FileExists(_tagfile))
                        _replacestr = JumboTCMS.Utils.DirFile.ReadFile(_tagfile);
                    else
                        _replacestr = "";
                    _pagestr = _pagestr.Replace(_loopbody, _replacestr);
                }
            }
        }
        /// <summary>
        /// 替换公共标签(解析次序：3)
        /// </summary>
        /// <param name="_pagestr"></param>
        private void replaceTag_SiteConfig(ref string _pagestr)
        {
            _pagestr = _pagestr.Replace("{site.Dir}", site.Dir);//老版本
            _pagestr = _pagestr.Replace("{site.Url}", site.Url);//老版本
            _pagestr = _pagestr.Replace("<jcms:site.keywords/>", site.Keywords);
            _pagestr = _pagestr.Replace("<jcms:site.description/>", site.Description);
            _pagestr = _pagestr.Replace("<jcms:site.author/>", "jumbot,随风缘");
            _pagestr = _pagestr.Replace("<jcms:site.url/>", site.Url);
            _pagestr = _pagestr.Replace("<jcms:site.dir/>", site.Dir);
            _pagestr = _pagestr.Replace("<jcms:site.home/>", Go2Site(site.IsHtml));

            _pagestr = _pagestr.Replace("<jcms:site.name/>", site.Name);
            _pagestr = _pagestr.Replace("<jcms:site.name2/>", site.Name2);
            _pagestr = _pagestr.Replace("{site.ICP}", site.ICP);
            _pagestr = _pagestr.Replace("{site.ForumUrl}", site.ForumUrl);
            if (this.MainChannel.Id != "0")
                _pagestr = _pagestr.Replace("<jcms:site.page.basehref/>", site.Url + site.Dir + this.MainChannel.Dir + "/");
            else
                _pagestr = _pagestr.Replace("<jcms:site.page.basehref/>", site.Url + site.Dir);
            _pagestr = _pagestr.Replace("<jcms:site.page.nav/>", this.PageNav);
            _pagestr = _pagestr.Replace("<jcms:site.page.title/>", this.PageTitle);
            _pagestr = _pagestr.Replace("<jcms:site.page.keywords/>", this.PageKeywords);
            _pagestr = _pagestr.Replace("<jcms:site.page.description/>", this.PageDescription);
            _pagestr = _pagestr.Replace("<jcms:site.version/>", site.Version);

        }
        /// <summary>
        /// 替换远程网页内容(解析次序：3)
        /// </summary>
        /// <param name="_pagestr"></param>
        private void replaceTag_GetRemoteWeb(ref string _pagestr)
        {
            string RegexString = "<jcms:remoteweb (?<tagcontent>.*?) />";
            string[] _tagcontent = JumboTCMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tagcontent", false);
            if (_tagcontent.Length > 0)//标签存在
            {
                string _loopbody = string.Empty;
                string _replacestr = string.Empty;
                string _viewstr = string.Empty;
                string _tagurl = string.Empty;
                string _tagcharset = string.Empty;
                System.Text.Encoding encodeType = System.Text.Encoding.Default;
                for (int i = 0; i < _tagcontent.Length; i++)
                {
                    _loopbody = "<jcms:remoteweb " + _tagcontent[i] + " />";
                    _tagurl = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "url");
                    _tagcharset = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "charset").ToLower();
                    switch (_tagcharset)
                    {
                        case "unicode":
                            encodeType = System.Text.Encoding.Unicode;
                            break;
                        case "utf-8":
                            encodeType = System.Text.Encoding.UTF8;
                            break;
                        case "gb2312":
                            encodeType = System.Text.Encoding.GetEncoding("GB2312");
                            break;
                        case "gbk":
                            encodeType = System.Text.Encoding.GetEncoding("GB2312");
                            break;
                        default:
                            encodeType = System.Text.Encoding.Default;
                            break;
                    }
                    JumboTCMS.Common.NewsCollection nc = new JumboTCMS.Common.NewsCollection();
                    _replacestr = nc.GetHttpPage(_tagurl, 8000, encodeType);
                    _pagestr = _pagestr.Replace(_loopbody, _replacestr);
                }
            }
        }
        /// <summary>
        /// 替换注释标签
        /// </summary>
        /// <param name="_pagestr">已取到的模板内容</param>
        private void replaceTag_NoShow(ref string _pagestr)
        {
            System.Collections.ArrayList TagArray = JumboTCMS.Utils.Strings.GetHtmls(_pagestr, "<!--~", "~-->", false, false);
            if (TagArray.Count > 0)//标签存在
            {
                string TempStr = string.Empty;
                string ReplaceStr;
                for (int i = 0; i < TagArray.Count; i++)
                {
                    TempStr = "<!--~" + TagArray[i].ToString() + "~-->";
                    ReplaceStr = "";
                    _pagestr = _pagestr.Replace(TempStr, ReplaceStr);
                }
            }
        }
        /// <summary>
        /// 替换循环频道标签(将频道信息赋值给循环体)(解析次序：5)
        /// </summary>
        /// <param name="_pagestr"></param>
        private void replaceTag_ChannelLoop(ref string _pagestr)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string RegexString = "<jcms:channelloop (?<tagcontent>.*?)>(?<tempstr>.*?)</jcms:channelloop>";
                string[] _tagcontent = JumboTCMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tagcontent", false);
                string[] _tempstr = JumboTCMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tempstr", false);
                if (_tagcontent.Length > 0)//标签存在
                {
                    string _loopbody = string.Empty;
                    string _replacestr = string.Empty;
                    string _viewstr = string.Empty;
                    string _tagrepeatnum, _tagisnav, _tagselectids, _tagorderfield, _tagordertype, _tagwherestr = string.Empty;
                    for (int i = 0; i < _tagcontent.Length; i++)
                    {
                        _loopbody = "<jcms:channelloop " + _tagcontent[i] + ">" + _tempstr[i] + "</jcms:channelloop>";
                        _tagisnav = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "isnav");
                        if (_tagisnav == "") _tagisnav = "0";
                        _tagrepeatnum = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "repeatnum");
                        _tagselectids = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "selectids");
                        _tagorderfield = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "orderfield");
                        if (_tagorderfield == "") _tagorderfield = "pid";
                        _tagordertype = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "ordertype");
                        if (_tagordertype == "") _tagordertype = "asc";
                        _tagwherestr = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "wherestr");
                        _doh.Reset();
                        string _sql = "select top " + _tagrepeatnum + " * FROM [jcms_normal_channel] WHERE [Enabled]=1";
                        if (_tagisnav == "1")
                            _sql += " AND [IsNav]=1";
                        if (_tagselectids != "")
                            _sql += " AND id in (" + _tagselectids.Replace("|", ",") + ")";
                        if (_tagwherestr != "")
                            _sql += " and " + _tagwherestr;
                        _sql += " ORDER BY " + _tagorderfield + " " + _tagordertype;
                        _doh.SqlCmd = _sql;
                        DataTable _dt = _doh.GetDataTable();
                        StringBuilder sb = new StringBuilder();
                        for (int j = 0; j < _dt.Rows.Count; j++)
                        {
                            _viewstr = _tempstr[i];
                            DataRow dr = _dt.Rows[j];
                            JumboTCMS.Entity.Normal_Channel _Channel = new JumboTCMS.DAL.Normal_ChannelDAL().GetEntity(dr);
                            new JumboTCMS.DAL.Normal_ChannelDAL().ExecuteTags(ref _viewstr, _Channel);
                            //_viewstr = _viewstr.Replace("{$ChannelId}", _dt.Rows[j]["Id"].ToString());
                            //_viewstr = _viewstr.Replace("{$ChannelName}", _dt.Rows[j]["Title"].ToString());
                            sb.Append(_viewstr);
                        }
                        _pagestr = _pagestr.Replace(_loopbody, sb.ToString());
                        _dt.Clear();
                        _dt.Dispose();
                    }
                }
            }
        }
        /// <summary>
        /// 替换频道栏目循环标签(不支持跨频道)
        /// </summary>
        /// <param name="_pagestr"></param>
        private void replaceTag_ChannelClassLoop(ref string _pagestr)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string RegexString = "<jcms:classloop (?<tagcontent>.*?)>(?<tempstr>.*?)</jcms:classloop>";
                string[] _tagcontent = JumboTCMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tagcontent", false);
                string[] _tempstr = JumboTCMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tempstr", false);
                if (_tagcontent.Length > 0)//标签存在
                {
                    string _loopbody = string.Empty;
                    string _replacestr = string.Empty;
                    string _viewstr = string.Empty;
                    string _tagrepeatnum, _tagselectids, _tagdepth, _tagparentid, _tagwherestr, _tagorderfield, _tagordertype, _hascontent = string.Empty;
                    for (int i = 0; i < _tagcontent.Length; i++)
                    {
                        _loopbody = "<jcms:classloop " + _tagcontent[i] + ">" + _tempstr[i] + "</jcms:classloop>";
                        string _tagchannelid = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "channelid");
                        _tagrepeatnum = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "repeatnum");
                        _tagselectids = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "selectids");
                        _tagdepth = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "depth");
                        _tagparentid = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "parentid");
                        _tagwherestr = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "wherestr");
                        _tagorderfield = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "orderfield");
                        if (_tagorderfield == "") _tagorderfield = "code";
                        _tagordertype = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "ordertype");
                        if (_tagordertype == "") _tagordertype = "asc";
                        if (_tagrepeatnum == "") _tagrepeatnum = "0";
                        _hascontent = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "hascontent");
                        if (_hascontent == "") _hascontent = "0";
                        if (_tagdepth == "") _tagdepth = "0";
                        string pStr = " [Id],[Title],[Info],[TopicNum],[Code],[ChannelId] FROM [jcms_normal_class] WHERE [IsOut]=0 AND [ChannelId]=" + _tagchannelid;
                        string oStr = " ORDER BY code asc";
                        if (_tagorderfield.ToLower() != "code")
                            oStr = " ORDER BY " + _tagorderfield + " " + _tagordertype + ",code asc";
                        else
                            oStr = " ORDER BY " + _tagorderfield + " " + _tagordertype;
                        _doh.Reset();
                        if (_tagdepth != "-1" && _tagdepth != "0")
                            pStr += " AND Len(Code)=" + (Str2Int(_tagdepth, 0) * 4);
                        if (_tagrepeatnum != "0")
                            pStr = " top " + _tagrepeatnum + pStr;
                        if (_tagparentid != "" && _tagparentid != "0")
                            pStr += " AND [ParentId]=" + _tagparentid;
                        if (_hascontent == "1")
                            pStr += " AND [TopicNum]>0";
                        if (_tagwherestr != "")
                            pStr += " AND " + _tagwherestr.Replace("小于", "<").Replace("大于", ">").Replace("不等于", "<>");
                        if (_tagselectids != "")
                            pStr += " AND [id] IN (" + _tagselectids.Replace("|", ",") + ")";

                        _doh.SqlCmd = "select" + pStr + oStr;
                        DataTable _dt = _doh.GetDataTable();
                        StringBuilder sb = new StringBuilder();
                        for (int j = 0; j < _dt.Rows.Count; j++)
                        {
                            if (_tagdepth == "-1")
                            {
                                _doh.Reset();
                                _doh.ConditionExpress = "[IsOut]=0 AND [ChannelId]=" + _tagchannelid + " AND [ParentId]=" + _dt.Rows[j]["Id"].ToString();
                                int countNum = _doh.Count("jcms_normal_class");
                                if (countNum > 1)//表示非末级栏目，直接跳过
                                    continue;
                            }
                            _viewstr = _tempstr[i];
                            _viewstr = _viewstr.Replace("{$ClassNO}", (j + 1).ToString());
                            executeTag_Class(ref _viewstr, _dt.Rows[j]["Id"].ToString());
                            sb.Append(_viewstr);
                        }
                        _pagestr = _pagestr.Replace(_loopbody, sb.ToString());
                        _dt.Clear();
                        _dt.Dispose();
                    }
                }
            }

        }
        /// <summary>
        /// 替换频道栏目循环标签(不支持跨频道)
        /// </summary>
        /// <param name="_pagestr"></param>
        private void replaceTag_ChannelClass2Loop(ref string _pagestr)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string RegexString = "<jcms:class2loop (?<tagcontent>.*?)>(?<tempstr>.*?)</jcms:class2loop>";
                string[] _tagcontent = JumboTCMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tagcontent", false);
                string[] _tempstr = JumboTCMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tempstr", false);
                if (_tagcontent.Length > 0)//标签存在
                {
                    string _loopbody = string.Empty;
                    string _replacestr = string.Empty;
                    string _viewstr = string.Empty;
                    string _tagrepeatnum, _tagselectids, _tagdepth, _tagparentid, _tagwherestr, _tagorderfield, _tagordertype, _hascontent = string.Empty;
                    for (int i = 0; i < _tagcontent.Length; i++)
                    {
                        _loopbody = "<jcms:class2loop " + _tagcontent[i] + ">" + _tempstr[i] + "</jcms:class2loop>";
                        string _tagchannelid = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "channelid");
                        _tagrepeatnum = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "repeatnum");
                        _tagselectids = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "selectids");
                        _tagdepth = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "depth");
                        _tagparentid = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "parentid");
                        _tagwherestr = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "wherestr");
                        _tagorderfield = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "orderfield");
                        if (_tagorderfield == "") _tagorderfield = "code";
                        _tagordertype = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "ordertype");
                        if (_tagordertype == "") _tagordertype = "asc";
                        if (_tagrepeatnum == "") _tagrepeatnum = "0";
                        _hascontent = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "hascontent");
                        if (_hascontent == "") _hascontent = "0";
                        if (_tagdepth == "") _tagdepth = "0";
                        string pStr = " [Id],[Title],[Info],[TopicNum],[Code],[ChannelId] FROM [jcms_normal_class] WHERE [IsOut]=0 AND [ChannelId]=" + _tagchannelid;
                        string oStr = " ORDER BY code asc";
                        if (_tagorderfield.ToLower() != "code")
                            oStr = " ORDER BY " + _tagorderfield + " " + _tagordertype + ",code asc";
                        else
                            oStr = " ORDER BY " + _tagorderfield + " " + _tagordertype;
                        _doh.Reset();
                        if (_tagdepth != "-1" && _tagdepth != "0")
                            pStr += " AND Len(Code)=" + (Str2Int(_tagdepth, 0) * 4);
                        if (_tagrepeatnum != "0")
                            pStr = " top " + _tagrepeatnum + pStr;
                        if (_tagparentid != "" && _tagparentid != "0")
                            pStr += " AND [ParentId]=" + _tagparentid;
                        if (_hascontent == "1")
                            pStr += " AND [TopicNum]>0";
                        if (_tagwherestr != "")
                            pStr += " AND " + _tagwherestr.Replace("小于", "<").Replace("大于", ">").Replace("不等于", "<>");
                        if (_tagselectids != "")
                            pStr += " AND [id] IN (" + _tagselectids.Replace("|", ",") + ")";

                        _doh.SqlCmd = "select" + pStr + oStr;
                        DataTable _dt = _doh.GetDataTable();
                        StringBuilder sb = new StringBuilder();
                        for (int j = 0; j < _dt.Rows.Count; j++)
                        {
                            if (_tagdepth == "-1")
                            {
                                _doh.Reset();
                                _doh.ConditionExpress = "[IsOut]=0 AND [ChannelId]=" + _tagchannelid + " AND [ParentId]=" + _dt.Rows[j]["Id"].ToString();
                                int countNum = _doh.Count("jcms_normal_class");
                                if (countNum > 1)//表示非末级栏目，直接跳过
                                    continue;
                            }
                            _viewstr = _tempstr[i];
                            _viewstr = _viewstr.Replace("{$Class2NO}", (j + 1).ToString());
                            executeTag_Class2(ref _viewstr, _dt.Rows[j]["Id"].ToString());
                            sb.Append(_viewstr);
                        }
                        _pagestr = _pagestr.Replace(_loopbody, sb.ToString());
                        _dt.Clear();
                        _dt.Dispose();
                    }
                }
            }

        }
        /// <summary>
        /// 解析栏目树标签(2012-02-24新增标签)
        /// </summary>
        /// <param name="_pagestr"></param>
        private void replaceTag_ClassTree(ref string _pagestr)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string RegexString = "<jcms:classtree (?<tagcontent>.*?)>(?<tempstr>.*?)</jcms:classtree>";
                string[] _tagcontent = JumboTCMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tagcontent", false);
                string[] _tempstr = JumboTCMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tempstr", false);
                if (_tagcontent.Length > 0)//标签存在
                {
                    string _loopbody = string.Empty;
                    string _replacestr = string.Empty;
                    string _viewstr = string.Empty;
                    string _tagchannelid, _tagclassid = string.Empty;
                    bool _tagincludechild = false;
                    for (int i = 0; i < _tagcontent.Length; i++)
                    {
                        _loopbody = "<jcms:classtree " + _tagcontent[i] + ">" + _tempstr[i] + "</jcms:classtree>";
                        _tagchannelid = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "channelid");
                        _tagclassid = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "classid");
                        if (_tagclassid == "") _tagclassid = "0";
                        _tagincludechild = (JumboTCMS.Utils.Strings.AttributeValue(_tagcontent[i], "includechild") != "0");

                        string _TemplateContent = _tempstr[i];
                        JumboTCMS.TEngine.TemplateManager manager = JumboTCMS.TEngine.TemplateManager.FromString(_TemplateContent);
                        manager.SetValue("tree", (new JumboTCMS.DAL.Normal_ClassDAL().GetClassTree(_tagchannelid, _tagclassid, _tagincludechild)));
                        _replacestr = manager.Process();
                        _pagestr = _pagestr.Replace(_loopbody, _replacestr);
                    }
                }
            }

        }
        /// <summary>
        /// 替换内容循环标签
        /// </summary>
        /// <param name="_pagestr"></param>
        private void replaceTag_ContentLoop(ref string _pagestr)
        {
            string RegexString = "<jcms:contentloop (?<tagcontent>.*?)>(?<tempstr>.*?)</jcms:contentloop>";
            string[] _tagcontent = JumboTCMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tagcontent", false);
            string[] _tempstr = JumboTCMS.Utils.Strings.GetRegValue(_pagestr, RegexString, "tempstr", false);
            if (_tagcontent.Length > 0)//标签存在
            {
                string _loopbody = string.Empty;
                string _replacestr = string.Empty;
                string _viewstr = string.Empty;
                for (int i = 0; i < _tagcontent.Length; i++)
                {
                    _loopbody = "<jcms:contentloop " + _tagcontent[i] + ">" + _tempstr[i] + "</jcms:contentloop>";
                    _replacestr = getContentList_RL(_tagcontent[i], _tempstr[i].Replace("<#foreach content>", "<#foreach collection=\"${contents}\" var=\"field\" index=\"i\">"));
                    _pagestr = _pagestr.Replace(_loopbody, _replacestr);
                }
            }
        }
        /// <summary>
        /// 提取列表供列表标签使用
        /// </summary>
        /// <param name="Parameter"></param>

        /// <returns></returns>
        private string getContentList_RL(string _tagcontent, string _tempstr)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string _viewstr = string.Empty;
                string _tagrepeatnum = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent, "repeatnum");
                if (_tagrepeatnum == "") _tagrepeatnum = "10";
                string _tagchannelid = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent, "channelid");
                if (_tagchannelid == "") _tagchannelid = "0";
                string _tagchanneltype = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent, "channeltype");
                if (_tagchanneltype == "") _tagchanneltype = "article";
                string _tagclassid = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent, "classid");
                if (_tagclassid == "") _tagclassid = "0";
                string _tagfields = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent, "fields");
                string _tagorderfield = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent, "orderfield");
                if (_tagorderfield == "") _tagorderfield = "adddate";
                string _tagordertype = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent, "ordertype");
                if (_tagordertype == "") _tagordertype = "desc";
                string _tagistop = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent, "istop");
                if (_tagistop == "") _tagistop = "0";
                string _tagisfocus = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent, "isfocus");
                if (_tagisfocus == "") _tagisfocus = "0";
                string _tagisimg = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent, "isimg");
                if (_tagisimg == "") _tagisimg = "0";
                string _tagtimerange = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent, "timerange");
                string _tagexceptids = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent, "exceptids");
                string _tagwherestr = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent, "wherestr");
                string _tagislike = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent, "islike");
                string _tagkeywords = JumboTCMS.Utils.Strings.AttributeValue(_tagcontent, "keywords");
                string _ccType = string.Empty;
                if (_tagchannelid != "0")
                {
                    _doh.Reset();
                    _doh.SqlCmd = "SELECT [Id],[Type] FROM [jcms_normal_channel] WHERE [Id]=" + _tagchannelid + " AND [Enabled]=1";
                    DataTable dtChannel = _doh.GetDataTable();
                    if (dtChannel.Rows.Count > 0)
                    {
                        _ccType = dtChannel.Rows[0]["Type"].ToString();
                    }
                    else
                    {
                        return "&nbsp;频道参数错误";
                    }
                    dtChannel.Clear();
                    dtChannel.Dispose();
                }
                else
                {
                    _ccType = _tagchanneltype;
                }
                JumboTCMS.DAL.Normal_ChannelDAL dal = new JumboTCMS.DAL.Normal_ChannelDAL();
                dal.ExecuteTags(ref _tempstr, _tagchannelid);
                if (_tagclassid != "0")
                    executeTag_Class(ref _tempstr, _tagclassid);

                string sql = "SELECT TOP " + _tagrepeatnum + " [Id],[ChannelId],(select ishtml from [jcms_normal_channel] where id=[jcms_module_" + _ccType + "].channelid) as channelishtml,[ClassId],[FirstPage]," + _tagfields + " FROM [jcms_module_" + _ccType + "] WHERE ([IsPass]=1";
                if (_tagchannelid != "0")
                {
                    string isChannel = " AND [ChannelId]=" + _tagchannelid;
                    sql += isChannel;
                    if (_tagclassid != "0")
                        sql += " And [ClassId] in (SELECT ID FROM [jcms_normal_class] WHERE [IsOut]=0 AND [Code] Like (SELECT Code FROM [jcms_normal_class] WHERE [IsOut]=0 AND [Id]=" + _tagclassid + isChannel + ")+'%')" + isChannel;
                }
                else
                {
                    if (_tagclassid != "0")
                        sql += " And [ClassId] in (SELECT ID FROM [jcms_normal_class] WHERE [IsOut]=0 AND [Code] Like (SELECT Code FROM [jcms_normal_class] WHERE [IsOut]=0 AND [Id]=" + _tagclassid + ")+'%')";
                    else
                        sql += " And [ChannelId] in (SELECT ID FROM [jcms_normal_channel] WHERE [Type]='" + _ccType + "' AND [Enabled]=1)";

                }
                if (_tagistop == "1")
                    sql += " And [IsTop]=1";
                else if (_tagistop == "-1")
                    sql += " And [IsTop]=0";
                if (_tagisfocus == "1")
                    sql += " And [IsFocus]=1";
                if (DBType == "0")
                {
                    switch (_tagtimerange)
                    {
                        case "1d":
                            sql += " AND datediff('d',AddDate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                        case "1w":
                            sql += " AND datediff('ww',AddDate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                        case "1m":
                            sql += " AND datediff('m',AddDate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                        case "1y":
                            sql += " AND AddDate>=#" + (DateTime.Now.Year + "-1-1") + "#";
                            break;
                    }
                }
                else
                {
                    switch (_tagtimerange)
                    {
                        case "1d":
                            sql += " AND datediff(d,AddDate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                        case "1w":
                            sql += " AND datediff(ww,AddDate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                        case "1m":
                            sql += " AND datediff(m,AddDate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                        case "1y":
                            sql += " AND AddDate>='" + (DateTime.Now.Year + "-1-1") + "'";
                            break;
                    }
                }
                if (_tagisimg == "1")
                    sql += " And [IsImg]=1 And (right(Img,4)='.jpg' Or right(Img,4)='.gif')";
                if (_tagwherestr != "")
                    sql += " AND " + _tagwherestr.Replace("小于", "<").Replace("大于", ">").Replace("不等于", "<>");
                if (_tagexceptids != "")
                    sql += " AND id not in(" + _tagexceptids + ")";
                if (_tagislike == "1")
                {
                    if (_tagkeywords == "") _tagkeywords = "将博";
                    _tagkeywords = _tagkeywords.Replace(",", " ").Replace("、", " ");
                    string[] key = _tagkeywords.Split(new string[] { " " }, StringSplitOptions.None);
                    string _joinstr = " AND (";
                    for (int i = 0; i < key.Length; i++)
                    {
                        if (key[i].Length > 1)
                        {
                            if (i == 0)
                                _joinstr += "[Tags] LIKE '%" + key[i].Trim() + "%'";
                            else
                                _joinstr += " OR [Tags] LIKE '%" + key[i].Trim() + "%'";
                        }
                    }
                    _joinstr += ")";
                    sql += _joinstr;
                }
                if (_tagorderfield.ToLower() != "rnd")
                {
                    if (_tagorderfield.ToLower() != "adddate")
                        sql += ") ORDER BY " + _tagorderfield + " " + _tagordertype + ",adddate Desc,id Desc";
                    else
                        sql += ") ORDER BY " + _tagorderfield + " " + _tagordertype + ",id Desc"; ;
                }
                else
                {
                    sql += ")" + ORDER_BY_RND();
                }
                _doh.Reset();
                _doh.SqlCmd = sql;
                DataTable dt = _doh.GetDataTable();
                string ReplaceStr = operateContentTag(_ccType, dt, _tempstr);
                ReplaceStr = ReplaceStr.Replace("{$ContentCount}", dt.Rows.Count.ToString());
                dt.Clear();
                dt.Dispose();
                return ReplaceStr;

            }

        }

        /// <summary>
        /// 解析栏目标签
        /// </summary>
        /// <param name="_pagestr"></param>
        /// <param name="_classid"></param>
        /// <returns></returns>
        private void executeTag_Class(ref string _pagestr, string _classid)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT [Id],[Title],[Info],[Img],[TopicNum],[Code],len(code) as len,[ChannelId],[ParentId] FROM [jcms_normal_class] WHERE [IsOut]=0 AND [Id]=" + _classid;
                DataTable _dt = _doh.GetDataTable();
                if (_dt.Rows.Count > 0)
                {
                    string _channelid = _dt.Rows[0]["ChannelId"].ToString();
                    string _parentid = _dt.Rows[0]["ParentId"].ToString();
                    if (_channelid == this.MainChannel.Id)//说明是当前
                        this.ThisChannel = this.MainChannel;
                    else
                        this.ThisChannel = new JumboTCMS.DAL.Normal_ChannelDAL().GetEntity(_channelid);
                    _pagestr = _pagestr.Replace("{$ClassId}", _dt.Rows[0]["Id"].ToString());
                    _pagestr = _pagestr.Replace("{$ClassName}", _dt.Rows[0]["Title"].ToString());
                    _pagestr = _pagestr.Replace("{$ClassInfo}", _dt.Rows[0]["Info"].ToString());
                    _pagestr = _pagestr.Replace("{$ClassImg}", _dt.Rows[0]["Img"].ToString());
                    _pagestr = _pagestr.Replace("{$ClassTopicNum}", _dt.Rows[0]["TopicNum"].ToString());
                    _pagestr = _pagestr.Replace("{$ClassLink}", Go2Class(1, this.ThisChannel.IsHtml, _channelid, _classid, false));
                    _pagestr = _pagestr.Replace("{$ClassCode}", _dt.Rows[0]["Code"].ToString());
                    _pagestr = _pagestr.Replace("{$ClassDepth}", (Str2Int(_dt.Rows[0]["Len"].ToString()) / 4).ToString());
                    _pagestr = _pagestr.Replace("{$ClassParentId}", _dt.Rows[0]["ParentId"].ToString());
                    if (_dt.Rows[0]["ParentId"].ToString() != "0")
                    {
                        JumboTCMS.Entity.Normal_Class _parentclass = new JumboTCMS.DAL.Normal_ClassDAL().GetEntity(_parentid);
                        _pagestr = _pagestr.Replace("{$ClassParentName}", _parentclass.Title);
                        _pagestr = _pagestr.Replace("{$ClassParentLink}", Go2Class(1, this.ThisChannel.IsHtml, _channelid, _parentid, false));
                        _pagestr = _pagestr.Replace("{$ClassParentCode}", _parentclass.Code);
                    }
                    else
                    {
                        _pagestr = _pagestr.Replace("{$ClassParentName}", this.ThisChannel.Title);
                        _pagestr = _pagestr.Replace("{$ClassParentLink}", Go2Channel(this.ThisChannel.IsHtml, _channelid, false));
                        _pagestr = _pagestr.Replace("{$ClassParentCode}", "");
                    }
                }
                _dt.Clear();
                _dt.Dispose();
            }
        }
        /// <summary>
        /// 解析栏目标签
        /// </summary>
        /// <param name="_pagestr"></param>
        /// <param name="_classid"></param>
        /// <returns></returns>
        private void executeTag_Class2(ref string _pagestr, string _classid)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT [Id],[Title],[Info],[Img],[TopicNum],[Code],len(code) as len,[ChannelId],[ParentId] FROM [jcms_normal_class] WHERE [IsOut]=0 AND [Id]=" + _classid;
                DataTable _dt = _doh.GetDataTable();
                if (_dt.Rows.Count > 0)
                {
                    _pagestr = _pagestr.Replace("{$Class2Id}", _dt.Rows[0]["Id"].ToString());
                    _pagestr = _pagestr.Replace("{$Class2Name}", _dt.Rows[0]["Title"].ToString());
                    _pagestr = _pagestr.Replace("{$Class2Info}", _dt.Rows[0]["Info"].ToString());
                    _pagestr = _pagestr.Replace("{$Class2Img}", _dt.Rows[0]["Img"].ToString());
                    _pagestr = _pagestr.Replace("{$Class2TopicNum}", _dt.Rows[0]["TopicNum"].ToString());
                    _pagestr = _pagestr.Replace("{$Class2Link}", Go2Class(1, this.MainChannel.IsHtml, _dt.Rows[0]["ChannelId"].ToString(), _dt.Rows[0]["Id"].ToString(), false));
                    _pagestr = _pagestr.Replace("{$Class2Code}", _dt.Rows[0]["Code"].ToString());
                    _pagestr = _pagestr.Replace("{$Class2Depth}", (Str2Int(_dt.Rows[0]["Len"].ToString()) / 4).ToString());
                }
                _dt.Clear();
                _dt.Dispose();
            }
        }

        /// <summary>
        /// 解析单条内容标签
        /// </summary>
        /// <param name="_pagestr"></param>
        /// <param name="_contentid"></param>
        /// <returns></returns>
        private void executeTag_Content(ref string _pagestr, string _contentid)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string _randomstr = "1" + RandomStr(4);
                string _tempstr = string.Empty;
                _doh.Reset();
                _doh.SqlCmd = "SELECT * FROM  [jcms_module_" + this.MainChannel.Type + "] WHERE [Id]=" + _contentid;
                DataTable dtContent = _doh.GetDataTable();
                if (dtContent.Rows.Count > 0)
                {
                    _tempstr = p__getNeightor(this.MainChannel.IsHtml, this.MainChannel.Type, dtContent.Rows[0]["ChannelId"].ToString(), dtContent.Rows[0]["ClassId"].ToString(), _contentid);
                    _pagestr = _pagestr.Replace("{$_getNeightor()}", _tempstr);
                    for (int i = 0; i < dtContent.Columns.Count; i++)
                    {
                        if (dtContent.Rows[0]["IsImg"].ToString() == "0" || dtContent.Rows[0]["Img"].ToString().Length == 0)
                            _pagestr = _pagestr.Replace("{$_img}", site.Dir + "style/common/nophoto.jpg");
                        else
                            _pagestr = _pagestr.Replace("{$_img}", dtContent.Rows[0]["Img"].ToString());
                        switch (dtContent.Columns[i].ColumnName.ToLower())
                        {
                            case "adddate":
                                _pagestr = _pagestr.Replace("{$_adddate}", Convert.ToDateTime(dtContent.Rows[0]["AddDate"]).ToString("yyyy-MM-dd"));
                                break;
                            case "viewnum":
                                _pagestr = _pagestr.Replace("{$_viewnum}", "<script src=\"" + site.Dir + "plus/viewcount.aspx?ccid=" + this.MainChannel.Id + "&cType=" + this.MainChannel.Type + "&id=" + _contentid + "&addit=1\"></script>");
                                break;
                            case "downnum":
                                _pagestr = _pagestr.Replace("{$_downnum}", "<script src=\"" + site.Dir + "plus/downcount.aspx?ccid=" + this.MainChannel.Id + "&cType=" + this.MainChannel.Type + "&id=" + _contentid + "\"></script>");
                                break;
                            default:
                                _pagestr = _pagestr.Replace("{$_" + dtContent.Columns[i].ColumnName.ToLower() + "}", dtContent.Rows[0][i].ToString());
                                break;
                        }
                    }
                }
                dtContent.Clear();
                dtContent.Dispose();
            }
        }
        /// <summary>
        /// 处理最后的内容
        /// </summary>
        /// <param name="_pagestr"></param>
        public void ExcuteLastHTML(ref string _pagestr)
        {
            replaceTag_NoShow(ref _pagestr);
        }
        /// <summary>
        /// 处理最后的内容并生成页面
        /// </summary>
        /// <param name="_pagestr"></param>
        /// <param name="_filepath"></param>
        /// <param name="noBom"></param>
        public void SaveHTML(string _pagestr, string _filepath, bool noBom)
        {
            ExcuteLastHTML(ref _pagestr);
            JumboTCMS.Utils.DirFile.SaveFile(_pagestr, _filepath, noBom);
        }
        public void SaveHTML(string _pagestr, string _filepath)
        {
            SaveHTML(_pagestr, _filepath, true);
        }
        #region 生成静态页面
        /// <summary>
        /// 生成首页文件
        /// </summary>

        public bool CreateDefaultFile()
        {
            string _pagestr = GetSiteDefaultPage();
            JumboTCMS.Utils.DirFile.SaveFile(_pagestr, "~/" + "index" + site.StaticExt);
            return true;
        }
        /// <summary>
        /// 生成频道首页(频道ID只能从外部传入，不支持跨频道)
        /// </summary>
        public void CreateChannelFile()
        {
            string _pagestr = GetSiteChannelPage();
            JumboTCMS.Utils.DirFile.SaveFile(_pagestr, "~/" + this.MainChannel.Dir + "/index" + site.StaticExt);
        }
        #endregion
        /// <summary>
        /// 获得首页内容
        /// </summary>
        /// <returns></returns>
        public string GetSiteDefaultPage()
        {
            string pId = string.Empty;
            string _pagestr = string.Empty;
            //得到首页的默认模板：方案组ID/主题ID/模板内容
            JumboTCMS.DAL.Normal_TemplateDAL dal = new JumboTCMS.DAL.Normal_TemplateDAL();
            dal.GetTemplateContent("0", ref pId, ref _pagestr);
            this.IsHtml = site.IsHtml;
            this.PageNav = site.Name + "&nbsp;&raquo;&nbsp;首页";
            this.PageTitle = site.Name + " - " + site.Description + site.TitleTail;
            this.PageKeywords = site.Keywords;
            this.PageDescription = site.Description;
            ReplacePublicTag(ref _pagestr);
            ReplaceChannelClassLoopTag(ref _pagestr);
            ReplaceContentLoopTag(ref _pagestr);
            ExcuteLastHTML(ref _pagestr);
            return _pagestr;
        }
        /// <summary>
        /// 获得频道页内容(频道ID只能从外部传入，不支持跨频道)
        /// </summary>
        /// <returns></returns>
        public string GetSiteChannelPage()
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string _pagestr = string.Empty;
                string TempId = this.MainChannel.TemplateId.ToString();
                string pId = string.Empty;
                //得到模板方案组ID/主题ID/模板内容
                JumboTCMS.DAL.Normal_TemplateDAL dal = new JumboTCMS.DAL.Normal_TemplateDAL();
                dal.GetTemplateContent(TempId, ref pId, ref _pagestr);
                this.IsHtml = PageIsHtml();
                this.PageNav = "<a href=\"" + Go2Site(site.IsHtml) + "\" class=\"home\"></a>&nbsp;&raquo;&nbsp;" + this.MainChannel.Title;
                this.PageTitle = this.MainChannel.Title + "_" + site.Name + site.TitleTail;
                this.PageKeywords = site.Keywords;
                this.PageDescription = JumboTCMS.Utils.Strings.SimpleLineSummary(this.MainChannel.Info);
                ReplacePublicTag(ref _pagestr);
                ReplaceChannelTag(ref _pagestr, this.MainChannel.Id.ToString());
                ReplaceChannelClassLoopTag(ref _pagestr);
                ReplaceContentLoopTag(ref _pagestr);
                ExcuteLastHTML(ref _pagestr);
                return _pagestr;
            }
        }
        #region 获得栏目页内容
        /// <summary>
        /// 获得栏目页内容(频道ID只能从外部传入,频道ID不能为0)
        /// </summary>
        /// <param name="_classid"></param>
        /// <param name="_currentpage"></param>
        /// <returns></returns>
        public string GetSiteClassPage(string _classid, int _currentpage)
        {
            Normal_Class _class = new JumboTCMS.DAL.Normal_ClassDAL().GetEntity(_classid, "[IsOut]=0 AND [ChannelId]=" + this.MainChannel.Id);
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                string pStr = " [ClassId] in (Select id FROM [jcms_normal_class] WHERE [IsOut]=0 AND [Code] LIKE '" + _class.Code + "%') and [IsPass]=1 AND [ChannelId]=" + this.MainChannel.Id;
                _doh.ConditionExpress = pStr;
                int _totalcount = _doh.Count("jcms_module_" + this.MainChannel.Type);
                int _pagecount = JumboTCMS.Utils.Int.PageCount(_totalcount, _class.PageSize);
                System.Collections.ArrayList ContentList = getClassSinglePage(_class, _pagecount, _currentpage);
                string _pagestr = ContentList[0].ToString();
                if (ContentList.Count > 2)
                {
                    string ViewStr = ContentList[1].ToString();
                    _pagestr = _pagestr.Replace(ViewStr, ContentList[2].ToString());
                }
                _pagestr = _pagestr.Replace("{$_getPageBar()}",
                    getPageBar(1, "html", 2, _totalcount, _class.PageSize, _currentpage, Go2Class(1, this.MainChannel.IsHtml, this.MainChannel.Id.ToString(), _classid, false), Go2Class(-1, this.MainChannel.IsHtml, this.MainChannel.Id.ToString(), _classid, false), Go2Class(-1, false, this.MainChannel.Id.ToString(), _classid, false), site.CreatePages)
                    );
                ExcuteLastHTML(ref _pagestr);
                return _pagestr;
            }
        }

        private System.Collections.ArrayList getClassSinglePage(Normal_Class _class, int _pagecount, int _page)
        {
            string pId = string.Empty;
            string _pagestr = string.Empty;

            //得到模板方案组ID/主题ID/模板内容
            new JumboTCMS.DAL.Normal_TemplateDAL().GetTemplateContent(_class.TemplateId, ref pId, ref _pagestr);
            this.PageNav = "<script type=\"text/javascript\" src=\"" + site.Dir + this.MainChannel.Dir + "/js/classnav_" + _class.Id + ".js\"></script>";
            if (this.MainChannel.IsTop)
                this.PageTitle = _class.Title + "_" + this.MainChannel.Title + "_" + site.Name + site.TitleTail;
            else
                this.PageTitle = _class.Title + "_" + site.Name + site.TitleTail;
            this.PageKeywords = site.Keywords;
            this.PageDescription = JumboTCMS.Utils.Strings.SimpleLineSummary(_class.Info);
            ReplacePublicTag(ref _pagestr);
            ReplaceChannelTag(ref _pagestr, this.MainChannel.Id);
            _pagestr = _pagestr.Replace("{$ChannelClassId}", _class.Id);
            ReplaceChannelClassLoopTag(ref _pagestr);
            ReplaceClassTag(ref _pagestr, _class.Id);
            ReplaceContentLoopTag(ref _pagestr);
            System.Collections.ArrayList ContentList = new System.Collections.ArrayList();
            ContentList.Add(_pagestr);
            getClassSinglePageListBody(_class, ref ContentList, _pagecount, _page);

            return ContentList;
        }
        private void getClassSinglePageListBody(Normal_Class _class, ref System.Collections.ArrayList ContentList, int _pagecount, int _page)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string whereStr = string.Empty;
                string _pagestr = ContentList[0].ToString();
                whereStr = " [ClassId] in (SELECT ID FROM [jcms_normal_class] WHERE [IsOut]=0 AND [Code] LIKE '" + _class.Code + "%')";
                whereStr += " AND [IsPass]=1 AND [ChannelId]=" + this.MainChannel.Id;
                int _pagesize = (_class.PageSize < 1) ? 20 : _class.PageSize;

                System.Collections.ArrayList TagArray = JumboTCMS.Utils.Strings.GetHtmls(_pagestr, "{$jcms:class(", "{$/jcms:class}", false, false);
                if (TagArray.Count > 0)//标签存在
                {
                    string LoopBody = string.Empty;
                    string TempStr = string.Empty;
                    string FiledsStr = string.Empty;
                    int StartTag, EndTag;

                    StartTag = TagArray[0].ToString().IndexOf(")}", 0);
                    FiledsStr = TagArray[0].ToString().Substring(0, StartTag).ToLower();
                    if (!("," + FiledsStr + ",").Contains(",adddate,")) FiledsStr += ",adddate";
                    EndTag = TagArray[0].ToString().Length;
                    LoopBody = "{$jcms:class(" + TagArray[0].ToString() + "{$/jcms:class}";
                    TempStr = TagArray[0].ToString().Substring(StartTag + 2, EndTag - StartTag - 2).Replace("<#foreach content>", "<#foreach collection=\"${contents}\" var=\"field\" index=\"i\">");//需要循环的部分
                    ContentList.Add(LoopBody);

                    if (_pagecount > 0)
                    {
                        if (_page == 0)
                        {
                            for (int i = 1; i < _pagecount + 1; i++)
                            {
                                NameValueCollection orders = new NameValueCollection();
                                orders.Add("AddDate", "desc");
                                orders.Add("Id", "desc");
                                string wStr = JumboTCMS.Utils.SqlHelp.GetMultiOrderPagerSQL("Id,ChannelId,ClassId,[IsPass],[FirstPage]," + FiledsStr,
                                    "jcms_module_" + this.MainChannel.Type,
                                    _pagesize,
                                    i,
                                    orders,
                                    whereStr);
                                _doh.Reset();
                                _doh.SqlCmd = wStr;
                                DataTable dtContent = _doh.GetDataTable();


                                ContentList.Add(operateContentTag(this.MainChannel.Type, dtContent, TempStr));
                                dtContent.Clear();
                                dtContent.Dispose();

                            }
                        }
                        else
                        {
                            _page = _page == 0 ? 1 : _page;
                            NameValueCollection orders = new NameValueCollection();
                            orders.Add("AddDate", "desc");
                            orders.Add("Id", "desc");
                            string wStr = JumboTCMS.Utils.SqlHelp.GetMultiOrderPagerSQL("Id,ChannelId,ClassId,[IsPass],[FirstPage]," + FiledsStr,
                                "jcms_module_" + this.MainChannel.Type,
                                _pagesize,
                                _page,
                                orders,
                                whereStr);
                            _doh.Reset();
                            _doh.SqlCmd = wStr;
                            DataTable dtContent = _doh.GetDataTable();
                            ContentList.Add(operateContentTag(this.MainChannel.Type, dtContent, TempStr));
                            dtContent.Clear();
                            dtContent.Dispose();
                        }
                    }
                    else
                        ContentList.Add("  ");
                }
            }

        }
        #endregion
        /// <summary>
        /// 处理内容标签(频道ID不固定，所以不能直接继承本类channel)
        /// </summary>
        /// <param name="_channeltype">唯一模型 要么article 要么soft</param>
        ///  <param name="_dt">获得的数据表</param>
        /// <param name="_tempstr">循环模版</param>
        /// <returns></returns>
        private string operateContentTag(string _channeltype, DataTable _dt, string _tempstr)
        {
            string _replacestr = _tempstr;
            _replacestr = _replacestr.Replace("$_{title}", "<#formattitle title=\"${field.title}\" />");
            _replacestr = _replacestr.Replace("$_{url}", "${field.firstpage}");
            _replacestr = _replacestr.Replace("$_{img}", "<#imgurl sitedir=\"" + site.Dir + "\"  isimg=\"${field.isimg}\" img=\"${field.img}\" />");
            _replacestr = _replacestr.Replace("$_{classname}", "<#classname classid=\"${field.classid}\" />");
            _replacestr = _replacestr.Replace("$_{classlink}", "<#classlink channelid=\"${field.channelid}\" channelishtml=\"${field.channelishtml}\" classid=\"${field.classid}\" />");
            _replacestr = _replacestr.Replace("$_{channelname}", "<#channelname channelid=\"${field.channelid}\" />");
            _replacestr = _replacestr.Replace("$_{channellink}", "<#channellink channelid=\"${field.channelid}\" channelishtml=\"${field.channelishtml}\" />");
            _replacestr = _replacestr.Replace("$_{viewnum}", "<#viewnum sitedir=\"" + site.Dir + "\" channeltype=\"" + _channeltype + "\" channelid=\"${field.channelid}\" contentid=\"${field.id}\" />");

            string _TemplateContent = _replacestr;
            JumboTCMS.TEngine.TemplateManager manager = JumboTCMS.TEngine.TemplateManager.FromString(_TemplateContent);
            string _content = "";
            manager.RegisterCustomTag("formattitle", new TemplateTag_GetFormatTitle());
            manager.RegisterCustomTag("imgurl", new TemplateTag_GetImgurl());
            manager.RegisterCustomTag("classname", new TemplateTag_GetClassName());
            manager.RegisterCustomTag("classlink", new TemplateTag_GetClassLink());
            manager.RegisterCustomTag("channelname", new TemplateTag_GetChannelName());
            manager.RegisterCustomTag("channellink", new TemplateTag_GetChannelLink());
            manager.RegisterCustomTag("cutstring", new TemplateTag_GetCutstring());
            manager.RegisterCustomTag("viewnum", new TemplateTag_GetViewnum());
            switch (_channeltype.ToLower())
            {
                case "document":
                    manager.SetValue("contents", (new JumboTCMS.Entity.Module_Documents()).DT2List(_dt));
                    break;
                case "paper":
                    manager.SetValue("contents", (new JumboTCMS.Entity.Module_Papers()).DT2List(_dt));
                    break;
                case "photo":
                    manager.SetValue("contents", (new JumboTCMS.Entity.Module_Photos()).DT2List(_dt));
                    break;
                case "product":
                    manager.SetValue("contents", (new JumboTCMS.Entity.Module_Products()).DT2List(_dt));
                    break;
                case "soft":
                    manager.SetValue("contents", (new JumboTCMS.Entity.Module_Softs()).DT2List(_dt));
                    break;
                case "video":
                    manager.SetValue("contents", (new JumboTCMS.Entity.Module_Videos()).DT2List(_dt));
                    break;
                default:
                    manager.SetValue("contents", (new JumboTCMS.Entity.Module_Articles()).DT2List(_dt));
                    break;
            }
            manager.SetValue("site", site);
            _content = manager.Process();
            return _content;
        }
    }
}