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
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _adv_view : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Str2Str(q("id"));
            Admin_Load("adv-mng", "stop");
            this.txtASPXTmpTag.Text = "<!--#include virtual=\"/_data/html/more/" + id + ".htm\" -->";
            this.txtSHTMTmpTag.Text = "<!--#include virtual=\"/_data/shtm/more/" + id + ".htm\" -->";
            this.txtJSTmpTag.Text = "<script type=\"text/javascript\" src=\"/_data/style/more/" + id + ".js\"></script>";
            this.Literal1.Text = new JumboTCMS.DAL.AdvDAL().GetAdvBody(id);
        }
    }
}
