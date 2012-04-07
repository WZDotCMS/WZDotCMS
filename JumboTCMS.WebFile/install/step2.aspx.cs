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
using System.Text;
using System.IO;
using JumboTCMS.Utils;
namespace JumboTCMS.WebFile.Install
{
    public partial class _step2 : JumboTCMS.UI.BasicPage
    {
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            ////防止网页后退--禁止缓存    
            Response.Expires = 0;
            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            Response.AddHeader("pragma", "no-cache");
            Response.CacheControl = "no-cache";
            System.Web.HttpContext.Current.Application.Lock();
            System.Web.HttpContext.Current.Application["jcmsV5_dbType"] = null;
            System.Web.HttpContext.Current.Application["jcmsV5_dbPath"] = null;
            System.Web.HttpContext.Current.Application["jcmsV5_dbConnStr"] = null;
            System.Web.HttpContext.Current.Application["jcmsV5"] = null;
            System.Web.HttpContext.Current.Application.UnLock();
            Step2();
            Response.Write(this._response);
        }
        private void Step2()
        {
            string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/site.config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            XmlTool.Update("Root/Name", q("sitename"));
            XmlTool.Update("Root/Name2", q("sitename2"));
            XmlTool.Save();
            XmlTool.Dispose();
            string _Email = q("email");
            string _UserName = q("username");
            string _UserPass = q("userpass");
            string _AdminName = q("adminname");
            string _AdminPass = q("adminpass");
            if (new JumboTCMS.DAL.Normal_UserDAL().Register(_UserName, _UserName, _UserPass, false, 0, _Email, "1980-1-1", "", _AdminName, _AdminPass, "", "") > 0)
            {
                //将超级管理员写入配置文件
                strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/site.config");
                XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
                XmlTool.Update("Root/Founders", "." + _AdminName + ".");
                XmlTool.Save();
                XmlTool.Dispose();
                new JumboTCMS.DAL.SiteDAL().CreateSiteFiles();
                SetupSystemDate();
                this._response = "ok";
            }
            else
                this._response = "管理员添加失败";
        }
    }
}