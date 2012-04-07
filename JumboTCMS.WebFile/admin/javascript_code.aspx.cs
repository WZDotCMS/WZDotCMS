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
    public partial class _javascriptcode : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("js-mng", "html");
            id = Str2Str(q("id"));
            if (!Page.IsPostBack)
            {
                doh.Reset();
                doh.ConditionExpress = "id=@id";
                doh.AddConditionParameter("@id", id);
                string _code = doh.GetField("jcms_normal_javascript", "Code").ToString();
                this.ltlCode.Text = this.txtCode.Text = "<script charset=\"utf-8\" language=\"javascript\" type=\"text/javascript\" src=\"" + site.Url + site.Dir + "plus/javascript.aspx?code=" + _code + "\"></script>";
            }
        }
    }
}
