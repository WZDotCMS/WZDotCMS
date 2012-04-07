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
    public partial class _adminlogsajax : JumboTCMS.UI.AdminCenter
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
                case "clear":
                    ajaxClear();
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
            string keys = q("keys");
            int mId = Str2Int(q("mId"), 0);
            int page = Int_ThisPage();
            int PSize = Str2Int(q("pagesize"), 20);
            string joinStr = "A.[AdminId]=B.[AdminId]";
            string whereStr1 = "1=1";//外围条件(带A.)
            string whereStr2 = "1=1";//分页条件(不带A.)
            if (keys.Trim().Length > 0)
            {
                whereStr1 += " and A.OperInfo LIKE '%" + keys + "%'";
                whereStr2 += " and OperInfo LIKE '%" + keys + "%'";
            }
            if (mId > 0)
            {
                whereStr1 += " and a.[AdminId]=" + mId.ToString();
                whereStr2 += " and [AdminId]=" + mId.ToString();
            }
            string jsonStr = "";
            new JumboTCMS.DAL.Normal_AdminlogsDAL().GetListJSON(page, PSize, joinStr, whereStr1, whereStr2, ref jsonStr);
            this._response = jsonStr;
        }
        private void ajaxClear()
        {
            new JumboTCMS.DAL.Normal_AdminlogsDAL().DeleteLogs();
            this._response = JsonResult(1, "成功清空");
        }
    }
}