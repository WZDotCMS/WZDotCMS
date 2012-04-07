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
using JumboTCMS.Utils;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _createhtmlajax : JumboTCMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            ChannelId = Str2Str(q("ccid"));
            Admin_Load(ChannelId + "-08", "json", true);
            if (q("oper") == "createchannel")
            {
                JumboTCMS.DAL.TemplateEngineDAL teDAL = new JumboTCMS.DAL.TemplateEngineDAL(ChannelId);
                teDAL.CreateChannelFile();
                teDAL.CreateDefaultFile();
                this._response = JsonResult(1, "频道及站点首页更新完成");
            }
            if (q("oper") == "createbyclass")
            {
                if (q("act") == "class")
                {
                    doh.Reset();
                    doh.SqlCmd = "SELECT Id FROM [jcms_normal_class] WHERE [IsOut]=0 AND [ChannelId]=" + ChannelId;
                    doh.SqlCmd += " and [Id] in (" + q("classid") + ")";
                    doh.SqlCmd += " ORDER BY code";
                    DataTable dtClass = doh.GetDataTable();
                    MakeClass(dtClass);
                    dtClass.Clear();
                    dtClass.Dispose();
                    this._response = "{result :\"1\",returnval :\"success\"}";
                }
                if (q("act") == "content")
                {
                    doh.Reset();
                    doh.SqlCmd = "SELECT id FROM [jcms_module_" + ChannelType + "] WHERE [ChannelId]=" + ChannelId + " and [IsPass]=1";
                    doh.SqlCmd += " and [ClassId] in (" + q("classid") + ")";
                    DataTable dtContent = doh.GetDataTable();
                    MakeView(dtContent);
                    dtContent.Clear();
                    dtContent.Dispose();
                    this._response = "{result :\"1\",returnval :\"success\"}";
                }
            }
            if (q("oper") == "createbyid")
            {
                string Sid = Str2Str(q("id1"));
                string Eid = Str2Str(q("id2"));
                string wSql = string.Empty;
                doh.Reset();
                doh.SqlCmd = "SELECT id FROM [jcms_module_" + ChannelType + "] WHERE [ChannelId]=" + ChannelId + " and [IsPass]=1";
                if (Sid != "0")
                {
                    if (Eid == "0")
                        wSql = " And id>=" + Sid;
                    else
                        wSql = " And id between " + Sid + " and " + Eid;
                }
                else
                    return;
                doh.SqlCmd += wSql;
                DataTable dtContent = doh.GetDataTable();
                MakeView(dtContent);
                dtContent.Clear();
                dtContent.Dispose();
                this._response = "{result :\"1\",returnval :\"success\"}";
            }
            Response.Write(this._response);
        }
        /// <summary>
        /// 生成栏目页
        /// </summary>
        private void MakeClass(DataTable dt)
        {
            int total = dt.Rows.Count;
            if (total > 0)
            {
                for (int i = 0; i < total; i++)
                {
                    CreateClassFile(MainChannel, dt.Rows[i]["Id"].ToString(), false);
                }
            }
        }
        /// <summary>
        /// 生成内容页
        /// </summary>
        private void MakeView(DataTable dt)
        {
            string ContentId = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ContentId = dt.Rows[i]["Id"].ToString();
                CreateContentFile(MainChannel, ContentId, -1);
            }
        }
    }
}
