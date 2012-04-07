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
    /// 内容评论
    /// </summary>
    public class Normal_ReviewDAL : Common
    {
        public Normal_ReviewDAL()
        {
            base.SetupSystemDate();
        }
        /// <summary>
        /// 得到评论列表
        /// </summary>
        /// <param name="_thispage">当前页码</param>
        /// <param name="_pagesize">每页记录条数</param>
        /// <param name="_channelid">频道ID</param>
        /// <param name="_contentid">内容ID</param>
        public string GetTopList(int _thispage, int _pagesize, string _channelid, string _contentid)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string sqlStr = "";
                int countNum = 0;
                string whereStr = "[IsPass]=1 AND [ParentId]=0";
                if (_channelid != "0") whereStr += " AND [ChannelId]=" + _channelid;
                if (_contentid != "0") whereStr += " AND [ContentId]=" + _contentid;
                _doh.Reset();
                _doh.ConditionExpress = whereStr;
                countNum = _doh.Count("jcms_normal_review");

                sqlStr = JumboTCMS.Utils.SqlHelp.GetSql("Id,ChannelId,ContentId,IP,UserName,AddDate,Content", "jcms_normal_review", "id", _pagesize, _thispage, "desc", whereStr);
                _doh.Reset();
                _doh.SqlCmd = sqlStr;
                DataTable dt = _doh.GetDataTable();
                string ResponseStr = "";
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    ResponseStr += "<li><a href=\"" + site.Dir + "review/default.aspx?ccid=" + dt.Rows[j]["ChannelId"].ToString() + "&id=" + dt.Rows[j]["ContentId"].ToString() + "#c" + dt.Rows[j]["Id"].ToString() + "\" target=\"_blank\">" + dt.Rows[j]["Content"].ToString() + "</a></li>";
                }
                dt.Clear();
                dt.Dispose();
                return ResponseStr;
            }
        }
    }
}
