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
    /// 发信日志表信息
    /// </summary>
    public class Normal_EmaillogsDAL : Common
    {
        public Normal_EmaillogsDAL()
        {
            base.SetupSystemDate();
        }
        /// <summary>
        /// 保存发信日志
        /// </summary>
        /// <param name="_adminid">管理员ID</param>
        /// <param name="_title">发信标题</param>
        /// <param name="_users">发信收信人</param>
        public void SaveLog(string _adminid, string _title, string _users)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.AddFieldItem("AdminId", _adminid);
                _doh.AddFieldItem("SendTitle", _title);
                _doh.AddFieldItem("SendUsers", _users);
                _doh.AddFieldItem("SendTime", DateTime.Now.ToString());
                _doh.AddFieldItem("SendIP", IPHelp.ClientIP);
                _doh.Insert("jcms_normal_emaillogs");
            }
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
                int _countnum = _doh.Count("jcms_normal_emaillogs");
                sqlStr = JumboTCMS.Utils.SqlHelp.GetSql("A.id as id,A.AdminId as AdminId,B.[AdminName] as AdminName,A.SendTitle as SendTitle,A.SendUsers as SendUsers,A.SendTime as SendTime,A.SendIP as SendIP", "jcms_normal_emaillogs", "jcms_normal_user", "Id", _pagesize, _thispage, "desc", _joinstr, _wherestr1, _wherestr2);
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
        /// 清空管理日志
        /// </summary>
        public void DeleteLogs()
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "1=1";
                _doh.Delete("jcms_normal_emaillogs");
            }
        }
    }
}
