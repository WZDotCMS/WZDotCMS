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
using System.Web.UI;
using System.Web.UI.WebControls;
namespace JumboTCMS.WebFile.Search
{
    public partial class _index : JumboTCMS.UI.FrontHtml
    {
        public string Keywords, SplitWords, ChannelType, ChannelId, Mode;
        public int CurrentPage = 1, PageSize = 10;
        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 8;//脚本过期时间
            CurrentPage = Int_ThisPage();
            Keywords = q("k").Replace("<", "").Replace(">", "");
            Mode = q("mode");
            //过滤特殊字符
            //Keywords = JumboTCMS.Utils.Strings.FilterSymbol(Keywords);
            //去除多余空格
            Keywords = System.Text.RegularExpressions.Regex.Replace(Keywords, "\\s{2,}", " ");
            ChannelType = q("type");
            ChannelId = Str2Str(q("ch"));
            SplitWords = JumboTCMS.Utils.WordSpliter.GetKeyword(Keywords);//自动分词
        }
    }
}
