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
namespace JumboTCMS.WebFile.Extends.Vote
{
    public partial class _index : JumboTCMS.UI.FrontHtml
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckExtendState("Vote", "html");
            Server.ScriptTimeout = 8;//脚本过期时间
            string ContentStr = LoadPlugin_Vote(Str2Str(q("id")));
            Response.Write(ContentStr);//直接输出
        }
        /// <summary>
        /// 生成链接页
        /// </summary>
        /// <returns></returns>
        private string LoadPlugin_Vote(string id)
        {
            string PageStr = string.Empty;
            PageStr = JumboTCMS.Utils.DirFile.ReadFile("~/templates/extends_vote_index.htm");
            ReplaceSiteTags(ref PageStr);
            doh.Reset();
            doh.SqlCmd = "SELECT [Id],[Title],[VoteText],[VoteNum],[VoteTotal],[Type],[Lock] FROM [jcms_extends_vote] WHERE [Id]=" + id;
            DataTable dtVote = doh.GetDataTable();
            if (dtVote.Rows.Count > 0)
            {
                for (int i = 0; i < dtVote.Columns.Count; i++)
                {
                    PageStr = PageStr.Replace("{$Vote" + dtVote.Columns[i].ColumnName + "}", dtVote.Rows[0][i].ToString());
                }
                PageStr = PageStr.Replace("{$VoteSON}", p__getVoteJSON(id));

            }
            else
                return "参数错误,请不要修改参数提交";
            dtVote.Clear();
            dtVote.Dispose();
            return PageStr;
        }
        /// <summary>
        /// 显示调查信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string p__getVoteJSON(string id)
        {
            string TempStr = "";
            TempStr = "";
            doh.Reset();
            doh.SqlCmd = "SELECT [Title],[VoteText],[VoteNum],[VoteTotal] FROM [jcms_extends_vote] WHERE [Id]=" + id + " And [Lock]=0";
            DataTable dtVote = doh.GetDataTable();
            if (dtVote.Rows.Count > 0)
            {
                string[] voteText = dtVote.Rows[0]["VoteText"].ToString().Split('|');
                string[] voteNum = dtVote.Rows[0]["VoteNum"].ToString().Split('|');
                int voteTotal = Convert.ToInt32(dtVote.Rows[0]["VoteTotal"].ToString());
                TempStr += "[";
                for (int i = 0; i < voteText.Length; i++)
                {
                    if (i > 0)
                        TempStr += ",";
                    int _persent = (voteTotal > 0) ? (100 * Convert.ToInt32(voteNum[i]) / voteTotal) : 0;
                    TempStr += "{";
                    TempStr += "no:" + (i + 1) + ",";
                    TempStr += "text:'" + voteText[i] + "',";
                    TempStr += "persent:'" + _persent + "',";
                    TempStr += "votenum:'" + voteNum[i] + "'";
                    TempStr += "}";
                }
                TempStr += "]";
            }
            dtVote.Clear();
            dtVote.Dispose();
            return TempStr;

        }
    }
}
