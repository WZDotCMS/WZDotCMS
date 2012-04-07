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
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _mail_setting : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("master", "html");
            if (!Page.IsPostBack)
            {
                string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/mail.config");
                JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
                this.txtAddress.Text = XmlTool.GetText("Root/Address");
                this.txtNickName.Text = XmlTool.GetText("Root/NickName");
                this.txtPassword.Text = XmlTool.GetText("Root/Password");
                this.txtSmtpHost.Text = XmlTool.GetText("Root/SmtpHost");
                this.txtSmtpPort.Text = XmlTool.GetText("Root/SmtpPort");
                XmlTool.Dispose();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string _MailAddress = this.txtAddress.Text;
            string _MailNickName = this.txtNickName.Text;
            string _MailPassword = this.txtPassword.Text;
            string _MailSmtpHost = this.txtSmtpHost.Text;
            string _MailSmtpPort = this.txtSmtpPort.Text;
            string _To = this.txtTestAddress.Text;
            string _Title = "邮箱配置测试邮件(请删)";
            string _Body = "邮件测试！<br>" +
                site.Name + "成功配置了系统邮箱！！！<br>" +
                "<a href=\"" + site.Url + site.Dir + "\" target=\"_blank\">" + site.Name +
                "</a>";
            string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/mail.config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            XmlTool.Update("Root/Address", this.txtAddress.Text);
            XmlTool.Update("Root/NickName", this.txtNickName.Text);
            XmlTool.Update("Root/Password", this.txtPassword.Text);
            XmlTool.Update("Root/SmtpHost", this.txtSmtpHost.Text);
            XmlTool.Update("Root/SmtpPort", this.txtSmtpPort.Text);
            XmlTool.Save();
            XmlTool.Dispose();
            if (JumboTCMS.Common.MailHelp.SendOK(_To, _Title, _Body, true, _MailAddress, _MailNickName, _MailPassword, _MailSmtpHost, Str2Int(_MailSmtpPort)))
            {
                FinalMessage("配置成功", "mail_setting.aspx", 0);
            }
            else
            {
                FinalMessage("配置有误:具体请查看<a href='" + site.Dir + "_data/log/mailerror_" + DateTime.Now.ToString("yyyyMMdd") + ".txt' target='_blank'>日志文件</a>。", "mail_setting.aspx", 0, 30);
            }
        }
    }
}
