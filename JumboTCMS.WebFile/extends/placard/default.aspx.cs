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
namespace JumboTCMS.WebFile.Extends.Placard
{
    public partial class _index : JumboTCMS.UI.FrontHtml
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckExtendState("Placard", "html");
            string ContentStr = LoadPlugin_Placard(Str2Str(q("id")));
            Response.Write(ContentStr);//直接输出
        }
        /// <summary>
        /// 生成公告页
        /// </summary>
        /// <returns></returns>
        private string LoadPlugin_Placard(string id)
        {
            string PageStr = string.Empty;
            PageStr = JumboTCMS.Utils.DirFile.ReadFile("~/templates/extends_placard_index.htm");
            ReplaceSiteTags(ref PageStr);
            doh.Reset();
            doh.SqlCmd = "SELECT [Id],[Title],[Content],[AddTime] FROM [jcms_extends_placard] WHERE [Id]=" + id;
            DataTable dtPlacard = doh.GetDataTable();
            if (dtPlacard.Rows.Count > 0)
            {
                for (int i = 0; i < dtPlacard.Columns.Count; i++)
                {
                    PageStr = PageStr.Replace("{$Placard" + dtPlacard.Columns[i].ColumnName + "}", dtPlacard.Rows[0][i].ToString());
                }
                PageStr = PageStr.Replace("{$PlacardAddDate}", Convert.ToDateTime(dtPlacard.Rows[0]["AddTime"].ToString()).ToShortDateString());

            }
            else
                return "参数错误,没有您想查找的公告内容";
            dtPlacard.Clear();
            dtPlacard.Dispose();
            return PageStr;

        }
    }
}
