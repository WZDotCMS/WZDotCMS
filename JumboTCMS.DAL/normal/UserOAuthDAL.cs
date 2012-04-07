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
using System.IO;
using System.Text;
using JumboTCMS.Utils;
using JumboTCMS.DBUtility;

namespace JumboTCMS.DAL
{
    /// <summary>
    /// 主题表信息
    /// </summary>
    public class Normal_UserOAuthDAL : Common
    {
        public Normal_UserOAuthDAL()
        {
            base.SetupSystemDate();
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
                int _countnum = _doh.Count("jcms_normal_user_oauth");
                sqlStr = JumboTCMS.Utils.SqlHelp.GetSql("*", "jcms_normal_user_oauth", "pId", _pagesize, _thispage, "asc", _wherestr);
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
        /// 移动
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_isup">true代表向上移动</param>
        /// <param name="_response"></param>
        /// <returns></returns>
        public bool Move(string _id, bool _isup, ref string _response)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                if (_id == "0")
                {
                    _response = "ID错误";
                    return false;
                }
                _doh.Reset();
                _doh.ConditionExpress = "id=@id";
                _doh.AddConditionParameter("@id", _id);
                string pId = _doh.GetField("jcms_normal_user_oauth", "pId").ToString();

                string temp;
                _doh.Reset();
                if (_isup)
                {
                    _doh.ConditionExpress = "pId<@pId ORDER BY pId desc";
                    _doh.AddConditionParameter("@pId", pId);
                }
                else
                {
                    _doh.ConditionExpress = "pId>@pId ORDER BY pId";
                    _doh.AddConditionParameter("@pId", pId);
                }
                temp = _doh.GetField("jcms_normal_user_oauth", "pId").ToString();
                if (temp == "")
                {
                    _response = "无须移动";
                    return false;
                }
                else
                {
                    _doh.Reset();
                    _doh.ConditionExpress = "pId=@pId";
                    _doh.AddConditionParameter("@pId", temp);
                    _doh.AddFieldItem("pId", "-100000");
                    _doh.Update("jcms_normal_user_oauth");
                    _doh.Reset();
                    _doh.ConditionExpress = "id=@id";
                    _doh.AddConditionParameter("@id", _id);
                    _doh.AddFieldItem("pId", temp);
                    _doh.Update("jcms_normal_user_oauth");
                    _doh.Reset();
                    _doh.ConditionExpress = "pId=@pId";
                    _doh.AddConditionParameter("@pId", "-100000");
                    _doh.AddFieldItem("pId", pId);
                    _doh.Update("jcms_normal_user_oauth");

                }
                return true;
            }
        }
        /// <summary>
        /// 批量操作插件
        /// </summary>
        /// <param name="_act">行为</param>
        /// <param name="_ids">id，以,隔开</param>
        public bool BatchOper(string _act, string _ids)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string[] idValue;
                idValue = _ids.Split(',');
                if (_act == "pass")
                {
                    for (int i = 0; i < idValue.Length; i++)
                    {
                        _doh.Reset();
                        _doh.ConditionExpress = "id=@id";
                        _doh.AddConditionParameter("@id", idValue[i]);
                        _doh.AddFieldItem("Enabled", 1);
                        _doh.Update("jcms_normal_user_oauth");
                    }
                }
                else if (_act == "nopass")
                {
                    for (int i = 0; i < idValue.Length; i++)
                    {
                        _doh.Reset();
                        _doh.ConditionExpress = "id=@id";
                        _doh.AddConditionParameter("@id", idValue[i]);
                        _doh.AddFieldItem("Enabled", 0);
                        _doh.Update("jcms_normal_user_oauth");
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 是否正在运行
        /// </summary>
        /// <param name="_oauthcode">接口代码</param>
        /// <returns></returns>
        public bool Running(string _oauthcode)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "code=@code and Enabled=1";
                _doh.AddConditionParameter("@code", _oauthcode);
                return (_doh.Exist("jcms_normal_user_oauth"));
            }
        }
    }
}
