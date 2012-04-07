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
using System.Text;
using System.Web;
using System.IO;
using JumboTCMS.Utils;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _channel_ajax : JumboTCMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckFormUrl())
            {
                Response.End();
            }
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
                case "ajaxDel":
                    ajaxDel();
                    break;
                case "ajaxUpdateUrlRewriter":
                    ajaxUpdateUrlRewriter();
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
            //不要加权限控制
            doh.Reset();
            doh.SqlCmd = "Select [Id],[Title],[Type],[pId],[ItemName],[Dir],[IsNav],[IsHtml],[Enabled],[SubDomain],[ClassDepth] FROM [jcms_normal_channel] ORDER BY pid";
            DataTable dt = doh.GetDataTable();
            this._response = "{result :\"1\",returnval :\"操作成功\"," + JumboTCMS.Utils.dtHelp.DT2JSON(dt) + "}";
        }
        private void ajaxMove()
        {
            Admin_Load("master", "json");
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
            string pId = doh.GetField("jcms_normal_channel", "pId").ToString();

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
            temp = doh.GetField("jcms_normal_channel", "pId").ToString();
            if (temp != "")
            {
                doh.Reset();
                doh.ConditionExpress = "pId=@pId";
                doh.AddConditionParameter("@pId", temp);
                doh.AddFieldItem("pId", "-100000");
                doh.Update("jcms_normal_channel");
                doh.Reset();
                doh.ConditionExpress = "id=@id";
                doh.AddConditionParameter("@id", id);
                doh.AddFieldItem("pId", temp);
                doh.Update("jcms_normal_channel");
                doh.Reset();
                doh.ConditionExpress = "pId=@pId";
                doh.AddConditionParameter("@pId", "-100000");
                doh.AddFieldItem("pId", pId);
                doh.Update("jcms_normal_channel");
                this._response = JsonResult(1, "成功移动");
            }
            else
                this._response = JsonResult(0, "无须移动");
        }
        private void ajaxDel()
        {
            Admin_Load("master", "json");
            string cId = f("id");
            doh.Reset();
            doh.ConditionExpress = "id=@id";
            doh.AddConditionParameter("@id", cId);
            string cType = doh.GetField("jcms_normal_channel", "Type").ToString();
            doh.Reset();
            doh.ConditionExpress = "[ChannelId]=" + cId + " and [IsPass]=1";
            int topicnum = doh.Count("jcms_module_" + cType);

            if ((topicnum != 0))
            {
                this._response = JsonResult(0, "该频道有数据或为系统频道");
                return;
            }
            doh.Reset();
            doh.ConditionExpress = "ChannelId=@id";
            doh.AddConditionParameter("@id", cId);
            doh.Delete("jcms_module_" + cType);
            doh.Reset();
            doh.ConditionExpress = "ChannelId=@id";
            doh.AddConditionParameter("@id", cId);
            doh.Delete("jcms_normal_class");
            doh.Reset();
            doh.ConditionExpress = "id=@id";
            doh.AddConditionParameter("@id", cId);
            doh.Delete("jcms_normal_channel");
            this._response = JsonResult(1, "频道成功删除");
        }

        private void ajaxUpdateUrlRewriter()
        {
            Admin_Load("master", "json");
            StringBuilder sb = new StringBuilder();
            doh.Reset();
            doh.SqlCmd = "SELECT [Id],[Dir],[SubDomain] FROM [jcms_normal_channel] WHERE len(SubDomain)>0 ORDER BY pId";
            DataTable dt = doh.GetDataTable();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string _channeldir = dt.Rows[i]["Dir"].ToString();
                string _domain = dt.Rows[i]["SubDomain"].ToString().Replace("http://", "").Replace("https://", "");
                sb.Append("  <if header=\"Host\" match=\"" + _domain + "\">\r\n");
                sb.Append("    <rewrite url=\"^/([A-Za-z0-9\\-_]*(/[A-Za-z0-9\\-_]+)*(.(aspx|html|shtm|shtml))?)$\" to=\"" + site.Dir + _channeldir + "/$1?i=1\" processing=\"stop\" />\r\n");
                sb.Append("  </if>\r\n");
            }
            dt.Clear();
            dt.Dispose();
            string TempStr = sb.ToString();
            string _ruleFile = JumboTCMS.Utils.DirFile.ReadFile("~/UrlRewriter.config");
            string _strBegin = "<!--频道二级域名begin-->";
            string _strEnd = "<!--频道二级域名end-->";
            System.Collections.ArrayList TagArray = JumboTCMS.Utils.Strings.GetHtmls(_ruleFile, _strBegin, _strEnd, true, true);
            if (TagArray.Count > 0)//标签存在
            {
                _ruleFile = _ruleFile.Replace(TagArray[0].ToString(), _strBegin + "\r\n" + TempStr + "\r\n" + _strEnd);
            }
            JumboTCMS.Utils.DirFile.SaveFile(_ruleFile, "~/UrlRewriter.config");
            this._response = JsonResult(1, "规则成功保存");
        }
        /// <summary>
        /// 执行频道的重建,审核,删除等操作
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
                    idValue[i] = Str2Str(idValue[i]);
                    doh.Reset();
                    doh.ConditionExpress = "id=@id";
                    doh.AddConditionParameter("@id", idValue[i]);
                    doh.AddFieldItem("Enabled", 1);
                    doh.Update("jcms_normal_channel");
                }
            }
            else if (act == "nopass")
            {
                for (int i = 0; i < idValue.Length; i++)
                {
                    idValue[i] = Str2Str(idValue[i]);
                    doh.Reset();
                    doh.ConditionExpress = "id=@id";
                    doh.AddConditionParameter("@id", idValue[i]);
                    doh.AddFieldItem("Enabled", 0);
                    doh.Update("jcms_normal_channel");
                }
            }
            else if (act == "nav")
            {
                for (int i = 0; i < idValue.Length; i++)
                {
                    idValue[i] = Str2Str(idValue[i]);
                    doh.Reset();
                    doh.ConditionExpress = "id=@id";
                    doh.AddConditionParameter("@id", idValue[i]);
                    doh.AddFieldItem("IsNav", 1);
                    doh.Update("jcms_normal_channel");
                }
            }
            else if (act == "nonav")
            {
                for (int i = 0; i < idValue.Length; i++)
                {
                    idValue[i] = Str2Str(idValue[i]);
                    doh.Reset();
                    doh.ConditionExpress = "id=@id";
                    doh.AddConditionParameter("@id", idValue[i]);
                    doh.AddFieldItem("IsNav", 0);
                    doh.Update("jcms_normal_channel");
                }
            }
            else if (act == "refresh")
            {
                for (int i = 0; i < idValue.Length; i++)
                {
                    idValue[i] = Str2Str(idValue[i]);
                    SetupChannelFile(idValue[i]);
                }
            }
            this._response = JsonResult(1, "操作成功");
        }
        protected void SetupChannelFile(string ccId)
        {
            doh.Reset();
            doh.SqlCmd = "SELECT [Dir],[Type] FROM [jcms_normal_channel] WHERE [Enabled]=1 and [Id]=" + ccId;
            DataTable dtChannel = doh.GetDataTable();
            for (int i = 0; i < dtChannel.Rows.Count; i++)
            {
                JumboTCMS.Utils.DirFile.CreateDir("~/" + dtChannel.Rows[i]["Dir"].ToString());
                JumboTCMS.Utils.DirFile.CreateDir("~/" + dtChannel.Rows[i]["Dir"].ToString() + "/class");
                JumboTCMS.Utils.DirFile.CreateDir("~/" + dtChannel.Rows[i]["Dir"].ToString() + "/" + dtChannel.Rows[i]["type"].ToString().ToLower());
                CopyChannelFiles(ccId);
            }
            dtChannel.Clear();
            dtChannel.Dispose();
        }
        protected void CopyChannelFiles(string ccId)
        {
            JumboTCMS.Entity.Normal_Channel _Channel = new JumboTCMS.DAL.Normal_ChannelDAL().GetEntity(ccId);
            CopyChannelFile("default", ccId, _Channel.Dir);
            CopyChannelFile("class", ccId, _Channel.Dir);
            CopyChannelFile(_Channel.Type.ToLower(), ccId, _Channel.Dir);

            if (Directory.Exists(Server.MapPath(site.Dir) + "controls\\images"))
                JumboTCMS.Utils.DirFile.CopyDir(Server.MapPath(site.Dir) + "controls\\images", Server.MapPath(site.Dir + _Channel.Dir));
            if (Directory.Exists(Server.MapPath(site.Dir) + "controls\\js"))
                JumboTCMS.Utils.DirFile.CopyDir(Server.MapPath(site.Dir) + "controls\\js", Server.MapPath(site.Dir + _Channel.Dir));
            if (Directory.Exists(Server.MapPath(site.Dir) + "controls\\uploadfiles"))
                JumboTCMS.Utils.DirFile.CopyDir(Server.MapPath(site.Dir) + "controls\\uploadfiles", Server.MapPath(site.Dir + _Channel.Dir));
        }
        protected void CopyChannelFile(string ccFileName, string ccId, string ccDir)
        {
            string TempStr = string.Empty;
            if (File.Exists(Server.MapPath(site.Dir) + "controls\\" + ccFileName + ".aspx"))
            {
                TempStr = File.ReadAllText(Server.MapPath(site.Dir) + "controls\\" + ccFileName + ".aspx");
                TempStr = TempStr.Replace("{$ChannelId}", ccId);
                JumboTCMS.Utils.DirFile.SaveFile(TempStr, "~/" + ccDir + "/" + ccFileName + ".aspx");
            }
        }
    }
}