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
    public partial class _product_admin_ajax : JumboTCMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckFormUrl())
            {
                Response.End();
            }
            ChannelId = Str2Str(q("ccid"));
            id = Str2Str(q("id"));
            Admin_Load("", "json", true);
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxGetList":
                    ajaxGetList();
                    break;
                case "ajaxBatchOper":
                    ajaxBatchOper();
                    break;
                case "ajaxDel":
                    ajaxDel();
                    break;
                case "ajaxCopy":
                    ajaxCopy();
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
                if (doh.Exist("jcms_module_product"))
                    this._response = JsonResult(0, "不可添加");
                else
                    this._response = JsonResult(1, "可以添加");
            }
            else
            {
                doh.Reset();
                doh.ConditionExpress = "title=@title and id<>" + q("id") + " and channelid=" + ChannelId;
                doh.AddConditionParameter("@title", q("txtTitle"));
                if (doh.Exist("jcms_module_product"))
                    this._response = JsonResult(0, "不可修改");
                else
                    this._response = JsonResult(1, "可以修改");
            }
        }
        private void ajaxGetList()
        {
            Admin_Load(ChannelId + "-00", "json");
            string cid = Str2Str(q("cid"));
            string _k = q("k");
            string _f = q("f");
            string _s = q("s");
            string _p = q("p");
            string _t = q("t");
            string _d = q("d");
            int page = Int_ThisPage();
            int PSize = Str2Int(q("pagesize"), 20);
            this._response = GetContentList(cid, _f, _k, _d, _s, Str2Str(q("isimg")), Str2Str(q("istop")), Str2Str(q("isfocus")), PSize, page);
       }
        private void ajaxDel()
        {
            Admin_Load(ChannelId + "-03", "json");
            string lId = f("id");
            doh.Reset();
            doh.ConditionExpress = "id=" + lId;
            doh.Delete("jcms_module_product");
            this._response = JsonResult(1, "成功删除");
        }
        private void ajaxCopy()
        {
            string sId = f("id");
            doh.Reset();
            doh.SqlCmd = "SELECT [ChannelId],[ClassId],[Title],[TColor],[Summary],[Editor],[Author],[Tags],[IsPass],[Price0],[Points],[IsImg],[Img],[IsTop],[UserId],[ReadGroup],[SourceFrom],[Content] FROM [jcms_module_product] WHERE [Id]=" + sId;
            DataTable dtContent = doh.GetDataTable();
            if (dtContent.Rows.Count > 0)
            {
                doh.Reset();
                for (int i = 0; i < dtContent.Columns.Count; i++)
                {
                    if (dtContent.Columns[i].ColumnName.ToLower() == "title")
                    {
                        string _title = dtContent.Rows[0][i].ToString().Split('_')[0];
                        doh.AddFieldItem(dtContent.Columns[i].ColumnName, _title + "_" + GetRandomNumberString(4, false));
                    }
                    else
                        doh.AddFieldItem(dtContent.Columns[i].ColumnName, dtContent.Rows[0][i].ToString());

                }
                doh.AddFieldItem("ViewNum", 0);
                doh.AddFieldItem("AddDate", DateTime.Now.ToString());
                doh.Insert("jcms_module_product");
            }
            dtContent.Clear();
            dtContent.Dispose();
            this._response = JsonResult(1, "成功克隆");
        }
        /// <summary>
        /// 执行批量操作
        /// </summary>
        /// <param name="oper"></param>
        /// <param name="ids"></param>
        private void ajaxBatchOper()
        {
            string act = q("act");
            string tocid = f("tocid");
            string ids = f("ids");
            BatchContent(act, tocid, ids, ChannelId, ChannelType, "json");
            this._response = JsonResult(1, "操作成功");
        }
    }
}