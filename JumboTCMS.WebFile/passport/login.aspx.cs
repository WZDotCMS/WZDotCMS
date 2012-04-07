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
    public partial class _login : JumboTCMS.UI.FrontPassport
    {
        public string Referer = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Cookie.GetValue(site.CookiePrev + "user") != null)
            {
                FinalMessage("请先注销当前用户再进行登录!", site.Dir, 0);
                Response.End();
            }
            Referer = site.Dir;
            if (q("refer") != "")
                Referer = q("refer");
            else
            {
                if (Request.ServerVariables["HTTP_REFERER"] != null)
                {
                    if (!Request.ServerVariables["HTTP_REFERER"].ToString().Contains("register") && !Request.ServerVariables["HTTP_REFERER"].ToString().Contains("logout"))
                        if (Request.Url.ToString() != Request.ServerVariables["HTTP_REFERER"].ToString())
                            Referer = Request.ServerVariables["HTTP_REFERER"].ToString();
                }
            }
        }
    }
}
