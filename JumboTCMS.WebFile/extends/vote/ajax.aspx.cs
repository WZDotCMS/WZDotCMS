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
using System.Collections.Generic;
using System.Data;
using System.Web;
using JumboTCMS.Utils;
namespace JumboTCMS.WebFile.Extends.Vote
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
                case "ajaxGetList":
                    ajaxGetList();
                    break;
                case "ajaxBatchOper":
                    ajaxBatchOper();
                    break;
                case "checkname":
                    ajaxCheckName();
                    break;
                case "updatefore":
                    ajaxUpdateFore();
                    break;
                case "ajaxPluginVoteAdd":
                    ajaxPluginVoteAdd();
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
        private void ajaxCheckName()
        {
            this._response = JsonResult(1, "可以重复");
        }
        private void ajaxGetList()
        {
            Admin_Load("master", "json");
            string ccid = Str2Str(q("ccid"));
            string id = Str2Str(q("id"));
            int PSize = Str2Int(q("pagesize"), 0) == 0 ? 15 : Str2Int(q("pagesize"), 0);
            int page = Int_ThisPage();
            int countNum = 0;

            string sqlStr = "";
            string whereStr = "1=1";
            doh.Reset();
            doh.ConditionExpress = whereStr;
            countNum = doh.Count("jcms_extends_vote");
            sqlStr = JumboTCMS.Utils.SqlHelp.GetSql("Id,Title,VoteTotal,[Type],[Lock]", "jcms_extends_vote", "Id", PSize, page, "desc", whereStr);

            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dt = doh.GetDataTable();
            this._response = "{result :\"1\"," +
                "returnval :\"操作成功\"," +
                "pagerbar :\"" + JumboTCMS.Utils.HtmlPager.GetPageBar(3, "js", 2, countNum, PSize, page, "javascript:ajaxList(<#page#>);") + "\"," +
                JumboTCMS.Utils.dtHelp.DT2JSON(dt) +
                "}";
            dt.Clear();
            dt.Dispose();
        }
        /// <summary>
        /// 执行批量操作
        /// </summary>
        /// <param name="oper"></param>
        /// <param name="ids"></param>
        private void ajaxBatchOper()
        {
            Admin_Load("master", "json");
            string act = q("act");
            string ids = f("ids");
            string[] idValue;
            idValue = ids.Split(',');
            string ClassId = string.Empty;
            if (act == "pass")
            {
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "id=@id";
                    doh.AddConditionParameter("@id", idValue[i]);
                    doh.AddFieldItem("Lock", 0);
                    doh.Update("jcms_extends_vote");
                }
            }
            else if (act == "nopass")
            {
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "id=@id";
                    doh.AddConditionParameter("@id", idValue[i]);
                    doh.AddFieldItem("Lock", 1);
                    doh.Update("jcms_extends_vote");
                }
            }
            else//均为删除
            {
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "id=@id";
                    doh.AddConditionParameter("@id", idValue[i]);
                    doh.Delete("jcms_extends_vote");
                }
            }
            this._response = JsonResult(1, "操作成功");
        }
        private void ajaxUpdateFore()
        {
            Admin_Load("master", "json");
            string _TemplateContent = JumboTCMS.Utils.DirFile.ReadFile("~/templates/_p_vote.htm");
            JumboTCMS.TEngine.TemplateManager manager = JumboTCMS.TEngine.TemplateManager.FromString(_TemplateContent);
            JumboTCMS.Entity.Extends_Vote vote = new JumboTCMS.DAL.Extends_VoteDAL().GetVote();
            manager.SetValue("vote", vote);
            manager.SetValue("site", site);
            string _content = manager.Process();
            JumboTCMS.Utils.DirFile.SaveFile(_content, "~/_data/html/p_vote.htm", false);
            JumboTCMS.Utils.DirFile.SaveFile(_content, "~/_data/shtm/p_vote.htm", true);
            JumboTCMS.Utils.DirFile.SaveFile(JumboTCMS.Utils.Strings.Html2Js(_content), "~/_data/style/p_vote.js");
            this._response = JsonResult(1, "更新完成");
        }
        private void ajaxPluginVoteAdd()
        {
            string _voteid = Str2Str(q("id"));
            if (JumboTCMS.Utils.Cookie.GetValue("Vote" + _voteid) != null)
                this._response = "请不要重复投票!";
            else
            {
                string[] voteResult = q("vote").Split(',');
                doh.Reset();
                doh.SqlCmd = "SELECT Title,VoteText,VoteNum,VoteTotal FROM [jcms_extends_vote] WHERE [Id]=" + q("id") + " And [Lock]=0";
                DataTable dtVote = doh.GetDataTable();
                if (dtVote.Rows.Count > 0)
                {
                    string[] voteText = dtVote.Rows[0]["VoteText"].ToString().Split('|');
                    string[] voteNum = dtVote.Rows[0]["VoteNum"].ToString().Split('|');
                    string[] userVote = new string[voteText.Length];
                    string res = "";
                    for (int i = 0; i < voteResult.Length; i++)
                    {
                        voteNum[Str2Int(voteResult[i]) - 1] = (Str2Int(voteNum[Str2Int(voteResult[i]) - 1]) + 1).ToString();
                    }
                    for (int i = 0; i < voteText.Length; i++)
                    {
                        res += "|" + voteNum[i];
                    }
                    res = res.Substring(1, res.Length - 1);

                    doh.Reset();
                    doh.ConditionExpress = "lock=0 and [Id]=" + _voteid;
                    doh.AddFieldItem("VoteNum", res);
                    doh.Update("jcms_extends_vote");
                    doh.Reset();
                    doh.ConditionExpress = "lock=0 and [Id]=" + _voteid;
                    doh.Add("jcms_extends_vote", "VoteTotal");
                    JumboTCMS.Utils.Cookie.SetObj("Vote" + _voteid, "ok");
                    this._response = "ok";
                }
                else
                    this._response = "数据错误,请稍后重试!";
                dtVote.Clear();
                dtVote.Dispose();
            }
        }
    }
}
