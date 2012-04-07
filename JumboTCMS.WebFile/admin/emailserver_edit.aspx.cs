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
    public partial class _emailserver_edit : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("0001", "html");
            id = Str2Str(q("id"));
            JumboTCMS.DBUtility.WebFormHandler wh = new JumboTCMS.DBUtility.WebFormHandler(doh, "jcms_normal_emailserver", btnSave);
            wh.AddBind(txtFromAddress, "FromAddress", true);
            wh.AddBind(txtFromName, "FromName", false);
            wh.AddBind(txtFromPwd, "FromPwd", false);
            wh.AddBind(txtSmtpHost, "SmtpHost", true);
            if (id == "0")
            {
                wh.Mode = JumboTCMS.DBUtility.OperationType.Add;
            }
            else
            {
                wh.ConditionExpress = "id=" + id;
                wh.Mode = JumboTCMS.DBUtility.OperationType.Modify;
            }
            wh.validator = chkForm;
            wh.AddOk += new EventHandler(save_ok);
            wh.ModifyOk += new EventHandler(save_ok);
        }
        protected void bind_ok(object sender, EventArgs e)
        {
        }
        protected bool chkForm()
        {
            if (!CheckFormUrl())
                return false;
            if (!Page.IsValid)
                return false;
            doh.Reset();
            doh.ConditionExpress = "fromaddress=@fromaddress and id<>" + id;
            doh.AddConditionParameter("@fromaddress", txtFromAddress.Text);
            if (doh.Exist("jcms_normal_emailserver"))
            {
                FinalMessage("邮箱重复!", "", 1);
                return false;
            }
            return true;
        }
        protected void save_ok(object sender, EventArgs e)
        {
            if (id == "0")
            {
                JumboTCMS.DBUtility.DbOperEventArgs de = (JumboTCMS.DBUtility.DbOperEventArgs)e;
                id = de.id.ToString();
            }
            string _To = this.txtToAddress.Text;
            string _Title = "邮箱配置测试邮件(请删)";
            string _Body = "邮件测试！<br>" +
                site.Name + "成功配置了系统邮箱！！！<br>" +
                "<a href=\"" + site.Url + site.Dir + "\" target=\"_blank\">" + site.Name +
                "</a>";
            string _MailFromAddress = this.txtFromAddress.Text;
            string _MailFromName = this.txtFromName.Text;
            string _MailFromPwd = this.txtFromPwd.Text;
            string _MailSmtpHost = this.txtSmtpHost.Text;
            string _MailSmtpPort = this.txtSmtpPort.Text;
            if (JumboTCMS.Common.MailHelp.SendOK(_To, _Title, _Body, true, _MailFromAddress, _MailFromName, _MailFromPwd, _MailSmtpHost, Str2Int(_MailSmtpPort)))
            {
                doh.Reset();
                doh.ConditionExpress = "id=@id";
                doh.AddConditionParameter("@id", id);
                doh.AddFieldItem("Enabled", 1);
                doh.Update("jcms_normal_emailserver");
                new JumboTCMS.DAL.Normal_UserMailDAL().ExportEmailServer();
                FinalMessage("成功保存", "close.htm", 0);
            }
            else
            {
                doh.Reset();
                doh.ConditionExpress = "id=@id";
                doh.AddConditionParameter("@id", id);
                doh.AddFieldItem("Enabled", 0);
                doh.Update("jcms_normal_emailserver");
                FinalMessage("配置有误:具体请查看<a href='" + site.Dir + "_data/log/mailerror_" + DateTime.Now.ToString("yyyyMMdd") + ".txt' target='_blank'>日志文件</a>。", "close.htm", 0, 30);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
