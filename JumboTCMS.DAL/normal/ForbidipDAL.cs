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
    /// 非法IP表信息
    /// </summary>
    public class Normal_ForbidipDAL : Common
    {
        public Normal_ForbidipDAL()
        {
            base.SetupSystemDate();
        }
        /// <summary>
        /// 是否存在记录
        /// </summary>
        /// <param name="_wherestr">条件</param>
        /// <returns></returns>
        public bool Exists(string _wherestr)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                int _ext = 0;
                _doh.Reset();
                _doh.ConditionExpress = _wherestr;
                if (_doh.Exist("jcms_normal_forbidip"))
                    _ext = 1;
                return (_ext == 1);
            }
        }
        /// <summary>
        /// 判断重复性(标题是否存在)
        /// </summary>
        /// <param name="_title">需要检索的标题</param>
        /// <param name="_id">除外的ID</param>
        /// <param name="_wherestr">其他条件</param>
        /// <returns></returns>
        public bool ExistTitle(string _title, string _id, string _wherestr)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                int _ext = 0;
                _doh.Reset();
                _doh.ConditionExpress = "title=@title and id<>" + _id;
                if (_wherestr != "") _doh.ConditionExpress += " and " + _wherestr;
                _doh.AddConditionParameter("@title", _title);
                if (_doh.Exist("jcms_normal_forbidip"))
                    _ext = 1;
                return (_ext == 1);
            }
        }
        /// <summary>
        /// 得到列表JSON数据
        /// </summary>
        /// <param name="_thispage">当前页码</param>
        /// <param name="_pagesize">每页记录条数</param>
        /// <param name="_wherestr">搜索条件</param>
        /// <param name="_jsonstr">返回值</param>
        public void GetListJSON(int _thispage, int _pagesize, string _wherestr, ref string _jsonstr)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = _wherestr;
                string sqlStr = "";
                int _countnum = _doh.Count("jcms_normal_forbidip");
                sqlStr = JumboTCMS.Utils.SqlHelp.GetSql("[Id],[StartIP2],[EndIP2],[ExpireDate],[Enabled]", "jcms_normal_forbidip", "Id", _pagesize, _thispage, "desc", _wherestr);
                _doh.Reset();
                _doh.SqlCmd = sqlStr;
                DataTable dt = _doh.GetDataTable();
                _jsonstr = "{result :\"1\"," +
                    "returnval :\"操作成功\"," +
                    "pagerbar :\"" + JumboTCMS.Utils.HtmlPager.GetPageBar(3, "js", 2, _countnum, _pagesize, _thispage, "javascript:ajaxList(<#page#>);") + "\"," +
                    JumboTCMS.Utils.dtHelp.DT2JSON(dt) +
                    "}";
                dt.Clear();
                dt.Dispose();
            }
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteByID(string _id)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "id=@id";
                _doh.AddConditionParameter("@id", _id);
                int _del = _doh.Delete("jcms_normal_forbidip");
                return (_del == 1);
            }
        }

        /// <summary>
        /// 获得单页内容的单条记录实体
        /// </summary>
        /// <param name="_id"></param>
        public JumboTCMS.Entity.Normal_Forbidip GetEntity(string _id)
        {
            JumboTCMS.Entity.Normal_Forbidip forbidip = new JumboTCMS.Entity.Normal_Forbidip();
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT * FROM [jcms_normal_forbidip] WHERE [Id]=" + _id;
                DataTable dt = _doh.GetDataTable();
                if (dt.Rows.Count > 0)
                {
                    forbidip.Id = dt.Rows[0]["Id"].ToString();
                    forbidip.StartIP = Convert.ToInt32(dt.Rows[0]["StartIP"].ToString());
                    forbidip.EndIP = Convert.ToInt32(dt.Rows[0]["EndIP"].ToString());
                    forbidip.StartIP2 = dt.Rows[0]["StartIP2"].ToString();
                    forbidip.EndIP2 = dt.Rows[0]["EndIP2"].ToString();
                    forbidip.ExpireDate = Convert.ToDateTime(dt.Rows[0]["ExpireDate"].ToString());

                }
                return forbidip;
            }
        }
        /// <summary>
        /// 更新起/始IP整型值
        /// </summary>
        /// <param name="_id">编号</param>
        /// <param name="_startip">开始IP</param>
        /// <param name="_endip">结束IP</param>
        public bool UpdateIPData(string _id, string _startip, string _endip)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "id=@id";
                _doh.AddConditionParameter("@id", _id);
                _doh.AddFieldItem("StartIP", JumboTCMS.Utils.IPHelp.IP2Long(System.Net.IPAddress.Parse(_startip)));
                _doh.AddFieldItem("EndIP", JumboTCMS.Utils.IPHelp.IP2Long(System.Net.IPAddress.Parse(_endip)));
                _doh.AddFieldItem("Enabled", 1);
                int _update = _doh.Update("jcms_normal_forbidip");
                return (_update == 1);
            }
        }
        /// <summary>
        /// 检测指定IP地址是否已受到屏蔽
        /// </summary>
        /// <param name="IP地址">要检测的IP地址</param>
        /// <returns>是否属于已屏蔽的IP</returns>
        public bool IPIsForbiding(string _ip)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                long ip = JumboTCMS.Utils.IPHelp.IP2Long(System.Net.IPAddress.Parse(_ip));
                _doh.Reset();
                if (this.DBType == "0")
                    _doh.ConditionExpress = "StartIP<=" + ip + " and EndIP>=" + ip + " AND datediff('d','" + DateTime.Now.ToShortDateString() + "',ExpireDate)>0";
                else
                    _doh.ConditionExpress = "StartIP<=" + ip + " and EndIP>=" + ip + " AND datediff(d,'" + DateTime.Now.ToShortDateString() + "',ExpireDate)>0";
                bool _isforbiding = _doh.Exist("jcms_normal_forbidip");
                return _isforbiding;
            }
        }
    }
}
