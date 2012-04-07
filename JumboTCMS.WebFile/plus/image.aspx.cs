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
using System.Web;
using System.Data;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
namespace JumboTCMS.WebFile.Plus
{
    public partial class _image : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //防止网页后退--禁止缓存    
            Response.Expires = 0;
            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            Response.AddHeader("pragma", "no-cache");
            Response.CacheControl = "no-cache";
            string url = q("url");
            ShowImage(url);
        }
        public void ShowImage(string _url)
        {
            try
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile(Server.MapPath(_url));
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                img.Save(ms, JumboTCMS.Utils.ImageHelp.ImgFormat(_url));
                Response.ClearContent();
                Response.BinaryWrite(ms.ToArray());
                Response.ContentType = "image/jpeg";//指定输出格式为图形
                img.Dispose();
                Response.End();
            }
            catch (System.Exception e)
            {
                throw e;
            }
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
