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
using System.IO;
using System.Text;
using JumboTCMS.Utils;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _useroauth_ajax : JumboTCMS.UI.AdminCenter
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
                case "ajaxBatchOper":
                    ajaxBatchOper();
                    break;
                case "move":
                    ajaxMove();
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
        private void ajaxGetList()
        {
            int page = Int_ThisPage();
            int PSize = Str2Int(q("pagesize"), 20);
            string whereStr = (q("enabled") != "1") ? "" : "[Enabled]=1";
            string jsonStr = "";
            new JumboTCMS.DAL.Normal_UserOAuthDAL().GetListJSON(page, PSize, whereStr, ref jsonStr);
            this._response = jsonStr;
        }
        private void ajaxMove()
        {
            string _err = "";
            if (new JumboTCMS.DAL.Normal_UserOAuthDAL().Move(f("id"), (f("up") == "1"), ref _err))
                this._response = JsonResult(1, "成功移动");
            else
                this._response = JsonResult(0, _err);
        }
        /// <summary>
        /// 执行批量操作
        /// </summary>
        /// <param name="oper"></param>
        /// <param name="ids"></param>
        private void ajaxBatchOper()
        {
            if (new JumboTCMS.DAL.Normal_UserOAuthDAL().BatchOper(q("act"), f("ids")))
                this._response = JsonResult(1, "操作成功");
            else
                this._response = JsonResult(0, "操作失败");
        }
        private void ajaxUpdateFore()
        {
            CreateOAuth();
            this._response = JsonResult(1, "更新完成");
        }
        /// <summary>
        /// 生成内容json
        /// </summary>
        private void CreateOAuth()
        {
            string TempStr = "var ___JSON_OAuths =  /*请勿手动修改*/\r\n{";
            doh.Reset();
            doh.SqlCmd = "SELECT * FROM [jcms_normal_user_oauth] ORDER BY pId";
            DataTable dt = doh.GetDataTable();
            TempStr += "recordcount: " + dt.Rows.Count + ", table: [";
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i > 0) TempStr += ",";
                    TempStr += "{title: '" + dt.Rows[i]["Title"].ToString() + "', " +
                        "code: '" + dt.Rows[i]["Code"].ToString().ToLower() + "'," +
                        "enabled: " + dt.Rows[i]["Enabled"].ToString().ToLower() +
                        "}";
                }
            }
            TempStr += "]";
            TempStr += "}";
            dt.Clear();
            dt.Dispose();
            string _globalJS = JumboTCMS.Utils.DirFile.ReadFile("~/_data/jcmsV5.js");
            string _strBegin = "//<!--第三方登录begin";
            string _strEnd = "//-->第三方登录end";
            System.Collections.ArrayList TagArray = JumboTCMS.Utils.Strings.GetHtmls(_globalJS, _strBegin, _strEnd, true, true);
            if (TagArray.Count > 0)//标签存在
            {
                _globalJS = _globalJS.Replace(TagArray[0].ToString(), _strBegin + "\r\n\r\n" + TempStr + "\r\n\r\n" + _strEnd);
            }
            JumboTCMS.Utils.DirFile.SaveFile(_globalJS, "~/_data/jcmsV5.js");
        }
    }
}