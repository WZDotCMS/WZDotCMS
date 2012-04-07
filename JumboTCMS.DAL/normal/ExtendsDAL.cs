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
using JumboTCMS.DBUtility;

namespace JumboTCMS.DAL
{
    /// <summary>
    /// 插件表信息
    /// </summary>
    public class Normal_ExtendsDAL : Common
    {
        public Normal_ExtendsDAL()
        {
            base.SetupSystemDate();
        }
        /// <summary>
        /// 是否存在记录
        /// </summary>
        /// <param name="_wherestr">条件</param>
        /// <returns></returns>
        public bool Exists(string _wherestr)
        {

            using (DbOperHandler _doh = new Common().Doh())
            {
                int _ext = 0;
                _doh.Reset();
                _doh.ConditionExpress = _wherestr;
                if (_doh.Exist("jcms_normal_extends"))
                    _ext = 1;
                return (_ext == 1);
            }

        }
        /// <summary>
        /// 是否正在运行
        /// </summary>
        /// <param name="_extendname">插件名称</param>
        /// <returns></returns>
        public bool Running(string _extendname)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "name=@name and Enabled=1";
                _doh.AddConditionParameter("@name", _extendname);
                return (_doh.Exist("jcms_normal_extends"));
            }
        }
        /// <summary>
        /// 判断重复性(标题是否存在)
        /// </summary>
        /// <param name="_title">需要检索的标题</param>
        /// <param name="_id">除外的ID</param>
        /// <param name="_wherestr">其他条件</param>
        /// <returns></returns>
        public bool ExistTitle(string _title, string _id, string _wherestr)
        {

            using (DbOperHandler _doh = new Common().Doh())
            {
                int _ext = 0;
                _doh.Reset();
                _doh.ConditionExpress = "title=@title and id<>" + _id;
                if (_wherestr != "") _doh.ConditionExpress += " and " + _wherestr;
                _doh.AddConditionParameter("@title", _title);
                if (_doh.Exist("jcms_normal_extends"))
                    _ext = 1;
                return (_ext == 1);
            }
        }
        /// <summary>
        /// 得到列表JSON数据
        /// </summary>
        /// <param name="_thispage">当前页码</param>
        /// <param name="_pagesize">每页记录条数</param>
        /// <param name="_wherestr">搜索条件</param>
        /// <param name="_jsonstr">返回值</param>
        public void GetListJSON(int _thispage, int _pagesize, string _wherestr, ref string _jsonstr)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = _wherestr;
                string sqlStr = "";
                int _countnum = _doh.Count("jcms_normal_extends");
                sqlStr = JumboTCMS.Utils.SqlHelp.GetSql("[Id],[Title],[Name],[Author],[Info],[Type],[pId],[BaseTable],[ManageUrl],[Locked],[Enabled]", "jcms_normal_extends", "pId", _pagesize, _thispage, "asc", _wherestr);
                _doh.Reset();
                _doh.SqlCmd = sqlStr;
                DataTable dt = _doh.GetDataTable();
                _jsonstr = "{result :\"1\"," +
                    "returnval :\"操作成功\"," +
                    "pagerbar :\"" + JumboTCMS.Utils.HtmlPager.GetPageBar(3, "js", 2, _countnum, _pagesize, _thispage, "javascript:ajaxList(<#page#>);") + "\"," +
                    JumboTCMS.Utils.dtHelp.DT2JSON(dt) +
                    "}";
                dt.Clear();
                dt.Dispose();
            }
        }
        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_isup">true代表向上移动</param>
        /// <param name="_response"></param>
        /// <returns></returns>
        public bool Move(string _id, bool _isup, ref string _response)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                if (_id == "0")
                {
                    _response = "ID错误";
                    return false;
                }
                _doh.Reset();
                _doh.ConditionExpress = "id=@id";
                _doh.AddConditionParameter("@id", _id);
                string pId = _doh.GetField("jcms_normal_extends", "pId").ToString();

                string temp;
                _doh.Reset();
                if (_isup)
                {
                    _doh.ConditionExpress = "pId<@pId ORDER BY pId desc";
                    _doh.AddConditionParameter("@pId", pId);
                }
                else
                {
                    _doh.ConditionExpress = "pId>@pId ORDER BY pId";
                    _doh.AddConditionParameter("@pId", pId);
                }
                temp = _doh.GetField("jcms_normal_extends", "pId").ToString();
                if (temp == "")
                {
                    _response = "无须移动";
                    return false;
                }
                else
                {
                    _doh.Reset();
                    _doh.ConditionExpress = "pId=@pId";
                    _doh.AddConditionParameter("@pId", temp);
                    _doh.AddFieldItem("pId", "-100000");
                    _doh.Update("jcms_normal_extends");
                    _doh.Reset();
                    _doh.ConditionExpress = "id=@id";
                    _doh.AddConditionParameter("@id", _id);
                    _doh.AddFieldItem("pId", temp);
                    _doh.Update("jcms_normal_extends");
                    _doh.Reset();
                    _doh.ConditionExpress = "pId=@pId";
                    _doh.AddConditionParameter("@pId", "-100000");
                    _doh.AddFieldItem("pId", pId);
                    _doh.Update("jcms_normal_extends");

                }
                return true;
            }
        }
        /// <summary>
        /// 获得新插件列表JSON
        /// </summary>
        /// <param name="_jsonstr"></param>
        public void GetNewJSON(ref string _jsonstr)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                DirectoryInfo di = new DirectoryInfo(HttpContext.Current.Server.MapPath(site.Dir + "extends/"));
                DirectoryInfo[] directorylist = di.GetDirectories();
                string tempstr = "table:[";
                int extendcount = 0;
                string _title = "";
                string _name = "";
                string _author = "";
                string _type = "";
                foreach (DirectoryInfo dii in directorylist)
                {
                    if (!JumboTCMS.Utils.DirFile.FileExists(site.Dir + "extends/" + dii.Name + "/install.config"))
                        continue;
                    _title = JumboTCMS.Utils.XmlCOM.ReadConfig(site.Dir + "extends/" + dii.Name + "/install", "Title");
                    _name = JumboTCMS.Utils.XmlCOM.ReadConfig(site.Dir + "extends/" + dii.Name + "/install", "Name");
                    _author = JumboTCMS.Utils.XmlCOM.ReadConfig(site.Dir + "extends/" + dii.Name + "/install", "Author");
                    _type = JumboTCMS.Utils.XmlCOM.ReadConfig(site.Dir + "extends/" + dii.Name + "/install", "Type");

                    _doh.Reset();
                    _doh.ConditionExpress = "name=@name";
                    _doh.AddConditionParameter("@name", _name);
                    if (_doh.Exist("jcms_normal_extends"))
                        continue;
                    tempstr += ",{title: '" + _title + "'," +
                        "name: '" + _name + "'," +
                        "author: '" + _author + "'," +
                        "type: '" + _type + "'}";
                    extendcount++;
                }
                tempstr += "]";
                tempstr = tempstr.Replace("table:[,", "table:[");
                _jsonstr = "{result :\"1\",returnval :\"操作成功\",recordcount:" + extendcount + "," + tempstr + "}";

            }
        }
        /// <summary>
        /// 安装插件
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_response">返回信息</param>
        public bool Install(string _name, ref string _response)
        {
            if (!JumboTCMS.Utils.DirFile.FileExists(site.Dir + "extends/" + _name + "/install.config"))
            {
                _response = "插件的安装文件损坏或不存在";
                return false;
            }
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "1=1";
                int pId = _doh.MaxValue("jcms_normal_extends", "pId");
                string _Title = JumboTCMS.Utils.XmlCOM.ReadConfig(site.Dir + "extends/" + _name + "/install", "Title");
                string _Author = JumboTCMS.Utils.XmlCOM.ReadConfig(site.Dir + "extends/" + _name + "/install", "Author");
                string _Info = JumboTCMS.Utils.XmlCOM.ReadConfig(site.Dir + "extends/" + _name + "/install", "Info");
                string _Type = JumboTCMS.Utils.XmlCOM.ReadConfig(site.Dir + "extends/" + _name + "/install", "Type");
                string _BaseTable = JumboTCMS.Utils.XmlCOM.ReadConfig(site.Dir + "extends/" + _name + "/install", "BaseTable");
                string _ManageUrl = JumboTCMS.Utils.XmlCOM.ReadConfig(site.Dir + "extends/" + _name + "/install", "ManageUrl");
                int _Locked = JumboTCMS.Utils.Validator.StrToInt(JumboTCMS.Utils.XmlCOM.ReadConfig(site.Dir + "extends/" + _name + "/install", "Locked"), 0);
                if (_BaseTable != "")//需要安装数据库
                {
                    string _SqlScriptText = JumboTCMS.Utils.XmlCOM.ReadConfig(site.Dir + "extends/" + _name + "/install", "SqlScript" + base.DBType);
                    string _SqlScriptFile = site.Dir + "extends/" + _name + "/install.sql";
                    JumboTCMS.Utils.DirFile.SaveFile(_SqlScriptText, _SqlScriptFile);
                    if (!ExecuteSqlInFile(HttpContext.Current.Server.MapPath(_SqlScriptFile)))
                    {
                        _response = "数据表创建有误,可能已存在";
                        return false;
                    }
                }
                _doh.Reset();
                _doh.AddFieldItem("Title", _Title);
                _doh.AddFieldItem("Name", _name);
                _doh.AddFieldItem("Author", _Author);
                _doh.AddFieldItem("Info", _Info);
                _doh.AddFieldItem("Type", _Type);
                _doh.AddFieldItem("BaseTable", _BaseTable);
                _doh.AddFieldItem("ManageUrl", _ManageUrl);
                _doh.AddFieldItem("Locked", _Locked);
                _doh.AddFieldItem("Enabled", 0);
                _doh.AddFieldItem("pId", pId + 1);
                _doh.Insert("jcms_normal_extends");
                _response = "插件安装成功";
            }
            return true;
        }
        /// <summary>
        /// 批量操作插件
        /// </summary>
        /// <param name="_act">行为</param>
        /// <param name="_ids">id，以,隔开</param>
        public bool BatchOper(string _act, string _ids)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string[] idValue;
                idValue = _ids.Split(',');
                if (_act == "pass")
                {
                    for (int i = 0; i < idValue.Length; i++)
                    {
                        _doh.Reset();
                        _doh.ConditionExpress = "id=@id";
                        _doh.AddConditionParameter("@id", idValue[i]);
                        _doh.AddFieldItem("Enabled", 1);
                        _doh.Update("jcms_normal_extends");
                    }
                }
                else if (_act == "nopass")
                {
                    for (int i = 0; i < idValue.Length; i++)
                    {
                        _doh.Reset();
                        _doh.ConditionExpress = "id=@id";
                        _doh.AddConditionParameter("@id", idValue[i]);
                        _doh.AddFieldItem("Enabled", 0);
                        _doh.Update("jcms_normal_extends");
                    }
                }
                string TempStr = "";
                _doh.Reset();
                _doh.SqlCmd = JumboTCMS.Utils.SqlHelp.GetSql("[Title],[Name],[Type],[Enabled]", "jcms_normal_extends", "pId", 100, 1, "desc", "");
                DataTable dt = _doh.GetDataTable();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if(dt.Rows[i]["Enabled"].ToString() =="1")
                        TempStr += "\r\nvar Plugin" + dt.Rows[i]["Name"].ToString() + "	= true;//" + dt.Rows[i]["Title"].ToString() + "插件";
                    else
                        TempStr += "\r\nvar Plugin" + dt.Rows[i]["Name"].ToString() + "	= false;//" + dt.Rows[i]["Title"].ToString() + "插件";
                }
                string _globalJS = JumboTCMS.Utils.DirFile.ReadFile("~/_data/jcmsV5.js");
                string _strBegin = "//<!--插件开关begin";
                string _strEnd = "//-->插件开关end";
                System.Collections.ArrayList TagArray = JumboTCMS.Utils.Strings.GetHtmls(_globalJS, _strBegin, _strEnd, true, true);
                if (TagArray.Count > 0)//标签存在
                {
                    _globalJS = _globalJS.Replace(TagArray[0].ToString(), _strBegin + "\r\n\r\n" + TempStr + "\r\n\r\n" + _strEnd);
                }
                JumboTCMS.Utils.DirFile.SaveFile(_globalJS, "~/_data/jcmsV5.js");
            }
            return true;
        }
        /// <summary>
        /// 删除一个插件
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_err">返回错误信息</param>
        /// <returns></returns>
        public bool DeleteByID(string _id, ref string _err)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                if (_id != "" && _id != "0")
                {
                    _doh.Reset();
                    _doh.ConditionExpress = "id=@id";
                    _doh.AddConditionParameter("@id", _id);
                    object[] _values = _doh.GetFields("jcms_normal_extends", "Name,Locked,Enabled,BaseTable");
                    string eName = _values[0].ToString();
                    string eLocked = _values[1].ToString();
                    string eEnabled = _values[2].ToString();
                    string _delTables = _values[3].ToString();
                    if (eLocked == "1")
                    {
                        _err = "锁定的插件不可删";
                        return false;
                    }

                    if (eEnabled == "1")
                    {
                        _err = "先把插件禁用再卸载";
                        return false;
                    }

                    if (_delTables.Trim().Length > 0)//需要删除插件表
                    {
                        string[] _delTable = _delTables.Split(',');
                        for (int i = 0; i < _delTable.Length; i++)
                        {
                            _doh.Reset();
                            _doh.DropTable(_delTable[i]);
                        }
                    }
                    //删除插件整个目录
                    JumboTCMS.Utils.DirFile.DeleteDir(site.Dir + "extends/" + eName + "/");
                    _doh.Reset();
                    _doh.ConditionExpress = "id=@id";
                    _doh.AddConditionParameter("@id", _id);
                    _doh.Delete("jcms_normal_extends");
                }
                else
                {
                    _err = "参数错误";
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// 获得插件的单条记录实体
        /// </summary>
        /// <param name="_id"></param>
        public JumboTCMS.Entity.Normal_Extends GetEntity(string _id)
        {
            JumboTCMS.Entity.Normal_Extends extend = new JumboTCMS.Entity.Normal_Extends();
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.SqlCmd = "SELECT * FROM [jcms_normal_extends] WHERE [Id]=" + _id;
                DataTable dt = _doh.GetDataTable();
                if (dt.Rows.Count > 0)
                {
                    extend.Id = dt.Rows[0]["Id"].ToString();
                    extend.Name = dt.Rows[0]["Name"].ToString();
                    extend.Title = dt.Rows[0]["Title"].ToString();
                    extend.Author = dt.Rows[0]["Author"].ToString();
                    extend.Info = dt.Rows[0]["Info"].ToString();
                    extend.Type = Str2Int(dt.Rows[0]["Type"].ToString());
                    extend.BaseTable = dt.Rows[0]["BaseTable"].ToString();
                    extend.ManageUrl = dt.Rows[0]["ManageUrl"].ToString();
                    extend.Locked = Str2Int(dt.Rows[0]["Locked"].ToString());
                    extend.Enabled = Str2Int(dt.Rows[0]["Enabled"].ToString());
                    extend.pId = Str2Int(dt.Rows[0]["pId"].ToString());

                }
                return extend;
            }
        }
    }
}
