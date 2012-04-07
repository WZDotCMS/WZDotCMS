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
using JumboTCMS.Common;

namespace JumboTCMS.WebFile.Plus
{
    public partial class _javascript : JumboTCMS.UI.FrontHtml
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string _code = q("code");
            if (_code.Length != 64)
                Response.Write("参数有误");
            string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/javascript.config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            string _TemplateContent = XmlTool.GetText("Lis/Li[Code=\"" + _code + "\"]/TemplateContent");
            XmlTool.Dispose();
            JumboTCMS.DAL.TemplateEngineDAL teDAL = new JumboTCMS.DAL.TemplateEngineDAL("0");
            string fileStr = ExecuteTags(_TemplateContent);
            Response.Write(JumboTCMS.Utils.Strings.Html2Js(fileStr));
        }
    }
}
