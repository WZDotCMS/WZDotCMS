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
    public partial class _service_add : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("master", "html");
            id = Str2Str(q("id"));
            JumboTCMS.DBUtility.WebFormHandler wh = new JumboTCMS.DBUtility.WebFormHandler(doh, "jcms_normal_user", btnSave);
            wh.AddBind(lblUserName, "UserName", true);
            wh.AddBind(txtServiceName, "ServiceName", true);
            this.txtServiceName.ReadOnly = false;
            wh.ConditionExpress = "id=" + id;
            wh.Mode = JumboTCMS.DBUtility.OperationType.Modify;
            wh.validator = chkForm;
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
            doh.Reset();
            doh.SqlCmd = "SELECT ServiceId FROM [jcms_normal_user] WHERE [ServiceName]='" + txtServiceName.Text + "'";
            if (doh.GetDataTable().Rows.Count > 0)
            {
                FinalMessage("用户名重复", "", 1);
                return false;
            }
            return true;
        }
        protected void save_ok(object sender, EventArgs e)
        {
            doh.Reset();
            doh.ConditionExpress = "id=" + id;
            doh.AddFieldItem("ServiceId", id);
            doh.AddFieldItem("GUID", "00000000-0000-0000-0000-" + id.PadLeft(12, '0'));
            doh.Update("jcms_normal_user");
            new JumboTCMS.DAL.Normal_UserDAL().RefreshServiceList();
            FinalMessage("成功保存", "close.htm", 0);
        }
    }
}
