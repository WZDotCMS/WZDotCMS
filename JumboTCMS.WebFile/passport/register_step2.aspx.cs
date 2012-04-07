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
using JumboTCMS.Utils;
using JumboTCMS.Common;

namespace JumboTCMS.WebFile.Passport
{
    public partial class _register_step2 : JumboTCMS.UI.FrontPassport
    {
        public string _UserName = "";
        public string _Email = "";
        public string _UserSign = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            _UserName = q("username");
            _Email = q("email");
            _UserSign = q("usersign");

            if (Cookie.GetValue(site.CookiePrev + "user") != null)
            {
                FinalMessage("请先注销当前用户再进行注册!", site.Dir, 0);
                Response.End();
            }
            if (!site.AllowReg)
            {
                FinalMessage("对不起，本站暂停注册!", site.Dir, 0);
                Response.End();
            }
            if (JumboTCMS.Utils.Session.Get("jcms_user_register") != "1")
            {
                Response.Write("请勿随便试这个功能");
                Response.End();
            }
        }

    }
}
