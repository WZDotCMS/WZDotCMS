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
using System.Collections.Specialized;
using System.Collections.Generic;
using JumboTCMS.API.Alipay;
namespace JumboTCMS.WebFile.API.Alipay
{
    /// <summary>
    /// 创建该页面文件时，请留心该页面文件是可以对其进行美工处理的，原因在于支付完成以后，当前窗口会从支付宝的页面跳转回这个页面。
    /// 该页面称作“返回页”，是同步被支付宝服务器所调用，可当作是支付完成后的提示信息页，如“您的某某某订单，多少金额已支付成功”。
    /// </summary>
    public partial class _return_url : JumboTCMS.UI.BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SortedDictionary<string, string> sPara = GetRequestGet();

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                Notify aliNotify = new Notify();
                bool verifyResult = aliNotify.Verify(sPara, Request.QueryString["notify_id"], Request.QueryString["sign"]);

                if (verifyResult)//验证成功
                {
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //请在这里加上商户的业务逻辑程序代码

                    //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                    //获取支付宝的通知返回参数，可参考技术文档中页面跳转同步通知参数列表
                    string trade_no = Request.QueryString["trade_no"];              //支付宝交易号
                    string order_no = Request.QueryString["out_trade_no"];	        //获取订单号
                    string total_fee = Request.QueryString["total_fee"];            //获取总金额
                    string subject = Request.QueryString["subject"];                //商品名称、订单名称
                    string body = Request.QueryString["body"];                      //商品描述、订单备注、描述
                    string buyer_email = Request.QueryString["buyer_email"];        //买家支付宝账号
                    string trade_status = Request.QueryString["trade_status"];      //交易状态
                    string userid = Request.QueryString["extra_common_param"];

                    if (Request.QueryString["trade_status"] == "TRADE_FINISHED" || Request.QueryString["trade_status"] == "TRADE_SUCCESS")
                    {
                        if (new JumboTCMS.DAL.Normal_RechargeDAL().UpdateOrder(userid, order_no, "支付宝"))
                        {
                            new JumboTCMS.DAL.Normal_UserNoticeDAL().SendNotite("在线充值", "您在线充值 " + Str2Int(total_fee) + " 元", userid, "recharge");
                            string username = new JumboTCMS.DAL.Normal_UserDAL().GetUserName(userid);
                            SendServiceNotice("会员在线充值", username + "在线充值 " + Str2Int(total_fee) + " 元", "1");
                            SendServiceNotice("会员在线充值", username + "在线充值 " + Str2Int(total_fee) + " 元", "3");
                        }
                    }
                    else
                    {
                        Response.Write("trade_status=" + Request.QueryString["trade_status"]);
                    }
                    Response.Redirect(site.Url + site.Dir + "api/alipay/show.aspx?msg=success");
                    //——请根据您的业务逻辑来编写程序（以上代码仅作参考）——

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                else//验证失败
                {
                    Response.Write("验证失败");
                }
            }
            else
            {
                Response.Write("无返回参数");
            }
        }

        /// <summary>
        /// 获取支付宝GET过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestGet()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.QueryString;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.QueryString[requestItem[i]]);
            }

            return sArray;
        }
    }
}