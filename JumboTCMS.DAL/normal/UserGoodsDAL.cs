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
    /// 会员商品信息
    /// </summary>
    public class Normal_UserGoodsDAL : Common
    {
        public Normal_UserGoodsDAL()
        {
            base.SetupSystemDate();
        }
        /// <summary>
        /// 新增购物信息
        /// </summary>
        /// <param name="_goods"></param>
        /// <returns></returns>
        public int NewGoods(JumboTCMS.Entity.Normal_UserGoods _goods)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.AddFieldItem("UserId", _goods.UserId);
                _doh.AddFieldItem("OrderNum", _goods.OrderNum);
                _doh.AddFieldItem("ProductId", _goods.ProductId);
                _doh.AddFieldItem("ProductName", _goods.ProductName);
                _doh.AddFieldItem("ProductImg", _goods.ProductImg);
                _doh.AddFieldItem("ProductLink", _goods.ProductLink);
                _doh.AddFieldItem("UnitPrice", _goods.UnitPrice);
                _doh.AddFieldItem("BuyCount", _goods.BuyCount);
                _doh.AddFieldItem("TotalPrice", _goods.TotalPrice);
                _doh.AddFieldItem("State", 0);
                _doh.AddFieldItem("GoodsTime", DateTime.Now.ToString());
                int _newid = _doh.Insert("jcms_normal_user_goods");
                return _newid;
            }
        }
        /// <summary>
        /// 统计会员的购物量
        /// </summary>
        /// <param name="_uid"></param>
        /// <returns></returns>
        public int CountGoods(string _uid)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "OrderNum='' and userid=" + _uid;
                return _doh.Count("jcms_normal_user_goods");
            }
        }
        /// <summary>
        /// 更新购物信息
        /// </summary>
        /// <param name="_uid"></param>
        /// <param name="_ids"></param>
        /// <param name="_ordernum"></param>
        /// <param name="_state"></param>
        /// <returns></returns>
        public int UpdateGoods(string _uid, string _ids, int _state)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                int _num = 0;
                if (_state == 1)
                {
                    _doh.Reset();
                    _doh.ConditionExpress = "Id in (" + _ids + ") and state=0 and userid=" + _uid;
                    _doh.AddFieldItem("State", 1);
                    _num = _doh.Update("jcms_normal_user_goods");
                }
                else if (_state == 2)
                {
                    _doh.Reset();
                    _doh.ConditionExpress = "Id in (" + _ids + ") and state=1 and userid=" + _uid;
                    _doh.AddFieldItem("State", 2);
                    _num = _doh.Update("jcms_normal_user_goods");
                }
                return _num;
            }
        }
    }
}
