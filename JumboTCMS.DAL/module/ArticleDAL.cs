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
    public class Module_articleDAL : Common, IModule
    {
        public Module_articleDAL()
        {
            base.SetupSystemDate();
        }
        public virtual void CreateContent(string _ChannelId, string _ContentId, int _CurrentPage)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT [Content],[FirstPage] FROM [jcms_module_article] WHERE [ChannelId]=" + _ChannelId + " and [Id]=" + _ContentId;
                DataTable dtContent = _doh.GetDataTable();
                string ArticleContent = dtContent.Rows[0]["Content"].ToString();
                string ContentFirstPage = dtContent.Rows[0]["FirstPage"].ToString();
                dtContent.Clear();
                dtContent.Dispose();
                if (ArticleContent != "")
                {
                    int pageCount = 1;
                    //处理文章内容分页
                    if (ArticleContent.Contains("[Jumbot_PageBreak]"))
                    {
                        string[] ContentArr = ArticleContent.Split(new string[] { "[Jumbot_PageBreak]" }, StringSplitOptions.RemoveEmptyEntries);
                        pageCount = ContentArr.Length;
                    }
                    if (ContentFirstPage.Length == 0)
                    {
                        _doh.Reset();
                        _doh.SqlCmd = "UPDATE [jcms_module_article] SET [FirstPage]='" + Go2View(1, true, _ChannelId, _ContentId, false) + "' WHERE [ChannelId]=" + _ChannelId + " and [IsPass]=1 and [Id]=" + _ContentId;
                        _doh.ExecuteSqlNonQuery();
                    }
                    for (int j = 1; j < (pageCount + 1); j++)
                    {
                        JumboTCMS.Utils.DirFile.SaveFile(GetContent(_ChannelId, _ContentId, j), Go2View(j, true, _ChannelId, _ContentId, true));
                    }
                }
            }
        }
        public virtual string GetContent(string _ChannelId, string _ContentId, int _CurrentPage)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                JumboTCMS.Entity.Normal_Channel _Channel = new JumboTCMS.DAL.Normal_ChannelDAL().GetEntity(_ChannelId);
                if (_Channel.Enabled == false)
                {
                    return "频道错误";
                }
                _doh.Reset();
                _doh.SqlCmd = "SELECT [ClassId] FROM [jcms_module_article] WHERE [ChannelId]=" + _ChannelId + " and [Id]=" + _ContentId;
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
                _doh.SqlCmd = "SELECT * FROM [jcms_module_article] WHERE [ChannelId]=" + _ChannelId + " and [Id]=" + _ContentId;
                DataTable dtContent = _doh.GetDataTable();
                string _FirstPage = dtContent.Rows[0]["FirstPage"].ToString();
                System.Collections.ArrayList ContentList = new System.Collections.ArrayList();
                p__GetChannel_Article(te, dtContent, ref PageStr, ref ContentList, 0);
                te.ReplaceContentTag(ref PageStr, _ContentId);
                te.ReplaceContentLoopTag(ref PageStr);//主要解决通过tags关联
                te.ExcuteLastHTML(ref PageStr);
                ContentList.Add(PageStr);
                p__replaceSingleArticle(dtContent, ref _CurrentPage, ref PageStr, ref ContentList);

                dtContent.Clear();
                dtContent.Dispose();

                return ContentList[0].ToString().Replace("{$Content}", ContentList[_CurrentPage].ToString()).Replace("{$_getPageBar()}", getPageBar(1, "html", 7, ContentList.Count - 1, 1, _CurrentPage, Go2View(1, (_Channel.IsHtml), _ChannelId, _ContentId, false), Go2View(-1, (_Channel.IsHtml), _ChannelId, _ContentId, false), Go2View(-1, (_Channel.IsHtml), _ChannelId, _ContentId, false), 0));
            }
        }
        /// <summary>
        /// 得到内容页地址
        /// </summary>
        /// <param name="_page"></param>
        /// <param name="_ishtml"></param>
        /// <param name="_channelid"></param>
        /// <param name="_contentid"></param>
        /// <param name="_truefile"></param>
        /// <returns></returns>
        public string GetContentLink(int _page, bool _ishtml, string _channelid, string _contentid, bool _truefile)
        {
            JumboTCMS.Entity.Normal_Channel _Channel = new JumboTCMS.DAL.Normal_ChannelDAL().GetEntity(_channelid);
            object[] _value = new ModuleContentDAL().GetSome(_channelid, _Channel.Type, _contentid);
            string _date = _value[0].ToString();
            string _firstpage = _value[1].ToString();
            string _aliaspage = _value[2].ToString();
            string TempUrl = JumboTCMS.Common.PageFormat.View(_ishtml, site.Dir, site.UrlReWriter, _page);
            if (_aliaspage.Length > 5 && _page == 1 && _ishtml)
                return _aliaspage;
            if ((_Channel.SubDomain.Length > 0) && (!_truefile))
                TempUrl = TempUrl.Replace("<#SiteDir#><#ChannelDir#>", _Channel.SubDomain);
            TempUrl = TempUrl.Replace("<#SiteDir#>", site.Dir);
            TempUrl = TempUrl.Replace("<#SiteStaticExt#>", site.StaticExt);
            TempUrl = TempUrl.Replace("<#ChannelId#>", _channelid);
            TempUrl = TempUrl.Replace("<#ChannelDir#>", _Channel.Dir.ToLower());
            TempUrl = TempUrl.Replace("<#ChannelType#>", _Channel.Type.ToLower());
            TempUrl = TempUrl.Replace("<#id#>", _contentid);
            if (_date != "")
            {
                TempUrl = TempUrl.Replace("<#year#>", DateTime.Parse(_date).ToString("yyyy"));
                TempUrl = TempUrl.Replace("<#month#>", DateTime.Parse(_date).ToString("MM"));
                TempUrl = TempUrl.Replace("<#day#>", DateTime.Parse(_date).ToString("dd"));
            }
            if (_page > 0) TempUrl = TempUrl.Replace("<#page#>", _page.ToString());
            return TempUrl;
        }
        /// <summary>
        /// 删除内容页
        /// </summary>
        /// <param name="_ChannelId"></param>
        /// <param name="_ContentId"></param>
        public void DeleteContent(string _ChannelId, string _ContentId)
        {
            JumboTCMS.Entity.Normal_Channel _Channel = new JumboTCMS.DAL.Normal_ChannelDAL().GetEntity(_ChannelId);

            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "[ChannelId]=" + _ChannelId + " AND [Id]=" + _ContentId;
                object[] _value = _doh.GetFields("jcms_module_" + _Channel.Type, "AddDate,FirstPage");
                string _date = _value[0].ToString();
                string _firstpage = _value[1].ToString();
                if (_firstpage.Length > 0 && _Channel.IsHtml)
                {
                    string _folderName = String.Format("/{0}{1}/{2}",
                        DateTime.Parse(_date).ToString("yyyy"),
                        DateTime.Parse(_date).ToString("MM"),
                        DateTime.Parse(_date).ToString("dd")
                        );
                    if (System.IO.Directory.Exists(HttpContext.Current.Server.MapPath(site.Dir + _Channel.Dir + _folderName)))
                    {
                        string htmFile = HttpContext.Current.Server.MapPath(Go2View(1, true, _ChannelId, _ContentId, true));
                        if (System.IO.File.Exists(htmFile))
                            System.IO.File.Delete(htmFile);
                        string[] htmFiles = System.IO.Directory.GetFiles(HttpContext.Current.Server.MapPath(site.Dir + _Channel.Dir + _folderName), _ContentId + "_*" + site.StaticExt);
                        foreach (string fileName in htmFiles)
                        {
                            if (System.IO.File.Exists(fileName))
                                System.IO.File.Delete(fileName);
                        }
                    }
                    _doh.Reset();
                    _doh.SqlCmd = "UPDATE [jcms_module_" + _Channel.Type + "] SET [FirstPage]='' WHERE [ChannelId]=" + _ChannelId + " AND [Id]=" + _ContentId;
                    _doh.ExecuteSqlNonQuery();
                }
            }
        }
        private void p__GetChannel_Article(TemplateEngineDAL te, DataTable dt, ref string PageStr, ref System.Collections.ArrayList ContentList, int i)
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
            //te.ReplaceContentLoopTag(ref PageStr);//先不要解析
        }
        private void p__replaceSingleArticle(DataTable dt, ref int _CurrentPage, ref string PageStr, ref System.Collections.ArrayList ContentList)
        {
            string ArticleContent = dt.Rows[0]["Content"].ToString();
            //处理UBB
            ArticleContent = JumboTCMS.Utils.Strings.UBB2HTML(ArticleContent);
            //处理文章内容分页
            if (ArticleContent.Contains("[Jumbot_PageBreak]"))
            {
                string[] ContentArr = ArticleContent.Split(new string[] { "[Jumbot_PageBreak]" }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < ContentArr.Length; j++)
                    ContentList.Add(ContentArr[j]);
            }
            else
                ContentList.Add(ArticleContent);
            if (_CurrentPage < 1 || _CurrentPage > (ContentList.Count))
                _CurrentPage = 1;
        }
    }
}
