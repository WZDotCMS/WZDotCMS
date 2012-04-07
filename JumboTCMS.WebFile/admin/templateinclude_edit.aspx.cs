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
    public partial class _templateinclude_edit : JumboTCMS.UI.AdminCenter
    {
        public string tpPath = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("master", "html");
            id = Str2Str(q("id"));
            string pid = Str2Str(q("pid"));

            doh.Reset();
            doh.ConditionExpress = "id=@id";
            doh.AddConditionParameter("@id", pid);
            tpPath = doh.GetField("jcms_normal_templateproject", "Dir").ToString();
            if (tpPath.Length == 0)
            {
                Response.Write("HTML模板方案选择有误!");
                Response.End();
                return;
            }
            JumboTCMS.DBUtility.WebFormHandler wh = new JumboTCMS.DBUtility.WebFormHandler(doh, "jcms_normal_templateinclude", btnSave);
            wh.AddBind(txtTitle, "Title", true);
            wh.AddBind(txtSource, "Source", true);
            wh.AddBind(txtInfo, "Info", true);
            wh.AddBind(txtSort, "Sort", false);
            wh.AddBind(rblNeedBuild, "SelectedValue", "NeedBuild", false);
            if (id == "0")
            {
                wh.Mode = JumboTCMS.DBUtility.OperationType.Add;
            }
            else
            {
                wh.ConditionExpress = "id=" + id;
                this.txtSource.Enabled = false;
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
            doh.Reset();
            doh.ConditionExpress = "Title=@title and id<>" + id;
            doh.AddConditionParameter("@title", txtTitle.Text);
            if (doh.Exist("jcms_normal_templateinclude"))
            {
                FinalMessage("标签名重复!", "", 1);
                return false;
            }
            return true;
        }
        protected void save_ok(object sender, EventArgs e)
        {
            if (id == "0")
            {
                JumboTCMS.DBUtility.DbOperEventArgs de = (JumboTCMS.DBUtility.DbOperEventArgs)e;
                id = de.id.ToString();

                doh.Reset();
                doh.ConditionExpress = "id=@id";
                doh.AddConditionParameter("@id", id);
                doh.AddFieldItem("pId", q("pid"));
                doh.Update("jcms_normal_templateinclude");
            }
            FinalMessage("成功保存", "close.htm", 0);
        }
    }
}
