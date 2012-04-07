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
namespace JumboTCMS.WebFile.Extends.QQOnline
{
    public partial class _ajax : JumboTCMS.UI.AdminCenter
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
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxGetList":
                    ajaxGetList();
                    break;
                case "ajaxBatchOper":
                    ajaxBatchOper();
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
            this._response = "{result :\"未知操作\"}";
        }
        private void ajaxCheckName()
        {
            this._response = JsonResult(1, "可以重复");
        }
        private void ajaxGetList()
        {
            Admin_Load("master", "json");
            string ccid = Str2Str(q("ccid"));
            string id = Str2Str(q("id"));
            int PSize = Str2Int(q("pagesize"), 0) == 0 ? 15 : Str2Int(q("pagesize"), 0);
            int page = Int_ThisPage();
            int countNum = 0;

            string sqlStr = "";
            string whereStr = "1=1";
            doh.Reset();
            doh.ConditionExpress = whereStr;
            countNum = doh.Count("jcms_extends_qqonline");
            sqlStr = JumboTCMS.Utils.SqlHelp.GetSql("*", "jcms_extends_qqonline", "Id", PSize, page, "desc", whereStr);
            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dt = doh.GetDataTable();
            this._response = "{result :\"1\"," +
                "returnval :\"操作成功\"," +
                "pagerbar :\"" + JumboTCMS.Utils.HtmlPager.GetPageBar(3, "js", 2, countNum, PSize, page, "javascript:ajaxList(<#page#>);") + "\"," +
                JumboTCMS.Utils.dtHelp.DT2JSON(dt) +
                "}";
            dt.Clear();
            dt.Dispose();
        }
        /// <summary>
        /// 执行批量操作
        /// </summary>
        /// <param name="oper"></param>
        /// <param name="ids"></param>
        private void ajaxBatchOper()
        {
            Admin_Load("master", "json");
            string act = q("act");
            string ids = f("ids");
            string[] idValue;
            idValue = ids.Split(',');
            string ClassId = string.Empty;
            if (act == "pass")
            {
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "id=@id";
                    doh.AddConditionParameter("@id", idValue[i]);
                    doh.AddFieldItem("State", 1);
                    doh.Update("jcms_extends_qqonline");
                }
            }
            else if (act == "nopass")
            {
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "id=@id";
                    doh.AddConditionParameter("@id", idValue[i]);
                    doh.AddFieldItem("State", 0);
                    doh.Update("jcms_extends_qqonline");
                }
            }
            else//均为删除
            {
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "id=@id";
                    doh.AddConditionParameter("@id", idValue[i]);
                    doh.Delete("jcms_extends_qqonline");
                }
            }
            this._response = JsonResult(1, "操作成功");
        }
        private void ajaxUpdateFore()
        {
            Admin_Load("master", "json");
            string strXmlFile1 = HttpContext.Current.Server.MapPath("~/_data/config/extends/qqonline.config");
            JumboTCMS.DBUtility.XmlControl XmlTool1 = new JumboTCMS.DBUtility.XmlControl(strXmlFile1);
            string SiteShowX = XmlTool1.GetText("Root/siteshowx");
            string SiteShowY = XmlTool1.GetText("Root/siteshowy");
            string SiteArea = XmlTool1.GetText("Root/sitearea");
            string SiteSkin = XmlTool1.GetText("Root/siteskin");
            XmlTool1.Dispose();
            doh.Reset();
            doh.SqlCmd = "SELECT * FROM [jcms_extends_qqonline] Where State=1 ORDER BY OrderNum Desc,Id Desc";
            DataTable dt = doh.GetDataTable();
            string _content = "var QQOnlineInfo = {siteshowx:'" + SiteShowX + "', siteshowy:'" + SiteShowY + "',sitename:'" + site.Name + "',sitearea:'" + SiteArea + "',siteskin:'" + SiteSkin + "'," + JumboTCMS.Utils.dtHelp.DT2JSON(dt) + "};\r\n";
            _content += "var online=new Array();\r\n";

            JumboTCMS.Utils.DirFile.SaveFile(_content, "~/extends/qqonline/top.js");
            var qqnum = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                qqnum += dt.Rows[i]["QQID"].ToString() + ":";
            }
            _content = "document.write('<script type=\"text/javascript\" src=\"" + site.Dir + "extends/qqonline/top.js\"></script>');\r\n";
            _content += "document.write('<script type=\"text/javascript\" src=\"http://webpresence.qq.com/getonline?Type=1&" + qqnum + "\"></script>');\r\n";
            _content += "document.write('<script type=\"text/javascript\" src=\"" + site.Dir + "extends/qqonline/bottom.js\"></script>');\r\n";
            JumboTCMS.Utils.DirFile.SaveFile(_content, "~/extends/qqonline/qqonline.js");
            dt.Clear();
            dt.Dispose();
            this._response = JsonResult(1, "更新完成");
        }
    }
}
