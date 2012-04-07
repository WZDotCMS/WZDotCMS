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
    public partial class _class_ajax : JumboTCMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ChannelId = Str2Str(q("ccid"));
            id = Str2Str(q("id"));
            Admin_Load("", "json", true);
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxGetList":
                    ajaxGetList();
                    break;
                case "ajaxDel":
                    ajaxDel();
                    break;
                case "ajaxTreeJson":
                    ajaxTreeJson();
                    break;
                case "move":
                    ajaxMove();
                    break;
                case "checkname":
                    ajaxCheckName();
                    break;
                case "createnav":
                    ajaxCreateNav();
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
            this._response = JsonResult(1, "可以重复");
        }
        private void ajaxGetList()
        {
            Admin_Load(ChannelId + "-07", "json");
            doh.Reset();
            doh.SqlCmd = "SELECT [ID],[Title],[Code],[PageSize],[IsOut],[SortRank],len(Code) as codelength,[ChannelId],[IsTop],[topicnum],(select title from [jcms_normal_template] where id=[jcms_normal_class].TemplateId) as templatename,(select title from [jcms_normal_template] where id=[jcms_normal_class].ContentTemp) as ContentTempName FROM [jcms_normal_class] WHERE [ChannelId]=" + ChannelId + " ORDER BY code";
            DataTable dt = doh.GetDataTable();
            this._response = "{result :\"1\",returnval :\"操作成功\"," + JumboTCMS.Utils.dtHelp.DT2JSON(dt) + "}";
        }
        private void ajaxDel()
        {
            Admin_Load(ChannelId + "-07", "json");
            string cId = f("id");
            string isOut = f("isout");
            doh.Reset();
            doh.ConditionExpress = "id=" + cId;
            string cCode = doh.GetField("jcms_normal_class", "code").ToString();
            doh.Reset();
            doh.ConditionExpress = " [ChannelId]=" + ChannelId + " AND [code] LIKE '" + cCode + "%' and len(code)>" + cCode.Length;
            bool isDel = doh.Exist("jcms_normal_class");
            if (isDel)
            {
                this._response = JsonResult(0, "含有子栏,不可删");
                return;
            }
            if (ChannelType.ToLower() != "system")
            {
                doh.Reset();
                doh.ConditionExpress = "[ClassId]=" + cId;
                isDel = doh.Exist("jcms_module_" + ChannelType);
                if (isDel)
                {
                    this._response = JsonResult(0, "含有内容,不可删");
                    return;
                }
            }
            if (isOut != "1") DeleteClassFile(cId, true);//删除静态文件
            doh.Reset();
            doh.ConditionExpress = "id=" + cId;
            doh.Delete("jcms_normal_class");
            this._response = JsonResult(1, "成功删除");
        }
        /// <summary>
        /// 树形结构的JSON
        /// </summary>
        private void ajaxTreeJson()
        {
            if (Str2Str(f("id")) == "0")
                this._response = getJson("0", ChannelId, false);
            else if (Str2Str(f("id")) == "-1")
                this._response = getJson("0", ChannelId, true);
            else
                this._response = getJson(Str2Str(f("id")), ChannelId, true);
        }
        // bool child(是否是子节点， true为根节点，false为子节点)
        private string getJson(string id, string ccid, bool child)
        {
            string json = "";
            if (!child)
            {
                json = "[{";
                json += string.Format("\"id\": \"-1\", \"text\": \"{0}\", \"value\": \"0\", \"showcheck\": true, \"isexpand\": true, \"checkstate\": 0, \"hasChildren\": true, \"ChildNodes\":{1}, complete: false",
                    ChannelName, getJson("-1", ccid, true));
                json += "}]";
            }
            else
            {
                if (id == "-1") id = "0";
                doh.Reset();
                doh.SqlCmd = string.Format("Select [ID],[Title] FROM [jcms_normal_class] WHERE [ParentId]={0} AND [ChannelId]={1} Order by code", id, ccid);
                DataTable dt = doh.GetDataTable();
                if (dt.Rows.Count == 0)
                    json = "[]";
                else
                {
                    json = "[";
                    foreach (DataRow item in dt.Rows)
                    {
                        json += "{";
                        doh.Reset();
                        doh.ConditionExpress = string.Format("[ParentId]={0} AND [ChannelId]={1}", item["id"].ToString(), ccid);
                        bool isChild = doh.Exist("jcms_normal_class");
                        if (isChild)
                        {
                            json += string.Format("\"id\": \"{0}\", \"text\": \"{1}(id:{2})\", \"value\": \"{2}\", \"showcheck\": true, \"isexpand\": true, \"checkstate\": 0, \"hasChildren\": true, \"ChildNodes\":{3}, complete: false",
                                item["id"].ToString(), item["Title"].ToString(), item["id"].ToString(), getJson(item["id"].ToString(), ccid, isChild));
                        }
                        else
                            json += string.Format("\"id\": \"{0}\", \"text\": \"{1}(id:{2})\", \"value\": \"{2}\", \"showcheck\": true, \"isexpand\": false, \"checkstate\": 0, \"hasChildren\": false, \"ChildNodes\": null, \"complete\": false",
                                item["id"].ToString(), item["Title"].ToString(), item["id"].ToString());
                        json += "},";
                    }
                    json = json.Substring(0, json.Length - 1);
                    json += "]";
                }
            }
            return json;
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
            doh.ConditionExpress = " [ChannelId]=" + ChannelId + " AND id=" + id;
            string oldCode = doh.GetField("jcms_normal_class", "code").ToString();
            int codeLen = oldCode.Length;
            string subStr = DBType == "0" ? "mid" : "substring";
            if (codeLen > 1)
            {
                string temp = string.Empty;
                string wStr = "";
                string newStr = "";
                for (int i = 0; i < codeLen; i++)
                    newStr += "-";
                if (codeLen > 4)
                    wStr = " and left(code," + Convert.ToString(codeLen - 4) + ")='" + oldCode.Substring(0, codeLen - 4) + "'";

                if (isUp == "1")
                    wStr = "SELECT TOP 1 code FROM [jcms_normal_class] WHERE [ChannelId]=" + ChannelId + " AND len(code)=" + codeLen.ToString() + " and code<'" + oldCode + "'" + wStr + " ORDER BY code desc";
                else
                    wStr = "SELECT TOP 1 code FROM [jcms_normal_class] WHERE [ChannelId]=" + ChannelId + " AND len(code)=" + codeLen.ToString() + " and code>'" + oldCode + "'" + wStr + " ORDER BY code asc";
                doh.Reset();
                doh.SqlCmd = wStr;
                DataTable dtClass = doh.GetDataTable();
                if (dtClass.Rows.Count > 0)
                    temp = dtClass.Rows[0]["code"].ToString();

                if (temp.Length > 1)
                {
                    //Move Under Class
                    wStr = "UPDATE [jcms_normal_class] SET [code]='" + newStr + "'+" + subStr + "(code," + Convert.ToString(codeLen + 1) + ",len(code)) where [ChannelId]=" + ChannelId + " AND left(code," + codeLen.ToString() + ")='" + temp + "'";
                    doh.Reset();
                    doh.SqlCmd = wStr;
                    doh.ExecuteSqlNonQuery();

                    //Update Target Class
                    wStr = "UPDATE [jcms_normal_class] SET [code]='" + temp + "'+" + subStr + "(code," + Convert.ToString(codeLen + 1) + ",len(code)) where [ChannelId]=" + ChannelId + " AND left(code," + codeLen.ToString() + ")='" + oldCode + "'";
                    doh.Reset();
                    doh.SqlCmd = wStr;
                    doh.ExecuteSqlNonQuery();

                    //Update Under Class
                    wStr = "UPDATE [jcms_normal_class] SET [code]='" + oldCode + "'+" + subStr + "(code," + Convert.ToString(newStr.Length + 1) + ",len(code)) where [ChannelId]=" + ChannelId + " AND left(code," + newStr.Length.ToString() + ")='" + newStr + "'";
                    doh.Reset();
                    doh.SqlCmd = wStr;
                    doh.ExecuteSqlNonQuery();

                }
                dtClass.Clear();
                dtClass.Dispose();
            }
            this._response = JsonResult(1, "成功移动");
        }
        private void ajaxCreateNav()
        {
            CreateNavigate(ChannelId);
            this._response = JsonResult(1, "栏目导航更新完成");
        }
    }
}