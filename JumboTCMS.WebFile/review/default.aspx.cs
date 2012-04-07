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
namespace JumboTCMS.WebFile.Review
{
    public partial class _review_index : JumboTCMS.UI.FrontHtml
    {
        protected void Page_Unload(object sender, EventArgs e)
        {
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckExtendState("Review", "html");
            string ContentId = Str2Str(q("id"));
            if (ContentId == "0")
            {
                FinalMessage("参数错误!", site.Dir, 0, 8);
                Response.End();
            }
            string ChannelId = Str2Str(q("ccid"));
            doh.Reset();
            doh.ConditionExpress = "id=@id and Enabled=1";
            doh.AddConditionParameter("@id", ChannelId);
            if (!doh.Exist("jcms_normal_channel"))
            {
                FinalMessage("频道不存在或被禁用!", site.Dir, 0, 8);
                Response.End();
            }
            JumboTCMS.DAL.TemplateEngineDAL teDAL = new JumboTCMS.DAL.TemplateEngineDAL(ChannelId);
            doh.Reset();
            doh.SqlCmd = "SELECT * FROM [jcms_module_" + teDAL.MainChannel.Type + "] WHERE [ChannelId]=" + ChannelId + " and [IsPass]=1 and [Id]=" + ContentId;
            DataTable dtContent = doh.GetDataTable();
            string PageStr = JumboTCMS.Utils.DirFile.ReadFile("~/templates/system_review_index.htm");
            ReplaceSiteTags(ref PageStr);
            teDAL.ReplaceContentTag(ref PageStr, ContentId);
            dtContent.Clear();
            dtContent.Dispose();
            Response.Write(PageStr);//直接输出
        }
    }
}
