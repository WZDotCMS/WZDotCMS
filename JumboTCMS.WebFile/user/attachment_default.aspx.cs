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
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.User
{
    public partial class _attachment_default : JumboTCMS.UI.UserCenter
    {
        private string _sUserUploadPath;
        private string _sUserUploadType;
        private int _sUserUploadSize = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            ChannelId = Str2Str(q("ccid"));
            User_Load("", "html", true);
            string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/upload_user.config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            this._sUserUploadPath = XmlTool.GetText("Module/" + ChannelType.ToLower() + "/path");
            this._sUserUploadType = XmlTool.GetText("Module/" + ChannelType.ToLower() + "/type");
            this._sUserUploadSize = Str2Int(XmlTool.GetText("Module/" + ChannelType.ToLower() + "/size"), 1024);
            XmlTool.Dispose();
            string DirectoryPath;
            DirectoryPath = site.Dir + ChannelDir + this._sUserUploadPath + "/" + DateTime.Now.ToString("yyyy-MM");
            JumboTCMS.Utils.DirFile.CreateDir("~/" + ChannelDir + this._sUserUploadPath + "/" + DateTime.Now.ToString("yyyy-MM"));
            string sFileName = DateTime.Now.ToString("yyyyMMddHHmmssffff");  // 文件名称
            //以下是通过flash将验证信息发送到地址栏
            //注意：Flash上传接收页在非IE的浏览器下获取不到Session和Cookies
            doh.Reset();
            doh.ConditionExpress = "id=@id";
            doh.AddConditionParameter("@id", UserId);
            doh.AddFieldItem("UserSign", UserPass);
            doh.Update("jcms_normal_user");
            this.flashUpload.UploadPage = "attachment_upfile.aspx";
            this.flashUpload.Args = "usersign=" + UserPass + ";userid=" + UserId + ";ccid=" + ChannelId;
            this.flashUpload.UploadFileSizeLimit = this._sUserUploadSize * 1024;
            this.flashUpload.FileTypeDescription = this._sUserUploadType;
        }
    }
}
