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
using System.Web.UI.WebControls;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Plus
{
    public partial class _rss : JumboTCMS.UI.FrontHtml
    {
        protected void Page_Unload(object sender, EventArgs e)
        {
            SavePageLog(1);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 8;//脚本过期时间
            int CurrentPage = Int_ThisPage();
            string ClassId = this.lblClassId.Text == "{$ClassId}" ? Str2Str(q("id")) : this.lblClassId.Text;
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
            string ClassName = string.Empty;
            string ClassCode = string.Empty;
            int PSize = Str2Int(q("PSize"), 20);
            if (ClassId == "0")
            {
                ClassName = "全部栏目";
            }
            else
            {
                doh.Reset();
                doh.SqlCmd = "SELECT * FROM [jcms_normal_class] WHERE [IsOut]=0 AND [ChannelId]=" + ChannelId + " and [Id]=" + ClassId;
                DataTable dtClass = doh.GetDataTable();
                if (dtClass.Rows.Count == 0)
                {
                    FinalMessage("栏目不存在或已被删除!", site.Dir, 0, 8);
                    Response.End();
                }
                ClassName = dtClass.Rows[0]["Title"].ToString();
                ClassCode = dtClass.Rows[0]["code"].ToString();
                dtClass.Clear();
                dtClass.Dispose();
            }
            JumboTCMS.DAL.TemplateEngineDAL teDAL = new JumboTCMS.DAL.TemplateEngineDAL(ChannelId);
            teDAL.IsHtml = site.IsHtml;
            Response.Charset = "utf-8";
            Response.ContentType = "text/xml";
            Response.Expires = 0;
            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            Response.AddHeader("pragma", "no-cache");
            Response.CacheControl = "no-cache";
            StringBuilder strCode = new StringBuilder();
            strCode.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n");
            strCode.Append("<rss version=\"2.0\">\r\n");
            strCode.Append("    <channel>\r\n");
            strCode.Append("        <title>" + site.Name + "_" + ClassName + "</title>\r\n");
            if (ClassId != "0")
                strCode.Append("        <link>" + Go2Class(1, false, ChannelId, ClassId, false) + "</link>\r\n");
            else
                strCode.Append("        <link>" + Go2Channel(site.IsHtml, ChannelId, false) + "</link>\r\n");
            strCode.Append("        <description>" + site.Description + "</description>\r\n");
            strCode.Append("        <copyright>Copyright (C) " + site.Name + "</copyright>\r\n");

            string whereStr = "";
            if (ClassId != "0")
                whereStr = " [ClassID] in (Select id FROM [jcms_normal_class] WHERE [Code] LIKE '" + ClassCode + "%') and [IsPass]=1 AND [ChannelId]=" + ChannelId;
            else
                whereStr = " [ChannelId]=" + ChannelId;
            int page = Int_ThisPage();
            string sqlStr = "";
            sqlStr = JumboTCMS.Utils.SqlHelp.GetSql("id,title,Author,AddDate,ChannelId,ClassId,Summary", "jcms_module_" + teDAL.MainChannel.Type, "id", PSize, page, "desc", whereStr);
            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dtContent = doh.GetDataTable();
            for (int i = 0; i < dtContent.Rows.Count; i++)
            {
                string aId = dtContent.Rows[i]["Id"].ToString();
                string aTitle = dtContent.Rows[i]["Title"].ToString();
                string aAuthor = dtContent.Rows[i]["Author"].ToString();
                string aAddDate = dtContent.Rows[i]["AddDate"].ToString();
                string aSummary = dtContent.Rows[i]["Summary"].ToString();
                string aClassId = dtContent.Rows[i]["ClassId"].ToString();
                strCode.Append("        <item>\r\n");
                strCode.Append("            <title><![CDATA[" + aTitle + "]]></title>\r\n");
                strCode.Append("            <link><![CDATA[" + Go2View(1, false, ChannelId, aId, false) + "]]></link>\r\n");
                strCode.Append("            <author><![CDATA[" + aAuthor + "]]></author>\r\n");
                strCode.Append("            <description><![CDATA[" + aSummary + "]]></description>\r\n");
                strCode.Append("            <AddDate><![CDATA[" + Convert.ToDateTime(aAddDate).ToString("yyyy-MM-dd HH:mm:ss") + "]]></AddDate>\r\n");
                strCode.Append("            <category><![CDATA[" + (new JumboTCMS.DAL.Normal_ClassDAL().GetClassName(aClassId)) + "]]></category>\r\n");
                strCode.Append("        </item>\r\n");
            }
            strCode.Append("    </channel>\r\n");
            strCode.Append("</rss>\r\n");
            Response.Write(strCode.ToString());
            dtContent.Clear();
            dtContent.Dispose();
        }

    }
}
