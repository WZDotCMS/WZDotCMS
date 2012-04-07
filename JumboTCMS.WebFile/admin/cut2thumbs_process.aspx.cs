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
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using JumboTCMS.Utils;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Admin.Cut2Thumbs
{
    public partial class _process : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ChannelId = Str2Str(q("ccid"));
            Admin_Load("", "html", true);
            if (!Page.IsPostBack)
            {
                string TempPhoto = q("tphoto").Replace(site.Url, "");
                string ToWidth = q("tow");
                string ToHeight = q("toh");
                this.w.Text = this.tow.Value = ToWidth;
                this.h.Text = this.toh.Value = ToHeight;
                this.PhotoUrl.Value = TempPhoto;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int tow, toh, x, y, w, h;
            string file;
            tow = Convert.ToInt16(this.tow.Value.ToString());
            toh = Convert.ToInt16(this.toh.Value.ToString());
            x = Convert.ToInt16(this.x.Text);
            y = Convert.ToInt16(this.y.Text);
            w = Convert.ToInt16(this.w.Text);
            h = Convert.ToInt16(this.h.Text);

            file = Server.MapPath(this.PhotoUrl.Value.ToString());
            string fileExtension = "." + JumboTCMS.Utils.DirFile.GetFileExt(this.PhotoUrl.Value.ToString());//缩略图后缀名
            string DirectoryPath = ChannelUploadPath + DateTime.Now.ToString("yyMMdd");
            JumboTCMS.Utils.DirFile.CreateDir(DirectoryPath);

            string sFileName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_thumbs" + fileExtension;  // 文件名称
            string thumbnailPath = Server.MapPath(DirectoryPath + "/" + sFileName);        // 服务器端文件路径

            JumboTCMS.Utils.ImageHelp.MakeMyThumbs(file, thumbnailPath, tow, toh, x, y, w, h);
            WriteJs("-1", "parent.opener.FillPhoto('" + DirectoryPath + "/" + sFileName + "');parent.close();");
        }

    }
}
