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
using JumboTCMS.Utils;
using JumboTCMS.DBUtility;

namespace JumboTCMS.DAL
{
    /// <summary>
    /// 会员通知表信息
    /// </summary>
    public class Normal_UserMailDAL : Common
    {
        public Normal_UserMailDAL()
        {
            base.SetupSystemDate();
        }
        /// <summary>
        /// 系统发邮件
        /// </summary>
        /// <param name="_To">收件人,单封邮件</param>
        /// <param name="_Title">标题</param>
        /// <param name="_Body">内容</param>
        /// <param name="_IsHtml">是否支持html</param>
        /// <param name="_MailServer">邮箱服务器列表</param>
        /// <returns></returns>
        public bool SendMails(string _To, string _Title, string _Body, bool _IsHtml, JumboTCMS.Entity.MailServer _MailServer)
        {
            _Body += "<br /><br />" + site.Name + "  <a href='" + site.Url + "'>" + site.Url + "</a>";
            return JumboTCMS.Common.MailHelp.SendOK(_To, _Title, _Body, _IsHtml, _MailServer);

        }
        public bool SendMails(string _To, string _Title, string _Body, JumboTCMS.Entity.MailServer _MailServer)
        {
            return SendMails(_To, _Title, _Body, true, _MailServer);
        }
        public bool SendMail(string _To, string _Title, string _Body)
        {
            string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/mail.config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            string _MailFrom = XmlTool.GetText("Root/Address");
            string _MailFromName = XmlTool.GetText("Root/NickName");
            string _MailPwd = XmlTool.GetText("Root/Password");
            string _MailSmtpHost = XmlTool.GetText("Root/SmtpHost");
            int _MailSmtpPort = Str2Int(XmlTool.GetText("Root/SmtpPort"));
            XmlTool.Dispose();
            _Body += "<br /><br />" + site.Name + "  <a href='" + site.Url + "'>" + site.Url + "</a>";
            return JumboTCMS.Common.MailHelp.SendOK(_To, _Title, _Body, true, _MailFrom, _MailFromName, _MailPwd, _MailSmtpHost, _MailSmtpPort);

        }
        /// <summary>
        /// 系统发邮件给客服
        /// </summary>
        /// <param name="_Title"></param>
        /// <param name="_Body"></param>
        /// <returns></returns>
        public bool SendServiceMail(string _Title, string _Body)
        {
            string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/message.config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            string _ServiceMail = XmlTool.GetText("Messages/Service/UserMail");
            XmlTool.Dispose();
            return SendMail(_ServiceMail, _Title, _Body);
        }
        /// <summary>
        /// 导出数据至配置文件
        /// </summary>
        /// <returns></returns>
        public bool ExportEmailServer()
        {
            string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/jcms(emailserver).config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            try
            {
                XmlTool.RemoveAll("Mails");
                XmlTool.Save();
                using (DbOperHandler _doh = new Common().Doh())
                {
                    _doh.Reset();
                    _doh.SqlCmd = "Select [Id],[FromAddress],[FromName],[FromPwd],[SmtpHost] FROM [jcms_normal_emailserver] WHERE [Enabled]=1 ORDER BY id asc";
                    DataTable dt = _doh.GetDataTable();
                    string _id = string.Empty;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        _id = dt.Rows[i]["Id"].ToString();
                        XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
                        XmlTool.InsertNode("Mails", "Mail", "ID", _id);
                        XmlTool.InsertElement("Mails/Mail[ID=\"" + _id + "\"]", "FromAddress", dt.Rows[i]["FromAddress"].ToString(), false);
                        XmlTool.InsertElement("Mails/Mail[ID=\"" + _id + "\"]", "FromName", dt.Rows[i]["FromName"].ToString(), false);
                        XmlTool.InsertElement("Mails/Mail[ID=\"" + _id + "\"]", "FromPwd", dt.Rows[i]["FromPwd"].ToString(), false);
                        XmlTool.InsertElement("Mails/Mail[ID=\"" + _id + "\"]", "SmtpHost", dt.Rows[i]["SmtpHost"].ToString(), false);
                        XmlTool.InsertElement("Mails/Mail[ID=\"" + _id + "\"]", "Used", DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss"), false);
                        XmlTool.Save();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 导入配置文件至数据库
        /// </summary>
        /// <returns></returns>
        public bool ImportEmailServer()
        {
            string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/jcms(emailserver).config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            try
            {
                using (DbOperHandler _doh = new Common().Doh())
                {
                    _doh.Reset();
                    _doh.Delete("jcms_normal_emailserver");
                    DataTable dt = XmlTool.GetTable("Mails");
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            _doh.Reset();
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                if (dt.Columns[j].ColumnName.ToLower() != "id" && dt.Columns[j].ColumnName.ToLower() != "used")
                                    _doh.AddFieldItem(dt.Columns[j].ColumnName.ToLower(), dt.Rows[i][j].ToString());
                            }
                            _doh.Insert("jcms_normal_emailserver");
                        }
                    }
                    dt.Clear();
                    dt.Dispose();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
