using System;
using System.Collections.Generic;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;
using JumboTCMS.OAuth.Tencent;
namespace JumboTCMS.WebFile.App.Tencent
{
    public partial class oauth : JumboTCMS.UI.FrontPassport
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckOAuthState("tencent");
            if (!site.AllowReg || site.CheckReg)
            {
                FinalMessage("对不起，本站不支持第三方登录!", site.Dir, 0);
                Response.End();
            }
            string _operType = q("type");
            switch (_operType)
            {
                case "login"://登陆
                    string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/oauth_tencent.config");
                    JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
                    string appid = XmlTool.GetText("Root/AppId");
                    string appkey = XmlTool.GetText("Root/AppKey");
                    XmlTool.Dispose();
                    string callBackUrl = site.Url + site.Dir + "app/tencent/oauth.aspx";
                    Utility.Login(appid, appkey, callBackUrl,
                        site.Url + site.Dir + "app/tencent/oauth.aspx?type=loginsuccess",
                        site.Url + site.Dir + "app/tencent/oauth.aspx?type=loginfail");
                    break;
                case "loginsuccess"://成功
                    string _Access_Token = Utility.OpenId;
                    if (_Access_Token != "")
                    {
                        string result = Utility.GetUserInfo(DataFormatEnum.Json);
                        Dictionary<string, object> newobj = (Dictionary<string, object>)JumboTCMS.Utils.fastJSON.JSON.Instance.ToObject(result);
                        string _UserName = (string)newobj["nickname"];
                        string _Birthday = "1980-01-01";
                        string _Email = "";
                        JumboTCMS.Utils.Cookie.SetObj("OAuth_Info", "{\"code\":\"tencent\",\"token\":\"" + _Access_Token + "\",\"username\":\"" + _UserName + "\",\"email\":\"" + _Email + "\",\"birthday\":\"" + _Birthday + "\"}");
                        Response.Redirect(site.Dir + "passport/register_third.aspx");
                    }
                    break;
                case "loginfail"://失败
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
                    break;
                default://腾讯调转
                    string oauth_token = Request["oauth_token"];
                    string oauth_vericode = Request["oauth_vericode"];

                    if (!string.IsNullOrEmpty(oauth_token) && !string.IsNullOrEmpty(oauth_vericode))
                    {
                        OAuthHelper.RequestAccessToken(OAuthHelper.AppId, OAuthHelper.AppKey, oauth_token, OAuthHelper.OAuth_Token_Secret, oauth_vericode);
                    }
                    else
                        Response.Write("找不到操作类型。");
                    break;
            }
        }
    }
}
