using System;
namespace JumboTCMS.WebFile.API.Alipay
{
    public partial class show : JumboTCMS.UI.FrontHtml
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["msg"].ToString().Trim() == "success")
                this.Lab_msg.Text = "<span class=\"em\">充值成功：</span><br>请查看博币是否已经到帐，如果还未到账，请联系本站客服人员。<br><br><br>";
            else
                this.Lab_msg.Text = "<span class=\"em\">充值失败：</span><br>" + Request["msg"].ToString().Trim() + "<br><br><br>";
        }
    }
}
