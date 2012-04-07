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
using System.Web.UI;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Admin.Attachment
{
    public partial class _preview : JumboTCMS.UI.AdminCenter
    {
        public string RootPath = string.Empty;
        public string ElementID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ChannelId = Str2Str(q("ccid"));
            ElementID = q("ElementID");
            Admin_Load("", "html", true);
            RootPath = ChannelUploadPath;
        }
    }
}
