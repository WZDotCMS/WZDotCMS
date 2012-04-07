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
using System.Collections.Generic;
using JumboTCMS.Utils;

namespace JumboTCMS.WebFile.Search
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
                case "ajaxGetContentList":
                    ajaxGetContentList();
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
        private void ajaxGetContentList()
        {
            string _ccid = Str2Str(q("ch"));
            string type = (q("type") == "" || q("type") == "all") ? JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "ModuleList") : q("type");
            string _mode = q("mode");
            int page = Str2Int(q("page"), 1);
            int PSize = Str2Int(q("pagesize"), 10);
            string keyword = q("k").Length < 2 ? q("k") : JumboTCMS.Utils.WordSpliter.GetKeyword(q("k"));//分词
            string keyword2 = q("k");
            if (_mode == "1") keyword2 = keyword;//自动分词
            int countNum = 0;
            double eventTime = 0;
            List<JumboTCMS.Utils.LuceneHelp.SearchItem> result = JumboTCMS.Utils.LuceneHelp.SearchIndex.Search(type, _ccid, keyword2, PSize, page, out countNum, out eventTime);
            string tempstr = "{recordcount :" + countNum + ", siteurl :'" + site.Url + "', eventtime :'" + eventTime + "', \n";
            tempstr += "table: [";
            if (result != null)
            {
                for (int j = 0; j < result.Count; j++)
                {
                    if (j > 0) tempstr += ",";
                    tempstr += "{id:" + result[j].Id + "," +
                        "channelid: '" + result[j].ChannelId + "', " +
                        "title: '" + HighLightKeyWord(result[j].Title, keyword).Replace("\\", "\\\\").Replace("\'", "\\\'").Replace("\t", " ").Replace("\r", " ").Replace("\n", " ") + "', " +
                        "summary: '" + HighLightKeyWord(result[j].Summary + "...", keyword).Replace("\\", "\\\\").Replace("\'", "\\\'").Replace("\t", " ").Replace("\r", " ").Replace("\n", " ") + "', " +
                        "tags: '" + HighLightKeyWord(result[j].Tags + "...", keyword).Replace("\\", "\\\\").Replace("\'", "\\\'").Replace("\t", " ").Replace("\r", " ").Replace("\n", " ") + "', " +
                        "adddate: '" + result[j].AddDate + "', " +
                        "url: '" + result[j].Url + "'" +
                        "}";
                }
            }
            tempstr += "],";
            tempstr += "pagebar:'" + (JumboTCMS.Utils.HtmlPager.GetPageBar(3, "js", 2, countNum, PSize, page, "javascript:ajaxSearchList(" + PSize + ",<#page#>);")).Replace("\\", "\\\\").Replace("\'", "\\\'").Replace("\t", " ").Replace("\r", " ").Replace("\n", " ") + "'}";
            this._response = tempstr;
        }

    }
}
