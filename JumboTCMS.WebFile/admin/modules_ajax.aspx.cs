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
    public partial class _modules_ajax : JumboTCMS.UI.AdminCenter
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
                case "move":
                    ajaxMove();
                    break;
                case "ajaxDel":
                    ajaxDel();
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
            doh.Reset();
            if (q("enabled") != "1")
                doh.SqlCmd = "Select [Id],[Title],[Type],[pId],[Locked],[Enabled] FROM [jcms_normal_modules] ORDER BY pid";
            else
                doh.SqlCmd = "Select [Id],[Title],[Type],[pId],[Locked],[Enabled] FROM [jcms_normal_modules] WHERE [Enabled]=1 ORDER BY pid";
            DataTable dt = doh.GetDataTable();
            this._response = "{result :\"1\",returnval :\"操作成功\"," + JumboTCMS.Utils.dtHelp.DT2JSON(dt) + "}";
        }
        private void ajaxMove()
        {
            string id = f("id");
            string isUp = f("up");
            if (id == "0")
            {
                this._response = JsonResult(0, "ID错误");
                return;
            }
            doh.Reset();
            doh.ConditionExpress = "id=@id";
            doh.AddConditionParameter("@id", id);
            string pId = doh.GetField("jcms_normal_modules", "pId").ToString();

            string temp;
            doh.Reset();
            if (isUp == "1")
            {
                doh.ConditionExpress = "pId<@pId ORDER BY pId desc";
                doh.AddConditionParameter("@pId", pId);
            }
            else
            {
                doh.ConditionExpress = "pId>@pId ORDER BY pId";
                doh.AddConditionParameter("@pId", pId);
            }
            temp = doh.GetField("jcms_normal_modules", "pId").ToString();
            if (temp != "")
            {
                doh.Reset();
                doh.ConditionExpress = "pId=@pId";
                doh.AddConditionParameter("@pId", temp);
                doh.AddFieldItem("pId", "-100000");
                doh.Update("jcms_normal_modules");
                doh.Reset();
                doh.ConditionExpress = "id=@id";
                doh.AddConditionParameter("@id", id);
                doh.AddFieldItem("pId", temp);
                doh.Update("jcms_normal_modules");
                doh.Reset();
                doh.ConditionExpress = "pId=@pId";
                doh.AddConditionParameter("@pId", "-100000");
                doh.AddFieldItem("pId", pId);
                doh.Update("jcms_normal_modules");
                this._response = JsonResult(1, "成功移动");
            }
            else
                this._response = JsonResult(0, "无须移动");
        }
        private void ajaxDel()
        {
            string cId = f("id");
            if (cId != "")
            {
                doh.Reset();
                doh.ConditionExpress = "id=@id";
                doh.AddConditionParameter("@id", cId);
                string cType = doh.GetField("jcms_normal_modules", "Type").ToString();
                if (cType == "article")
                {
                    this._response = JsonResult(0, "该模型为核心模型");
                    return;
                }
                doh.Reset();
                doh.ConditionExpress = "[Type]='" + cType + "'";
                int channelnum = doh.Count("jcms_normal_channel");
                if (channelnum != 0)
                {
                    this._response = JsonResult(0, "该模型已被使用");
                    return;
                }
                doh.Reset();
                doh.ConditionExpress = "id=@id";
                doh.AddConditionParameter("@id", cId);
                doh.Delete("jcms_normal_modules");
                this._response = JsonResult(1, "模型成功删除");
            }
            else
                this._response = JsonResult(0, "参数错误");
        }

        private void ajaxUpdateFore()
        {
            CreateModule();
            this._response = JsonResult(1, "更新完成");
        }
        /// <summary>
        /// 生成内容json
        /// </summary>
        private void CreateModule()
        {
            string TempStr = "var ___JSON_Modules =  /*请勿手动修改*/\r\n{";
            string ModuleList = "";
            doh.Reset();
            doh.SqlCmd = "SELECT [Id],[Title],[Type],[SearchFieldValues],[SearchFieldTexts] FROM [jcms_normal_modules] WHERE [Enabled]=1 ORDER BY pId";
            DataTable dt = doh.GetDataTable();
            TempStr += "recordcount: " + dt.Rows.Count + ", table: [";
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i > 0)
                    {
                        TempStr += ",";
                        ModuleList += ",";
                    }
                    TempStr += "{" +
                        "no: " + i + ", " +
                        "id: " + dt.Rows[i]["Id"].ToString() + ", " +
                        "title: '" + dt.Rows[i]["Title"].ToString() + "', " +
                        "type: '" + dt.Rows[i]["Type"].ToString().ToLower() + "', " +
                        "fieldvalues: '" + dt.Rows[i]["SearchFieldValues"].ToString().ToLower() + "', " +
                        "fieldtexts: '" + dt.Rows[i]["SearchFieldTexts"].ToString().ToLower() + "'" +
                        "}";
                    ModuleList += dt.Rows[i]["Type"].ToString().ToLower();
                }
            }
            TempStr += "]";
            TempStr += "}";
            dt.Clear();
            dt.Dispose();
            string _globalJS = JumboTCMS.Utils.DirFile.ReadFile("~/_data/jcmsV5.js");
            string _strBegin = "//<!--模型begin";
            string _strEnd = "//-->模型end";
            System.Collections.ArrayList TagArray = JumboTCMS.Utils.Strings.GetHtmls(_globalJS, _strBegin, _strEnd, true, true);
            if (TagArray.Count > 0)//标签存在
            {
                _globalJS = _globalJS.Replace(TagArray[0].ToString(), _strBegin + "\r\n\r\n" + TempStr + "\r\n\r\n" + _strEnd);
            }
            JumboTCMS.Utils.DirFile.SaveFile(_globalJS, "~/_data/jcmsV5.js");
            JumboTCMS.Utils.XmlCOM.UpdateConfig("~/_data/config/site", "ModuleList", ModuleList);
        }
    }
}