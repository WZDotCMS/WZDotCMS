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

namespace JumboTCMS.WebFile.Passport
{
    public partial class _logout : JumboTCMS.UI.UserCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (new JumboTCMS.DAL.Normal_UserDAL().ChkUserLogout(q("userkey")))
            {
                if(q("refer") != "")
                    FinalMessage("已清除您的登录信息", q("refer"), 0);
                else
                    FinalMessage("已清除您的登录信息", Request.ServerVariables["HTTP_REFERER"].ToString(), 0);
            }
            else
                FinalMessage("无法确定您的身份", site.Dir, 0);
        }
    }
}
