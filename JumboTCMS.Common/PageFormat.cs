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
namespace JumboTCMS.Common
{
    /// <summary>
    /// 页面地址格式
    /// </summary>
    public static class PageFormat
    {
        /// <summary>
        /// 站点首页
        /// </summary>
        public static string Site(string _siteDir, bool urlRewrite)
        {
            string strXmlFile = HttpContext.Current.Server.MapPath(_siteDir + "_data/config/pageformat.config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            string TempUrl = "";
            if (urlRewrite)
                TempUrl = XmlTool.GetText("Pages/Site/P_1");
            else
                TempUrl = XmlTool.GetText("Pages/Site/P_0");
            XmlTool.Dispose();
            return TempUrl;
        }
        /// <summary>
        /// 频道首页
        /// </summary>
        public static string Channel(string _siteDir, bool urlRewrite)
        {
            string strXmlFile = HttpContext.Current.Server.MapPath(_siteDir + "_data/config/pageformat.config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            string TempUrl = "";
            if (urlRewrite)
                TempUrl = XmlTool.GetText("Pages/Channel/P_1");
            else
                TempUrl = XmlTool.GetText("Pages/Channel/P_0");
            XmlTool.Dispose();
            return TempUrl;
        }
        /// <summary>
        /// 栏目页
        /// </summary>
        public static string Class(bool _isHtml, string _siteDir, bool urlRewrite, int page)
        {
            string strXmlFile = HttpContext.Current.Server.MapPath(_siteDir + "_data/config/pageformat.config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            string TempUrl = "";
            if (_isHtml)
            {
                if (page == 1)
                    TempUrl = XmlTool.GetText("Pages/Class/P_2_1");
                else
                    TempUrl = XmlTool.GetText("Pages/Class/P_2_N");
            }
            else
            {
                if (urlRewrite)
                    if (page == 1)
                        TempUrl = XmlTool.GetText("Pages/Class/P_1_1");
                    else
                        TempUrl = XmlTool.GetText("Pages/Class/P_1_N");
                else
                    if (page == 1)
                        TempUrl = XmlTool.GetText("Pages/Class/P_0_1");
                    else
                        TempUrl = XmlTool.GetText("Pages/Class/P_0_N");
            }
            XmlTool.Dispose();
            return TempUrl;
        }
        /// <summary>
        /// RSS页
        /// </summary>
        public static string Rss(bool _isHtml, string _siteDir, bool urlRewrite, int page)
        {
            string strXmlFile = HttpContext.Current.Server.MapPath(_siteDir + "_data/config/pageformat.config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            string TempUrl = "";
            if (urlRewrite)
                if (page == 1)
                    TempUrl = XmlTool.GetText("Pages/Rss/P_1_1");
                else
                    TempUrl = XmlTool.GetText("Pages/Rss/P_1_N");
            else
                if (page == 1)
                    TempUrl = XmlTool.GetText("Pages/Rss/P_0_1");
                else
                    TempUrl = XmlTool.GetText("Pages/Rss/P_0_N");
            XmlTool.Dispose();
            return TempUrl;
        }
        /// <summary>
        /// 内容页
        /// </summary>
        public static string View(bool _isHtml, string _siteDir, bool urlRewrite, int page)
        {
            string strXmlFile = HttpContext.Current.Server.MapPath(_siteDir + "_data/config/pageformat.config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            string TempUrl = "";
            if (_isHtml)
            {
                if (page == 1)
                    TempUrl = XmlTool.GetText("Pages/View/P_2_1");
                else
                    TempUrl = XmlTool.GetText("Pages/View/P_2_N");
            }
            else
            {
                if (urlRewrite)
                    if (page == 1)
                        TempUrl = XmlTool.GetText("Pages/View/P_1_1");
                    else
                        TempUrl = XmlTool.GetText("Pages/View/P_1_N");
                else
                    if (page == 1)
                        TempUrl = XmlTool.GetText("Pages/View/P_0_1");
                    else
                        TempUrl = XmlTool.GetText("Pages/View/P_0_N");
            }
            XmlTool.Dispose();
            return TempUrl;
        }
    }
}
