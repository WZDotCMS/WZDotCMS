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
using JumboTCMS.Utils;
namespace JumboTCMS.WebFile.Digg
{
    public partial class _index : JumboTCMS.UI.FrontHtml
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckFormUrl())
            {
                Response.End();
            }
            Server.ScriptTimeout = 8;//脚本过期时间
            GetDiggHTML();
            Response.Write(this._response);
        }
        private void GetDiggHTML()
        {
            string _TemplateContent = JumboTCMS.Utils.DirFile.ReadFile("~/templates/_p_digg.htm");
            JumboTCMS.TEngine.TemplateManager manager = JumboTCMS.TEngine.TemplateManager.FromString(_TemplateContent);
            JumboTCMS.Entity.Normal_Digg digg = new JumboTCMS.DAL.Normal_DiggDAL().GetDigg(q("cType"), Str2Str(q("id")));
            manager.SetValue("digg", digg);
            manager.SetValue("site", site);
            string _content = manager.Process();
            this._response = JumboTCMS.Utils.Strings.Html2Js(_content);
        }

    }
}
