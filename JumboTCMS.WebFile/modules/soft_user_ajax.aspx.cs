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
using System.Web.UI.WebControls;
using JumboTCMS.Utils;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Modules
{
    public partial class _Soft_user_ajax : JumboTCMS.UI.UserCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ChannelId = Str2Str(q("ccid"));
            id = Str2Str(q("id"));
            User_Load("", "json", true);
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxGetList":
                    ajaxGetList();
                    break;
                case "ajaxDel":
                    ajaxDel();
                    break;
                case "checkname":
                    ajaxCheckName();
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
            if (id == "0")
            {
                doh.Reset();
                doh.ConditionExpress = "title=@title and channelid=" + ChannelId;
                doh.AddConditionParameter("@title", q("txtTitle"));
                if (doh.Exist("jcms_module_soft"))
                    this._response = JsonResult(0, "不可添加");
                else
                    this._response = JsonResult(1, "可以添加");
            }
            else
            {
                doh.Reset();
                doh.ConditionExpress = "title=@title and id<>" + q("id") + " and channelid=" + ChannelId;
                doh.AddConditionParameter("@title", q("txtTitle"));
                if (doh.Exist("jcms_module_soft"))
                    this._response = JsonResult(0, "不可修改");
                else
                    this._response = JsonResult(1, "可以修改");
            }
        }
        private void ajaxGetList()
        {
            User_Load("", "json");
            string cid = Str2Str(q("cid"));
            string _k = q("k");
            string _f = q("f");
            string _s = q("s");
            string _p = q("p");
            string _t = q("t");
            string _d = q("d");
            int page = Int_ThisPage();
            int PSize = Str2Int(q("pagesize"), 20);

            this._response = GetContentList("soft", cid, _f, _k, _d, _s, "2", PSize, page);
        }
        private void ajaxDel()
        {
            User_Load("", "json");
            string lId = f("id");
            this._response = checkDel(lId, "soft");
        }
    }
}