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
using JumboTCMS.Common;

namespace JumboTCMS.WebFile.Admin.Attachment
{
    public partial class _tree : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
	string dir;
	if(Request.Form["dir"] == null || Request.Form["dir"].Length <= 0)
		dir = "/";
	else
		dir = Server.UrlDecode(Request.Form["dir"]);
	System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Server.MapPath(dir));
	Response.Write("<ul class=\"jqueryFileTree\" style=\"display: none;\">\n");
	foreach (System.IO.DirectoryInfo di_child in di.GetDirectories())
		Response.Write("\t<li class=\"directory collapsed\"><a href=\"#\" rel=\"" + dir + di_child.Name + "/\">" + di_child.Name + "</a></li>\n");
	foreach (System.IO.FileInfo fi in di.GetFiles())
	{
		string ext = ""; 
		if(fi.Extension.Length > 1)
			ext = fi.Extension.Substring(1).ToLower();
			
		Response.Write("\t<li class=\"file ext_" + ext + "\"><a href=\"#\" rel=\"" + dir + fi.Name + "\">" + fi.Name + "</a></li>\n");		
	}
	Response.Write("</ul>");
        }
    }
}
