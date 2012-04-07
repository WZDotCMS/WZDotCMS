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
using System.Collections.Generic;
using JumboTCMS.Utils;
using JumboTCMS.Common;

namespace JumboTCMS.WebFile.Passport
{
    public partial class _register_third : JumboTCMS.UI.FrontPassport
    {
        public string _Email = "";
        public string _UserName = "";
        public string _Sex = "1";
        public string _Birthday = "1980-01-01";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!site.AllowReg || site.CheckReg)
            {
                FinalMessage("对不起，本站不允许使用第三方登录!", site.Dir, 0);
                Response.End();
            }
            if (JumboTCMS.Utils.Cookie.GetValue("OAuth_Info") == null || JumboTCMS.Utils.Cookie.GetValue("OAuth_Info") == "")
            {
                FinalMessage("接口会话已失效，请重新登录第三方网站", site.Dir + "passport/login.aspx", 0);
                Response.End();
            }
            string oauth_info = JumboTCMS.Utils.Cookie.GetValue("OAuth_Info");
            Dictionary<string, object> newobj = (Dictionary<string, object>)JumboTCMS.Utils.fastJSON.JSON.Instance.ToObject(oauth_info);
            string OAuth_Code = (string)newobj["code"];
            string OAuth_Token = (string)newobj["token"];
            _Email = (string)newobj["email"];
            _UserName = (string)newobj["username"];
            _Birthday = (string)newobj["birthday"];
            doh.Reset();
            doh.ConditionExpress = "[Token_" + OAuth_Code + "]='" + OAuth_Token + "' and state=1";
            string _userid = doh.GetField("jcms_normal_user", "id").ToString();
            if (_userid != "")
            {
                JumboTCMS.Entity.Normal_User _User = new JumboTCMS.DAL.Normal_UserDAL().GetEntity(_userid);
                new JumboTCMS.DAL.Normal_UserDAL().ChkUserLogin(_User.UserName, _User.UserPass, 1, true);
                Response.Redirect(site.Dir + "");
            }
        }
    }
}
