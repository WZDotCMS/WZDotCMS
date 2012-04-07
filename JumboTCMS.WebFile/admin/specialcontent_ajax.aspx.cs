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
namespace JumboTCMS.WebFile.Admin
{
    public partial class _specialcontent_ajax : JumboTCMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;
        public string sId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("master", "json");
            sId = Str2Str(q("sid"));
            if (!JumboTCMS.Utils.Validator.IsNumeric(sId))
            {
                this._response = JsonResult(0, "专题有误");
            }
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxGetList":
                    ajaxGetList();
                    break;
                case "ajaxDel":
                    ajaxDel();
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
        private void ajaxGetList()
        {
            int page = Int_ThisPage();
            int PSize = Str2Int(q("pagesize"), 20);
            string joinStr = "A.[ChannelId]=B.Id";
            string whereStr1 = "A.[sId]=" + sId;//外围条件(带A.)
            string whereStr2 = "[sId]=" + sId;//分页条件(不带A.)
            string jsonStr = "";
            new JumboTCMS.DAL.Normal_SpecialContentDAL().GetListJSON(page, PSize, joinStr, whereStr1, whereStr2, ref jsonStr);
            this._response = jsonStr;
        }
        private void ajaxDel()
        {
            string cId = f("id");
            if (new JumboTCMS.DAL.Normal_SpecialContentDAL().DeleteByID(cId))
                this._response = JsonResult(1, "删除成功");
            else
                this._response = JsonResult(0, "删除失败");
        }
    }
}