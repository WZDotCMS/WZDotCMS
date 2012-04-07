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
using System.Web;
using System.Web.Caching;
using System.Text;

namespace JumboTCMS.Utils
{
    /// <summary>
    /// App操作类
    /// </summary>
    public static class App
    {
        public static string Url
        {
            get
            {
                if (HttpContext.Current.Request.Url.Port == 80)
                    return "http://" + HttpContext.Current.Request.Url.Host;
                else
                    return "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port;
            }
        }
        /// <summary>
        /// 应用程序路径，以/结尾
        /// </summary>
        /// <returns>如:/，/cms/</returns>
        public static string Path
        {
            get
            {
                string _ApplicationPath = System.Web.HttpContext.Current.Request.ApplicationPath;
                if (_ApplicationPath != "/")
                    _ApplicationPath += "/";
                return _ApplicationPath;
            }
        }
    }
}
