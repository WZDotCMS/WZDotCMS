/*
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
using System.Data;
using System.Web;
using System.Web.UI;
using JumboTCMS.Utils;
using JumboTCMS.DBUtility;

namespace JumboTCMS.DAL
{
    public class Module_softDAL : Module_articleDAL
    {
        public Module_softDAL()
        {
        }
        public override void CreateContent(string _ChannelId, string _ContentId, int _CurrentPage)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT [FirstPage] FROM [jcms_module_soft] WHERE [ChannelId]=" + _ChannelId + " and [Id]=" + _ContentId;
                DataTable dtContent = _doh.GetDataTable();
                string ContentFirstPage = dtContent.Rows[0]["FirstPage"].ToString();
                dtContent.Clear();
                dtContent.Dispose();
                if (ContentFirstPage.Length == 0)
                {
                    _doh.Reset();
                    _doh.SqlCmd = "UPDATE [jcms_module_soft] SET [FirstPage]='" + Go2View(1, true, _ChannelId, _ContentId, false) + "' WHERE [ChannelId]=" + _ChannelId + " and [IsPass]=1 and [Id]=" + _ContentId;
                    _doh.ExecuteSqlNonQuery();
                }
                JumboTCMS.Utils.DirFile.SaveFile(GetContent(_ChannelId, _ContentId, 1), Go2View(1, true, _ChannelId, _ContentId, true));
            }
        }
        public override string GetContent(string _ChannelId, string _ContentId, int _CurrentPage)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                JumboTCMS.Entity.Normal_Channel _Channel = new JumboTCMS.DAL.Normal_ChannelDAL().GetEntity(_ChannelId);
                if (_Channel.Enabled == false)
                {
                    return "频道错误";
                }
                _doh.Reset();
                _doh.SqlCmd = "SELECT [ClassId] FROM [jcms_module_soft] WHERE [ChannelId]=" + _ChannelId + " and [Id]=" + _ContentId;
                DataTable dtSearch = _doh.GetDataTable();
                if (dtSearch.Rows.Count == 0)
                {
                    dtSearch.Clear();
                    dtSearch.Dispose();
                    return "内容错误";
                }
                string ClassId = dtSearch.Rows[0]["ClassId"].ToString();
                dtSearch.Clear();
                dtSearch.Dispose();
                TemplateEngineDAL te = new TemplateEngineDAL(_ChannelId);
                _doh.Reset();
                _doh.SqlCmd = "SELECT Id FROM [jcms_normal_class] WHERE [IsOut]=0 AND [ChannelId]=" + _ChannelId + " and [Id]=" + ClassId;
                if (_doh.GetDataTable().Rows.Count == 0)
                {
                    return "栏目错误";
                }
                string PageStr = string.Empty;
                _doh.Reset();
                _doh.SqlCmd = "SELECT * FROM [jcms_module_soft] WHERE [ChannelId]=" + _ChannelId + " and [Id]=" + _ContentId;
                DataTable dtContent = _doh.GetDataTable();
                System.Collections.ArrayList ContentList = new System.Collections.ArrayList();
                p__GetChannel_Soft(te, dtContent, ref PageStr, ref ContentList, 0);
                te.ReplaceContentTag(ref PageStr, _ContentId);
                te.ReplaceContentLoopTag(ref PageStr);//主要解决通过tags关联
                te.ExcuteLastHTML(ref PageStr);
                ContentList.Add(PageStr);
                p__replaceSingleSoft(dtContent, ref PageStr, ref ContentList);

                dtContent.Clear();
                dtContent.Dispose();
                return ContentList[0].ToString().Replace("{$DownUrl}", ContentList[1].ToString());
            }

        }
        private void p__GetChannel_Soft(TemplateEngineDAL te, DataTable dt, ref string PageStr, ref System.Collections.ArrayList ContentList, int i)
        {
            string TempId, ClassName;
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "IsOut=0 AND id=" + dt.Rows[i]["ClassId"].ToString();
                TempId = _doh.GetField("jcms_normal_class", "ContentTemp").ToString();
                ClassName = _doh.GetField("jcms_normal_class", "Title").ToString();
            }
            string pId = string.Empty;
            //得到模板方案组ID/主题ID/模板内容
            new JumboTCMS.DAL.Normal_TemplateDAL().GetTemplateContent(TempId, ref pId, ref PageStr);
            te.PageNav = "<script type=\"text/javascript\" src=\"" + site.Dir + te.MainChannel.Dir + "/js/classnav_" + dt.Rows[i]["ClassId"].ToString() + ".js\"></script>";
            if (te.MainChannel.IsTop)
                te.PageTitle = dt.Rows[i]["Title"] + "_" + ClassName + "_" + te.MainChannel.Title + "_" + site.Name + site.TitleTail;
            else
                te.PageTitle = dt.Rows[i]["Title"] + "_" + ClassName + "_" + site.Name + site.TitleTail;
            te.PageKeywords = JumboTCMS.Utils.WordSpliter.GetKeyword(dt.Rows[i]["Title"].ToString()) + "," + site.Keywords;
            te.PageDescription = JumboTCMS.Utils.Strings.SimpleLineSummary(dt.Rows[i]["Summary"].ToString());
            te.ReplacePublicTag(ref PageStr);
            te.ReplaceChannelTag(ref PageStr, dt.Rows[i]["ChannelId"].ToString());
            te.ReplaceChannelClassLoopTag(ref PageStr);
            te.ReplaceClassTag(ref PageStr, dt.Rows[i]["ClassId"].ToString());
            //te.ReplaceContentLoopTag(ref PageStr);
        }
        private void p__replaceSingleSoft(DataTable dt, ref string PageStr, ref System.Collections.ArrayList ContentList)
        {
            //下载地址分割
            string DownUrl = dt.Rows[0]["DownUrl"].ToString().Replace("\r\n", "\r");
            string[] _DownUrl = DownUrl.Split(new string[] { "\r" }, StringSplitOptions.None);
            string TempStr = string.Empty;
            for (int j = 0; j < _DownUrl.Length; j++)
            {
                string _url = _DownUrl[j];
                string _thisTXT = _url.Contains("|||") ? _url.Split(new string[] { "|||" }, StringSplitOptions.None)[0] : "本地下载";
                string _thisURL = _url.Contains("|||") ? _url.Split(new string[] { "|||" }, StringSplitOptions.None)[1] : _url;
                if (_thisURL.StartsWith("http://") || _thisURL.StartsWith("https://") || _thisURL.StartsWith("ftp://"))
                    TempStr += "&nbsp;&nbsp;<a href=\"" + _thisURL + "\" target=\"_blank\">" + _thisTXT + "</a>";
                else
                    TempStr += "&nbsp;&nbsp;<a href=\"javascript:Go2PageForm('soft_down.aspx?ChannelId=" + dt.Rows[0]["ChannelId"].ToString() + "&Id=" + dt.Rows[0]["Id"].ToString() + "&NO=" + j + "');\">" + _thisTXT + "</a>";
            }
            ContentList.Add(TempStr);
        }
    }
}
