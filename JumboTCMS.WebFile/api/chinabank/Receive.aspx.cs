using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
namespace JumboTCMS.WebFile.API.ChinaBank
{
    public partial class Receive : JumboTCMS.UI.BasicPage
    {
        protected string v_oid;		// 订单号
        protected string v_pstatus;	// 支付状态码
        //20（支付成功，对使用实时银行卡进行扣款的订单）；
        //30（支付失败，对使用实时银行卡进行扣款的订单）；

        protected string v_pstring;	//支付状态描述
        protected string v_pmode;	//支付银行
        protected string v_amount;	//支付金额
        protected string v_moneytype;	//币种		
        protected string remark1;	// 备注1
        protected string remark2;	// 备注1
        protected string v_md5str;
        protected string status_msg;
        protected string str;	// 备注1
        protected void Page_Load(object sender, EventArgs e)
        {
            string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/payment_chinabank.config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            string key = XmlTool.GetText("Root/key"); //人民币网关密钥
            XmlTool.Dispose();

            v_oid = Request["v_oid"];
            v_pstatus = Request["v_pstatus"];
            v_pstring = Request["v_pstring"];
            v_pmode = Request["v_pmode"];
            v_md5str = Request["v_md5str"];
            v_amount = Request["v_amount"];
            v_moneytype = Request["v_moneytype"];
            remark1 = Request["remark1"];
            remark2 = Request["remark2"];
            string userid = remark1;
            string total_fee = v_amount;
            string str = v_oid + v_pstatus + v_amount + v_moneytype + key;

            str = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "md5").ToUpper();

            if (str == v_md5str)
            {

                if (v_pstatus.Equals("20"))
                {
                    //支付成功
                    if (new JumboTCMS.DAL.Normal_RechargeDAL().UpdateOrder(userid, v_oid, "网银在线"))
                    {
                        new JumboTCMS.DAL.Normal_UserNoticeDAL().SendNotite("在线充值", "您在线充值 " + Str2Int(total_fee) + " 元", userid, "recharge");
                        string username = new JumboTCMS.DAL.Normal_UserDAL().GetUserName(userid);
                        SendServiceNotice("会员在线充值", username + "在线充值 " + Str2Int(total_fee) + " 元", "1");
                        SendServiceNotice("会员在线充值", username + "在线充值 " + Str2Int(total_fee) + " 元", "3");
                    }
                }
            }
            else
            {

                status_msg = "校验失败，数据可疑";
            }
        }
    }
}
