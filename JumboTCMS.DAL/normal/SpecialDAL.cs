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
using System.Collections.Generic;
using System.Web;
using JumboTCMS.Utils;
using JumboTCMS.Entity;
using JumboTCMS.DBUtility;

namespace JumboTCMS.DAL
{
    /// <summary>
    /// 专题表信息
    /// </summary>
    public class Normal_SpecialDAL : Common
    {
        public Normal_SpecialDAL()
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
                if (_doh.Exist("jcms_normal_special"))
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
                if (_doh.Exist("jcms_normal_special"))
                    _ext = 1;
                return (_ext == 1);
            }
        }
        /// <summary>
        /// 判断重复性(文件名是否存在)
        /// </summary>
        /// <param name="_source">需要检索的文件名</param>
        /// <param name="_id">除外的ID</param>
        /// <param name="_wherestr">其他条件</param>
        /// <returns></returns>
        public bool ExistSource(string _source, string _id, string _wherestr)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                int _ext = 0;
                _doh.Reset();
                _doh.ConditionExpress = "source=@source and id<>" + _id;
                if (_wherestr != "") _doh.ConditionExpress += " and " + _wherestr;
                _doh.AddConditionParameter("@source", _source);
                if (_doh.Exist("jcms_normal_special"))
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
                int _countnum = _doh.Count("jcms_normal_special");
                sqlStr = JumboTCMS.Utils.SqlHelp.GetSql("[ID],[Title],[Source]", "jcms_normal_special", "Id", _pagesize, _thispage, "desc", _wherestr);
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
        /// 删除一条数据
        /// </summary>
        public bool DeleteByID(string _id)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "sId=@id";
                _doh.AddConditionParameter("@id", _id);
                _doh.Delete("jcms_normal_specialcontent");
                _doh.Reset();
                _doh.ConditionExpress = "id=@id";
                _doh.AddConditionParameter("@id", _id);
                int _del = _doh.Delete("jcms_normal_special");
                return (_del == 1);
            }
        }

        /// <summary>
        /// 获得单页内容的单条记录实体
        /// </summary>
        /// <param name="_id"></param>
        public JumboTCMS.Entity.Normal_Special GetEntity(string _id)
        {
            Normal_Special special = new Normal_Special();
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT * FROM [jcms_normal_special] WHERE [Id]=" + _id;
                DataTable dt = _doh.GetDataTable();
                if (dt.Rows.Count > 0)
                {
                    special.Id = dt.Rows[0]["Id"].ToString();
                    special.Title = dt.Rows[0]["Title"].ToString();
                    special.Info = dt.Rows[0]["Info"].ToString();
                    special.Source = dt.Rows[0]["Source"].ToString();

                }
                return special;
            }
        }
        /// <summary>
        /// 解析专题标签
        /// </summary>
        /// <param name="pagestr">原内容</param>
        /// <param name="_id">SpecialId不能为0</param>
        public void ExecuteTags(ref string _pagestr, string _id)
        {
            if (_id == "0") return;
            Normal_Special special = GetEntity(_id);
            _pagestr = _pagestr.Replace("{$SpecialId}", _id);
            _pagestr = _pagestr.Replace("{$SpecialName}", special.Title);
            _pagestr = _pagestr.Replace("{$SpecialInfo}", special.Info);

        }
        /// <summary>
        /// 得到列表
        /// </summary>
        public List<Normal_Special> SpecialList(int _pagesize)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                List<Normal_Special> specials;
                specials = new List<Normal_Special>();
                _doh.Reset();
                _doh.SqlCmd = "SELECT TOP " + _pagesize + " [Id],[Title],[Info],[Source] FROM [jcms_Normal_Special] ORDER BY Id Desc";
                DataTable dtSpecial = _doh.GetDataTable();
                if (dtSpecial.Rows.Count > 0)
                {
                    for (int i = 0; i < dtSpecial.Rows.Count; i++)
                    {
                        specials.Add(new Normal_Special(dtSpecial.Rows[i]["Id"].ToString(),
                            dtSpecial.Rows[i]["Title"].ToString(),
                            dtSpecial.Rows[i]["Info"].ToString(),
                            dtSpecial.Rows[i]["Source"].ToString()
                            ));
                    }
                }
                dtSpecial.Clear();
                dtSpecial.Dispose();
                return specials;
            }
        }
    }
}
