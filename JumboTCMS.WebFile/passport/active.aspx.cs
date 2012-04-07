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
using JumboTCMS.Common;

namespace JumboTCMS.WebFile.Passport
{
    public partial class _active : JumboTCMS.UI.BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string uUserName = q("username");
            string uEmail = q("email");
            string uUserSign = q("usersign");
            doh.Reset();
            doh.ConditionExpress = "username=@username and usersign=@usersign";
            doh.AddConditionParameter("@username", uUserName);
            doh.AddConditionParameter("@usersign", uUserSign);
            doh.AddFieldItem("State", 1);
            doh.AddFieldItem("UserSign", "");
            if (doh.Update("jcms_normal_user") == 1)
                Response.Write("<script>alert('您的帐号已激活成功');window.location.href='" + site.Dir + "passport/login.aspx';</script>");
            else
                Response.Write("<script>alert('参数失败');window.close();</script>");
        }
    }
}
