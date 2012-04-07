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
    public partial class _javascriptedit : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("js-mng", "stop");
            id = Str2Str(q("id"));
            JumboTCMS.DBUtility.WebFormHandler wh = new JumboTCMS.DBUtility.WebFormHandler(doh, "jcms_normal_javascript", btnSave);
            wh.AddBind(txtTitle, "Title", true);
            wh.AddBind(txtCode, "Code", true);
            wh.AddBind(txtTemplateContent, "TemplateContent", true);
            if (id == "0")
            {
                this.txtCode.Text = GetRandomNumberString(64, false);
                wh.Mode = JumboTCMS.DBUtility.OperationType.Add;
            }
            else
            {
                this.txtCode.Enabled = false;
                wh.ConditionExpress = "id=" + id;
                wh.Mode = JumboTCMS.DBUtility.OperationType.Modify;
            }
            wh.validator = chkForm;
            wh.AddOk += new EventHandler(save_ok);
            wh.ModifyOk += new EventHandler(save_ok);
        }
        protected void bind_ok(object sender, EventArgs e)
        {
        }
        protected bool chkForm()
        {
            if (!CheckFormUrl())
                return false;
            if (!Page.IsValid)
                return false;
            return true;
        }
        protected void save_ok(object sender, EventArgs e)
        {
            ajaxCreateJavascript();
            FinalMessage("成功保存", "close.htm", 0);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
        }
    }
}
