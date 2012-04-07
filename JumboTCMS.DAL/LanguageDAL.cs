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
using System.Collections.Generic;
using System.Data;
using System.Web;
using JumboTCMS.Common;
using JumboTCMS.DBUtility;
using JumboTCMS.Entity;
using JumboTCMS.Utils;
using Newtonsoft.Json;
namespace JumboTCMS.DAL
{
    /// <summary>
    /// 语言包
    /// </summary>
    public class LanguageDAL
    {
        public LanguageDAL()
        { }
        /// <summary>
        /// 绑定语言包(V6之后深入开发)
        /// </summary>
        /// <param name="_lng">如cn表示中文，en表示英文</param>
        /// <returns></returns>
        public Language GetEntity(string _lng)
        {
            string json = JumboTCMS.Utils.DirFile.ReadFile("~/_data/languages/" + _lng + ".js");
            json = JumboTCMS.Utils.Strings.GetHtml(json, "//<!--语言包begin", "//-->语言包end");
            Language lng = (Language)JavaScriptConvert.DeserializeObject(json, typeof(Language));
            return lng;
        }
    }
}
