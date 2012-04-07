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
using System.Web.UI.WebControls;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _user_edit : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("master", "html");
            id = Str2Str(q("id"));
            JumboTCMS.DBUtility.WebFormHandler wh = new JumboTCMS.DBUtility.WebFormHandler(doh, "jcms_normal_user", btnSave);
            wh.AddBind(txtUserName, "UserName", true);
            wh.AddBind(txtMax, "Integral", true);
            this.txtUserName.ReadOnly = true;
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
            return true;
        }
        protected void save_ok(object sender, EventArgs e)
        {
            string _uName = this.txtUserName.Text;
            JumboTCMS.DAL.Normal_UserDAL _User = new JumboTCMS.DAL.Normal_UserDAL();
            JumboTCMS.DAL.Normal_AdminlogsDAL _Adminlogs = new JumboTCMS.DAL.Normal_AdminlogsDAL();
            JumboTCMS.DAL.Normal_UserLogsDAL _Userlogs = new JumboTCMS.DAL.Normal_UserLogsDAL();

            if (this.txtUserPass.Text.Length > 0)
            {
                _User.ChangePsd(id, JumboTCMS.Utils.MD5.Lower32(this.txtUserPass.Text));
                _Adminlogs.SaveLog(AdminId, "修改了ID为" + id + "的用户的密码为:" + this.txtUserPass.Text);
            }
            if (this.txtPoints.Text != "0")
            {
                _User.AddPoints(id, Str2Int(this.txtPoints.Text));
                _Adminlogs.SaveLog(AdminId, "给[" + _uName + "]充博币:" + this.txtPoints.Text);
                _Userlogs.SaveLog(id, AdminName + "给你充了博币" + this.txtPoints.Text + "", 4);
            }
            if (this.ddlVIPYears.SelectedValue != "0")
            {
                _User.AddVIPYears(id, Str2Int(this.ddlVIPYears.SelectedValue));
                _Adminlogs.SaveLog(AdminId, "给[" + _uName + "]包了" + this.ddlVIPYears.SelectedValue + "年VIP服务");
                _Userlogs.SaveLog(id, AdminName + "给你包了" + this.ddlVIPYears.SelectedValue + "年VIP服务", 5);

            }
            if (this.txtIntegral.Text != "0")
            {
                _User.DeductIntegral(id, Str2Int(this.txtIntegral.Text));
                _Adminlogs.SaveLog(AdminId, "给[" + _uName + "]扣积分:" + this.txtIntegral.Text);
                _Userlogs.SaveLog(id, AdminName + "给你扣除" + this.txtIntegral.Text + "积分", 4);
            }
            FinalMessage("成功保存", "close.htm", 0);
        }
    }
}
