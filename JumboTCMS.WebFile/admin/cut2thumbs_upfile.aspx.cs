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
using JumboTCMS.Common;

namespace JumboTCMS.WebFile.Admin.Cut2Thumbs
{
    public partial class _upfile : JumboTCMS.UI.AdminCenter
    {
        private string _sAdminUploadPath;
        private string _sAdminUploadType;
        private int _sAdminUploadSize = 0;
        private int _sPhotoMaxWidth = 600;
        protected void Page_Load(object sender, EventArgs e)
        {
            ChannelId = Str2Str(q("ccid"));
            Admin_Load("", "json", true);
            this._sAdminUploadPath = site.Dir + "_data/tempfiles";
            this._sAdminUploadType = "*.jpg;*.jpeg;*.bmp;*.gif;*.png;";
            this._sAdminUploadSize = 2048;
            if (this.Page.Request.Files.Count > 0)
            {
                HttpPostedFile oFile = this.Page.Request.Files[0];//得到要上传文件
                if (oFile != null && oFile.ContentLength > 0)
                {
                    if (!JumboTCMS.Utils.FileValidation.IsSecureUploadPhoto(oFile))
                    {
                        SaveVisitLog(2, 0);
                        Response.Write("不安全的图片格式，换一张吧。");
                    }
                    else
                    {
                        try
                        {
                            string fileContentType = oFile.ContentType; //文件类型
                            string fileExtension = System.IO.Path.GetExtension(oFile.FileName).ToLower(); //上传文件的扩展名
                            string F_Type = fileExtension.Substring(1, fileExtension.Length - 1);
                            if (this._sAdminUploadType.ToLower().Contains("*.*") || this._sAdminUploadType.ToLower().Contains("*." + F_Type + ";"))//检测是否为允许的上传文件类型
                            {
                                if (this._sAdminUploadSize * 1024 >= oFile.ContentLength)//检测文件大小是否超过限制
                                {
                                    string DirectoryPath;
                                    DirectoryPath = this._sAdminUploadPath + "/admin_" + AdminId;
                                    JumboTCMS.Utils.DirFile.CreateDir(this._sAdminUploadPath + "/admin_" + AdminId);

                                    string sFileName = "Temp" + fileExtension;  // 文件名称
                                    string FullPath = DirectoryPath + "/" + sFileName;        // 服务器端文件路径
                                    oFile.SaveAs(Server.MapPath(FullPath));
                                    if (JumboTCMS.Utils.FileValidation.IsSecureUpfilePhoto(Server.MapPath(FullPath)))
                                    {
                                        string[] toWidthHeight = q("ThumbsSize").Split('|');
                                        string toWidth = toWidthHeight[0];
                                        string toHeight = toWidthHeight[1];
                                        string cutType = q("CutType");
                                        System.Drawing.Image originalImage = System.Drawing.Image.FromFile(Server.MapPath(FullPath));
                                        if (originalImage.Width < Convert.ToInt32(toWidth) || originalImage.Height < Convert.ToInt32(toHeight))
                                        {
                                            Response.Write(JsonResult(0, "原图片尺寸不得小于缩略图尺寸。"));
                                            originalImage.Dispose();
                                        }
                                        else
                                        {
                                            if (originalImage.Width > _sPhotoMaxWidth)
                                            {
                                                string fileExt = fileExtension;//缩略图后缀名
                                                JumboTCMS.Utils.ImageHelp.Image2Thumbs(originalImage, Server.MapPath(FullPath + fileExt), _sPhotoMaxWidth, Convert.ToInt32(_sPhotoMaxWidth * originalImage.Height / originalImage.Width), "HW");
                                                FullPath += fileExt;
                                            }
                                            originalImage.Dispose();
                                            Response.Write(JsonResult(1, FullPath));
                                        }
                                    }
                                    else
                                    {
                                        SaveVisitLog(2, 0);
                                        Response.Write("不安全的图片格式，换一张吧。");
                                    }

                                }
                                else//文件大小超过限制
                                {
                                    Response.Write(JsonResult(0, "图片大小" + Convert.ToInt32(oFile.ContentLength / 1024) + "KB,超出限制。"));
                                }
                            }
                            else
                            {
                                Response.Write(JsonResult(0, "上传的不是图片。"));
                            }
                        }
                        catch
                        {
                            Response.Write(JsonResult(0, "程序异常，上传未成功。"));
                        }
                    }
                }
                else
                {
                    Response.Write(JsonResult(0, "请选择上传文件。"));
                }
            }
            else
                Response.Write(JsonResult(0, "上传有误。"));
        }
    }
}
