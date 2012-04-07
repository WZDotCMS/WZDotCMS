using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JumboTCMS.OAuth.Renren;
using JumboTCMS.OAuth.Renren.APIUtility;
namespace JumboTCMS.WebFile.App.Renren
{
    public partial class oauth : JumboTCMS.UI.FrontPassport
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckOAuthState("renren");
            if (!site.AllowReg || site.CheckReg)
            {
                FinalMessage("对不起，本站不支持第三方登录!", site.Dir, 0);
                Response.End();
            }
            string _operType = q("type");
            string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/oauth_renren.config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            string AppKey = XmlTool.GetText("Root/AppKey");
            string AppSecret = XmlTool.GetText("Root/AppSecret");
            string CallBackURI = site.Url + site.Dir + "app/renren/oauth.aspx?type=logined";
            XmlTool.Dispose();
            switch (_operType)
            {
                case "login"://登陆
                    //引导用户进行授权
                    new RenrenApiClient().GetAuthorizationCode(AppKey, CallBackURI);
                    break;
                case "logined"://登陆后
                    if (q("code") != "")
                    {
                        string _Access_Token = new APIValidation().GetAccessToken(AppKey, AppSecret, CallBackURI);
                        if (_Access_Token != "")
                        {
                            string _UserName = "";
                            string _Birthday = "1980-01-01";
                            string _Email = "";
                            JumboTCMS.Utils.Cookie.SetObj("OAuth_Info", "{\"code\":\"renren\",\"token\":\"" + _Access_Token + "\",\"username\":\"" + _UserName + "\",\"email\":\"" + _Email + "\",\"birthday\":\"" + _Birthday + "\"}");
                            Response.Redirect(site.Dir + "passport/register_third.aspx");
                        }
                        Response.Write(new APIValidation().GetTest(AppKey, AppSecret, q("code"), CallBackURI));
                    }
                    string _html2 = @"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html xmlns=""http://www.w3.org/1999/xhtml"">
<head>
    <title></title>
</head>
<body>
接口失败" + @"
</body>
</html>";
                    Response.Write(_html2);
                    break;
                default: break;
            }
        }
    }
}