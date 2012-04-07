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
using System.Collections.Specialized;
using System.Data;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Text;
using JumboTCMS.Common;
namespace JumboTCMS.UI
{
    /// <summary>
    /// BasicPage 的摘要说明
    /// </summary>
    public class BasicPage : JumboTCMS.DBUtility.UI.PageUI
    {
        public string Edition = "Standard";//版本类型，请勿改，开源版改了也没啥用
        public string vbCrlf = "\r\n";//换行符
        public bool NeedLicense = false;//是否需要许可证(对IP访问不限制)
        private string _dbType = "0";
        protected JumboTCMS.Entity.Site site = new JumboTCMS.Entity.Site();
        override protected void OnInit(EventArgs e)
        {
            Server.ScriptTimeout = 90;//默认脚本过期时间
            LoadJumboTCMS();
            base.OnInit(e);

        }
        public void LoadJumboTCMS()
        {
            this.ConnectDb();
            if (System.Web.HttpContext.Current.Application["jcmsV5"] == null)
            {
                SetupSystemDate();
            }
            site = (JumboTCMS.Entity.Site)System.Web.HttpContext.Current.Application["jcmsV5"];
            if (site.Url.Contains("jumbo") || site.Url.Contains("localhost") || site.Url.Contains("127.0.0.1")) Edition = "All";
        }
        /// <summary>
        /// 数据库类型,0代表Access,1代表Sql Server
        /// </summary>
        public string DBType
        {
            get { return this._dbType.ToString(); }
            set { this._dbType = value; }
        }
        public string ORDER_BY_RND()
        {
            /*Access版本的随机没Sql Server的好，凑合着用吧
             * */
            if (DBType == "0")
            {
                Random rand = new Random((int)DateTime.Now.Ticks);
                return " ORDER BY rnd(-(id+" + rand.Next(99999) + "))";
            }
            else
                return " ORDER BY newid()";
        }
        /// <summary>
        /// 连接数据库
        /// </summary>
        public override void ConnectDb()
        {
            if (doh == null)
            {
                try
                {
                    if (System.Web.HttpContext.Current.Application["jcmsV5_dbType"] == null)
                    {
                        System.Web.HttpContext.Current.Application.Lock();
                        System.Web.HttpContext.Current.Application["jcmsV5_dbType"] = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/conn", "dbType");
                        System.Web.HttpContext.Current.Application.UnLock();
                    }
                    this._dbType = System.Web.HttpContext.Current.Application["jcmsV5_dbType"].ToString();
                    if (this._dbType == "0")
                    {
                        if (System.Web.HttpContext.Current.Application["jcmsV5_dbPath"] == null)
                        {
                            string dbPath = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/conn", "dbPath");
                            System.Web.HttpContext.Current.Application.Lock();
                            System.Web.HttpContext.Current.Application["jcmsV5_dbPath"] = dbPath;
                            System.Web.HttpContext.Current.Application.UnLock();
                        }
                        doh = new JumboTCMS.DBUtility.OleDbOperHandler(HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Application["jcmsV5_dbPath"].ToString()));
                    }
                    else
                    {
                        this._dbType = "1";
                        if (System.Web.HttpContext.Current.Application["jcmsV5_dbConnStr"] == null)
                        {
                            string dbServerIP = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/conn", "dbServerIP");
                            string dbLoginName = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/conn", "dbLoginName");
                            string dbLoginPass = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/conn", "dbLoginPass");
                            string dbName = JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/conn", "dbName");
                            string dbConnStr = "Data Source=" + dbServerIP + ";Initial Catalog=" + dbName + ";User ID=" + dbLoginName + ";Password=" + dbLoginPass + ";Pooling=true";
                            System.Web.HttpContext.Current.Application.Lock();
                            System.Web.HttpContext.Current.Application["jcmsV5_dbConnStr"] = dbConnStr;
                            System.Web.HttpContext.Current.Application.UnLock();
                        }
                        System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Web.HttpContext.Current.Application["jcmsV5_dbConnStr"].ToString());
                        doh = new JumboTCMS.DBUtility.SqlDbOperHandler(conn);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void CloseDB()
        {
            if (doh != null) doh.Dispose();
        }
        /// <summary>
        /// 判断IP的合法性
        /// </summary>
        public void CheckClientIP()
        {
            JumboTCMS.DAL.Normal_ForbidipDAL dal = new JumboTCMS.DAL.Normal_ForbidipDAL();
            if (new JumboTCMS.DAL.Normal_ForbidipDAL().IPIsForbiding(Const.GetUserIp))
            {
                HttpContext.Current.Response.Redirect("~/errorip.aspx");
                HttpContext.Current.Response.End();
            }
        }
        /// <summary>
        /// 生成随机数字字符串
        /// </summary>
        /// <param name="int_NumberLength">数字长度</param>
        /// <returns></returns>
        public string GetRandomNumberString(int int_NumberLength)
        {
            return GetRandomNumberString(int_NumberLength, false);
        }
        /// <summary>
        /// 生成随机数字字符串
        /// </summary>
        /// <param name="int_NumberLength">数字长度</param>
        /// <returns></returns>
        public string GetRandomNumberString(int int_NumberLength, bool onlyNumber)
        {
            Random random = new Random();
            return GetRandomNumberString(int_NumberLength, onlyNumber, random);
        }
        /// <summary>
        /// 生成随机数字字符串
        /// </summary>
        /// <param name="int_NumberLength">数字长度</param>
        /// <returns></returns>
        public string GetRandomNumberString(int int_NumberLength, bool onlyNumber, Random random)
        {
            string strings = "123456789";
            if (!onlyNumber) strings += "abcdefghjkmnpqrstuvwxyz";
            char[] chars = strings.ToCharArray();
            string returnCode = string.Empty;
            for (int i = 0; i < int_NumberLength; i++)
                returnCode += chars[random.Next(0, chars.Length)].ToString();
            return returnCode;
        }
        /// <summary>
        /// 生成产品订单号，全站统一格式
        /// </summary>
        /// <returns></returns>
        public string GetProductOrderNum()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss") + GetRandomNumberString(4, true);
        }
        public void DownloadFile(string _filePath)
        {
            Response.Clear();
            bool success = true;
            if (_filePath.StartsWith("http://") || _filePath.StartsWith("https://") || _filePath.StartsWith("ftp://"))
                Response.Redirect(_filePath);
            else if (!JumboTCMS.Utils.DirFile.FileExists(_filePath))
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(HttpContext.Current.Server.MapPath("~/_data/log/nofile_" + DateTime.Now.ToString("yyyyMMdd") + ".log"), true, System.Text.Encoding.UTF8);
                sw.WriteLine(System.DateTime.Now.ToString());
                sw.WriteLine("\tIP 地 址：" + Const.GetUserIp);
                sw.WriteLine("\t访 问 者：" + ThisUser());
                sw.WriteLine("\t浏 览 器：" + HttpContext.Current.Request.Browser.Browser + " " + HttpContext.Current.Request.Browser.Version);
                sw.WriteLine("\t下载页面：" + ServerUrl() + Const.GetCurrentUrl);
                sw.WriteLine("\t无效文件：" + _filePath);
                sw.WriteLine("---------------------------------------------------------------------------------------------------");
                sw.Close();
                Response.Write("指定的文件不存在,请通知管理员");
            }
            else
            {
                success = JumboTCMS.Utils.DirFile.DownloadFile(Request, Response, Server.MapPath(_filePath), 1024000);
                if (!success)
                {
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(HttpContext.Current.Server.MapPath("~/_data/log/downerror_" + DateTime.Now.ToString("yyyyMMdd") + ".log"), true, System.Text.Encoding.UTF8);
                    sw.WriteLine(System.DateTime.Now.ToString());
                    sw.WriteLine("\tIP 地 址：" + Const.GetUserIp);
                    sw.WriteLine("\t访 问 者：" + ThisUser());
                    sw.WriteLine("\t浏 览 器：" + HttpContext.Current.Request.Browser.Browser + " " + HttpContext.Current.Request.Browser.Version);
                    sw.WriteLine("\t下载页面：" + ServerUrl() + Const.GetCurrentUrl);
                    sw.WriteLine("\t失败文件：" + _filePath);
                    sw.WriteLine("---------------------------------------------------------------------------------------------------");
                    sw.Close();
                    Response.Redirect(_filePath);
                }
            }
            Response.End();
        }
        /// <summary>
        /// 当前访客
        /// </summary>
        public string ThisUser()
        {
            if (JumboTCMS.Utils.Cookie.GetValue(site.CookiePrev + "user") != null)
                return JumboTCMS.Utils.Cookie.GetValue(site.CookiePrev + "user", "name");
            else
                return "游客";
        }
        /// <summary>
        /// 简单的防止站外提交表单
        /// 仿一般黑客，防不住高手
        /// </summary>
        /// <returns></returns>
        public bool CheckFormUrl()
        {
            if (q("debugkey") == site.DebugKey) return true;
            if (HttpContext.Current.Request.UrlReferrer == null)
            {
                SaveVisitLog(2, 0);
                return false;
            }
            if ((HttpContext.Current.Request.UrlReferrer.Host) != (HttpContext.Current.Request.Url.Host))
            {
                SaveVisitLog(2, 0);
                return false;
            }
            return true;
        }
        /// <summary>
        /// 处理过程完成
        /// </summary>
        /// <param name="pageMsg">页面提示信息</param>
        /// <param name="go2Url">如果倒退步数为0，就转到该地址</param>
        /// <param name="BackStep">倒退步数</param>
        protected void FinalMessage(string pageMsg, string go2Url, int BackStep)
        {
            FinalMessage(pageMsg, go2Url, BackStep, 2);
        }
        /// <summary>
        /// 处理过程完成
        /// </summary>
        /// <param name="pageMsg">页面提示信息</param>
        /// <param name="go2Url">如果倒退步数为0，就转到该地址</param>
        /// <param name="BackStep">倒退步数</param>
        /// <param name="BackStep">自动转向的秒数</param>
        protected void FinalMessage(string pageMsg, string go2Url, int BackStep, int Seconds)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>\r\n");
            sb.Append("<html xmlns='http://www.w3.org/1999/xhtml'>\r\n");
            sb.Append("<head>\r\n");
            sb.Append("<meta http-equiv='Content-Type' content='text/html; charset=utf-8' />\r\n");
            sb.Append("<head>\r\n");
            sb.Append("<title>系统提示 - " + site.Name + "</title>\r\n");
            sb.Append("<style>\r\n");
            sb.Append("body {padding:0; margin:0; }\r\n");
            sb.Append("#infoBox{padding:0; margin:0; position:absolute; top:40%; width:100%; text-align:center;}\r\n");
            sb.Append("#info{padding:0; margin:0;position:relative; top:-40%; right:0; border:0px #B4E0F7 solid; text-align:center;}\r\n");
            sb.Append("</style>\r\n");
            sb.Append("<script language=\"javascript\">\r\n");
            sb.Append("var seconds=" + Seconds + ";\r\n");
            sb.Append("for(i=1;i<=seconds;i++)\r\n");
            sb.Append("{window.setTimeout(\"update(\" + i + \")\", i * 1000);}\r\n");
            sb.Append("function update(num)\r\n");
            sb.Append("{\r\n");
            sb.Append("if(num == seconds)\r\n");
            if (BackStep > 0)
                sb.Append("{ history.go(" + (0 - BackStep) + "); }\r\n");
            else
            {
                if (go2Url != "")
                    sb.Append("{ self.location.href='" + go2Url + "'; }\r\n");
                else
                    sb.Append("{window.close();}\r\n");
            }
            sb.Append("else\r\n");
            sb.Append("{ }\r\n");
            sb.Append("}\r\n");
            sb.Append("</script>\r\n");
            sb.Append("<base target='_self' />\r\n");
            sb.Append("</head>\r\n");
            sb.Append("<body>\r\n");
            sb.Append("<div id='infoBox'>\r\n");
            sb.Append("<div id='info'>\r\n");
            sb.Append("<div style='text-align:center;margin:0 auto;width:320px;padding-top:4px;line-height:26px;height:26px;font-weight:bold;color:#2259A6;font-size:14px;border:1px #B4E0F7 solid;background:#CAEAFF;'>提示信息</div>\r\n");
            sb.Append("<div style='text-align:center;padding:20px 0 20px 0;margin:0 auto;width:320px;font-size:12px;background:#F5FBFF;border-right:1px #B4E0F7 solid;border-bottom:1px #B4E0F7 solid;border-left:1px #B4E0F7 solid;'>\r\n");
            sb.Append(pageMsg + "<br /><br />\r\n");
            if (BackStep > 0)
                sb.Append("        <a href=\"javascript:history.go(" + (0 - BackStep) + ")\">如果您的浏览器没有自动跳转，请点击这里</a>\r\n");
            else
                sb.Append("        <a href=\"" + go2Url + "\">如果您的浏览器没有自动跳转，请点击这里</a>\r\n");
            sb.Append("    </div>\r\n");
            sb.Append("</div>\r\n");
            sb.Append("</div>\r\n");
            sb.Append("</body>\r\n");
            sb.Append("</html>\r\n");
            HttpContext.Current.Response.Write(sb.ToString());
            //以下这行千万别手痒痒删掉
            HttpContext.Current.Response.End();

        }
        /// <summary>
        /// 当前页码
        /// </summary>
        public int Int_ThisPage()
        {
            int _page = Str2Int(q("page"), 0) < 1 ? 1 : Str2Int(q("page"), 0);
            return _page;
        }

        /// <summary>
        /// 执行Sql脚本文件
        /// </summary>
        /// <param name="pathToScriptFile">物理路径</param>
        /// <returns></returns>
        public bool ExecuteSqlInFile(string pathToScriptFile)
        {
            if (this._dbType == "1")
                return JumboTCMS.Utils.ExecuteSqlBlock.Go("1", System.Web.HttpContext.Current.Application["jcmsV5_dbConnStr"].ToString(), pathToScriptFile);
            else
                return JumboTCMS.Utils.ExecuteSqlBlock.Go("0", "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath(System.Web.HttpContext.Current.Application["jcmsV5_dbPath"].ToString()), pathToScriptFile);
        }
        /// <summary>
        /// 附加被选择的字段
        /// </summary>
        /// <param name="_fields">格式为[字段1],[字段2]</param>
        /// <returns></returns>
        public static string JoinFields(string _fields)
        {
            if (_fields.Trim().Length == 0)
                return "";
            else
                return "," + _fields;
        }

        /// <summary>
        /// 页面访问超时后记录日志
        /// </summary>
        /// <param name="_second">超时秒数</param>
        public void SavePageLog(int _second)
        {
            SaveVisitLog(1, _second);
        }
        /// <summary>
        /// 保存访问日志
        /// </summary>
        /// <param name="_type">1代表访问者,2代表非法</param>
        /// <param name="_second">脚本秒数</param>
        public void SaveVisitLog(int _type, int _second)
        {
            SaveVisitLog(_type, _second, "");
        }
        /// <summary>
        /// 保存访问日志
        /// </summary>
        /// <param name="_type">1代表访问者,2代表非法</param>
        /// <param name="_second">脚本秒数</param>
        /// <param name="_logfilename">自定义log保存路径</param>
        public void SaveVisitLog(int _type, int _second, string _logfilename)
        {
            if (_type == 1)
            {
                string _savefile = _logfilename == "" ? "~/_data/log/vister_" + DateTime.Now.ToString("yyyyMMdd") + ".log" : _logfilename;
                Single s = (Single)DateTime.Now.Subtract(HttpContext.Current.Timestamp).TotalSeconds;
                if (s > _second)
                {
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(HttpContext.Current.Server.MapPath(_savefile), true, System.Text.Encoding.UTF8);
                    sw.WriteLine(System.DateTime.Now.ToString());
                    sw.WriteLine("\tIP 地 址：" + Const.GetUserIp);
                    sw.WriteLine("\t访 问 者：" + ThisUser());
                    sw.WriteLine("\t浏 览 器：" + HttpContext.Current.Request.Browser.Browser + " " + HttpContext.Current.Request.Browser.Version);
                    sw.WriteLine("\t耗    时：" + ((Single)DateTime.Now.Subtract(HttpContext.Current.Timestamp).TotalSeconds).ToString("0.000") + "秒");
                    sw.WriteLine("\t地    址：" + ServerUrl() + Const.GetCurrentUrl);
                    sw.WriteLine("---------------------------------------------------------------------------------------------------");
                    sw.Close();
                    sw.Dispose();
                }
            }
            else
            {
                string _savefile = _logfilename == "" ? "~/_data/log/hacker_" + DateTime.Now.ToString("yyyyMMdd") + ".log" : _logfilename;
                System.IO.StreamWriter sw = new System.IO.StreamWriter(HttpContext.Current.Server.MapPath(_savefile), true, System.Text.Encoding.UTF8);
                sw.WriteLine(System.DateTime.Now.ToString());
                sw.WriteLine("\tIP 地 址：" + Const.GetUserIp);
                sw.WriteLine("\t访 问 者：" + ThisUser());
                sw.WriteLine("\t浏 览 器：" + HttpContext.Current.Request.Browser.Browser + " " + HttpContext.Current.Request.Browser.Version);
                sw.WriteLine("\t来    源：" + ServerUrl() + Const.GetRefererUrl);
                sw.WriteLine("\t地    址：" + ServerUrl() + Const.GetCurrentUrl);
                sw.WriteLine("---------------------------------------------------------------------------------------------------");
                sw.Close();
                sw.Dispose();
            }
        }
        /// <summary>
        /// 服务器地址
        /// </summary>
        /// <returns></returns>
        protected string ServerUrl()
        {
            if (HttpContext.Current.Request.ServerVariables["Server_Port"].ToString() == "80")
                return "http://" + HttpContext.Current.Request.Url.Host;
            else
                return "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.ServerVariables["Server_Port"].ToString();
        }
        /// <summary>
        /// 产生随机数字字符串
        /// </summary>
        /// <returns></returns>
        public string RandomStr(int Num)
        {
            int number;
            char code;
            string returnCode = String.Empty;

            Random random = new Random();

            for (int i = 0; i < Num; i++)
            {
                number = random.Next();
                code = (char)('0' + (char)(number % 10));
                returnCode += code.ToString();
            }
            return returnCode;
        }
        /// <summary>
        /// 初始化系统信息
        /// </summary>
        protected void SetupSystemDate()
        {
            site = new JumboTCMS.DAL.SiteDAL().GetEntity();
            System.Web.HttpContext.Current.Application.Lock();
            System.Web.HttpContext.Current.Application["jcmsV5"] = site;
            System.Web.HttpContext.Current.Application.UnLock();
        }
        /// <summary>
        /// 输出js
        /// </summary>
        /// <param name="sType"></param>
        /// <param name="jsContent"></param>
        protected void WriteJs(string sType, string jsContent)
        {
            if (sType == "-1")
                Page.ClientScript.RegisterStartupScript(this.GetType(), "writejs", jsContent, true);
            else
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "writejs", jsContent, true);

        }
        /// <summary>
        /// 输出json结果
        /// </summary>
        /// <param name="success">是否操作成功,0表示失败;1表示成功</param>
        /// <param name="str">输出字符串</param>
        /// <returns></returns>
        protected string JsonResult(int success, string str)
        {
            return "{result :\"" + success.ToString() + "\",returnval :\"" + str + "\"}";

        }
        /// <summary>
        /// 高光显示关键字
        /// </summary>
        /// <param name="PageStr">内容</param>
        /// <param name="keys">关键字</param>
        /// <returns></returns>
        protected string p__HighLight(string PageStr, string keys)
        {
            string[] key = keys.Split(new string[] { " " }, StringSplitOptions.None);
            for (int i = 0; i < key.Length; i++)
            {
                PageStr = PageStr.Replace(key[i].Trim(), "<font color=#C60A00>" + key[i].Trim() + "</font>");
            }
            return PageStr;
        }
        /// <summary>
        /// 替换关键字为红色
        /// </summary>
        /// <param name="pain">原始内容</param>
        /// <param name="keyword">关键字，支持多关键字</param>
        protected string HighLightKeyWord(string pain, string keys)
        {
            string _pain = pain;
            string[] key = keys.Split(new string[] { " " }, StringSplitOptions.None);
            if (key.Length < 1)
                return _pain;
            for (int i = 0; i < key.Length; i++)
            {
                System.Text.RegularExpressions.MatchCollection m = System.Text.RegularExpressions.Regex.Matches(_pain, key[i].Trim(), System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                //忽略大小写搜索字符串中的关键字
                for (int j = 0; j < m.Count; j++)//循环在匹配的子串前后插东东
                {
                    //j×31为插入html标签使pain字符串增加的长度:
                    _pain = _pain.Insert((m[j].Index + key[i].Trim().Length + j * 31), "</span>");//关键字后插入html标签
                    _pain = _pain.Insert((m[j].Index + j * 31), "<span style=\"color:red\">");//关键字前插入html标签
                }
            }
            return _pain;
        }
        /// <summary>
        /// 获得逐级缩进的栏目名
        /// </summary>
        /// <param name="sName">栏目名</param>
        /// <param name="sCode">栏目code</param>
        /// <returns>逐级缩进的栏目名</returns>
        public string getListName(string sName, string sCode)
        {
            int Level = (sCode.Length / 4 - 1);
            string sStr = "";
            if (Level > 0)
            {
                for (int i = 0; i < Level; i++)
                    sStr += "├－";
            }
            return sStr + sName;
        }

        /// <summary>
        /// 通用分页
        /// </summary>
        /// <param name="mode">支持1=simple,2=normal,3=full</param>
        /// <param name="countNum">记录数</param>
        /// <param name="currentPage">第几页</param>
        /// <param name="FieldName">参数名</param>
        /// <param name="FieldValue">参数值</param>
        /// <returns></returns>
        public string PageList(int mode, int countNum, int PSize, int currentPage, string[] FieldName, string[] FieldValue)
        {
            string Script_Name = HttpContext.Current.Request.ServerVariables["Script_Name"].ToString();
            string pString = "";
            for (int i = 0; i < FieldName.Length; i++)
            {
                pString += FieldName[i].ToString() + "=" + FieldValue[i].ToString() + "&";
            }
            string Http = Script_Name + "?" + pString + "page=<#page#>";
            return JumboTCMS.Utils.HtmlPager.GetPageBar(mode, "html", 0, countNum, PSize, currentPage, Http);
        }
        /// <summary>
        /// 智能分页
        /// </summary>
        /// <param name="mode">支持1=simple,2=normal,3=full</param>
        /// <param name="countNum">记录数</param>
        /// <param name="currentPage">第几页</param>
        /// <returns></returns>
        public string AutoPageBar(int mode, int stepNum, int countNum, int PSize, int currentPage)
        {
            string Http = GetUrlPrefix() + "<#page#>";
            return JumboTCMS.Utils.HtmlPager.GetPageBar(mode, "html", stepNum, countNum, PSize, currentPage, Http);
        }
        /// <summary>
        /// 当前地址前缀
        /// </summary>
        public string GetUrlPrefix()
        {
            HttpRequest Request = HttpContext.Current.Request;
            string strUrl;
            strUrl = HttpContext.Current.Request.ServerVariables["Url"];
            if (HttpContext.Current.Request.QueryString.Count == 0) //如果无参数
                return strUrl + "?page=";
            else
            {
                //if (JumboTCMS.Utils.Strings.Left(HttpContext.Current.Request.ServerVariables["Query_String"], 5) == "page=")//只有页参数
                if (HttpContext.Current.Request.ServerVariables["Query_String"].StartsWith("page=", StringComparison.OrdinalIgnoreCase))//只有页参数
                    return strUrl + "?page=";
                else
                {
                    string[] strUrl_left;
                    strUrl_left = HttpContext.Current.Request.ServerVariables["Query_String"].Split(new string[] { "page=" }, StringSplitOptions.None);
                    if (strUrl_left.Length == 1)//没有页参数
                        return strUrl + "?" + strUrl_left[0] + "&page=";
                    else
                        return strUrl + "?" + strUrl_left[0] + "page=";
                }

            }

        }
        /// <summary>
        /// 获得翻页Bar，适合js和html
        /// </summary>
        /// <param name="mode">支持1=simple,2=normal,3=full</param>
        /// <param name="stype"></param>
        /// <param name="countNum"></param>
        /// <param name="PSize"></param>
        /// <param name="currentPage"></param>
        /// <param name="Http"></param>
        /// <returns></returns>
        public string getPageBar(int mode, string stype, int stepNum, int countNum, int PSize, int currentPage, string HttpN)
        {
            return JumboTCMS.Utils.HtmlPager.GetPageBar(mode, stype, stepNum, countNum, PSize, currentPage, HttpN);
        }
        /// <summary>
        /// 获得翻页Bar，适合js和html
        /// </summary>
        /// <param name="mode">支持1=simple,2=normal,3=full</param>
        /// <param name="stype"></param>
        /// <param name="stepNum"></param>
        /// <param name="countNum"></param>
        /// <param name="PSize"></param>
        /// <param name="currentPage"></param>
        /// <param name="Http1"></param>
        /// <param name="HttpM"></param>
        /// <param name="HttpN"></param>
        /// <param name="limitPage"></param>
        /// <returns></returns>
        public string getPageBar(int mode, string stype, int stepNum, int countNum, int PSize, int currentPage, string Http1, string HttpM, string HttpN, int limitPage)
        {
            return JumboTCMS.Utils.HtmlPager.GetPageBar(mode, stype, stepNum, countNum, PSize, currentPage, Http1, HttpM, HttpN, limitPage);
        }

        /// <summary>
        /// 获取querystring
        /// </summary>
        /// <param name="s">参数名</param>
        /// <returns>返回值</returns>
        public string q(string s)
        {
            if (HttpContext.Current.Request.QueryString[s] != null && HttpContext.Current.Request.QueryString[s] != "")
            {
                return JumboTCMS.Utils.Strings.SafetyStr(HttpContext.Current.Request.QueryString[s].ToString());
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取post得到的参数
        /// </summary>
        /// <param name="s">参数名</param>
        /// <returns>返回值</returns>
        public string f(string s)
        {
            if (HttpContext.Current.Request.Form[s] != null && HttpContext.Current.Request.Form[s] != "")
            {
                return HttpContext.Current.Request.Form[s].ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// 返回非负整数，默认为t
        /// </summary>
        /// <param name="s">参数值</param>
        /// <returns>返回值</returns>
        public int Str2Int(string s, int t)
        {
            return JumboTCMS.Utils.Validator.StrToInt(s, t);
        }

        /// <summary>
        /// 返回非负整数，默认为0
        /// </summary>
        /// <param name="s">参数值</param>
        /// <returns>返回值</returns>
        public int Str2Int(string s)
        {
            return Str2Int(s, 0);
        }

        /// <summary>
        /// 返回非空字符串，默认为"0"
        /// </summary>
        /// <param name="s">参数值</param>
        /// <returns>返回值</returns>
        public string Str2Str(string s)
        {
            return JumboTCMS.Utils.Validator.StrToInt(s, 0).ToString();
        }
        /// <summary>
        /// 字符串长度
        /// </summary>
        protected int GetStringLen(string str)
        {
            byte[] bs = System.Text.Encoding.Default.GetBytes(str);
            return bs.Length;
        }
        /// <summary>
        /// 字符串截断
        /// </summary>
        /// <param name="str"></param>
        /// <param name="Length">以汉字计算，比如Length为100表示取200个字符，100个汉字</param>
        /// <returns></returns>
        protected string GetCutString(string str, int Length)
        {
            Length *= 2;
            byte[] bs = System.Text.Encoding.Default.GetBytes(str);
            if (bs.Length <= Length)
            {
                return str;
            }
            else
            {
                return System.Text.Encoding.Default.GetString(bs, 0, Length);
            }
        }
        #region 保存Js文件
        /// <summary>
        /// 保存js文件
        /// </summary>
        /// <param name="TxtStr">文件内容</param>
        /// <param name="TxtFile">输出路径，物理路径</param>
        protected void SaveJsFile(string TxtStr, string TxtFile)
        {
            SaveJsFile(TxtStr, TxtFile, "2");
        }
        /// <summary>
        /// 保存js文件
        /// </summary>
        /// <param name="TxtStr">文件内容</param>
        /// <param name="TxtFile">输出路径，物理路径</param>
        /// <param name="Edcode">编码：1=gb2312,2=utf-8,3=unicode</param>
        protected void SaveJsFile(string TxtStr, string TxtFile, string Edcode)
        {
            System.Text.Encoding FileType = System.Text.Encoding.Default;
            switch (Edcode)
            {
                case "3":
                    FileType = System.Text.Encoding.Unicode;
                    break;
                case "2":
                    FileType = System.Text.Encoding.UTF8;
                    break;
                case "1":
                    FileType = System.Text.Encoding.GetEncoding("GB2312");
                    break;
            }
            JumboTCMS.Utils.DirFile.CreateFolder(JumboTCMS.Utils.DirFile.GetFolderPath(false, TxtFile));
            System.IO.StreamWriter sw = new System.IO.StreamWriter(TxtFile, false, FileType);
            sw.Write("/*本文件由jcms于 " + System.DateTime.Now.ToString() + " 自动生成,请勿手动修改*/\r\n" + TxtStr);
            sw.Close();
        }
        #endregion
        #region 保存Css文件
        /// <summary>
        /// 保存Css文件
        /// </summary>
        /// <param name="TxtStr">文件内容</param>
        /// <param name="TxtFile">输出路径，物理路径</param>
        protected void SaveCssFile(string TxtStr, string TxtFile)
        {
            SaveCssFile(TxtStr, TxtFile, "2");
        }
        /// <summary>
        /// 保存Css文件
        /// </summary>
        /// <param name="TxtStr">文件内容</param>
        /// <param name="TxtFile">输出路径，物理路径</param>
        /// <param name="Edcode">编码：1=gb2312,2=utf-8,3=unicode</param>
        protected void SaveCssFile(string TxtStr, string TxtFile, string Edcode)
        {
            System.Text.Encoding FileType = System.Text.Encoding.Default;
            switch (Edcode)
            {
                case "3":
                    FileType = System.Text.Encoding.Unicode;
                    break;
                case "2":
                    FileType = System.Text.Encoding.UTF8;
                    break;
                case "1":
                    FileType = System.Text.Encoding.GetEncoding("GB2312");
                    break;
            }
            JumboTCMS.Utils.DirFile.CreateFolder(JumboTCMS.Utils.DirFile.GetFolderPath(false, TxtFile));
            System.IO.StreamWriter sw = new System.IO.StreamWriter(TxtFile, false, FileType);
            sw.Write("/*本文件由jcms于 " + System.DateTime.Now.ToString() + " 自动生成,请勿手动修改*/\r\n" + TxtStr);
            sw.Close();
        }
        #endregion
        #region 处理Cache文件
        /// <summary>
        /// 读取Cache文件并保存到Html文件
        /// </summary>
        /// <param name="CacheStr">缓存内容</param>
        /// <param name="OutFile">输出路径，物理路径</param>
        protected void SaveCacheFile(string CacheStr, string OutFile)
        {
            SaveCacheFile(CacheStr, OutFile, "2");
        }
        /// <summary>
        /// 保存Cache文件
        /// </summary>
        /// <param name="CacheStr">缓存内容</param>
        /// <param name="OutFile">输出路径，物理路径</param>
        /// <param name="Edcode">编码：1=gb2312,2=utf-8,3=unicode</param>
        protected void SaveCacheFile(string CacheStr, string OutFile, string Edcode)
        {
            System.Text.Encoding FileType = System.Text.Encoding.Default;
            switch (Edcode)
            {
                case "3":
                    FileType = System.Text.Encoding.Unicode;
                    break;
                case "2":
                    FileType = System.Text.Encoding.UTF8;
                    break;
                case "1":
                    FileType = System.Text.Encoding.GetEncoding("GB2312");
                    break;
            }
            JumboTCMS.Utils.DirFile.CreateFolder(JumboTCMS.Utils.DirFile.GetFolderPath(false, OutFile));
            try
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(OutFile, false, FileType);
                //下面这行测试所用，可以注释
                //CacheStr += "\r\n<!--Published " + System.DateTime.Now.ToString() + "-->";
                sw.Write(CacheStr);
                sw.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// <summary>
        /// 链接到站点首页
        /// </summary>
        public string Go2Site(bool _ishtml)
        {
            return (new JumboTCMS.DAL.Common(true)).Go2Site(_ishtml);
        }
        #region 链接到页面
        /// <summary>
        /// 链接到频道首页
        /// </summary>
        public string Go2Channel(bool _ishtml, string _channelid, bool _truefile)
        {
            return (new JumboTCMS.DAL.Common(true)).Go2Channel(_ishtml, _channelid, _truefile);
        }
        /// <summary>
        /// 链接到内容页
        /// </summary>
        /// <param name="_page">页码</param>
        /// <param name="_ishtml">是否静态</param>
        /// <param name="_channelid">频道ID</param>
        /// <param name="_contentid">内容ID</param>
        /// <returns></returns>
        public string Go2View(int _page, bool _ishtml, string _channelid, string _contentid, bool _truefile)
        {
            return (new JumboTCMS.DAL.Common(true)).Go2View(_page, _ishtml, _channelid, _contentid, _truefile);
        }
        /// <summary>
        /// 链接到栏目页
        /// </summary>
        public string Go2Class(int _page, bool _ishtml, string _channelid, string _classid, bool _truefile)
        {
            return (new JumboTCMS.DAL.Normal_ClassDAL()).GetClassLink(_page, _ishtml, _channelid, _classid, _truefile);
        }
        /// <summary>
        /// 链接到RSS页
        /// </summary>
        public string Go2Rss(int _page, bool _ishtml, string _channelid, string _classid)
        {
            return (new JumboTCMS.DAL.Common(true)).Go2Rss(_page, _ishtml, _channelid, _classid);
        }
        #endregion
        /// <summary>
        /// 频道管理权限菜单
        /// </summary>
        /// <returns></returns>
        protected string[] powerMenu()
        {
            //实际权限为前面加频道ID
            string[] menu = new string[10];
            menu[0] = "内容浏览";
            menu[1] = "内容录入";
            menu[2] = "内容修改";
            menu[3] = "内容删除";
            menu[4] = "内容审核";
            menu[5] = "内容推荐";
            menu[6] = "内容移动";
            menu[7] = "栏目管理";
            menu[8] = "前台更新";

            return menu;
        }
        /// <summary>
        /// 发Mobile信息给手机
        /// </summary>
        /// <param name="_ReceiveMobiles">多个手机号以逗号隔开</param>
        /// <param name="_Content"></param>
        /// <returns></returns>
        public bool SendMobileMessage(string _ReceiveMobiles, string _Content)
        {
            JumboTCMS.Utils.smsHelp.SendSMS(_ReceiveMobiles, _Content + "【" + site.Name + "】");
            return true;
        }
        /// <summary>
        /// 通知客服
        /// </summary>
        /// <param name="_Title"></param>
        /// <param name="_Content"></param>
        /// <param name="_type">1表示站内通知,2表示邮件通知</param>
        /// <returns></returns>
        public bool SendServiceNotice(string _Title, string _Content, string _type)
        {
            if (_type == "2")
                return new JumboTCMS.DAL.Normal_UserMailDAL().SendServiceMail(_Title, _Content);
            else
                return new JumboTCMS.DAL.Normal_UserMessageDAL().SendServiceMessage(_Title, _Content);
        }
        /// <summary>
        /// 解析一般模板标签
        /// </summary>
        /// <param name="PageStr"></param>
        /// <returns></returns>
        protected string ExecuteTags(string PageStr)
        {
            JumboTCMS.DAL.TemplateEngineDAL teDAL = new JumboTCMS.DAL.TemplateEngineDAL("0");
            teDAL.IsHtml = site.IsHtml;
            teDAL.ReplacePublicTag(ref PageStr);
            teDAL.ReplaceChannelClassLoopTag(ref PageStr);
            teDAL.ReplaceContentLoopTag(ref PageStr);
            return PageStr;
        }
        #region 生成静态页面
        /// <summary>
        /// 生成栏目文件
        /// </summary>
        /// <param name="_classId"></param>
        /// <param name="CreateParent"></param>

        protected void CreateClassFile(JumboTCMS.Entity.Normal_Channel _channel, string _classId, bool CreateParent)
        {
            JumboTCMS.DAL.TemplateEngineDAL teDAL = new JumboTCMS.DAL.TemplateEngineDAL(_channel.Id);
            int pageCount = new JumboTCMS.DAL.Normal_ClassDAL().GetContetPageCount(_channel.Id, _classId, true);
            int maxPage = JumboTCMS.Utils.Int.Min(site.CreatePages, pageCount);
            string PageStr = string.Empty;
            for (int i = 1; i < (maxPage + 1); i++)
            {
                PageStr = teDAL.GetSiteClassPage(_classId, i);
                JumboTCMS.Utils.DirFile.SaveFile(PageStr, Go2Class(i, true, _channel.Id, _classId, true));
            }
            doh.Reset();
            doh.SqlCmd = "SELECT Id, ParentId FROM [jcms_normal_class] WHERE [IsOut]=0 AND [ChannelId]=" + _channel.Id + " AND [Id]=" + _classId;
            DataTable dtClass = doh.GetDataTable();
            if (dtClass.Rows.Count > 0 && dtClass.Rows[0]["ParentId"].ToString() != "0" && CreateParent == true)
            {
                CreateClassFile(_channel, dtClass.Rows[0]["ParentId"].ToString(), true);
            }
            dtClass.Clear();
            dtClass.Dispose();

        }

        /// <summary>
        /// 生成内容页
        /// </summary>
        /// <param name="_contentID">内容ID</param>
        /// <param name="_currentPage">指定的页码,-1表示所有</param>
        protected void CreateContentFile(JumboTCMS.Entity.Normal_Channel _channel, string _contentID, int _currentPage)
        {
            JumboTCMS.DAL.ModuleCommand.CreateContent(_channel.Type, _channel.Id, _contentID, _currentPage);
        }
        #endregion
        /// <summary>
        /// 格式化标签
        /// </summary>
        /// <param name="_tags"></param>
        /// <param name="_title"></param>
        /// <param name="_autosplit"></param>
        /// <returns></returns>
        public string FormatTags(string _tags, string _title, bool _autosplit)
        {
            string _tag = _tags;
            if (_autosplit && _tag == "")
                _tag = JumboTCMS.Utils.WordSpliter.GetKeyword(_title, ",");
            else
                _tag = JumboTCMS.Utils.Strings.SafetyStr(_tag);
            string[] _taglist = _tag.Split(',');
            string _returnTags = "";
            int _returnNum = 0;
            for (int i = 0; i < _taglist.Length; i++)
            {
                if (_taglist[i].Length > 1 && _returnNum < 4)
                {
                    if (_returnTags.Length == 0)
                        _returnTags = _taglist[i].Trim();
                    else
                        _returnTags += "," + _taglist[i].Trim();
                    _returnNum++;
                }
            }
            return _returnTags;
        }
    }
}
