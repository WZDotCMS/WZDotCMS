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
using JumboTCMS.Utils;
using JumboTCMS.Common;
using System.Text;
using Lucene.Net;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.QueryParsers;
using Lucene.Net.Analysis.Standard;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _other_ajax : JumboTCMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            this._operType = q("oper");
            switch (this._operType)
            {
                case "leftmenu":
                    GetLeftMenu();
                    break;
                case "login":
                    Login();
                    break;
                case "logout":
                    Logout();
                    break;
                case "chkadminpower":
                    ChkAdminPower();
                    break;
                case "ajaxClearSystemCache":
                    ajaxClearSystemCache();
                    break;
                case "ajaxCreateSystemCount":
                    ajaxCreateSystemCount();
                    break;
                case "ajaxCreateIndexPage":
                    ajaxCreateIndexPage();
                    break;
                case "ajaxCreateSearchIndex":
                    ajaxCreateSearchIndex();
                    break;
                case "ajaxChinese2Pinyin":
                    ajaxChinese2Pinyin();
                    break;
                default:
                    DefaultResponse();
                    break;
            }
            Response.Write(this._response);
        }
        private void DefaultResponse()
        {
            Admin_Load("", "json");
            this._response = JsonResult(1, "成功登录");
        }
        private void GetLeftMenu()
        {
            Admin_Load("", "json");
            int menuId = Str2Int(q("m"), 1);
            int minId = 0;
            int maxId = 0;
            string[,] menu = leftMenu();
            StringBuilder sb = new StringBuilder();
            if (menuId < publicMenu)
            {
                minId = menuId;
                maxId = menuId;
            }
            else
            {
                minId = menuId;
                maxId = menu.GetLength(0) - 1;
            }
            int menuNum = (maxId - minId + 1);
            string firstlink = "home.aspx";
            bool searchlink = true;
            sb.Append("{result:\"1\", returnval:'获取成功', recordcount:" + (maxId - minId + 1) + ", table:[");
            int NO = 0;
            for (int i = minId; i < maxId + 1; i++)
            {
                if (menu[i, 0] == null) break;
                if (NO > 0) sb.Append(",");
                NO++;
                sb.Append("{no:" + NO + ", ");
                sb.Append("title:'" + menu[i, 0].Split('$')[0] + "', ");
                sb.Append("table:[");
                for (int j = 1; j < menu.GetLength(1); j++)
                {
                    if (menu[i, j] == null) break;
                    if (searchlink)
                    {
                        firstlink = menu[i, j].Split('|')[0];
                        searchlink = false;
                    }
                    if (j > 1) sb.Append(",");
                    sb.Append("{no:" + j + ", ");
                    sb.Append("ischannel:'" + menu[i, 0].Split('$')[1] + "',");
                    sb.Append("url:'" + menu[i, j].Split('|')[0] + "',");
                    sb.Append("title:'" + menu[i, j].Split('|')[1] + "'");
                    if (menu[i, 0].Split('$')[1] == "1")
                        sb.Append(",channelid:'" + menu[i, j].Split('|')[2] + "'");
                    sb.Append("}");
                }
                sb.Append("]}");
            }
            sb.Append("],firstlink:'" + firstlink + "'}");
            this._response = sb.ToString();
        }
        private void Login()
        {
            string _name = f("name");
            string _pass = JumboTCMS.Utils.Strings.Left(f("pass"), 14);
            int _type = Str2Int(f("type"), 0);
            int iExpires = 0;
            if (_type > 0)
                iExpires = 60 * 60 * 24 * _type;//保存天数
            string _loginInfo = new JumboTCMS.DAL.AdminDAL().ChkAdminLogin(_name, _pass, iExpires);
            this._response = _loginInfo;
        }
        private void Logout()
        {
            new JumboTCMS.DAL.AdminDAL().ChkAdminLogout();
            this._response = JsonResult(1, "成功退出");
        }
        private void ChkAdminPower()
        {
            Admin_Load(q("power"), "json");
            this._response = JsonResult(1, "身份合法");
        }
        private void ajaxClearSystemCache()
        {
            Admin_Load("master", "json");
            new JumboTCMS.DAL.SiteDAL().CreateSiteFiles();
            SetupSystemDate();
            this._response = JsonResult(1, "基本参数更新完成");
        }
        private void ajaxCreateSystemCount()
        {
            Admin_Load("master", "json");
            CreateCount("0");
            this._response = JsonResult(1, "统计更新完成");
        }
        private void ajaxCreateIndexPage()
        {
            Admin_Load("", "json");
            JumboTCMS.DAL.TemplateEngineDAL teDAL = new JumboTCMS.DAL.TemplateEngineDAL("0");
            teDAL.CreateDefaultFile();
            this._response = JsonResult(1, "网站首页更新完成");
        }
        private void ajaxCreateSearchIndex()
        {
            Admin_Load("master", "json");
            string[] _type = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "ModuleList").Split(',');
            for (int i = 0; i < _type.Length; i++)
            {
                CreateSearchIndex(_type[i], Str2Int(q("create")) == 1);
            }
            this._response = JsonResult(1, "索引更新完成");
        }
        private IndexWriter CreateSearchIndex(string _type, bool _create)
        {
            string strXmlFile = Server.MapPath("~/_data/config/jcms(searchindex).config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            string _lastid = XmlTool.GetText("Module/" + _type + "/lastid");
            XmlTool.Dispose();
            string INDEX_STORE_PATH = Server.MapPath("~/_data/index/" + _type + "/");  //INDEX_STORE_PATH 为索引存储目录
            IndexWriter writer = null;
            try
            {
                if (!_create)
                {
                    try
                    {
                        writer = new IndexWriter(INDEX_STORE_PATH, new StandardAnalyzer(), false);
                    }
                    catch (Exception)
                    {
                        writer = new IndexWriter(INDEX_STORE_PATH, new StandardAnalyzer(), true);
                    }
                }
                else
                {
                    writer = new IndexWriter(INDEX_STORE_PATH, new StandardAnalyzer(), true);
                    _lastid = "0";
                }
                doh.Reset();
                doh.ConditionExpress = "[id]>" + _lastid;
                if (!doh.Exist("jcms_module_" + _type))
                    return null;
                doh.Reset();
                doh.SqlCmd = "select Id,ChannelId,ClassId,AddDate,Title,Summary,Tags,FirstPage from [jcms_module_" + _type + "] WHERE [Ispass]=1 AND [id]>" + _lastid;
                DataTable dtContent = doh.GetDataTable();
                //建立索引字段
                for (int j = 0; j < dtContent.Rows.Count; j++)
                {
                    string _url = dtContent.Rows[j]["FirstPage"].ToString();

                    Document doc = new Document();
                    Field field = new Field("id", dtContent.Rows[j]["Id"].ToString(), Field.Store.YES, Field.Index.UN_TOKENIZED);//存储，不索引
                    doc.Add(field);
                    field = new Field("channelid", dtContent.Rows[j]["channelid"].ToString(), Field.Store.YES, Field.Index.UN_TOKENIZED);//存储，不索引
                    doc.Add(field);
                    field = new Field("url", _url, Field.Store.YES, Field.Index.NO);
                    doc.Add(field);
                    field = new Field("tablename", _type, Field.Store.YES, Field.Index.TOKENIZED);//存储，索引
                    doc.Add(field);
                    field = new Field("title", dtContent.Rows[j]["title"].ToString(), Field.Store.YES, Field.Index.TOKENIZED);//存储，索引
                    doc.Add(field);
                    field = new Field("adddate", dtContent.Rows[j]["adddate"].ToString(), Field.Store.YES, Field.Index.UN_TOKENIZED);//存储，不索引
                    doc.Add(field);
                    field = new Field("summary", dtContent.Rows[j]["Summary"].ToString(), Field.Store.YES, Field.Index.TOKENIZED);//存储，索引
                    doc.Add(field);
                    field = new Field("tags", dtContent.Rows[j]["Tags"].ToString(), Field.Store.YES, Field.Index.TOKENIZED);//存储，索引
                    doc.Add(field);
                    writer.AddDocument(doc);
                }
                dtContent.Clear();
                dtContent.Dispose();
                //writer.Optimize();不要写这句，否则为覆盖
                writer.Close();
            }
            catch (Exception)
            {
            }
            doh.Reset();
            doh.ConditionExpress = "[Ispass]=1";
            int _maxid = doh.MaxValue("jcms_module_" + _type, "Id");
            strXmlFile = Server.MapPath("~/_data/config/jcms(searchindex).config");
            XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            XmlTool.Update("Module/" + _type + "/lastid", _maxid.ToString());
            XmlTool.Update("Module/" + _type + "/lasttime", System.DateTime.Now.ToString(), true);
            XmlTool.Save();
            XmlTool.Dispose();
            return writer;
        }
        private void ajaxChinese2Pinyin()
        {
            Admin_Load("", "json");
            int t = Str2Int(f("t"), 0);
            if (t == 1)
                this._response = JsonResult(1, JumboTCMS.Utils.ChineseSpell.MakeSpellCode(f("chinese"), "", SpellOptions.TranslateUnknowWordToInterrogation));
            else
                this._response = JsonResult(1, JumboTCMS.Utils.ChineseSpell.MakeSpellCode(f("chinese"), "", SpellOptions.FirstLetterOnly));
        }
    }
}
