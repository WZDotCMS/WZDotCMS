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
    public partial class _template_ajax : JumboTCMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;
        public string pId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("master", "json");
            pId = q("pid");
            if (!JumboTCMS.Utils.Validator.IsNumeric(pId))
            {
                this._response = JsonResult(0, "HTML模板方案有误");
            }
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxGetList":
                    ajaxGetList();
                    break;
                case "ajaxDel":
                    ajaxDel();
                    break;
                case "ajaxDef":
                    ajaxDef();
                    break;
                case "checkname":
                    ajaxCheckName();
                    break;
                default:
                    DefaultResponse();
                    break;
            }
            Response.Write(this._response);
        }

        private void DefaultResponse()
        {
            this._response = JsonResult(0, "未知操作");
        }
        private void ajaxCheckName()
        {
            if (q("id") == "0")
            {
                doh.Reset();
                doh.ConditionExpress = "title=@title";
                doh.AddConditionParameter("@title", q("txtTitle"));
                if (doh.Exist("jcms_normal_template"))
                    this._response = JsonResult(0, "不可添加");
                else
                    this._response = JsonResult(1, "可以添加");
            }
            else
            {
                doh.Reset();
                doh.ConditionExpress = "title=@title and id<>" + q("id");
                doh.AddConditionParameter("@title", q("txtTitle"));
                if (doh.Exist("jcms_normal_template"))
                    this._response = JsonResult(0, "不可修改");
                else
                    this._response = JsonResult(1, "可以修改");
            }
        }
        private void ajaxGetList()
        {
            doh.Reset();
            doh.SqlCmd = "Select [ID],[Title],[stype],[IsDefault],[Type],[source] FROM [jcms_normal_template] WHERE [pId]= " + pId + " ORDER BY type desc,stype";
            DataTable dt = doh.GetDataTable();
            this._response = "{result :\"1\",returnval :\"操作成功\"," + JumboTCMS.Utils.dtHelp.DT2JSON(dt) + "}";
        }
        private void ajaxDel()
        {
            string tId = f("id");

            string IsDefault = "1";
            string TempSType = string.Empty;
            bool isUsing = false;
            doh.Reset();
            doh.ConditionExpress = "id=" + tId;
            TempSType = doh.GetField("jcms_normal_template", "sType").ToString();
            doh.Reset();
            doh.ConditionExpress = "id=" + tId;
            IsDefault = doh.GetField("jcms_normal_template", "IsDefault").ToString();
            if (IsDefault == "1")//默认模板
                isUsing = true;
            else
            {
                if (TempSType.ToLower() == "channel")
                {
                    doh.Reset();
                    doh.SqlCmd = "SELECT ID FROM [jcms_normal_channel] WHERE [TemplateId]=" + tId;
                    if (doh.GetDataTable().Rows.Count > 0)
                        isUsing = true;
                }
                else
                {
                    doh.Reset();
                    doh.SqlCmd = "SELECT ID FROM [jcms_normal_class] WHERE [IsOut]=0 AND [TemplateId]=" + tId + " or ContentTemp=" + tId;
                    if (doh.GetDataTable().Rows.Count > 0)
                        isUsing = true;
                }
            }
            if (isUsing)
                this._response = JsonResult(0, "正在使用或默认模板不允许删除");
            else
            {
                doh.Reset();
                doh.ConditionExpress = "id=" + tId;
                doh.Delete("jcms_normal_template");
                this._response = JsonResult(1, "成功删除");
            }
        }
        private void ajaxDef()
        {
            string tId = f("id");
            string sType = string.Empty;
            string Type = string.Empty;
            doh.Reset();
            doh.ConditionExpress = "id=" + tId;
            sType = doh.GetField("jcms_normal_template", "sType").ToString();
            doh.Reset();
            doh.ConditionExpress = "id=" + tId;
            Type = doh.GetField("jcms_normal_template", "Type").ToString();
            doh.Reset();
            doh.ConditionExpress = "stype=@stype and type=@type and IsDefault=1";
            doh.AddConditionParameter("@stype", sType);
            doh.AddConditionParameter("@type", Type);
            doh.AddFieldItem("IsDefault", 0);
            doh.Update("jcms_normal_template");
            doh.Reset();
            doh.ConditionExpress = "id=" + tId;
            doh.AddFieldItem("IsDefault", 1);
            doh.Update("jcms_normal_template");
            doh.Reset();
            this._response = JsonResult(1, "成功设置");
        }
    }
}