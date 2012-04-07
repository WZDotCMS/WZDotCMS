using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace JumboTCMS.WebFile.API._99bill
{
    public partial class show : System.Web.UI.Page
    {
        ///在本文件中，商家应从数据库中，查询到订单的状态信息以及订单的处理结果。给出支付人响应的提示。

        ///本范例采用最简单的模式，直接从receive页面获取支付状态提示给用户。
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["msg"].ToString().Trim() == "success")
                this.Lab_msg.Text = "<span class=\"em\">充值成功：</span><br>请查看博币是否已经到帐，如果还未到账，请联系本站客服人员。<br><br><br>";
            else
                this.Lab_msg.Text = "<span class=\"em\">充值失败：</span><br>未知的原因<br><br><br>";
        }
    }
}
