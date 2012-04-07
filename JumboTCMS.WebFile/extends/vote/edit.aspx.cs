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
namespace JumboTCMS.WebFile.Extends.Vote
{
    public partial class _edit : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("master", "html");
            id = Str2Str(q("id"));
            doh.Reset();
            JumboTCMS.DBUtility.WebFormHandler wh = new JumboTCMS.DBUtility.WebFormHandler(doh, "jcms_extends_vote", btnSave);
            wh.AddBind(txtTitle, "Title", true);
            wh.AddBind(txtContent, "VoteText", true);
            wh.AddBind(rblType, "SelectedValue", "Type", false);
            if (id != "0")
            {
                wh.ConditionExpress = "id=" + id.ToString();
                wh.Mode = JumboTCMS.DBUtility.OperationType.Modify;
            }
            else
                wh.Mode = JumboTCMS.DBUtility.OperationType.Add;
            wh.AddOk += new EventHandler(save_ok);
            wh.ModifyOk += new EventHandler(save_ok);
            wh.validator = chkForm;
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
            int voteTotal = txtContent.Text.Split('|').Length;
            string voteNum = "0";
            for (int i = 0; i < voteTotal - 1; i++)
            {
                voteNum += "|0";
            }
            if (id == "0")
            {
                JumboTCMS.DBUtility.DbOperEventArgs de = (JumboTCMS.DBUtility.DbOperEventArgs)e;
                id = de.id.ToString();
            }
            doh.Reset();
            doh.ConditionExpress = "id=" + id;
            doh.AddFieldItem("VoteNum", voteNum);
            doh.AddFieldItem("VoteTotal", 0);
            doh.AddFieldItem("lock", 1);
            doh.Update("jcms_extends_vote");
            FinalMessage("成功保存", site.Dir + "admin/close.htm", 0);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.txtContent.Text = this.txtContent.Text.Replace("\r", "").Replace("\n", "").Replace("'", "").Replace("\\", "");
        }
    }
}
