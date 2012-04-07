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
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Ajax
{
    public partial class _user : JumboTCMS.UI.FrontAjax
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 8;//脚本过期时间
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxLoginbar":
                    GetLoginbar();
                    break;
                case "ajaxUserInfo":
                    GetUserInfo();
                    break;
                default:
                    DefaultResponse();
                    break;
            }

            Response.Write(this._response);
        }

        private void DefaultResponse()
        {
            this._response = "{result :\"未知操作\"}";
        }
        private void GetUserInfo()
        {
            string tempBody = string.Empty;
            string _userid = "0";
            string _groupname = string.Empty;
            if (Cookie.GetValue(site.CookiePrev + "user") != null)
            {
                _userid = Cookie.GetValue(site.CookiePrev + "user", "id");
                _groupname = Cookie.GetValue(site.CookiePrev + "user", "groupname");
                tempBody = "{result :\"1\",";
                JumboTCMS.Entity.Normal_User _User = new JumboTCMS.DAL.Normal_UserDAL().GetEntity(_userid);
                doh.Reset();
                doh.ConditionExpress = "ReceiveUserId=" + _userid + " and state=0";
                int _newmessage = doh.Count("jcms_normal_user_message");
                doh.Reset();
                doh.ConditionExpress = "UserId=" + _userid + " and state=0";
                int _newnotice = doh.Count("jcms_normal_user_notice");
                int _newcart = new JumboTCMS.DAL.Normal_UserCartDAL().GetNewGoods(_userid);
                tempBody += "userid :\"" + _User.Id + "\"," +
                    "username :\"" + _User.UserName + "\"," +
                    "nickname :\"" + _User.NickName + "\"," +
                    "signature :\"" + _User.Signature + "\"," +
                    "userpass :\"" + _User.UserPass + "\"," +
                    "userkey :\"" + _User.UserPass.Substring(4, 8) + "\"," +
                    "email :\"" + _User.Email + "\"," +
                    "sex :\"" + _User.Sex + "\"," +
                    "isvip :\"" + _User.IsVIP + "\"," +
                    "vipdate :\"" + _User.VIPDate + "\"," +
                    "truename :\"" + _User.TrueName + "\"," +
                    "idtype :\"" + _User.IDType + "\"," +
                    "idcard :\"" + _User.IDCard + "\"," +
                    "points :\"" + _User.Points + "\"," +
                    "integral :\"" + _User.Integral + "\"," +
                    "groupname :\"" + _groupname + "\"," +
                    "newmessage :\"" + _newmessage + "\"," +
                    "newnotice :\"" + _newnotice + "\"," +
                    "newcart :\"" + _newcart + "\"," +
                    "birthday :\"" + _User.BirthDay + "\"," +
                    "provincecity :\"" + _User.ProvinceCity + "\"," +
                    "workunit :\"" + _User.WorkUnit + "\"," +
                    "address :\"" + _User.Address + "\"," +
                    "zipcode :\"" + _User.ZipCode + "\"," +
                    "qq :\"" + _User.QQ + "\"," +
                    "msn :\"" + _User.MSN + "\"," +
                    "mobiletel :\"" + _User.MobileTel + "\"," +
                    "telephone :\"" + _User.Telephone + "\"," +
                    "homepage :\"" + _User.HomePage + "\"," +
                    "adminid :\"" + _User.AdminId + "\"," +
                    "adminlogined :\"" + ((Cookie.GetValue(site.CookiePrev + "admin") != null) ? "1" : "0") + "\"," +
                    "adminname :\"" + _User.AdminName + "\"," +
                    "forumname :\"" + _User.ForumName + "\"" +
                    "}";
            }
            else
            {
                tempBody = "{result :\"0\",";
                tempBody += "userid :\"0\"," +
                    "username :\"\"," +
                    "nickname :\"\"," +
                    "userpass :\"\"," +
                    "userkey :\"\"," +
                    "email :\"\"," +
                    "sex :\"0\"," +
                    "isvip :\"0\"," +
                    "vipdate :\"\"," +
                    "truename :\"\"," +
                    "idtype :\"\"," +
                    "idcard :\"\"," +
                    "points :\"\"," +
                    "integral :\"\"," +
                    "groupname :\"\"," +
                    "newmessage :\"0\"," +
                    "newnotice :\"0\"," +
                    "newcart :\"0\"," +
                    "birthday :\"\"," +
                    "provincecity :\"\"," +
                    "workunit :\"\"," +
                    "address :\"\"," +
                    "zipcode :\"\"," +
                    "qq :\"\"," +
                    "msn :\"\"," +
                    "mobiletel :\"\"," +
                    "telephone :\"\"," +
                    "homepage :\"\"," +
                    "adminid :\"0\"," +
                    "adminlogined :\"" + ((Cookie.GetValue(site.CookiePrev + "admin") != null) ? "1" : "0") + "\"," +
                    "adminname :\"\"," +
                    "forumname :\"\"" +
                    "}";
            }
            this._response = tempBody;
        }
        private void GetLoginbar()
        {
            string tempBody = string.Empty;
            string returninfo = string.Empty;
            if (f("state") == "1")
            {
                string uName = f("name");
                string uPass = f("pass");
                returninfo = new JumboTCMS.DAL.Normal_UserDAL().ChkUserLogin(uName, uPass, 1);
            }
            if (Cookie.GetValue(site.CookiePrev + "user") != null)
            {
                string UserId = Cookie.GetValue(site.CookiePrev + "user", "id");
                string UserName = Cookie.GetValue(site.CookiePrev + "user", "name");
                string UserNickName = Cookie.GetValue(site.CookiePrev + "user", "nickname");
                string UserPass = Cookie.GetValue(site.CookiePrev + "user", "password");
                string UserGroupName = Cookie.GetValue(site.CookiePrev + "user", "groupname");
                doh.Reset();
                doh.ConditionExpress = "ReceiveUserId=" + UserId + " and state=0";
                int _newmessage = doh.Count("jcms_normal_user_message");
                doh.Reset();
                doh.ConditionExpress = "UserId=" + UserId + " and state=0";
                int _newnotice = doh.Count("jcms_normal_user_notice");
                int _newcart = new JumboTCMS.DAL.Normal_UserCartDAL().GetNewGoods(UserId);
                JumboTCMS.Entity.Normal_User _User = new JumboTCMS.DAL.Normal_UserDAL().GetEntity(UserId);
                tempBody = "{result :\"1\"," +
                    "userid :\"" + UserId + "\"," +
                    "username :\"" + UserName + "\"," +
                    "nickname :\"" + UserNickName + "\"," +
                    "userpass :\"" + UserPass + "\"," +
                    "userkey :\"" + UserPass.Substring(4, 8) + "\"," +
                    "points :\"" + _User.Points + "\"," +
                    "integral :\"" + _User.Integral + "\"," +
                    "isvip :\"" + _User.IsVIP + "\"," +
                    "vipdate :\"" + _User.VIPDate + "\"," +
                    "groupname :\"" + UserGroupName + "\"," +
                    "newmessage :\"" + _newmessage + "\"," +
                    "newnotice :\"" + _newnotice + "\"," +
                    "newcart :\"" + _newcart + "\"," +
                    "adminid :\"" + _User.AdminId + "\"" +
                    "}";
                this._response = tempBody;
            }
            else
            {
                this._response = "{result :\"0\"";
                if (f("state") == "1")
                    this._response += ",returnval :\"" + returninfo + "\"";
                this._response += "}";
            }
        }
    }
}