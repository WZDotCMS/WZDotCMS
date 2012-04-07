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
    /// 友情链接插件
    /// </summary>
    public class Extends_VoteDAL : Common
    {
        public Extends_VoteDAL()
        {
            base.SetupSystemDate();
        }
        /// <summary>
        /// 得到列表
        /// </summary>
        public Extends_Vote GetVote()
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                Extends_Vote vote = new Extends_Vote();
                _doh.Reset();
                _doh.SqlCmd = "SELECT TOP 1 [Id],[Title],[VoteText],[VoteNum],[VoteTotal],[Type] FROM [jcms_extends_vote] WHERE [Lock]=0 ORDER BY Id Desc";
                DataTable dtVote = _doh.GetDataTable();
                if (dtVote.Rows.Count > 0)
                {
                    vote.Id = dtVote.Rows[0]["Id"].ToString();
                    vote.Title = dtVote.Rows[0]["Title"].ToString();
                    vote.VoteTotal = Str2Int(dtVote.Rows[0]["VoteTotal"].ToString());
                    string[] itemtext = dtVote.Rows[0]["VoteText"].ToString().Split('|');
                    string[] itemclicks = dtVote.Rows[0]["VoteNum"].ToString().Split('|');
                    List<Extends_VoteItem> voteitems = new List<Extends_VoteItem>();
                    for (int i = 0; i < itemtext.Length; i++)
                    {
                        voteitems.Add(new Extends_VoteItem(itemtext[i], Str2Int(itemclicks[i])));
                    }
                    vote.Item = voteitems;
                    vote.Type = Str2Int(dtVote.Rows[0]["Type"].ToString());
                }
                else
                    vote.Id = "0";
                dtVote.Clear();
                dtVote.Dispose();
                return vote;
            }
        }
    }
}
