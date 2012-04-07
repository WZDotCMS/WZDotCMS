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
using System.Web.UI.WebControls;
using JumboTCMS.Utils;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _special_ajax : JumboTCMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckFormUrl())
            {
                Response.End();
            }
            Admin_Load("master", "json");
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxGetList":
                    ajaxGetList();
                    break;
                case "ajaxDel":
                    ajaxDel();
                    break;
                case "ajaxCreateSpecial":
                    ajaxCreateSpecial();
                    break;
                case "checkname":
                    ajaxCheckName();
                    break;
                case "updatefore":
                    ajaxUpdateFore();
                    break;
                default:
                    DefaultResponse();
                    break;
            }
            Response.Write(this._response);
        }

        private void DefaultResponse()
        {
            this._response = JsonResult(0, "未知操作");
        }
        private void ajaxCheckName()
        {
            JumboTCMS.DAL.Normal_SpecialDAL dal = new JumboTCMS.DAL.Normal_SpecialDAL();
            if (dal.ExistTitle(q("txtTitle"), q("id"), ""))
                this._response = JsonResult(0, "不可添加");
            else
                this._response = JsonResult(1, "可以添加");
        }
        private void ajaxGetList()
        {
            int page = Int_ThisPage();
            int PSize = Str2Int(q("pagesize"), 20);
            string whereStr = "1=1";
            string jsonStr = "";
            new JumboTCMS.DAL.Normal_SpecialDAL().GetListJSON(page, PSize, whereStr, ref jsonStr);
            this._response = jsonStr;
        }
        private void ajaxDel()
        {
            string sId = f("id");
            if (new JumboTCMS.DAL.Normal_SpecialDAL().DeleteByID(sId))
                this._response = JsonResult(1, "删除成功");
            else
                this._response = JsonResult(0, "删除失败");
        }
        private void ajaxCreateSpecial()
        {
            string sId = f("id");
            JumboTCMS.Entity.Normal_Special eSpecial = new JumboTCMS.DAL.Normal_SpecialDAL().GetEntity(sId);
            string PageStr = JumboTCMS.Utils.DirFile.ReadFile("~/_data/special/_" + eSpecial.Source);
            JumboTCMS.DAL.TemplateEngineDAL teDAL = new JumboTCMS.DAL.TemplateEngineDAL("0");
            teDAL.IsHtml = site.IsHtml;
            teDAL.PageNav = "<a href=\"" + Go2Site(site.IsHtml) + "\" class=\"home\"></a>&nbsp;&raquo;&nbsp;专题&nbsp;&raquo;&nbsp;" + eSpecial.Title;
            teDAL.PageTitle = eSpecial.Title + " - 专题 - " + site.Name + site.TitleTail;
            teDAL.PageKeywords = site.Keywords;
            teDAL.PageDescription = JumboTCMS.Utils.Strings.SimpleLineSummary(eSpecial.Info);
            teDAL.ReplacePublicTag(ref PageStr);
            teDAL.ReplaceSpecialTag(ref PageStr, sId);
            PageStr = PageStr.Replace("<@textarea ", "<textarea ");
            PageStr = PageStr.Replace("</textarea@>", "</textarea>");
            teDAL.SaveHTML(PageStr, site.Dir + "special/" + eSpecial.Source);
            this._response = JsonResult(1, "生成成功");
        }
        private void ajaxUpdateFore()
        {
            CreateSpecialListPage();
            string _TemplateContent = JumboTCMS.Utils.DirFile.ReadFile("~/templates/_j_speciallist.htm");
            JumboTCMS.TEngine.TemplateManager manager = JumboTCMS.TEngine.TemplateManager.FromString(_TemplateContent);
            List<JumboTCMS.Entity.Normal_Special> specials = new JumboTCMS.DAL.Normal_SpecialDAL().SpecialList(10);
            manager.SetValue("specials", specials);
            manager.SetValue("site", site);
            string _content = manager.Process();
            JumboTCMS.Utils.DirFile.SaveFile(_content, "~/_data/html/j_speciallist.htm");
            JumboTCMS.Utils.DirFile.SaveFile(JumboTCMS.Utils.Strings.Html2Js(_content), "~/_data/style/j_speciallist.js");
            this._response = JsonResult(1, "更新完成");
        }
        /// <summary>
        /// 生成特约专题列表页
        /// </summary>
        private void CreateSpecialListPage()
        {
            string PageStr = JumboTCMS.Utils.DirFile.ReadFile("~/templates/speciallist_index.htm");
            JumboTCMS.DAL.TemplateEngineDAL teDAL = new JumboTCMS.DAL.TemplateEngineDAL("0");
            teDAL.IsHtml = site.IsHtml;
            teDAL.PageNav = "<a href=\"" + Go2Site(site.IsHtml) + "\" class=\"home\"></a>&nbsp;&raquo;&nbsp;过往专题列表";
            teDAL.PageTitle = "过往专题列表 - " + site.Name + site.TitleTail;
            teDAL.PageKeywords = site.Keywords;
            teDAL.PageDescription = site.Description;
            teDAL.ReplacePublicTag(ref PageStr);
            teDAL.SaveHTML(PageStr, site.Dir + "speciallist.htm");
        }
    }
}