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
    /// 会员通知表信息
    /// </summary>
    public class Normal_UserNoticeDAL : Common
    {
        public Normal_UserNoticeDAL()
        {
            base.SetupSystemDate();
        }
        /// <summary>
        /// 发站内通知
        /// </summary>
        /// <param name="_Title">标题</param>
        /// <param name="_Content">内容</param>
        /// <param name="_ReceiveUserId">接收人ID,0表示所有人</param>
        /// <param name="_NoticeType">类型，比如：friend</param>
        public bool SendNotite(string _Title, string _Content, string _ReceiveUserId, string _NoticeType)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.AddFieldItem("Title", _Title);
                _doh.AddFieldItem("AddDate", DateTime.Now.ToString());
                _doh.AddFieldItem("Content", _Content);
                _doh.AddFieldItem("UserId", _ReceiveUserId);
                _doh.AddFieldItem("NoticeType", _NoticeType);
                _doh.AddFieldItem("State", 0);
                _doh.AddFieldItem("ReadTime", DateTime.Now.ToString());
                _doh.Insert("jcms_normal_user_notice");
                return true;
            }
        }
    }
}
