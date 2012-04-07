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
    /// 标签表信息
    /// </summary>
    public class Normal_TagDAL : Common
    {
        public Normal_TagDAL()
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
                if (_doh.Exist("jcms_normal_tag"))
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
                if (_doh.Exist("jcms_normal_tag"))
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
                int _countnum = _doh.Count("jcms_normal_tag");
                sqlStr = JumboTCMS.Utils.SqlHelp.GetSql("[ID],[Title],[source]", "jcms_normal_tag", "Id", _pagesize, _thispage, "desc", _wherestr);
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
                int _del = _doh.Delete("jcms_normal_tag");
                return (_del == 1);
            }
        }

        /// <summary>
        /// 获得标签的单条记录实体
        /// </summary>
        /// <param name="_id"></param>
        public JumboTCMS.Entity.Normal_Tag GetEntity(string _id)
        {
            JumboTCMS.Entity.Normal_Tag tag = new JumboTCMS.Entity.Normal_Tag();
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT * FROM [jcms_normal_tag] WHERE [Id]=" + _id;
                DataTable dt = _doh.GetDataTable();
                if (dt.Rows.Count > 0)
                {
                    tag.Id = dt.Rows[0]["Id"].ToString();
                    tag.Title = dt.Rows[0]["Title"].ToString();
                    tag.ClickTimes = Validator.StrToInt(dt.Rows[0]["ClickTimes"].ToString(), 0);
                    tag.ChannelId = Validator.StrToInt(dt.Rows[0]["ChannelId"].ToString(), 0);

                }
                return tag;
            }
        }
        /// <summary>
        /// 自动增添Tag标签到数据库
        /// </summary>
        /// <param name="_channelid">频道ID</param>
        /// <param name="_tags">要增加的Tag，多个Tag以,隔开</param>
        public void InsertTags(string _channelid, string _tags)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                if (_tags.Length == 0) return;
                string[] tag = _tags.Split(',');
                for (int i = 0; i < tag.Length; i++)
                {
                    if (!ExistTitle(tag[i].ToString(), "0", "ChannelId=" + _channelid))
                    {
                        _doh.Reset();
                        _doh.AddFieldItem("Title", tag[i].ToString());
                        _doh.AddFieldItem("ClickTimes", "0");
                        _doh.AddFieldItem("ChannelId", _channelid);
                        _doh.Insert("jcms_normal_tag");
                    }
                }
            }
        }
        /// <summary>
        /// 增加标签点击数
        /// </summary>
        /// <param name="_channelid"></param>
        /// <param name="_tagname"></param>
        public void AddClickTimes(string _channelid, string _tagname)
        {
            if (_tagname.Length == 0) return;
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "Title=@Title and ChannelId=" + _channelid;
                _doh.AddConditionParameter("@Title", _tagname);
                _doh.Add("jcms_normal_tag", "ClickTimes");
            }
        }
    }
}
