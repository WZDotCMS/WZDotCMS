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
    /// 赞助信息-------表映射实体
    /// </summary>

    public class Normal_Order
    {
        public Normal_Order()
        { }

        private string _id;
        private string _ordernum;
        private string _userid;
        private DateTime _ordertime;
        private int _money;
        private int _state;
        /// <summary>
        /// 
        /// </summary>
        public string Id
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
        /// 会员ID
        /// </summary>
        public string UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 充值金额
        /// </summary>
        public int Money
        {
            set { _money = value; }
            get { return _money; }
        }
        /// <summary>
        /// 充值时间
        /// </summary>
        public DateTime OrderTime
        {
            set { _ordertime = value; }
            get { return _ordertime; }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public int State
        {
            set { _state = value; }
            get { return _state; }
        }

    }
}



