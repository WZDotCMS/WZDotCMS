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
using JumboTCMS.Common;

namespace JumboTCMS.WebFile.User
{
    public partial class _member_avatar : JumboTCMS.UI.UserCenter
    {
        public string ServiceUrl = string.Empty;
        public string UserSign = string.Empty;
        public string FileFilter = "*.jpg;*.bmp;*.png;";
        public string MaxSize = string.Empty;
        public string FlashVars = string.Empty;
        private string _operType = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            User_Load("", "html");
            doh.Reset();
            doh.ConditionExpress = "id=@id";
            doh.AddConditionParameter("@id", UserId);
            doh.AddFieldItem("UserSign", UserPass);
            doh.Update("jcms_normal_user");
            ServiceUrl = ServerUrl() + site.Dir + "user/ajax.aspx";
            UserSign = UserPass;
            MaxSize = "" + (1 * 1024) + "";
        }
    }
}
