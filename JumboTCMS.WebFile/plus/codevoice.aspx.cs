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
namespace JumboTCMS.WebFile.Plus
{
    public partial class _codevoice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.ContentType = "audio/mpeg";
            Response.WriteFile("../statics/sound/begin.mp3");
            string checkCode = q("code");
            if (checkCode.Length > 0)
                for (int i = 0; i < checkCode.Length; i++)
                {
                    Response.WriteFile("../statics/sound/" + checkCode[i] + ".mp3");
                }
            Response.WriteFile("../statics/sound/end.mp3");
        }
        /// <summary>
        /// 获取querystring
        /// </summary>
        /// <param name="s">参数名</param>
        /// <returns>返回值</returns>
        public string q(string s)
        {
            if (HttpContext.Current.Request.QueryString[s] != null && HttpContext.Current.Request.QueryString[s] != "")
            {
                return JumboTCMS.Utils.Strings.SafetyStr(HttpContext.Current.Request.QueryString[s].ToString());
            }
            return string.Empty;
        }
    }
}
