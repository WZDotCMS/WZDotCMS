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
using System.Web.UI.WebControls;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Controls
{
    public partial class _content : JumboTCMS.UI.FrontHtml
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 8;//脚本过期时间
            int CurrentPage = Int_ThisPage();
            string ContentId = this.lblContentId.Text == "{$ContentId}" ? Str2Str(q("id")) : this.lblContentId.Text;
            if (ContentId == "0")
            {
                FinalMessage("参数错误!", site.Dir, 0, 8);
                Response.End();
            }
            string ChannelId = this.lblChannelId.Text == "{$ChannelId}" ? Str2Str(q("ChannelId")) : this.lblChannelId.Text;
            doh.Reset();
            if (q("preview") == "1")
                doh.ConditionExpress = "id=@id";
            else
                doh.ConditionExpress = "id=@id and Enabled=1";
            doh.AddConditionParameter("@id", ChannelId);
            if (!doh.Exist("jcms_normal_channel"))
            {
                FinalMessage("频道不存在或被禁用!", site.Dir, 0, 8);
                Response.End();
            }
            JumboTCMS.DAL.TemplateEngineDAL teDAL = new JumboTCMS.DAL.TemplateEngineDAL(ChannelId);
            doh.Reset();
            doh.SqlCmd = "SELECT [ClassId] FROM [jcms_module_" + teDAL.MainChannel.Type + "] WHERE [ChannelId]=" + ChannelId + " and [Id]=" + ContentId;
            if (q("preview") != "1")
                doh.SqlCmd += " and [IsPass]=1";
            DataTable dtSearch = doh.GetDataTable();
            if (dtSearch.Rows.Count == 0)
            {
                dtSearch.Clear();
                dtSearch.Dispose();
                FinalMessage("内容不存在或未审核!", site.Dir, 0, 8);
                Response.End();
            }
            string ClassId = dtSearch.Rows[0]["ClassId"].ToString();
            dtSearch.Clear();
            dtSearch.Dispose();

            if (!teDAL.CanReadContent(ContentId, ClassId))
            {
                FinalMessage("阅读权限不足!", site.Dir, 0, 8);
                Response.End();
            }
            doh.Reset();
            doh.SqlCmd = "SELECT Id FROM [jcms_normal_class] WHERE [IsOut]=0 AND [ChannelId]=" + ChannelId + " and [Id]=" + ClassId;
            if (doh.GetDataTable().Rows.Count == 0)
            {
                FinalMessage("内容所属栏目有误!", site.Dir, 0, 8);
                Response.End();
            }
            if (!teDAL.PageIsHtml() || CurrentPage > site.CreatePages || q("preview") == "1")//直接动态
            {
                teDAL.IsHtml = false;
                string TxtStr = GetContentFile(ChannelId, teDAL.MainChannel.Type, ContentId, CurrentPage);
                teDAL.ReplaceShtmlTag(ref TxtStr);
                Response.Write(TxtStr);//直接输出
            }
            else
            {
                teDAL.IsHtml = true;
                string HtmlUrl = Go2View(CurrentPage, true, ChannelId, ContentId, true);
                if (!System.IO.File.Exists(Server.MapPath(HtmlUrl)))//静态
                {
                    string TxtStr = GetContentFile(ChannelId, teDAL.MainChannel.Type, ContentId, CurrentPage);
                    SaveCacheFile(TxtStr, Server.MapPath(HtmlUrl));
                }
                Response.Redirect(HtmlUrl);
            }
        }
    }
}
