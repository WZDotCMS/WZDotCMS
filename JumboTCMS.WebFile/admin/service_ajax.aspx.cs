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
using JumboTCMS.Utils;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _service_ajax : JumboTCMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckFormUrl())
            {
                Response.End();
            }
            Admin_Load("master", "json");
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxGetList":
                    ajaxGetList();
                    break;
                case "ajaxDel":
                    ajaxDel();
                    break;
                case "checkusername":
                    ajaxCheckUserName();
                    break;
                case "checkservicename":
                    ajaxCheckServiceName();
                    break;
                default:
                    DefaultResponse();
                    break;
            }
            Response.Write(this._response);
        }
        private void ajaxCheckUserName()
        {
            doh.Reset();
            doh.ConditionExpress = "username=@username";
            doh.AddConditionParameter("@username", q("txtUserName"));
            if (doh.Exist("jcms_normal_user"))
                this._response = JsonResult(1, "有此用户");
            else
                this._response = JsonResult(0, "帐号不存在");
        }

        private void ajaxCheckServiceName()
        {
            if (q("id") == "0")
            {
                doh.Reset();
                doh.ConditionExpress = "servicename=@servicename";
                doh.AddConditionParameter("@servicename", q("txtServiceName"));
                if (doh.Exist("jcms_normal_user"))
                    this._response = "{result :\"0\",returnval :\"不可添加\"}";
                else
                    this._response = "{result :\"1\",returnval :\"可以添加\"}";
            }
            else
                this._response = "{result :\"0\",returnval :\"不可修改\"}";
        }
        private void DefaultResponse()
        {
            this._response = JsonResult(0, "未知操作");
        }
        private void ajaxGetList()
        {
            doh.Reset();
            doh.SqlCmd = "Select [Id],[ServiceId],[ServiceName],[GUID],[Email],[LastTime3],[LastIp3] FROM [jcms_normal_user] WHERE [ServiceId]>0 ORDER BY Serviceid desc";
            DataTable dt = doh.GetDataTable();
            this._response = "{result :\"1\",returnval :\"操作成功\"," + JumboTCMS.Utils.dtHelp.DT2JSON(dt) + "}";
        }
        private void ajaxDel()
        {
            string aId = f("id");
            doh.Reset();
            doh.ConditionExpress = "id=" + aId;
            doh.AddFieldItem("ServiceId", 0);
            doh.AddFieldItem("ServiceName", "");
            doh.AddFieldItem("GUID", "");
            doh.Update("jcms_normal_user");
            new JumboTCMS.DAL.Normal_UserDAL().RefreshServiceList();
            this._response = "{result :\"1\",returnval :\"成功删除\"}";
        }
    }
}