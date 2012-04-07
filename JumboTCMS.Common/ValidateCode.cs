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
namespace JumboTCMS.Common
{
    /// <summary>
    /// 验证码操作
    /// </summary>
    public static class ValidateCode
    {
        /// <summary>
        /// 创建验证码
        /// </summary>
        /// <param name="_length"></param>
        /// <param name="_cover">是否覆盖老的值</param>
        public static void CreateValidateCode(int _length, bool _cover)
        {
            if (_cover)
                SaveCookie(_length);
            else
            {
                if (JumboTCMS.Utils.Cookie.GetValue("ValidateCode") == null)
                    SaveCookie(_length);
            }
        }
        public static void SaveCookie(int _length)
        {
            char[] chars = "0123456789".ToCharArray();
            JumboTCMS.Entity.Site site = (JumboTCMS.Entity.Site)System.Web.HttpContext.Current.Application["jcmsV5"];
            Random random = new Random();
            string validateCode = string.Empty;
            for (int i = 0; i < _length; i++)
                validateCode += chars[random.Next(0, chars.Length)].ToString();
            JumboTCMS.Utils.Cookie.SetObj("ValidateCode", 1, validateCode, site.CookieDomain, "/");
        }
        /// <summary>
        ///  获得验证码
        /// </summary>
        /// <param name="_code">需要判断的值</param>
        /// <param name="_init">是否初始化新的值</param>
        /// <returns></returns>
        public static string GetValidateCode(int _length, bool _init)
        {
            if (_init)//需要初始化新的cookie
                CreateValidateCode(_length, false);
            return JumboTCMS.Utils.Cookie.GetValue("ValidateCode");
        }
        /// <summary>
        /// 判断验证码,如果判断正确则生成新的验证码
        /// </summary>
        /// <param name="_code">不能是空值，否则为false</param>
        /// <returns></returns>
        public static bool CheckValidateCode(string _code)
        {
            if (_code == null || _code.Length == 0)
                return false;
            if (GetValidateCode(4, false).ToLower() == _code.ToLower())
            {
                CreateValidateCode(4, true);
                return true;
            }
            return false;
        }
    }
}
