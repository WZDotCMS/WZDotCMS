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
using System.Data.OleDb;
using System.Web;
using JumboTCMS.Common;
using JumboTCMS.Utils;
using System.Runtime.InteropServices;
namespace JumboTCMS.WebFile.Plus.Weather
{
    public partial class _json : JumboTCMS.UI.BasicPage
    {
        private JumboTCMS.DBUtility.DbOperHandler _doh = null;
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (_doh != null)
            {
                _doh.Dispose();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("~/_data/database/weather.config");
            if (_doh == null)
                _doh = new JumboTCMS.DBUtility.OleDbOperHandler(new OleDbConnection(connectionString));
            string citycode = f("citycode");
            if (citycode == "")//不是用户选择
            {
                if (JumboTCMS.Utils.Cookie.GetValue("WeatherCityId") == null)
                {
                    string IP = Const.GetUserIp;
                    citycode = new JumboTCMS.Tools.Weather.DAL().IP2CityCode(IP, _doh);
                    JumboTCMS.Utils.Cookie.SetObj("WeatherCityId", 1, citycode, "", "/");
                }
                citycode = JumboTCMS.Utils.Cookie.GetValue("WeatherCityId");
            }
            else
            {
                if (f("savecookie") == "1")
                    JumboTCMS.Utils.Cookie.SetObj("WeatherCityId", 1, citycode, "", "/");
            }
            Response.Write(new JumboTCMS.Tools.Weather.DAL().GetWeatherJson(citycode));
        }
    }
}
