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
using JumboTCMS.DBUtility;

namespace JumboTCMS.DAL
{
    /// <summary>
    /// 充值订单表信息
    /// </summary>
    public class Normal_RechargeDAL : Common
    {
        public Normal_RechargeDAL()
        {
            base.SetupSystemDate();
        }
        /// <summary>
        ///  新增充值信息
        /// </summary>
        /// <param name="_uid"></param>
        /// <param name="_points">博币</param>
        /// <param name="_payway">如：alipay、tenpay等</param>
        /// <returns></returns>
        public string NewOrder(string _uid, int _points, string _payway)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string _ordernum = GetProductOrderNum();//订单号
                _doh.Reset();
                _doh.AddFieldItem("UserId", _uid);
                _doh.AddFieldItem("OrderNum", _ordernum);
                _doh.AddFieldItem("Points", _points);
                _doh.AddFieldItem("State", 0);
                _doh.AddFieldItem("PaymentWay", _payway);
                _doh.AddFieldItem("OrderTime", DateTime.Now.ToString());
                _doh.AddFieldItem("OrderIP", IPHelp.ClientIP);
                _doh.Insert("jcms_normal_recharge");
                return _ordernum;
            }
        }
        /// <summary>
        /// 在线支付成功，给会员充博币
        /// </summary>
        /// <param name="_uid"></param>
        /// <param name="_ordernum"></param>
        /// <param name="_payway">如：支付宝、财付通等</param>
        /// <returns></returns>
        public bool UpdateOrder(string _uid, string _ordernum, string _payway)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "OrderNum='" + _ordernum + "' and state=0 and userid=" + _uid;
                int _points = Str2Int(_doh.GetField("jcms_normal_recharge", "Points").ToString());
                if (_points > 0)//充值的博币
                {
                    new Normal_UserDAL().AddPoints(_uid, _points);
                    _doh.Reset();
                    _doh.ConditionExpress = "OrderNum='" + _ordernum + "' and state=0 and userid=" + _uid;
                    _doh.AddFieldItem("State", 1);
                    _doh.AddFieldItem("PaymentWay", _payway);
                    _doh.Update("jcms_normal_recharge");
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
