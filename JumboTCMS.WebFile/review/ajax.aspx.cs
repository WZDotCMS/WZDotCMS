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
namespace JumboTCMS.WebFile.Review
{
    public partial class _review_ajax : JumboTCMS.UI.AdminCenter
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
                case "ajaxReview":
                    ajaxReview();
                    break;
                case "ajaxReviewCount":
                    ajaxReviewCount();
                    break;
                case "ajaxReviewAdd":
                    ajaxReviewAdd();
                    break;
                case "ajaxReviewDel":
                    ajaxReviewDel();
                    break;
                case "ajaxReviewList":
                    ajaxReviewList();
                    break;
                case "ajaxReviewUserInfo":
                    ajaxReviewUserInfo();
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
        private void ajaxReview()
        {
            this._response = "{channelid :\"" + q("ccid") + "\",contentid:" + q("id") + "}";
        }
        private void ajaxReviewCount()
        {

            doh.ConditionExpress = "contentid=" + q("id") + " and channelid=" + q("ccid");
            if (!doh.Exist("jcms_normal_review"))
            {
                doh.Reset();
                doh.AddFieldItem("ContentId", q("id"));
                doh.AddFieldItem("ChannelId", q("ccid"));
                doh.AddFieldItem("ReviewNum", 0);
                doh.Insert("jcms_normal_review");
            }
            doh.Reset();
            doh.ConditionExpress = "contentid=" + q("id") + " and channelid=" + q("ccid");
            this._response = "{count :\"" + Validator.IntStr(doh.GetField("jcms_normal_review", "ReviewNum").ToString()) + "\"}";
        }
        private void ajaxReviewAdd()
        {
            string uId, uName, pId;
            pId = f("parentid");
            string _code = f("code");
            int _GuestPost = Str2Int(JumboTCMS.Utils.XmlCOM.ReadConfig(site.Dir + "_data/config/review", "GuestPost"), 0);
            int _NeedCheck = Str2Int(JumboTCMS.Utils.XmlCOM.ReadConfig(site.Dir + "_data/config/review", "NeedCheck"), 0);
            int _PostTimer = Str2Int(JumboTCMS.Utils.XmlCOM.ReadConfig(site.Dir + "_data/config/review", "PostTimer"), 0);
            int _State;
            if (!JumboTCMS.Common.ValidateCode.CheckValidateCode(_code))
            {
                this._response = JsonResult(0, "验证码错误");
                return;
            }
            if (!JumboTCMS.Utils.Validator.IsNumeric(f("ccid")))
            {
                this._response = JsonResult(0, "频道有误");
                return;
            }
            if (!JumboTCMS.Utils.Validator.IsNumeric(f("id")))
            {
                this._response = JsonResult(0, "ID有误");
                return;
            }
            if (pId != "0")//回复评论
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
            else//评论
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
                countNum = doh.Count("jcms_normal_review");
                if (countNum > 0)//说明周期内评论过
                {
                    this._response = JsonResult(0, _PostTimer + "秒内只能评论一次!");
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
                    if (_GuestPost == 0)//游客不允许评论
                    {
                        this._response = JsonResult(0, "请登录后再进行评论");
                        return;
                    }
                    uId = "0";
                    uName = JumboTCMS.Utils.Strings.HtmlEncode(f("name")).Replace("[", "").Replace("]", "");
                    _State = (_NeedCheck == 0) ? 1 : 0;
                }
            }
            doh.Reset();
            doh.AddFieldItem("ChannelId", f("ccid"));
            doh.AddFieldItem("ParentId", pId);
            doh.AddFieldItem("ContentId", f("id"));
            doh.AddFieldItem("AddDate", DateTime.Now.ToString());
            doh.AddFieldItem("Content", GetCutString(JumboTCMS.Utils.Strings.HtmlEncode(f("content")), 200));
            doh.AddFieldItem("IP", Const.GetUserIp);
            doh.AddFieldItem("UserName", uName);
            doh.AddFieldItem("IsPass", _State);
            doh.Insert("jcms_normal_review");
            if (pId != "0")
                this._response = JsonResult(1, "回复成功");
            else
                this._response = JsonResult(1, "发表成功");
        }
        private void ajaxReviewDel()
        {
            string _reviewid = Str2Str(f("reviewid"));
            if (Cookie.GetValue(site.CookiePrev + "admin") == null)
                this._response = JsonResult(0, "管理员登录后才可删除");
            else
            {
                doh.Reset();
                doh.ConditionExpress = "id=" + _reviewid;
                if (doh.Delete("jcms_normal_review") == 1)
                {
                    doh.Reset();
                    doh.ConditionExpress = "parentid=" + _reviewid;
                    doh.Delete("jcms_normal_review");
                    this._response = JsonResult(1, "删除成功");
                }
                else
                    this._response = JsonResult(0, "未知错误");//编号有错误
            }
        }
        private void ajaxReviewList()
        {
            string ccid = Str2Str(q("ccid"));
            string id = Str2Str(q("id"));
            if (!JumboTCMS.Utils.Validator.IsNumeric(id))
            {
                this._response = "ID有误!";
                return;
            }
            int PSize = Str2Int(JumboTCMS.Utils.XmlCOM.ReadConfig(site.Dir + "_data/config/review", "PageSize"), 10);
            int page = Int_ThisPage();
            int countNum = 0;
            string sqlStr = "";
            string joinStr = "A.[Id]=B.ParentId";
            string whereStr1 = "A.[IsPass]=1 AND A.[ParentId]=0 AND A.[ChannelId]=" + ccid + " AND A.[ContentId]=" + id;//外围条件(带A.)
            string whereStr2 = "[IsPass]=1 AND [ChannelId]=" + ccid + " AND [ContentId]=" + id;//分页条件(不带A.)
            doh.Reset();
            doh.ConditionExpress = whereStr2;
            countNum = doh.Count("jcms_normal_review");
            sqlStr = JumboTCMS.Utils.SqlHelp.GetSql("A.Id as Id,A.IP as ip,A.UserName as UserName,A.AddDate as AddDate, A.Content as Content,B.UserName as ReplyUserName,B.AddDate as ReplyAddDate,B.Content as ReplyContent", "jcms_normal_review", "jcms_normal_review", "Id", PSize, page, "desc", joinStr, whereStr1, whereStr2);
            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dt = doh.GetDataTable();
            this._response = "{result :\"1\"," +
                "returnval :\"操作成功\"," +
                "pagerbar :\"" + JumboTCMS.Utils.HtmlPager.GetPageBar(3, "js", 2, countNum, PSize, page, "javascript:ajaxReviewList(<#page#>);") + "\"," +
                JumboTCMS.Utils.dtHelp.DT2JSON(dt, (PSize * (page - 1))) +
                "}";
            dt.Clear();
            dt.Dispose();
        }
        private void ajaxReviewUserInfo()
        {
            if (Cookie.GetValue(site.CookiePrev + "user") != null)
            {
                this._response = "{username :\"" + Cookie.GetValue(site.CookiePrev + "user", "name") + "\"}";
            }
            else
            {
                this._response = "{username :\"\"}";
            }
        }
    }
}
