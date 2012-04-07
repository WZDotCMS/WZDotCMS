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
    public partial class _modules_setting : JumboTCMS.UI.AdminCenter
    {
        public string UserDisplay = "";
        private string _moduleType = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("master", "html");
            _moduleType = q("type");
            if (!("|article|soft|photo|video|").Contains("|" + _moduleType + "|"))
            {
                UserDisplay = "none";
            }
            if (!Page.IsPostBack)
            {
                string strXmlFile1 = HttpContext.Current.Server.MapPath("~/_data/config/upload_admin.config");
                JumboTCMS.DBUtility.XmlControl XmlTool1 = new JumboTCMS.DBUtility.XmlControl(strXmlFile1);
                string _AdminUploadType = XmlTool1.GetText("Module/" + _moduleType + "/type");
                string _AdminUploadSize = XmlTool1.GetText("Module/" + _moduleType + "/size");
                this.txtAdminUploadType.Text = _AdminUploadType;
                this.txtAdminUploadSize.Text = _AdminUploadSize;
                XmlTool1.Dispose();
                string strXmlFile2 = HttpContext.Current.Server.MapPath("~/_data/config/upload_user.config");
                JumboTCMS.DBUtility.XmlControl XmlTool2 = new JumboTCMS.DBUtility.XmlControl(strXmlFile2);
                string _UserUploadType = XmlTool2.GetText("Module/" + _moduleType + "/type");
                string _UserUploadSize = XmlTool2.GetText("Module/" + _moduleType + "/size");
                this.txtUserUploadType.Text = _UserUploadType;
                this.txtUserUploadSize.Text = _UserUploadSize;
                XmlTool2.Dispose();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/upload_admin.config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            XmlTool.Update("Module/" + _moduleType + "/type", this.txtAdminUploadType.Text, true);
            XmlTool.Update("Module/" + _moduleType + "/size", this.txtAdminUploadSize.Text);
            XmlTool.Save();
            XmlTool.Dispose();
            FinalMessage("保存成功!", "modules_setting.aspx?type=" + _moduleType, 0);
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/upload_user.config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            XmlTool.Update("Module/" + _moduleType + "/type", this.txtUserUploadType.Text, true);
            XmlTool.Update("Module/" + _moduleType + "/size", this.txtUserUploadSize.Text);
            XmlTool.Save();
            XmlTool.Dispose();
            FinalMessage("保存成功!", "modules_setting.aspx?type=" + _moduleType, 0);
        }
    }
}
