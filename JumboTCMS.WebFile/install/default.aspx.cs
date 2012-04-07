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
namespace JumboTCMS.WebFile.Install
{
    public partial class _default : System.Web.UI.Page
    {
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Request.Url.Host.Contains("localhost") || Request.Url.Host.Contains("127.0.0.1")) && (System.IO.File.Exists(System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\_data\\" + "install.dat")))
            {
                Response.Redirect("../");
                Response.End();
            }
            System.Web.HttpContext.Current.Application.Lock();
            System.Web.HttpContext.Current.Application["jcmsV5_dbType"] = null;
            System.Web.HttpContext.Current.Application["jcmsV5_dbPath"] = null;
            System.Web.HttpContext.Current.Application["jcmsV5_dbConnStr"] = null;
            System.Web.HttpContext.Current.Application["jcmsV5"] = null;
            System.Web.HttpContext.Current.Application.UnLock();
        }
    }
}