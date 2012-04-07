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
using System.Web;
using JumboTCMS.Utils;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.API
{
    public partial class _submit : JumboTCMS.UI.UserCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            User_Load("", "json");
            string payWay = f("payway");
            int points = Str2Int(f("txtPoints"));
            string productName = f("txtProductName");
            string productDesc = f("txtProductDesc");
            //生成订单
            string orderNum = new JumboTCMS.DAL.Normal_RechargeDAL().NewOrder(UserId, points, payWay);//订单号
            Response.Write("<script>top.location.href='" + site.Dir + "api/" + payWay + "/default.aspx"
                + "?userid=" + UserId
                + "&payerName=" + System.Web.HttpUtility.UrlEncode(UserName)
                + "&orderNum=" + orderNum
                + "&orderAmount=" + points
                + "&productName=" + System.Web.HttpUtility.UrlEncode(productName)
                + "&productDesc=" + System.Web.HttpUtility.UrlEncode(productDesc)
                + "';</script>");
        }
    }
}