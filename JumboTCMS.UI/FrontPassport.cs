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
using System.Text;
namespace JumboTCMS.UI
{
    public class FrontPassport : BasicPage
    {
        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (JumboTCMS.Utils.Cookie.GetValue("passport_theme") == null)
                PassportTheme = site.PassportTheme;
            else
                PassportTheme = JumboTCMS.Utils.Cookie.GetValue("passport_theme");
        }
        public string PassportTheme = "";
        /// <summary>
        /// 判断接口是否已经启用
        /// </summary>
        /// <param name="_oauthcode"></param>
        public void CheckOAuthState(string _oauthcode)
        {
            if (new JumboTCMS.DAL.Normal_UserOAuthDAL().Running(_oauthcode))
                return;
            Response.Write("接口未启动");
            Response.End();
        }

    }
}