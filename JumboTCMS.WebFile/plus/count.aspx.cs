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
    public partial class _count : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //防止网页后退--禁止缓存    
            Response.Expires = 0;
            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            Response.AddHeader("pragma", "no-cache");
            Response.CacheControl = "no-cache";
            if (HttpContext.Current.Request.UrlReferrer != null)
            {
                if (JumboTCMS.Utils.Cookie.GetValue("JCMS_count") == null)
                {
                    JumboTCMS.Utils.Cookie.SetObj("JCMS_count", 5, "ok");
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(Request.PhysicalApplicationPath + "\\_data\\log\\powered_" + DateTime.Now.ToString("yyyyMMdd") + ".txt", true, System.Text.Encoding.UTF8);
                    sw.WriteLine(System.DateTime.Now.ToString());
                    sw.WriteLine("\t使用网站：" + HttpContext.Current.Request.UrlReferrer.Host);
                    sw.WriteLine("\t浏 览 器：" + HttpContext.Current.Request.Browser.Browser + " " + HttpContext.Current.Request.Browser.Version);
                    sw.WriteLine("---------------------------------------------------------------------------------------------------");
                    sw.Close();
                    sw.Dispose();
                }
            }
            string url = "../_data/powered_by_jumbotcms.jpg";
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
    }
}
