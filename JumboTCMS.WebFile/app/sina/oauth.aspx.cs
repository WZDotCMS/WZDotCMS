using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JumboTCMS.OAuth.Sina;
namespace JumboTCMS.WebFile.App.Sina
{
    public partial class oauth : JumboTCMS.UI.FrontPassport
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckOAuthState("sina");
            if (!site.AllowReg || site.CheckReg)
            {
                FinalMessage("对不起，本站不支持第三方登录!", site.Dir, 0);
                Response.End();
            }
            string _operType = q("type");
            switch (_operType)
            {
                case "login"://登陆
                    string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/oauth_sina.config");
                    JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
                    SinaConfig.AppKey = XmlTool.GetText("Root/AppKey");
                    SinaConfig.AppSecret = XmlTool.GetText("Root/AppSecret");
                    XmlTool.Dispose();
                    SinaConfig.CallBackURI = site.Url + site.Dir + "app/sina/oauth.aspx?type=logined";
                    new JumboTCMS.OAuth.Sina.SinaApi().ConnectSina();
                    break;
                case "logined"://登陆后
                    if (!string.IsNullOrEmpty(Request["oauth_verifier"]))
                    {
                        new JumboTCMS.OAuth.Sina.SinaApi().getAccessToken(Request["oauth_verifier"].ToString());
                    }
                    SinaApi sinaApi = new SinaApi();
                    SinaUser SinaUser = sinaApi.verify_credentials();
                    if (SinaUser == null)
                    {
                        string _html2 = @"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html xmlns=""http://www.w3.org/1999/xhtml"">
<head>
    <title></title>
</head>
<body>
error_code=" + Request["error_code"] + @"
</body>
</html>";
                        Response.Write(_html2);
                        return;
                    }
                    string _Access_Token = SinaUser.id.ToString();
                    if (_Access_Token != "")
                    {
                        string _UserName = SinaUser.screen_name;
                        string _Birthday = "1980-01-01";
                        string _Email = "";
                        JumboTCMS.Utils.Cookie.SetObj("OAuth_Info", "{\"code\":\"sina\",\"token\":\"" + _Access_Token + "\",\"username\":\"" + _UserName + "\",\"email\":\"" + _Email + "\",\"birthday\":\"" + _Birthday + "\"}");
                        Response.Redirect(site.Dir + "passport/register_third.aspx");
                    }
                    break;
                default: break;
            }
        }
    }
}