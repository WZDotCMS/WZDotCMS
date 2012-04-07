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
namespace JumboTCMS.WebFile.Modules
{
    public partial class _photo_user_edit : JumboTCMS.UI.UserCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ChannelId = Str2Str(q("ccid"));
            id = Str2Str(q("id"));
            User_Load("", "html", true);
            checkEdit(id, "photo");
            this.txtEditor.Text = UserName;
            this.txtUserId.Text = UserId;
            getEditDropDownList(ref ddlClassId, 0);

            doh.Reset();
            JumboTCMS.DBUtility.WebFormHandler wh = new JumboTCMS.DBUtility.WebFormHandler(doh, "jcms_module_photo", btnSave);
            wh.AddBind(txtTitle, "Title", true);
            wh.AddBind(ddlClassId, "ClassId", false);
            wh.AddBind(txtSourceFrom, "SourceFrom", true);
            wh.AddBind(txtAuthor, "Author", true);
            wh.AddBind(txtEditor, "Editor", true);
            wh.AddBind(txtUserId, "UserId", false);
            wh.AddBind(txtTags, "Tags", true);
            wh.AddBind(txtImg, "Img", true);
            wh.AddBind(rblIsTop, "SelectedValue", "IsTop", false);
            wh.AddBind(txtPageSize, "PageSize", false);
            wh.AddBind(txtSummary, "Summary", true);
            wh.AddBind(ref ChannelId, "ChannelId", false);
            wh.AddBind(chkIsEdit, "1", "IsPass", false);
            wh.AddBind(txtAddDate, "AddDate", true);
            wh.AddBind(txtPhotoUrl, "PhotoUrl", true);
            if (id == "0")
            {
                wh.Mode = JumboTCMS.DBUtility.OperationType.Add;
                this.txtAddDate.Text = DateTime.Now.ToString();
            }
            else
            {
                wh.ConditionExpress = "UserId=" + UserId + " and [IsPass]=0 and id=" + id;
                wh.Mode = JumboTCMS.DBUtility.OperationType.Modify;
            }
            wh.BindBeforeModifyOk += new EventHandler(bind_ok);
            wh.AddOk += new EventHandler(save_ok);
            wh.ModifyOk += new EventHandler(save_ok);
            wh.validator = chkForm;
        }
        /// <summary>
        /// 绑定数据后的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void bind_ok(object sender, EventArgs e)
        {
            this.txtSummary.Text = JumboTCMS.Utils.Strings.HtmlDecode(this.txtSummary.Text);
        }
        protected bool chkForm()
        {
            if (!CheckFormUrl())
                return false;
            if (!Page.IsValid)
                return false;
            if (this.txtPhotoUrl.Text.Length == 0)
            {
                lbPhotoUrlMsg.Text = "请填写图片地址!";
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
            }
            doh.Reset();
            doh.ConditionExpress = "id=@id";
            doh.AddConditionParameter("@id", id);
            if (txtImg.Text != "")
                doh.AddFieldItem("IsImg", "1");
            else
                doh.AddFieldItem("IsImg", "0");
            string[] setting = (string[])UserSetting.Split(',');
            bool _NeedCheck = (setting[10] == "1");
            if(_NeedCheck)
                doh.AddFieldItem("IsPass", 0);
            else
                doh.AddFieldItem("IsPass", 1);
            doh.Update("jcms_module_photo");
            FinalMessage("成功保存", "../user/close.htm", 0);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.txtTitle.Text = JumboTCMS.Utils.Strings.SafetyStr(this.txtTitle.Text);
            this.txtSummary.Text = GetCutString(JumboTCMS.Utils.Strings.HtmlEncode(this.txtSummary.Text), 200).Trim();
            //格式化标签
            this.txtTags.Text = JumboTCMS.Utils.Strings.SafetyStr(this.txtTags.Text);
            //格式化地址
            this.txtPhotoUrl.Text = this.txtPhotoUrl.Text.Replace("\'", "").Replace("\"", "");
            //新加关键词
            new JumboTCMS.DAL.Normal_TagDAL().InsertTags(ChannelId, this.txtTags.Text);

        }
    }
}
