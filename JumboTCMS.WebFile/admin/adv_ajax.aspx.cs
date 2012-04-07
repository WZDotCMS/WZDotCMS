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
    public partial class _adv_ajax : JumboTCMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckFormUrl())
            {
                Response.End();
            }

            id = Str2Str(q("id"));
            Admin_Load("", "json");
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxGetList":
                    ajaxGetList();
                    break;
                case "ajaxDelete":
                    ajaxDelete();
                    break;
                case "ajaxState":
                    ajaxState();
                    break;
                case "ajaxBatchUpdate":
                    ajaxBatchUpdate();
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
            Admin_Load("adv-mng", "json");
            string type = q("type");
            string _k = q("k");
            int page = Int_ThisPage();
            int PSize = Str2Int(q("pagesize"), 20);
            int countNum = 0;
            string sqlStr = "";
            string joinStr = "A.[AdvType]=B.Code";
            string whereStr1 = "1=1";
            string whereStr2 = "1=1";
            if (type != "")
            {
                whereStr1 += " AND A.AdvType='" + type + "'";
                whereStr2 += " AND AdvType='" + type + "'";
            }
            if (_k.Length > 0)
            {
                whereStr1 += " and A.Title LIKE '%" + _k + "%'";
                whereStr2 += " and Title LIKE '%" + _k + "%'";
            }
            doh.Reset();
            doh.ConditionExpress = whereStr2;
            countNum = doh.Count("jcms_normal_adv");
            sqlStr = JumboTCMS.Utils.SqlHelp.GetSql("A.*,B.Title as classname", "jcms_normal_adv", "jcms_normal_advclass", "id", PSize, page, "desc", joinStr, whereStr1, whereStr2);
            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dt = doh.GetDataTable();
            this._response = "{result :\"1\"," +
                "returnval :\"操作成功\"," +
                "pagerbar :\"" + JumboTCMS.Utils.HtmlPager.GetPageBar(3, "js", 2, countNum, PSize, page, "javascript:ajaxList(<#page#>);") + "\"," +
                JumboTCMS.Utils.dtHelp.DT2JSON(dt, (PSize * (page - 1))) +
                "}";
        }
        private void ajaxDelete()
        {
            Admin_Load("adv-mng", "json");
            string aId = f("id");
            doh.Reset();
            doh.ConditionExpress = "id=" + aId;
            doh.Delete("jcms_normal_adv");
            string _filename1 = "~/_data/html/more/" + aId + ".htm";
            string _filename2 = "~/_data/shtm/more/" + aId + ".htm";
            string _filename3 = "~/_data/style/more/" + aId + ".js";
            JumboTCMS.Utils.DirFile.SaveFile("<!---->", _filename1, false);
            JumboTCMS.Utils.DirFile.SaveFile("<!---->", _filename2, true);
            JumboTCMS.Utils.DirFile.SaveFile("//", _filename3, true);
            this._response = JsonResult(1, "成功删除");
        }
        private void ajaxState()
        {
            Admin_Load("adv-mng", "json");
            string aId = f("id");
            string state = Str2Str(f("state"));
            doh.Reset();
            doh.ConditionExpress = "id=" + aId;
            doh.AddFieldItem("State", state);
            doh.Update("jcms_normal_adv");
            new JumboTCMS.DAL.AdvDAL().CreateAdv(aId, state);
            this._response = JsonResult(1, "成功设置");
        }
        private void ajaxBatchUpdate()
        {
            Admin_Load("adv-mng", "json");
            if (BatchUpdateAdv())
                this._response = JsonResult(1, "成功设置");
            else
                this._response = JsonResult(0, "操作有误");
        }
    }
}