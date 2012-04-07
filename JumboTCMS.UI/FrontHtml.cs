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
    public class FrontHtml : BasicPage
    {
        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            CheckClientIP();
        }
        public bool CheckCookiesCode()
        {
            string _code = q("code");
            return JumboTCMS.Common.ValidateCode.CheckValidateCode(_code);
        }
        /// <summary>
        /// 解析主站的基本信息
        /// </summary>
        /// <param name="PageStr"></param>
        protected void ReplaceSiteTags(ref string PageStr)
        {
            JumboTCMS.DAL.TemplateEngineDAL teDAL = new JumboTCMS.DAL.TemplateEngineDAL("0");
            teDAL.IsHtml = site.IsHtml;
            teDAL.ReplaceSiteTags(ref PageStr);
        }
        /// <summary>
        /// 获得页面html
        /// </summary>
        /// <param name="_page"></param>
        /// <returns></returns>
        protected string LoadPageHtml(string _page)
        {
            if (!_page.StartsWith("/") && !_page.StartsWith("~/"))
                _page = "~/templates/" + _page;
            if (!JumboTCMS.Utils.DirFile.FileExists(_page + ".htm"))
                return _page + ".htm文件不存在";
            string PageStr = JumboTCMS.Utils.DirFile.ReadFile(_page + ".htm");
            return ExecuteTags(PageStr);
        }
        protected string GetContentFile(string _channelID, string _channelType, string _contentID, int _currentPage)
        {
            return JumboTCMS.DAL.ModuleCommand.GetContent(_channelType, _channelID, _contentID, _currentPage);
        }
        /// <summary>
        /// 判断插件是否已经启用
        /// </summary>
        /// <param name="ExtendName"></param>
        public void CheckExtendState(string _extendname, string _pagetype)
        {
            if (new JumboTCMS.DAL.Normal_ExtendsDAL().Running(_extendname))
                return;
            if (_pagetype != "js")
                Response.Write("插件未启动");
            else
                Response.Write("document.write('插件未启动');");
            Response.End();
        }

    }
}