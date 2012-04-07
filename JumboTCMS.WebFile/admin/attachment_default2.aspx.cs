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
namespace JumboTCMS.WebForm.Admin.Attachment
{
    public partial class _default2 : JumboTCMS.UI.AdminCenter
    {
        public string UploadFileType;
        public int UploadFileSizeLimit = 0;
        public string UploadPage;
        public string Args;
        protected void Page_Load(object sender, EventArgs e)
        {
            ChannelId = Str2Str(q("ccid"));
            Admin_Load("", "html", true);

            //以下是通过flash将验证信息发送到地址栏
            //注意：Flash上传接收页在非IE的浏览器下获取不到Session和Cookies
            doh.Reset();
            doh.ConditionExpress = "adminid=@adminid";
            doh.AddConditionParameter("@adminid", AdminId);
            doh.AddFieldItem("AdminSign", AdminPass);
            doh.Update("jcms_normal_user");
            UploadPage = site.Dir + "admin/attachment_upfile.aspx?adminsign=" + AdminPass + "&adminid=" + AdminId + "&ccid=" + ChannelId;
            UploadFileSizeLimit = ChannelUploadSize;
            UploadFileType = ChannelUploadType;
        }
    }
}
