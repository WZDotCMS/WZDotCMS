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
using System.IO;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _discuz_api : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("master", "html");
            if (!Page.IsPostBack)
            {
                string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/site.config");
                JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
                this.txtForumAPIKey.Text = XmlTool.GetText("Root/ForumAPIKey");
                this.txtForumSecret.Text = XmlTool.GetText("Root/ForumSecret");
                this.txtForumUrl.Text = XmlTool.GetText("Root/ForumUrl");
                XmlTool.Dispose();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/site.config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            XmlTool.Update("Root/ForumAPIKey", this.txtForumAPIKey.Text);
            XmlTool.Update("Root/ForumSecret", this.txtForumSecret.Text);
            XmlTool.Update("Root/ForumUrl", this.txtForumUrl.Text);
            XmlTool.Save();
            XmlTool.Dispose();
            SetupSystemDate();
            FinalMessage("保存成功!", "discuz_api.aspx", 0);
        }
    }
}
