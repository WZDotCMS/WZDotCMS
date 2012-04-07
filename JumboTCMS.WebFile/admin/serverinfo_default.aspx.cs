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
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Globalization;
using Microsoft.Win32;
using System.IO;
using System.Management;
using System.Diagnostics;

using JumboTCMS.Common;

namespace JumboTCMS.WebFile.Admin
{
    public partial class _serverinfo_index : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("", "html");
            if (!Page.IsPostBack)
            {
                lbServerName.Text = "http://" + Request.Url.Host;
                lbIp.Text = Request.ServerVariables["LOCAl_ADDR"].ToString();
                lbDomain.Text = Request.ServerVariables["SERVER_NAME"].ToString();
                lbPort.Text = Request.ServerVariables["Server_Port"].ToString();
                lbIISVer.Text = Request.ServerVariables["Server_SoftWare"].ToString();
                lbPhPath.Text = Request.PhysicalApplicationPath;
                lbOperat.Text = Environment.OSVersion.ToString();
                lbSystemPath.Text = Environment.SystemDirectory.ToString();
                lbTimeOut.Text = (Server.ScriptTimeout).ToString() + "秒";
                lbLan.Text = CultureInfo.InstalledUICulture.EnglishName;
                lbAspnetVer.Text = string.Concat(new object[] { Environment.Version.Major, ".", Environment.Version.Minor, Environment.Version.Build, ".", Environment.Version.Revision });
                lbCurrentTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Internet Explorer\Version Vector");
                lbIEVer.Text = key.GetValue("IE", "未检测到").ToString();
                lbServerLastStartToNow.Text = ((Environment.TickCount / 0x3e8) / 60).ToString() + "分钟";

                string[] achDrives = Directory.GetLogicalDrives();
                for (int i = 0; i < Directory.GetLogicalDrives().Length - 1; i++)
                {
                    lbLogicDriver.Text = lbLogicDriver.Text + achDrives[i].ToString();
                }

                lbCpuNum.Text = Environment.GetEnvironmentVariable("NUMBER_OF_PROCESSORS").ToString();
                lbCpuType.Text = Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER").ToString();
                lbMemory.Text = (Environment.WorkingSet / 1024).ToString() + "M";
                lbMemoryPro.Text = ((Double)GC.GetTotalMemory(false) / 1048576).ToString("N2") + "M";
                lbMemoryNet.Text = ((Double)Process.GetCurrentProcess().WorkingSet64 / 1048576).ToString("N2") + "M";
                lbCpuNet.Text = ((TimeSpan)Process.GetCurrentProcess().TotalProcessorTime).TotalSeconds.ToString("N2");
                lbSessionNum.Text = Session.Contents.Count.ToString();
                lbSession.Text = Session.Contents.SessionID;
                lbUser.Text = Environment.UserName;
            }
        }
    }
}
