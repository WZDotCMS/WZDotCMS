using System;
using System.Data;
using System.Web;
using JumboTCMS.API.Tenpay;

namespace JumboTCMS.WebFile.API.Tenpay
{
    public partial class return_url : JumboTCMS.UI.FrontHtml
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/payment_tenpay.config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            //商户号
            String bargainor_id = XmlTool.GetText("Root/bargainor_id");
            //密钥
            String key = XmlTool.GetText("Root/key");
            XmlTool.Dispose();

            PayResponseHandler resHandler = new PayResponseHandler(Context);

            resHandler.setKey(key);

            //判断签名
            if (resHandler.isTenpaySign())
            {
                //交易单号
                string transaction_id = resHandler.getParameter("transaction_id");

                //金额金额,以分为单位
                string total_fee = resHandler.getParameter("total_fee");

                //支付结果
                string pay_result = resHandler.getParameter("pay_result");

                //会员ID
                string userid = resHandler.getParameter("attach");

                if ("0".Equals(pay_result))
                {
                    //------------------------------
                    //处理业务开始
                    //------------------------------ 
                    if (new JumboTCMS.DAL.Normal_RechargeDAL().UpdateOrder(userid, transaction_id, "财付通"))
                    {
                        new JumboTCMS.DAL.Normal_UserNoticeDAL().SendNotite("在线充值", "您在线充值 " + Str2Int(total_fee) / 100 + " 元", userid, "recharge");
                        string username = new JumboTCMS.DAL.Normal_UserDAL().GetUserName(userid);
                        SendServiceNotice("会员在线充值", username + "在线充值 " + Str2Int(total_fee) / 100 + " 元", "1");
                        SendServiceNotice("会员在线充值", username + "在线充值 " + Str2Int(total_fee) / 100 + " 元", "3");
                    }
                    //------------------------------
                    //处理业务完毕
                    //------------------------------

                    resHandler.doShow(site.Url + site.Dir + "api/tenpay/show.aspx?msg=success");
                }
                else
                {
                    //当做不成功处理
                    //Response.Write("支付失败");
                    resHandler.doShow(site.Url + site.Dir + "api/tenpay/show.aspx?msg=error");
                }

            }
            else
            {
                //Response.Write("认证签名失败");
                resHandler.doShow(site.Url + site.Dir + "api/tenpay/show.aspx?msg=error");
            }

        }
    }
}
