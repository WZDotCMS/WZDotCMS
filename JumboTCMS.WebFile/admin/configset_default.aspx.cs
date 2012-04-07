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
    public partial class _config_index : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("master", "html");
            if (!Page.IsPostBack)
            {
                string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/site.config");
                JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
                this.txtName.Text = XmlTool.GetText("Root/Name");
                this.txtName2.Text = XmlTool.GetText("Root/Name2");
                this.txtUrl.Text = XmlTool.GetText("Root/Url");
                this.txtICP.Text = XmlTool.GetText("Root/ICP");
                this.txtKeywords.Text = XmlTool.GetText("Root/Keywords");
                this.txtDescription.Text = XmlTool.GetText("Root/Description");
                this.rblAllowReg.Items.FindByValue(XmlTool.GetText("Root/AllowReg")).Selected = true;
                this.rblCheckReg.Items.FindByValue(XmlTool.GetText("Root/CheckReg")).Selected = true;
                this.rblIsHtml.Items.FindByValue(XmlTool.GetText("Root/IsHtml")).Selected = true;
                this.rbStaticExt.Items.FindByValue(XmlTool.GetText("Root/StaticExt").ToLower()).Selected = true;
                this.rblPassportTheme.Items.FindByValue(XmlTool.GetText("Root/PassportTheme")).Selected = true;
                XmlTool.Dispose();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/site.config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            XmlTool.Update("Root/Name", this.txtName.Text);
            XmlTool.Update("Root/Name2", this.txtName2.Text);
            XmlTool.Update("Root/Url", this.txtUrl.Text);
            XmlTool.Update("Root/ICP", this.txtICP.Text);
            XmlTool.Update("Root/Keywords", this.txtKeywords.Text);
            XmlTool.Update("Root/Description", this.txtDescription.Text);
            XmlTool.Update("Root/AllowReg", this.rblAllowReg.SelectedItem.Value);
            XmlTool.Update("Root/CheckReg", this.rblCheckReg.SelectedItem.Value);
            XmlTool.Update("Root/IsHtml", this.rblIsHtml.SelectedItem.Value);
            XmlTool.Update("Root/StaticExt", this.rbStaticExt.SelectedItem.Value);
            XmlTool.Update("Root/PassportTheme", this.rblPassportTheme.SelectedItem.Value);
            XmlTool.Save();
            XmlTool.Dispose();
            if (this.rblIsHtml.SelectedValue == "0")//非静态化
            {
                doh.Reset();
                doh.AddFieldItem("IsHtml", 0);
                doh.Update("jcms_normal_channel");
            }
            new JumboTCMS.DAL.SiteDAL().CreateSiteFiles();
            SetupSystemDate();
            new JumboTCMS.DAL.Normal_AdminlogsDAL().SaveLog(AdminId, "修改了网站参数");
            FinalMessage("保存成功,已更新缓存!", "configset_default.aspx", 0);
        }
    }
}
