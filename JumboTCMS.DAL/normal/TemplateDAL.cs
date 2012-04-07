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
    /// 模板表信息
    /// </summary>
    public class Normal_TemplateDAL : Common
    {
        public Normal_TemplateDAL()
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
                if (_doh.Exist("jcms_normal_template"))
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
                if (_doh.Exist("jcms_normal_template"))
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
                int _countnum = _doh.Count("jcms_normal_template");
                sqlStr = JumboTCMS.Utils.SqlHelp.GetSql("[Id],[StartIP2],[EndIP2],[ExpireDate],[Enabled]", "jcms_normal_template", "Id", _pagesize, _thispage, "desc", _wherestr);
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
                int _del = _doh.Delete("jcms_normal_template");
                return (_del == 1);
            }
        }

        /// <summary>
        /// 获得单页内容的单条记录实体
        /// </summary>
        /// <param name="_id"></param>
        public JumboTCMS.Entity.Normal_Template GetEntity(string _id)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                JumboTCMS.Entity.Normal_Template template = new JumboTCMS.Entity.Normal_Template();
                _doh.Reset();
                _doh.SqlCmd = "SELECT * FROM [jcms_normal_template] WHERE [Id]=" + _id;
                DataTable dt = _doh.GetDataTable();
                if (dt.Rows.Count > 0)
                {
                    ///

                }
                return template;
            }
        }
        /// <summary>
        /// 获得模板内容
        /// </summary>
        /// <param name="_id">模板ID，0表示获得默认的首页模板</param>
        /// <param name="_projectid">输出方案ID</param>
        /// <param name="_pagestr">输出模板内容</param>
        public void GetTemplateContent(string _id, ref string _projectid, ref string _pagestr)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                if (_id != "0")
                    _doh.SqlCmd = "SELECT TOP 1 [pid],[Source] FROM [jcms_normal_template] WHERE [Id]=" + _id;
                else
                    _doh.SqlCmd = "SELECT TOP 1 [pid],[Source] FROM [jcms_normal_template] WHERE [Type]='System' AND [sType]='Index' ORDER BY IsDefault desc";
                DataTable dtTemplate = _doh.GetDataTable();
                if (dtTemplate.Rows.Count > 0)
                {
                    _projectid = dtTemplate.Rows[0]["pid"].ToString();
                    _pagestr = JumboTCMS.Utils.DirFile.ReadFile("~/templates/" + (new Normal_TemplateProjectDAL()).GetDir(_projectid) + "/" + dtTemplate.Rows[0]["Source"].ToString());
                }
                dtTemplate.Clear();
                dtTemplate.Dispose();
            }
        }
        public string GetSource(string _id)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT [Source] FROM [jcms_normal_template] WHERE [Id]=" + _id;
                DataTable dt = _doh.GetDataTable();
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["Source"].ToString();
                }
                return string.Empty;
            }
        }
    }
}
