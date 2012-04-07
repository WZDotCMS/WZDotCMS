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
    /// 创建该页面文件时，请留心该页面文件中无任何HTML代码及空格。
    /// 该页面称作“通知页”，是异步被支付宝服务器所调用。
    /// 当支付宝的订单状态改变时，支付宝服务器则会自动调用此页面，因此请做好自身网站订单信息与支付宝上的订单的同步工作
    /// </summary>
    public partial class _notify_url : JumboTCMS.UI.BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SortedDictionary<string, string> sPara = GetRequestPost();

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                Notify aliNotify = new Notify();
                bool verifyResult = aliNotify.Verify(sPara, Request.Form["notify_id"], Request.Form["sign"]);

                if (verifyResult)//验证成功
                {
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //请在这里加上商户的业务逻辑程序代码

                    //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                    //获取支付宝的通知返回参数，可参考技术文档中服务器异步通知参数列表
                    string trade_no = Request.Form["trade_no"];         //支付宝交易号
                    string order_no = Request.Form["out_trade_no"];     //获取订单号
                    string total_fee = Request.Form["total_fee"];       //获取总金额
                    string subject = Request.Form["subject"];           //商品名称、订单名称
                    string body = Request.Form["body"];                 //商品描述、订单备注、描述
                    string buyer_email = Request.Form["buyer_email"];   //买家支付宝账号
                    string trade_status = Request.Form["trade_status"]; //交易状态
                    string userid = Request.Form["extra_common_param"];

                    if (Request.Form["trade_status"] == "TRADE_FINISHED" || Request.Form["trade_status"] == "TRADE_SUCCESS")
                    {
                        if (new JumboTCMS.DAL.Normal_RechargeDAL().UpdateOrder(userid, order_no, "支付宝"))
                        {
                            new JumboTCMS.DAL.Normal_UserNoticeDAL().SendNotite("在线充值", "您在线充值 " + Str2Int(total_fee) + " 元", userid, "recharge");
                            string username = new JumboTCMS.DAL.Normal_UserDAL().GetUserName(userid);
                            SendServiceNotice("会员在线充值", username + "在线充值 " + Str2Int(total_fee) + " 元", "1");
                            SendServiceNotice("会员在线充值", username + "在线充值 " + Str2Int(total_fee) + " 元", "3");
                        }
                        Response.Write("success");  //请不要修改或删除
                    }
                    else
                    {
                        Response.Write("success");  //其他状态判断。普通即时到帐中，其他状态不用判断，直接打印success。
                    }

                    //——请根据您的业务逻辑来编写程序（以上代码仅作参考）——

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                else//验证失败
                {
                    Response.Write("fail");
                }
            }
            else
            {
                Response.Write("无通知参数");
            }
        }

        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestPost()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.Form;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.Form[requestItem[i]]);
            }

            return sArray;
        }
    }
}