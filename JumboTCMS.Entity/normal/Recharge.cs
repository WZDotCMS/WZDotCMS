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
namespace JumboTCMS.Entity
{
    /// <summary>
    /// 充值订单-------表映射实体
    /// </summary>

    public class Normal_Recharge
    {
        public Normal_Recharge()
        { }

        private int _id;
        private string _ordernum = "";
        private string _paymentway = "";
        private int _points = 0;
        private DateTime _ordertime = DateTime.Now;
        private string _orderip = "";
        private int _state = 0;
        private int _userid = 0;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNum
        {
            set { _ordernum = value; }
            get { return _ordernum; }
        }
        /// <summary>
        /// 支付方式
        /// 如：alipay、tenpay等
        /// </summary>
        public string PaymentWay
        {
            set { _paymentway = value; }
            get { return _paymentway; }
        }
        /// <summary>
        /// 订单付款后返回给会员的博币
        /// </summary>
        public int Points
        {
            set { _points = value; }
            get { return _points; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime OrderTime
        {
            set { _ordertime = value; }
            get { return _ordertime; }
        }
        public string OrderIP
        {
            set { _orderip = value; }
            get { return _orderip; }
        }
        /// <summary>
        /// 状态
        /// 0表示未付款；1表示已付款
        /// </summary>
        public int State
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 会员编号
        /// </summary>
        public int UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }

    }
}

