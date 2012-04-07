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
using System.Web.UI.WebControls;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Admin.Cut2Thumbs
{
    public partial class _index : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ChannelId = Str2Str(q("ccid"));
            Admin_Load("", "html", true);
            string FrameName = q("fname");
            string TempPhoto = q("tphoto").Replace(site.Url, "").Split('?')[0];
            string fileExtension = "." + JumboTCMS.Utils.DirFile.GetFileExt(TempPhoto);//缩略图后缀名
            string ToWidth = q("tow");
            string ToHeight = q("toh");
            string CutType = q("type");
            if (CutType == "1")//手工裁剪
            {
                string printhtml = "";
                printhtml += "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Frameset//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd\">\r\n";
                printhtml += "<html   xmlns=\"http://www.w3.org/1999/xhtml\">\r\n";
                printhtml += "<head>\r\n";
                printhtml += "<meta   http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n";
                printhtml += "<title>图片裁剪</title>\r\n";
                printhtml += "</head>\r\n";
                printhtml += "<frameset rows=\"30,*\" cols=\"*\" framespacing=\"0\" frameborder=\"no\" border=\"0\">\r\n";
                printhtml += "<frame src=\"cut2thumbs_process.aspx?ccid=" + ChannelId + "&tphoto=" + TempPhoto + "&tow=" + ToWidth + "&toh=" + ToHeight + "\" name=\"topFrame\" id=\"topFrame\" />\r\n";
                printhtml += "<frame src=\"cut2thumbs_preview.aspx?ccid=" + ChannelId + "&tphoto=" + TempPhoto + "&tow=" + ToWidth + "&toh=" + ToHeight + "\" name=\"mainFrame\" scrolling=\"auto\" noresize=\"noresize\" id=\"mainFrame\" />\r\n";
                printhtml += "</frameset>\r\n";
                printhtml += "<noframes><body>\r\n";
                printhtml += "</body>\r\n";
                printhtml += "</noframes></html>\r\n";
                Response.Write(printhtml);
            }
            else//自动缩放
            {
                string DirectoryPath = ChannelUploadPath + DateTime.Now.ToString("yyMMdd");
                JumboTCMS.Utils.DirFile.CreateDir(DirectoryPath);

                string sFileName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_thumbs" + fileExtension;  // 文件名称
                string thumbnailPath = Server.MapPath(DirectoryPath + "/" + sFileName);        // 服务器端文件路径

                JumboTCMS.Utils.ImageHelp.LocalImage2Thumbs(Server.MapPath(TempPhoto), thumbnailPath, Convert.ToInt32(ToWidth), Convert.ToInt32(ToHeight), CutType);
                Response.Write("<script>opener.FillPhoto('" + DirectoryPath + "/" + sFileName + "');window.close();</script>");
            }
        }
    }
}
