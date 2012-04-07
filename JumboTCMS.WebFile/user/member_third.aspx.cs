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
using JumboTCMS.Common;

namespace JumboTCMS.WebFile.User
{
    public partial class _member_third : JumboTCMS.UI.UserCenter
    {
        public bool Bind_Sina = false;
        public bool Bind_Tencent = false;
        public bool Bind_Renren = false;
        public bool Bind_Baidu = false;
        public bool Bind_Kaixin = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            User_Load("", "html");
            doh.Reset();
            doh.ConditionExpress = "id=" + UserId;
            object[] value = doh.GetFields("jcms_normal_user", "Token_Sina,Token_Tencent,Token_Renren,Token_Baidu,Token_Kaixin");
            Bind_Sina = (value[0].ToString().Length > 0);
            Bind_Tencent = (value[1].ToString().Length > 0);
            Bind_Renren = (value[2].ToString().Length > 0);
            Bind_Baidu = (value[3].ToString().Length > 0);
            Bind_Kaixin = (value[4].ToString().Length > 0);
        }
    }
}
