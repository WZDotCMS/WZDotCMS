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
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using JumboTCMS.Common;
using JumboTCMS.DBUtility;

namespace JumboTCMS.DAL
{
    public class Common
    {
        public static string connectionString = Const.ConnectionString;
        public string DBType = Const.DatabaseType;
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
        public string vbCrlf = "\r\n";//换行符
        protected JumboTCMS.Entity.Site site;
        public Common()
        {
            if (System.Web.HttpContext.Current.Application["jcmsV5"] == null)
            {
                System.Web.HttpContext.Current.Application.Lock();
                System.Web.HttpContext.Current.Application["jcmsV5"] = new JumboTCMS.DAL.SiteDAL().GetEntity();
                System.Web.HttpContext.Current.Application.UnLock();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_formatsystem">是否初始化site</param>
        public Common(bool _formatsystem)
        {
            if (System.Web.HttpContext.Current.Application["jcmsV5"] == null)
            {
                System.Web.HttpContext.Current.Application.Lock();
                System.Web.HttpContext.Current.Application["jcmsV5"] = new JumboTCMS.DAL.SiteDAL().GetEntity();
                System.Web.HttpContext.Current.Application.UnLock();
            }
            if (_formatsystem) site = (JumboTCMS.Entity.Site)System.Web.HttpContext.Current.Application["jcmsV5"];
        }
        /// <summary>
        /// 初始化系统信息
        /// </summary>
        protected void SetupSystemDate()
        {

            site = (JumboTCMS.Entity.Site)System.Web.HttpContext.Current.Application["jcmsV5"];
        }
        public DbOperHandler Doh()
        {
            if (DBType == "0")
            {
                return new OleDbOperHandler(new OleDbConnection(connectionString));
            }
            else
            {
                return new SqlDbOperHandler(new SqlConnection(connectionString));
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
        /// 执行Sql脚本文件
        /// </summary>
        /// <param name="pathToScriptFile">物理路径</param>
        /// <returns></returns>
        public bool ExecuteSqlInFile(string pathToScriptFile)
        {
            return JumboTCMS.Utils.ExecuteSqlBlock.Go(DBType, connectionString, pathToScriptFile);

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
            string TempUrl = JumboTCMS.Common.PageFormat.Site(site.Dir, site.UrlReWriter);
            TempUrl = TempUrl.Replace("<#SiteDir#>", site.Dir);
            TempUrl = TempUrl.Replace("<#SiteStaticExt#>", site.StaticExt);
            return TempUrl;
        }
        /// <summary>
        /// 链接到频道首页
        /// </summary>
        public string Go2Channel(bool _ishtml, string _channelid, bool _truefile)
        {
            return (new JumboTCMS.DAL.Normal_ChannelDAL()).GetChannelLink(_ishtml, _channelid, _truefile);
        }
        /// <summary>
        /// 链接到栏目页
        /// </summary>
        public string Go2Class(int _page, bool _ishtml, string _channelid, string _classid, bool _truefile)
        {
            return (new JumboTCMS.DAL.Normal_ClassDAL()).GetClassLink(_page, _ishtml, _channelid, _classid, _truefile);
        }
        /// <summary>
        /// 链接到内容页
        /// </summary>
        /// <param name="_page">页码</param>
        /// <param name="_ishtml">是否静态</param>
        /// <param name="_channelid">频道ID</param>
        /// <param name="_contentid">内容ID</param>
        /// <param name="_initialize">是否初始化</param>
        /// <returns></returns>
        public string Go2View(int _page, bool _ishtml, string _channelid, string _contentid, bool _truefile)
        {
            JumboTCMS.Entity.Normal_Channel _Channel = new JumboTCMS.DAL.Normal_ChannelDAL().GetEntity(_channelid);
            return ModuleCommand.GetContentLink(_Channel.Type.ToLower(), _page, _ishtml, _channelid, _contentid, _truefile);
        }
        /// <summary>
        /// 链接到RSS页
        /// </summary>
        /// <param name="_page"></param>
        /// <param name="_ishtml"></param>
        /// <param name="_channelid"></param>
        /// <param name="_classid"></param>
        /// <returns></returns>
        public string Go2Rss(int _page, bool _ishtml, string _channelid, string _classid)
        {
            JumboTCMS.Entity.Normal_Channel _Channel = new JumboTCMS.DAL.Normal_ChannelDAL().GetEntity(_channelid);
            string TempUrl = PageFormat.Rss(_ishtml, site.Dir, site.UrlReWriter, _page);
            TempUrl = TempUrl.Replace("<#SiteDir#>", site.Dir);
            TempUrl = TempUrl.Replace("<#SiteStaticExt#>", site.StaticExt);
            TempUrl = TempUrl.Replace("<#ChannelId#>", _channelid);
            TempUrl = TempUrl.Replace("<#ChannelDir#>", _Channel.Dir.ToLower());
            TempUrl = TempUrl.Replace("<#ChannelType#>", _Channel.Type.ToLower());
            TempUrl = TempUrl.Replace("<#id#>", _classid);
            if (_page > 0) TempUrl = TempUrl.Replace("<#page#>", _page.ToString());
            return TempUrl;
        }
    }
}
