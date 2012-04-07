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
using System.Text;
using System.Web;
using System.Web.UI;
using JumboTCMS.DBUtility;
using JumboTCMS.Utils;

namespace JumboTCMS.DAL
{
    public class Module_photoDAL : Module_articleDAL
    {
        public Module_photoDAL()
        {
        }
        public override void CreateContent(string _ChannelId, string _ContentId, int _CurrentPage)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT [PhotoUrl],[FirstPage] FROM [jcms_module_photo] WHERE [ChannelId]=" + _ChannelId + " and [Id]=" + _ContentId;
                DataTable dtContent = _doh.GetDataTable();
                //图片地址分割处理
                string PhotoUrl = dtContent.Rows[0]["PhotoUrl"].ToString().Replace("\r\n", "\r");
                string ContentFirstPage = dtContent.Rows[0]["FirstPage"].ToString();
                dtContent.Clear();
                dtContent.Dispose();
                if (PhotoUrl != "")
                {
                    string[] PhotoUrlArr = PhotoUrl.Split(new string[] { "\r" }, StringSplitOptions.RemoveEmptyEntries);
                    int pageCount = PhotoUrlArr.Length;
                    if (ContentFirstPage.Length == 0)
                    {
                        _doh.Reset();
                        _doh.SqlCmd = "UPDATE [jcms_module_photo] SET [FirstPage]='" + Go2View(1, true, _ChannelId, _ContentId, false) + "' WHERE  [ChannelId]=" + _ChannelId + " and [IsPass]=1 and [Id]=" + _ContentId;
                        _doh.ExecuteSqlNonQuery();
                    }
                    for (int j = 1; j < (pageCount + 1); j++)
                    {
                        JumboTCMS.Utils.DirFile.SaveFile(GetContent(_ChannelId, _ContentId, j), Go2View(j, true, _ChannelId, _ContentId, true));
                    }
                }
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
                _doh.SqlCmd = "SELECT [ClassId] FROM [jcms_module_photo] WHERE [ChannelId]=" + _ChannelId + " and [Id]=" + _ContentId;
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
                _doh.SqlCmd = "SELECT * FROM [jcms_module_photo] WHERE [ChannelId]=" + _ChannelId + " and [Id]=" + _ContentId;
                DataTable dtContent = _doh.GetDataTable();
                string _FirstPage = dtContent.Rows[0]["FirstPage"].ToString();
                System.Collections.ArrayList ContentList = new System.Collections.ArrayList();
                p__GetChannel_Photo(te, dtContent, ref PageStr, ref ContentList, 0);
                te.ReplaceContentTag(ref PageStr, _ContentId);
                te.ReplaceContentLoopTag(ref PageStr);//主要解决通过tags关联
                te.ExcuteLastHTML(ref PageStr);
                ContentList.Add(PageStr);
                p__replaceSinglePhoto(dtContent, ref _CurrentPage, ref PageStr, ref ContentList);
                int _TotalPage = Convert.ToInt16(ContentList[1].ToString());//总页数
                dtContent.Clear();
                dtContent.Dispose();

                string _PrevLink = _CurrentPage == 1 ? "#" : Go2View(_CurrentPage - 1, (_Channel.IsHtml), _ChannelId, _ContentId, false);
                string _NextLink = _CurrentPage == _TotalPage ? "#" : Go2View(_CurrentPage + 1, (_Channel.IsHtml), _ChannelId, _ContentId, false);
                string _html = ContentList[0].ToString();
                string[] ThisPhotoUrl = ContentList[2].ToString().Split(new string[] { "|||" }, StringSplitOptions.RemoveEmptyEntries);
                string CurrentPhotoUrl = ThisPhotoUrl[ThisPhotoUrl.Length - 1];
                string CurrentPhotoTitle = ThisPhotoUrl.Length == 1 ? "" : ThisPhotoUrl[0];
                return _html
                    .Replace("{$CurrentPage}", _CurrentPage.ToString())
                    .Replace("{$TotalPage}", ContentList[1].ToString())
                    .Replace("{$CurrentPhotoUrl}", CurrentPhotoUrl)
                    .Replace("{$CurrentPhotoTitle}", CurrentPhotoTitle)
                    .Replace("{$SlideJSON}", ContentList[3].ToString())
                    .Replace("{$PrevLink}", _PrevLink)
                    .Replace("{$NextLink}", _NextLink);
            }
        }
        private void p__GetChannel_Photo(TemplateEngineDAL te, DataTable dt, ref string PageStr, ref System.Collections.ArrayList ContentList, int i)
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

        private void p__replaceSinglePhoto(DataTable dt, ref int _CurrentPage, ref string PageStr, ref System.Collections.ArrayList ContentList)
        {
            //大图分割处理
            string PhotoUrl = dt.Rows[0]["PhotoUrl"].ToString().Replace("\r\n", "\r");
            string[] PhotoUrlArr = PhotoUrl.Split(new string[] { "\r" }, StringSplitOptions.RemoveEmptyEntries);
            //缩略图分割处理
            string ThumbsUrl = dt.Rows[0]["ThumbsUrl"].ToString().Replace("\r\n", "\r");
            string[] ThumbsUrlArr = ThumbsUrl.Split(new string[] { "\r" }, StringSplitOptions.RemoveEmptyEntries);
            ContentList.Add(PhotoUrlArr.Length);
            if (_CurrentPage < 1 || _CurrentPage > PhotoUrlArr.Length)
                _CurrentPage = 1;
            ContentList.Add(PhotoUrlArr[_CurrentPage - 1]);//当前显示的图片

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("[");
            for (int i = 0; i < PhotoUrlArr.Length; i++)
            {
                string[] ThisPhotoUrl = PhotoUrlArr[i].Split(new string[] { "|||" }, StringSplitOptions.RemoveEmptyEntries);
                string thumbnailImage = ThumbsUrlArr[i];
                string title = ThisPhotoUrl.Length == 1 ? "" : ThisPhotoUrl[0];
                if (i > 0)
                    jsonBuilder.Append(",");
                jsonBuilder.Append("{");
                jsonBuilder.Append("no:" + (i + 1) + ",");
                jsonBuilder.Append("img: '" + thumbnailImage + "',");
                jsonBuilder.Append("link: '" + dt.Rows[0]["FirstPage"].ToString() + "',");
                jsonBuilder.Append("title: '" + title + "'");
                jsonBuilder.Append("}");
            }
            jsonBuilder.Append("]");
            ContentList.Add(jsonBuilder.ToString());
        }
    }
}
