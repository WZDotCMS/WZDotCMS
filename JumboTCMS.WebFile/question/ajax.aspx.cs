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
using JumboTCMS.Common;
using JumboTCMS.Utils;
namespace JumboTCMS.WebFile.Question
{
    public partial class _ajax : JumboTCMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckFormUrl())
            {
                Response.End();
            }
            Server.ScriptTimeout = 8;//脚本过期时间
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxQuestionAdd":
                    ajaxQuestionAdd();
                    break;
                case "ajaxQuestionDel":
                    ajaxQuestionDel();
                    break;
                case "ajaxQuestionList":
                    ajaxQuestionList();
                    break;
                default:
                    DefaultResponse();
                    break;
            }

            Response.Write(this._response);
        }

        private void DefaultResponse()
        {
            this._response = "{result :\"未知操作\"}";
        }
        private void ajaxQuestionAdd()
        {
            string uId, uName, pId;
            pId = f("parentid");
            string _code = f("code");
            int _GuestPost = Str2Int(JumboTCMS.Utils.XmlCOM.ReadConfig(site.Dir + "_data/config/question", "GuestPost"), 0);
            int _NeedCheck = Str2Int(JumboTCMS.Utils.XmlCOM.ReadConfig(site.Dir + "_data/config/question", "NeedCheck"), 0);
            int _PostTimer = Str2Int(JumboTCMS.Utils.XmlCOM.ReadConfig(site.Dir + "_data/config/question", "PostTimer"), 0);
            int _State;
            if (!JumboTCMS.Common.ValidateCode.CheckValidateCode(_code))
            {
                this._response = JsonResult(0, "验证码错误");
                return;
            }
            if (pId != "0")//回复留言
            {
                if (Cookie.GetValue(site.CookiePrev + "admin") == null)
                {
                    this._response = JsonResult(0, "管理员登录后才可回复");
                    return;
                }
                uId = Cookie.GetValue(site.CookiePrev + "admin", "id");
                uName = Cookie.GetValue(site.CookiePrev + "admin", "name");
                _State = 1;
            }
            else//留言
            {
                #region  判断时间周期
                int countNum = 0;
                string whereStr = "[ParentId]=0 AND [IP]='" + Const.GetUserIp + "'";
                if (DBType == "0")
                    whereStr += " and datediff('s',adddate,'" + DateTime.Now.ToString() + "')<" + _PostTimer;
                else
                    whereStr += " and datediff(s,adddate,'" + DateTime.Now.ToString() + "')<" + _PostTimer;
                doh.Reset();
                doh.ConditionExpress = whereStr;
                countNum = doh.Count("jcms_normal_question");
                if (countNum > 0)//说明周期内留过言
                {
                    this._response = JsonResult(0, _PostTimer + "秒内只能留言一次!");
                    return;
                }
                #endregion
                if (Cookie.GetValue(site.CookiePrev + "user") != null)
                {
                    uId = Cookie.GetValue(site.CookiePrev + "user", "id");
                    uName = Cookie.GetValue(site.CookiePrev + "user", "name");
                    _State = (_NeedCheck == 0) ? 1 : 0;
                }
                else
                {
                    if (_GuestPost == 0)//游客不允许留言
                    {
                        this._response = JsonResult(0, "请登录后再进行留言");
                        return;
                    }
                    uId = "0";
                    uName = JumboTCMS.Utils.Strings.HtmlEncode(f("name"));
                    _State = (_NeedCheck == 0) ? 1 : 0;
                }
            }
            doh.Reset();
            doh.AddFieldItem("ParentId", pId);
            doh.AddFieldItem("AddDate", DateTime.Now.ToString());
            doh.AddFieldItem("Title", JumboTCMS.Utils.Strings.HtmlEncode(f("title")));
            doh.AddFieldItem("Content", GetCutString(JumboTCMS.Utils.Strings.HtmlEncode(f("content")), 200));
            doh.AddFieldItem("IP", Const.GetUserIp);
            doh.AddFieldItem("UserId", uId);
            doh.AddFieldItem("UserName", uName);
            doh.AddFieldItem("IsPass", _State);
            doh.AddFieldItem("ClassId", f("classid"));
            doh.Insert("jcms_normal_question");
            if (pId != "0")
                this._response = JsonResult(1, "回复成功");
            else
                this._response = JsonResult(1, "发表成功");
        }
        private void ajaxQuestionDel()
        {
            string questionid = Str2Str(f("questionid"));
            if (Cookie.GetValue(site.CookiePrev + "admin") == null)
            {
                this._response = JsonResult(0, "管理员登录后才可删除");
            }
            else
            {
                doh.Reset();
                doh.ConditionExpress = "id=" + questionid;
                if (doh.Delete("jcms_normal_question") == 1)
                {
                    doh.Reset();
                    doh.ConditionExpress = "parentid=" + questionid;
                    doh.Delete("jcms_normal_question");
                    this._response = JsonResult(1, "删除成功");
                }
                else
                    this._response = JsonResult(0, "未知错误");//编号有错误
            }
        }
        private void ajaxQuestionList()
        {
            int PSize = Str2Int(JumboTCMS.Utils.XmlCOM.ReadConfig(site.Dir + "_data/config/question", "PageSize"), 10);
            int page = Str2Int(q("page"), 1);
            string classid = Str2Str(q("classid"));
            int countNum = 0;
            string sqlStr = "";
            string joinStr = "A.[Id]=B.ParentId";
            string whereStr1 = "A.[ParentId]=0 AND A.[ClassId]=" + classid;//外围条件(带A.)
            string whereStr2 = "[ParentId]=0 AND [ClassId]=" + classid;//分页条件(不带A.)
            doh.Reset();
            doh.ConditionExpress = whereStr2;
            countNum = doh.Count("jcms_normal_question");
            sqlStr = JumboTCMS.Utils.SqlHelp.GetSql("A.IsPass,A.Id as Id,A.IP as ip,A.UserId as UserId,A.UserName as UserName,A.AddDate as AddDate,A.Title as Title, A.Content as Content,B.UserId as ReplyUserId,B.UserName as ReplyUserName,B.AddDate as ReplyAddDate,B.Content as ReplyContent", "jcms_normal_question", "jcms_normal_question", "Id", PSize, page, "desc", joinStr, whereStr1, whereStr2);
            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dt = doh.GetDataTable();
            this._response = "{result :\"1\"," +
                "returnval :\"操作成功\"," +
                "pagerbar :\"" + JumboTCMS.Utils.HtmlPager.GetPageBar(1, "js", 2, countNum, PSize, page, "javascript:ajaxQuestionList(<#page#>);") + "\"," +
                JumboTCMS.Utils.dtHelp.DT2JSON(dt, (PSize * (page - 1))) +
                "}";
            dt.Clear();
            dt.Dispose();
        }
    }
}
