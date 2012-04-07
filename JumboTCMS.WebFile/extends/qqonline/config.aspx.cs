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
namespace JumboTCMS.WebFile.Extends.QQOnline
{
    public partial class _config : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("master", "html");
            if (!Page.IsPostBack)
            {
                string strXmlFile1 = HttpContext.Current.Server.MapPath("~/_data/config/extends/qqonline.config");
                JumboTCMS.DBUtility.XmlControl XmlTool1 = new JumboTCMS.DBUtility.XmlControl(strXmlFile1);
                this.txtSiteShowX.Text = XmlTool1.GetText("Root/siteshowx");
                this.txtSiteShowY.Text = XmlTool1.GetText("Root/siteshowy");
                this.rblSiteArea.SelectedValue = XmlTool1.GetText("Root/sitearea");
                this.rblSiteSkin.SelectedValue = XmlTool1.GetText("Root/siteskin");
                XmlTool1.Dispose();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string strXmlFile1 = HttpContext.Current.Server.MapPath("~/_data/config/extends/qqonline.config");
            JumboTCMS.DBUtility.XmlControl XmlTool1 = new JumboTCMS.DBUtility.XmlControl(strXmlFile1);
            XmlTool1.Update("Root/siteshowx", this.txtSiteShowX.Text);
            XmlTool1.Update("Root/siteshowy", this.txtSiteShowY.Text);
            XmlTool1.Update("Root/sitearea", this.rblSiteArea.SelectedValue);
            XmlTool1.Update("Root/siteskin", this.rblSiteSkin.SelectedValue);
            XmlTool1.Save();
            XmlTool1.Dispose();
            FinalMessage("成功保存", site.Dir + "admin/close.htm", 0);
        }
    }
}
