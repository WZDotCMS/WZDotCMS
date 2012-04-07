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
    /// 会员好友表信息
    /// </summary>
    public class Normal_UserFriendsDAL : Common
    {
        public Normal_UserFriendsDAL()
        {
            base.SetupSystemDate();
        }
        /// <summary>
        /// 判断是否已经为好友
        /// </summary>
        /// <param name="_userid"></param>
        /// <param name="_friendid"></param>
        /// <returns></returns>
        public bool Exists(string _userid, string _friendid)
        {
            int _ext = 0;
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "friendid=@friendid and userid=@userid";
                _doh.AddConditionParameter("@friendid", _friendid);
                _doh.AddConditionParameter("@userid", _userid);
                if (_doh.Exist("jcms_normal_user_friends"))
                    _ext = 1;
            }
            return (_ext == 1);
        }
        /// <summary>
        /// 得到列表JSON数据
        /// </summary>
        /// <param name="_thispage">当前页码</param>
        /// <param name="_pagesize">每页记录条数</param>
        /// <param name="_joinstr">关联条件</param>
        /// <param name="_wherestr1">外围条件(带A.)</param>
        /// <param name="_wherestr2">分页条件(不带A.)</param>
        /// <param name="_jsonstr">返回值</param>
        public void GetListJSON(int _thispage, int _pagesize, string _joinstr, string _wherestr1, string _wherestr2, ref string _jsonstr)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = _wherestr2;
                string sqlStr = "";
                int _countnum = _doh.Count("jcms_normal_user_friends");
                sqlStr = JumboTCMS.Utils.SqlHelp.GetSql("A.id as id,A.FriendId as FriendUserId,B.UserName as FriendUserName,A.AddDate as AddDate", "jcms_normal_user_friends", "jcms_normal_user", "Id", _pagesize, _thispage, "desc", _joinstr, _wherestr1, _wherestr2);
                _doh.Reset();
                _doh.SqlCmd = sqlStr;
                DataTable dt = _doh.GetDataTable();
                _jsonstr = "{result :\"1\"," +
                    "returnval :\"操作成功\"," +
                    "pagerbar :\"" + JumboTCMS.Utils.HtmlPager.GetPageBar(3, "js", 2, _countnum, _pagesize, _thispage, "javascript:ajaxList(<#page#>);") + "\"," +
                    JumboTCMS.Utils.dtHelp.DT2JSON(dt, (_pagesize * (_thispage - 1))) +
                    "}";
                dt.Clear();
                dt.Dispose();
            }
        }
        /// <summary>
        /// 得到所有好友列表
        /// </summary>
        /// <param name="_userid"></param>
        /// <returns></returns>
        public DataTable GetTable(string _userid)
        {
            DataTable dt = null;
            using (DbOperHandler _doh = new Common().Doh())
            {
                string sqlStr = "";
                sqlStr = JumboTCMS.Utils.SqlHelp.GetSql("A.FriendId as FriendUserId,B.UserName as FriendUserName,B.NickName as FriendNickName,B.Sex as FriendSex,B.Birthday as FriendBirthday,B.ProvinceCity as FriendProvinceCity,B.HomePage as FriendHomePage,B.Signature as FriendSignature", "jcms_normal_user_friends", "jcms_normal_user", "Id", 200, 1, "desc", "A.FriendId=B.Id", "A.UserId=" + _userid, "UserId=" + _userid);
                _doh.Reset();
                _doh.SqlCmd = sqlStr;
                dt = _doh.GetDataTable();
            }
            return dt;
        }
        /// <summary>
        /// 加为好友，如果已经存在返回false
        /// </summary>
        /// <param name="_userid">主动方ID</param>
        /// <param name="_username">主动方name</param>
        /// <param name="_friendid">被动方ID</param>
        /// <returns></returns>
        public bool AddFriend(string _userid, string _username, string _friendid)
        {
            if (Exists(_userid, _friendid)) return false;//已经存在
            _username = _username == "" ? "user(id:" + _userid + ")" : _username;
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.AddFieldItem("FriendId", _friendid);
                _doh.AddFieldItem("UserId", _userid);
                _doh.AddFieldItem("AddDate", DateTime.Now.ToString());
                _doh.Insert("jcms_normal_user_friends");
                new JumboTCMS.DAL.Normal_UserNoticeDAL().SendNotite("加好友", "<a href=\"javascript:void(0);\" onclick=\"ShowUserPage(" + _userid + ");\">" + _username + "</a> 把你加为了好友", _friendid, "friend");
            }
            return true;
        }
        /// <summary>
        /// 通过对方ID删除好友，如果不存在返回false
        /// </summary>
        /// <param name="_userid">发起方id</param>
        /// <param name="_username">发起方name</param>
        /// <param name="_friendid">自增长id</param>
        /// <returns></returns>
        public bool DeleteByFriendID(string _userid, string _username, string _friendid)
        {
            _username = _username == "" ? "user(id:" + _userid + ")" : _username;
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "friendid=@friendid and userid=@userid";
                _doh.AddConditionParameter("@friendid", _friendid);
                _doh.AddConditionParameter("@userid", _userid);
                int _del = _doh.Delete("jcms_normal_user_friends");
                if (_del == 1)
                    new JumboTCMS.DAL.Normal_UserNoticeDAL().SendNotite("解除好友", "<a href=\"javascript:void(0);\" onclick=\"ShowUserPage(" + _userid + ");\">" + _username + "</a> 和你解除了好友关系", _friendid, "friend");

                return (_del == 1);
            }
        }
    }
}
