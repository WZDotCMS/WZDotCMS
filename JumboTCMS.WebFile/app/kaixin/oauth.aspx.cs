using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JumboTCMS.OAuth.Kaixin;
using System.Configuration;
namespace JumboTCMS.WebFile.App.Kaixin
{
    public partial class oauth : JumboTCMS.UI.FrontPassport
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckOAuthState("kaixin");
            if (!site.AllowReg || site.CheckReg)
            {
                FinalMessage("对不起，本站不支持第三方登录!", site.Dir, 0);
                Response.End();
            }
            string _operType = q("type");
            string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/oauth_kaixin.config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            string AppKey = XmlTool.GetText("Root/AppKey");
            string AppSecret = XmlTool.GetText("Root/AppSecret");
            string CallBackURI = site.Url + site.Dir + "app/kaixin/oauth.aspx?type=logined";
            XmlTool.Dispose();
            KxOAuth2 oauth2 = new KxOAuth2();
            switch (_operType)
            {
                case "login"://登陆
                    string authorizeUrl = "https://api.kaixin001.com/oauth2/authorize";
                    string redirectUrl = oauth2.AuthorizeCodeAuthorizeUrl(authorizeUrl, AppKey, CallBackURI, KxConfig.SCOPE);
                    //string redirectUrl = oauth2.ImplictAuthorizeUrl(authorizeUrl, KxConfig.AppKey, KxConfig.CallBackURI, KxConfig.SCOPE);
                    Response.Redirect(redirectUrl, true);
                    break;
                case "logined"://登陆后
                    AccessToken accessToken = null;
                    if (Request.Params["state"] != null)
                    {
                        //ImplicitGrant的回调
                        if (Request.Params["ImplicitGrantURL"] == null)
                        {
                            //第一次没有通过javascript返回Url
                            return;
                        }
                        else
                        {
                            //第二次已经有了包含AccessToken的ImplicitGrantURL
                            accessToken = oauth2.ImplictAuthorizeParseAccessToken(Request.Params["ImplicitGrantURL"]);
                        }
                    }
                    else
                    {
                        //AuthorizationCode的回调
                        string accessTokenUrl = "https://api.kaixin001.com/oauth2/access_token";
                        accessToken = (AccessToken)oauth2.AuthorizeCodeAccessToken(accessTokenUrl, Request.Params["code"], AppKey, AppSecret, CallBackURI);
                    }
                    string _accesstoken = accessToken.access_token;
                    if (_accesstoken.Length > 32 && _accesstoken.Contains("_"))
                    {
                        string _Access_Token = _accesstoken.Split('_')[0];
                        if (_Access_Token != "")
                        {
                            string userinfoUrl = "https://api.kaixin001.com/users/me.json";
                            string result = oauth2.GetUserInfo(userinfoUrl, _accesstoken, "uid,name,gender");
                            //Response.Write(result);
                            //Response.End();
                            Dictionary<string, object> newobj = (Dictionary<string, object>)JumboTCMS.Utils.fastJSON.JSON.Instance.ToObject(result);
                            string _UserName = (string)newobj["name"];
                            string _Birthday = "1980-01-01";
                            string _Email = "";
                            JumboTCMS.Utils.Cookie.SetObj("OAuth_Info", "{\"code\":\"kaixin\",\"token\":\"" + _Access_Token + "\",\"username\":\"" + _UserName + "\",\"email\":\"" + _Email + "\",\"birthday\":\"" + _Birthday + "\"}");
                            Response.Redirect(site.Dir + "passport/register_third.aspx");
                        }
                    }
                    break;
                default: break;
            }
        }
    }
}