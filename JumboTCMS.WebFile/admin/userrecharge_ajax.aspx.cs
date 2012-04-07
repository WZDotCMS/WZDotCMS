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
using JumboTCMS.Utils;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _userrecharge_ajax : JumboTCMS.UI.AdminCenter
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
            string whereStr = "1=1";
            doh.Reset();
            doh.ConditionExpress = whereStr;
            string sqlStr = "";
            int _countnum = doh.Count("jcms_normal_recharge");
            sqlStr = JumboTCMS.Utils.SqlHelp.GetSql("*,(select username from [jcms_normal_user] where id =jcms_normal_recharge.userid) as username", "jcms_normal_recharge", "Id", PSize, page, "desc", whereStr);
            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dt = doh.GetDataTable();
            this._response = "{result :\"1\"," +
                "returnval :\"操作成功\"," +
                "pagerbar :\"" + JumboTCMS.Utils.HtmlPager.GetPageBar(3, "js", 2, _countnum, PSize, page, "javascript:ajaxList(<#page#>);") + "\"," +
                JumboTCMS.Utils.dtHelp.DT2JSON(dt, (PSize * (page - 1))) +
                "}";
            dt.Clear();
            dt.Dispose();
        }
    }
}