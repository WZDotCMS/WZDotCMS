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
    public partial class _usergroup_edit : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("master", "html");
            id = Str2Str(q("id"));
            JumboTCMS.DBUtility.WebFormHandler wh = new JumboTCMS.DBUtility.WebFormHandler(doh, "jcms_normal_usergroup", btnSave);
            wh.AddBind(txtGroupName, "GroupName", true);
            wh.AddBind(rblIsLogin, "SelectedValue", "islogin", false);
            if (id == "0")
            {
                wh.Mode = JumboTCMS.DBUtility.OperationType.Add;
            }
            else
            {
                wh.ConditionExpress = "id=" + id;
                wh.Mode = JumboTCMS.DBUtility.OperationType.Modify;
            }
            wh.BindBeforeAddOk += new EventHandler(bind_ok);
            wh.BindBeforeModifyOk += new EventHandler(bind_ok);
            wh.AddOk += new EventHandler(save_ok);
            wh.ModifyOk += new EventHandler(save_ok);
            wh.validator = chkForm;
        }
        protected void bind_ok(object sender, EventArgs e)
        {
            string[] setting = "0,0,0,0|23,1,10,10,1,0,1,1,5,1,1,5,1,1,5,".Split(',');
            if (id != "0")
            {
                doh.Reset();
                doh.ConditionExpress = "id=" + id;
                setting = doh.GetField("jcms_normal_usergroup", "Setting").ToString().Split(',');
                if (setting.Length < 17)
                    setting = "0,0,0,0|23,1,10,10,1,0,1,1,5,1,1,5,1,1,5,".Split(',');
            }
            this.GroupSet0.Items.FindByValue(setting[0].ToString()).Selected = true;
            this.GroupSet1.Items.FindByValue(setting[1].ToString()).Selected = true;
            this.GroupSet2.Items.FindByValue(setting[2].ToString()).Selected = true;
            this.GroupSet3.Text = setting[3].ToString();
            this.GroupSet4.Items.FindByValue(setting[4].ToString()).Selected = true;
            this.GroupSet5.Text = setting[5].ToString();
            this.GroupSet6.Text = setting[6].ToString();
            this.GroupSet7.Items.FindByValue(setting[7].ToString()).Selected = true;
            this.GroupSet8.Items.FindByValue(setting[8].ToString()).Selected = true;
            this.GroupSet9.Items.FindByValue(setting[9].ToString()).Selected = true;
            this.GroupSet10.Items.FindByValue(setting[10].ToString()).Selected = true;
            this.GroupSet11.Text = setting[11].ToString();
            this.GroupSet12.Items.FindByValue(setting[12].ToString()).Selected = true;
            this.GroupSet13.Items.FindByValue(setting[13].ToString()).Selected = true;
            this.GroupSet14.Text = setting[14].ToString();
            this.GroupSet15.Items.FindByValue(setting[15].ToString()).Selected = true;
            this.GroupSet16.Items.FindByValue(setting[16].ToString()).Selected = true;
            this.GroupSet17.Text = setting[17].ToString();

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
            string setting = this.GroupSet0.SelectedValue + "," +
                this.GroupSet1.SelectedValue + "," +
                this.GroupSet2.SelectedValue + "," +
                this.GroupSet3.Text + "," +
                this.GroupSet4.SelectedValue + "," +
                this.GroupSet5.Text + "," +
                this.GroupSet6.Text + "," +
                this.GroupSet7.SelectedValue + "," +
                this.GroupSet8.SelectedValue + "," +
                this.GroupSet9.SelectedValue + "," +
                this.GroupSet10.SelectedValue + "," +
                this.GroupSet11.Text + "," +
                this.GroupSet12.SelectedValue + "," +
                this.GroupSet13.SelectedValue + "," +
                this.GroupSet14.Text + "," +
                this.GroupSet15.SelectedValue + "," +
                this.GroupSet16.SelectedValue + "," +
                this.GroupSet17.Text + ",";
            if (id == "0")
            {
                JumboTCMS.DBUtility.DbOperEventArgs de = (JumboTCMS.DBUtility.DbOperEventArgs)e;
                id = de.id.ToString();
            }
            doh.Reset();
            doh.ConditionExpress = "id=" + id;
            doh.AddFieldItem("Setting", setting);
            doh.Update("jcms_normal_usergroup");
            FinalMessage("成功保存", "close.htm", 0);
        }
    }
}
