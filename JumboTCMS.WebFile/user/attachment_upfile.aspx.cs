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
using System.IO;
using System.Data;
namespace JumboTCMS.WebFile.User
{
    public partial class _attachment_upfile : JumboTCMS.UI.UserCenter
    {
        private string _sUserUploadPath;
        private string _sUserUploadType;
        private int _sUserUploadSize = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //请勿使用Session和Cookies来判断权限
            ChannelId = Str2Str(q("ccid"));
            User_Load("ok", "html", true);
            if (q("usersign").Length != 32 || q("userid") == "")
            {
                Response.Write("验证信息有误");
                Response.End();
            }
            doh.Reset();
            doh.ConditionExpress = "id=@id and usersign=@usersign and state=1";
            doh.AddConditionParameter("@id", Str2Int(q("userid")));
            doh.AddConditionParameter("@usersign", q("usersign"));
            if (!doh.Exist("jcms_normal_user"))
            {
                Response.Write("验证信息有误");
                Response.End();
            }
            if (Request.Files.Count > 0)
            {
                HttpPostedFile oFile = Request.Files[0];//得到要上传文件
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
                            string fileExtension = System.IO.Path.GetExtension(oFile.FileName).ToLower(); //上传文件的扩展名
                            string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/upload_user.config");
                            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
                            this._sUserUploadPath = XmlTool.GetText("Module/" + ChannelType.ToLower() + "/path");
                            this._sUserUploadType = XmlTool.GetText("Module/" + ChannelType.ToLower() + "/type");
                            this._sUserUploadSize = Str2Int(XmlTool.GetText("Module/" + ChannelType.ToLower() + "/size"), 1024);
                            XmlTool.Dispose();
                            if (this._sUserUploadType.ToLower().Contains("*" + fileExtension + ";"))//检测是否为允许的上传文件类型
                            {
                                if (this._sUserUploadSize * 1024 >= oFile.ContentLength)//检测文件大小是否超过限制
                                {
                                    string DirectoryPath;
                                    DirectoryPath = site.Dir + ChannelDir + this._sUserUploadPath + "/" + DateTime.Now.ToString("yyyy-MM");
                                    JumboTCMS.Utils.DirFile.CreateDir("~/" + ChannelDir + this._sUserUploadPath + "/" + DateTime.Now.ToString("yyyy-MM"));
                                    string sFileName = DateTime.Now.ToString("yyyyMMddHHmmssffff");  //文件名称
                                    string FullPath = DirectoryPath + "/" + sFileName + fileExtension;//最终文件路径
                                    oFile.SaveAs(Server.MapPath(FullPath));
                                    if (JumboTCMS.Utils.FileValidation.IsSecureUpfilePhoto(Server.MapPath(FullPath)))
                                        Response.Write("ok|" + FullPath);
                                    else
                                    {
                                        SaveVisitLog(2, 0);
                                        Response.Write("不安全的图片格式，换一张吧。");
                                    }
                                }
                                else//文件大小超过限制
                                    Response.Write("文件大小超过限制。");
                            }
                            else //文件类型不允许上传
                                Response.Write("文件类型不允许上传。");
                        }
                        catch
                        {
                            Response.Write("程序异常，上传未成功。");
                        }
                    }
                }
                else
                    Response.Write("请选择上传文件。");
            }
            else
                Response.Write("上传有误。");
        }

    }
}
