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
using System.IO;
using System.Web;
namespace JumboTCMS.Common.Handler
{
    public class FileHandler : IHttpHandler
    {
        public FileHandler()
        {
        }

        public void ProcessRequest(HttpContext context)
        {
            string fileName = context.Request.QueryString["file"];

            if (File.Exists(fileName))
            {
                context.Response.AppendHeader("Content-Type", "application/octet-stream");
                context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + context.Server.UrlEncode(Path.GetFileName(fileName)));
                context.Response.WriteFile(fileName, false);
            }
            else
            {
                context.Response.Status = "404 File Not Found";
                context.Response.StatusCode = 404;
                context.Response.StatusDescription = "File Not Found";
                context.Response.Write("File Not Found");
            }
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}
