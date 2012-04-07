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
    public partial class __authorize__edit : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("master", "html");
            id = Str2Str(q("id"));
            this.txtAddTime.Attributes.Add("onFocus", "WdatePicker({isShowClear:false,readOnly:true,skin:'blue',startDate:'" + System.DateTime.Now.AddYears(1).ToShortDateString() + "'})");
            if (id == "0")
                this.txtAddTime.Attributes.Add("value", System.DateTime.Now.ToShortDateString());
            this.txtValidity.Attributes.Add("onFocus", "WdatePicker({isShowClear:false,readOnly:true,skin:'blue',startDate:'" + System.DateTime.Now.AddYears(1).ToShortDateString() + "'})");
            if (id == "0")
                this.txtValidity.Attributes.Add("value", "2099-12-31");
            if (!Page.IsPostBack)
            {
                doh.Reset();
                doh.SqlCmd = "SELECT [ID],[Title] FROM [jcms_official_authorization_sitetype] ORDER BY PId";
                DataTable dtSiteType = doh.GetDataTable();
                this.ddlSiteType.DataSource = dtSiteType;
                this.ddlSiteType.DataTextField = "Title";
                this.ddlSiteType.DataValueField = "ID";
                this.ddlSiteType.DataBind();
                dtSiteType.Clear();
                dtSiteType.Dispose();
            }
            JumboTCMS.DBUtility.WebFormHandler wh = new JumboTCMS.DBUtility.WebFormHandler(doh, "jcms_official_authorization", btnSave);
            wh.AddBind(txtDomain, "Domain", true);
            wh.AddBind(txtLinkEmail, "LinkEmail", true);
            wh.AddBind(txtDefaultPage, "DefaultPage", true);
            wh.AddBind(ddlAccreditType, "AccreditType", false);
            wh.AddBind(ddlSiteType, "SiteType", false);
            wh.AddBind(txtAddTime, "AddTime", true);
            wh.AddBind(txtValidity, "Validity", true);
            wh.AddBind(txtWebName, "WebName", true);
            wh.AddBind(txtMobileTel, "MobileTel", true);
            if (id == "0")
            {
                wh.Mode = JumboTCMS.DBUtility.OperationType.Add;
            }
            else
            {
                wh.ConditionExpress = "id=" + id;
                wh.Mode = JumboTCMS.DBUtility.OperationType.Modify;
            }
            wh.validator = chkForm;
            wh.AddOk += new EventHandler(save_ok);
            wh.BindBeforeModifyOk += new EventHandler(bind_ok);
            wh.ModifyOk += new EventHandler(save_ok);
        }
        protected void bind_ok(object sender, EventArgs e)
        {
            this.txtAddTime.Text = Convert.ToDateTime(this.txtAddTime.Text).ToString("yyyy-MM-dd");
            this.txtValidity.Text = Convert.ToDateTime(this.txtValidity.Text).ToString("yyyy-MM-dd");
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
            string _webname = this.txtWebName.Text;
            string _defaultpage = this.txtDefaultPage.Text;
            doh.Reset();
            doh.ConditionExpress = "id=@id";
            doh.AddConditionParameter("@id", id);
            if (id == "0")
            {
                JumboTCMS.DBUtility.DbOperEventArgs de = (JumboTCMS.DBUtility.DbOperEventArgs)e;
                id = de.id.ToString();
                doh.AddFieldItem("AddTime", System.DateTime.Now.ToString("yyyy-MM-dd"));
                doh.AddFieldItem("Validity", "2099-12-31");
            }
            if (this.ddlAccreditType.SelectedValue == "1")
            {
                if (JumboTCMS.Utils.Validator.IsFreeSite(_defaultpage, _webname))
                {
                    doh.AddFieldItem("State", 1);
                }
                else
                {
                    doh.AddFieldItem("State", 0);
                }
            }
            else
                doh.AddFieldItem("State", 1);
            doh.Update("jcms_official_authorization");
            FinalMessage("保存成功", "close.htm", 0);
        }
    }
}
