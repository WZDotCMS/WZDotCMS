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
namespace JumboTCMS.WebFile.Question
{
    public partial class _toplist : JumboTCMS.UI.FrontHtml
    {
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            int TopNum = (Str2Int(q("top"), 0) < 1 || Str2Int(q("top"), 0) > 20) ? 10 : Str2Int(q("top"), 0);
            int PSize = Str2Int(JumboTCMS.Utils.XmlCOM.ReadConfig(site.Dir + "_data/config/question", "PageSize"), 10);
            string classid = Str2Str(q("classid"));
            string ResponseStr = new JumboTCMS.DAL.Normal_QuestionDAL().GetTopList(TopNum, PSize, classid);
            if (q("act") == "ajax")
                Response.Write(ResponseStr);
            else
                Response.Write(JumboTCMS.Utils.Strings.Html2Js(ResponseStr));
        }
    }
}
