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
using JumboTCMS.Utils;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Ajax
{
    public partial class _content : JumboTCMS.UI.FrontAjax
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 8;//脚本过期时间
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxViewCount":
                    ajaxViewCount();
                    break;
                case "ajaxGo2View":
                    ajaxGo2View();
                    break;
                case "ajaxGetList":
                    ajaxGetList();
                    break;
                case "addFavorite":
                    AddFavorite();
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
        private void ajaxViewCount()
        {
            if (JumboTCMS.Utils.Cookie.GetValue(q("cType") + "ViewNum" + q("id")) == null && Str2Int(q("addit")) == 1)
            {
                doh.Reset();
                doh.ConditionExpress = "id=" + Str2Int(q("id"));
                doh.Add("jcms_module_" + q("cType"), "ViewNum");
                JumboTCMS.Utils.Cookie.SetObj(q("cType") + "ViewNum" + Str2Int(q("id")), "ok");
            }
            doh.Reset();
            doh.ConditionExpress = "id=" + Str2Int(q("id"));
            this._response = "{count :\"" + Validator.IntStr(doh.GetField("jcms_module_" + q("cType"), "ViewNum").ToString()) + "\"}";
        }
        private void ajaxGo2View()
        {
            string _channelid = Str2Str(f("channelid"));
            string _contentid = Str2Str(f("contentid"));
            this._response = JsonResult(1, Go2View(1, false, _channelid, _contentid, false));
        }
        private void AddFavorite()
        {
            string uId;
            int favCount = 0;
            if (Cookie.GetValue(site.CookiePrev + "user") != null)
            {
                string[] setting = (string[])Cookie.GetValue(site.CookiePrev + "user", "setting").Split(',');
                bool _CanFavorite = (setting[12] == "1");
                favCount = Str2Int(setting[14]);
                if (!_CanFavorite)
                {
                    this._response = "您所在的组不允许收藏";
                    return;
                }
                uId = Cookie.GetValue(site.CookiePrev + "user", "id");
                doh.Reset();
                doh.ConditionExpress = "UserId=@uid and ChannelId=@ccid and ContentId=@id";
                doh.AddConditionParameter("@uid", uId);
                doh.AddConditionParameter("@ccid", Str2Str(q("ccid")));
                doh.AddConditionParameter("@id", Str2Str(q("id")));
                if (doh.Exist("jcms_normal_user_favorite"))
                {
                    this._response = "此内容已被你收藏";
                    return;
                }
                if (favCount > 0)
                {
                    doh.Reset();
                    doh.ConditionExpress = "[UserId]=" + uId;
                    int aleadyFav = doh.Count("jcms_normal_user_favorite");
                    if (aleadyFav >= favCount)
                    {
                        this._response = "收藏的内容已满";
                        return;
                    }
                }
                doh.Reset();
                doh.ConditionExpress = "ChannelId=@ccid and Id=@id";
                doh.AddConditionParameter("@ccid", q("ccid"));
                doh.AddConditionParameter("@id", q("id"));
                string _cTitle = doh.GetField("jcms_module_" + q("cType"), "Title").ToString();
                doh.Reset();
                doh.AddFieldItem("Title", _cTitle);
                doh.AddFieldItem("ChannelId", Str2Int(q("ccid")));
                doh.AddFieldItem("ContentId", Str2Int(q("id")));
                doh.AddFieldItem("ModuleType", q("cType"));
                doh.AddFieldItem("AddDate", DateTime.Now.ToString());
                doh.AddFieldItem("UserId", uId);
                doh.Insert("jcms_normal_user_favorite");
                this._response = "成功收藏";
            }
            else
            {
                this._response = "只有会员才能收藏";
            }
        }
        private void ajaxGetList()
        {
            string _ctype = q("type");
            doh.Reset();
            if (!doh.ExistTable("jcms_module_" + _ctype))
            {
                this._response = "{recordcount :-1,returnval :'查询有误'}";
                return;
            }
            string _ccid = Str2Str(q("ch"));
            string _k = q("k");
            int page = Str2Int(q("page"), 1);
            int PSize = Str2Int(q("pagesize"), 1);
            string _skind = "title summary";//暂时能匹配标题和简介
            string sqlStr = "";
            int countNum = 0;
            string whereStr = "[IsPass]=1";
            if (_ccid != "0") whereStr += " AND [ChannelId]=" + _ccid;
            string searchField = string.Empty;
            string _k2 = JumboTCMS.Utils.WordSpliter.GetKeyword(_k, " ");
            string[] key = _k2.Split(new string[] { " " }, StringSplitOptions.None);
            string keys = "";
            for (int i = 0; i < key.Length; i++)
            {
                if (i == 0)
                    keys = "\"" + key[i].Trim() + "\"";
                else
                    keys += " OR \"" + key[i].Trim() + "\"";
            }
            whereStr += " AND (CONTAINS(title,'\"" + _k + "\"') OR CONTAINS(Summary,'\"" + _k + "\"'))";
            doh.Reset();
            doh.ConditionExpress = whereStr;
            countNum = doh.Count("jcms_module_" + _ctype);
            sqlStr = JumboTCMS.Utils.SqlHelp.GetSql("Id,ChannelId,ClassId,AddDate,Title,Summary,Tags,FirstPage", "jcms_module_" + _ctype, "id", PSize, page, "desc", whereStr);
            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dtContent = doh.GetDataTable();
            string tempstr = "{recordcount :" + countNum.ToString() + ", siteurl :'" + site.Url + "', \n";
            tempstr += "table: [";
            for (int j = 0; j < dtContent.Rows.Count; j++)
            {
                string _url = dtContent.Rows[j]["FirstPage"].ToString();
                if (j > 0) tempstr += ",";
                tempstr += "{id: " + dtContent.Rows[j]["Id"].ToString() + ", " +
                    "title: '" + p__HighLight(dtContent.Rows[j]["Title"].ToString(), _k2).Replace("\\", "\\\\").Replace("\'", "\\\'").Replace("\t", " ").Replace("\r", " ").Replace("\n", " ") + "', " +
                    "summary: '" + p__HighLight(dtContent.Rows[j]["Summary"].ToString(), _k2).Replace("\\", "\\\\").Replace("\'", "\\\'").Replace("\t", " ").Replace("\r", " ").Replace("\n", " ") + "', " +
                    "adddate: '" + dtContent.Rows[j]["AddDate"].ToString() + "', " +
                    "tags: '" + dtContent.Rows[j]["Tags"].ToString() + "', " +
                    "channelid: " + dtContent.Rows[j]["ChannelId"].ToString() + ", " +
                    "classid: " + dtContent.Rows[j]["ClassId"].ToString() + ", " +
                    "classname: '" + (new JumboTCMS.DAL.Normal_ClassDAL().GetClassName(dtContent.Rows[j]["ClassId"].ToString())) + "', " +
                    "classurl: '" + Go2Class(1, false, dtContent.Rows[j]["ChannelId"].ToString(), dtContent.Rows[j]["ClassId"].ToString(), false) + "', " +
                    "url: '" + _url + "'" +
                    "}";
            }
            dtContent.Clear();
            dtContent.Dispose();
            if (_skind == "tag")
            {
                //标签增加点击
                new JumboTCMS.DAL.Normal_TagDAL().AddClickTimes(_ccid, _k);
            }
            tempstr += "],";
            tempstr += "pagebar:'" + (JumboTCMS.Utils.HtmlPager.GetPageBar(3, "js", 2, countNum, PSize, page, "javascript:ajaxSearchList(" + PSize + ",<#page#>);")).Replace("\\", "\\\\").Replace("\'", "\\\'").Replace("\t", " ").Replace("\r", " ").Replace("\n", " ") + "'}";
            this._response = tempstr;
        }
    }
}