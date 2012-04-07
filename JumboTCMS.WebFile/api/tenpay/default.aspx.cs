using System;
using System.Data;
using System.Web;
using JumboTCMS.API.Tenpay;
//---------------------------------------------------------
//财付通即时到帐支付请求示例，商户按照此文档进行开发即可
//---------------------------------------------------------

namespace JumboTCMS.WebFile.API.Tenpay
{
    public partial class _default : JumboTCMS.UI.FrontHtml
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

            //当前时间 yyyyMMdd
            string date = DateTime.Now.ToString("yyyyMMdd");

            //生成订单10位序列号，此处用时间和随机数生成，商户根据自己调整，保证唯一
            string strReq = DateTime.Now.ToString("HHmmss") + TenpayUtil.BuildRandomStr(4);

            //商户订单号，不超过32位，财付通只做记录，不保证唯一性
            string sp_billno = q("orderNum");//订单号

            //财付通订单号，10位商户号+8位日期+10位序列号，需保证全局唯一
            string transaction_id = bargainor_id + date + strReq;
            string return_url = site.Url + site.Dir + "api/tenpay/return_url.aspx";

            //创建PayRequestHandler实例
            PayRequestHandler reqHandler = new PayRequestHandler(Context);

            //设置密钥
            reqHandler.setKey(key);

            //初始化
            reqHandler.init();

            //-----------------------------
            //设置支付参数
            //-----------------------------
            reqHandler.setParameter("bargainor_id", bargainor_id);			//商户号
            reqHandler.setParameter("sp_billno", sp_billno);				//商家订单号
            reqHandler.setParameter("transaction_id", transaction_id);		//财付通交易单号
            reqHandler.setParameter("return_url", return_url);				//支付通知url
            reqHandler.setParameter("desc", q("productName"));	//商品名称
            reqHandler.setParameter("attach", q("userid"));	//会员ID
            reqHandler.setParameter("total_fee", (Str2Int(q("orderAmount")) * 100).ToString());						//商品金额,以分为单位


            //用户ip,测试环境时不要加这个ip参数，正式环境再加此参数
            reqHandler.setParameter("spbill_create_ip", Page.Request.UserHostAddress);

            //获取请求带参数的url
            string requestUrl = reqHandler.getRequestURL();
            //Response.Redirect(requestUrl);
            this.HyperLink1.NavigateUrl = requestUrl;
            //FinalMessage("正在进入财付通网站...", requestUrl, 0, 4);
        }
    }
}
