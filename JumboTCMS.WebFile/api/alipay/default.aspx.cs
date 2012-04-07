using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using JumboTCMS.API.Alipay;

namespace JumboTCMS.WebFile.API.Alipay
{
    public partial class _default : JumboTCMS.UI.BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ////////////////////////////////////////////请求参数////////////////////////////////////////////

            //必填参数//

            //请与贵网站订单系统中的唯一订单号匹配
            string out_trade_no = q("orderNum");//订单号
            //订单名称，显示在支付宝收银台里的“商品名称”里，显示在支付宝的交易管理的“商品名称”的列表里。
            string subject = q("productName");                            //商品名称
            //订单描述、订单详细、订单备注，显示在支付宝收银台里的“商品描述”里
            string body = q("productDesc");                                 //商品描述
            //订单总金额，显示在支付宝收银台里的“应付总额”里
            string total_fee = q("orderAmount");
            string userid = q("userid");                            //会员id


            //扩展功能参数——默认支付方式//

            //默认支付方式，代码见“即时到帐接口”技术文档
            string paymethod = "";
            //默认网银代号，代号列表见“即时到帐接口”技术文档“附录”→“银行列表”
            string defaultbank = "";

            //扩展功能参数——防钓鱼//
            //防钓鱼时间戳
            string anti_phishing_key = "";
            string exter_invoke_ip = "";
            string show_url = "http://www.alipay.com/";
            //自定义参数，可存放任何内容（除=、&等特殊字符外），不会显示在页面上
            string extra_common_param = userid;
            //默认买家支付宝账号
            string buyer_email = "";
            string royalty_type = "";
            string royalty_parameters = "";


            //把请求参数打包成数组
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("payment_type", "1");
            sParaTemp.Add("show_url", show_url);
            sParaTemp.Add("out_trade_no", out_trade_no);
            sParaTemp.Add("subject", subject);
            sParaTemp.Add("body", body);
            sParaTemp.Add("total_fee", total_fee);
            sParaTemp.Add("paymethod", paymethod);
            sParaTemp.Add("defaultbank", defaultbank);
            sParaTemp.Add("anti_phishing_key", anti_phishing_key);
            sParaTemp.Add("exter_invoke_ip", exter_invoke_ip);
            sParaTemp.Add("extra_common_param", extra_common_param);
            sParaTemp.Add("buyer_email", buyer_email);
            sParaTemp.Add("royalty_type", royalty_type);
            sParaTemp.Add("royalty_parameters", royalty_parameters);

            //构造即时到帐接口表单提交HTML数据，无需修改
            Service ali = new Service();
            string sHtmlText = ali.Create_direct_pay_by_user(sParaTemp);
            Response.Write(sHtmlText);
        }
    }
}
