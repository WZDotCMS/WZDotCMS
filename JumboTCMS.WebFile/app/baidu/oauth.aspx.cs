using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JumboTCMS.OAuth.Baidu;
namespace JumboTCMS.WebFile.App.Baidu
{
    public partial class oauth : JumboTCMS.UI.FrontPassport
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckOAuthState("baidu");
            if (!site.AllowReg || site.CheckReg)
            {
                FinalMessage("对不起，本站不支持第三方登录!", site.Dir, 0);
                Response.End();
            }
            this.Response.AddHeader("P3P", "CP=CAO PSA OUR");//解决IE里Iframe的Cookie问题
            string _operType = q("type");
            string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/oauth_baidu.config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            string AppKey = XmlTool.GetText("Root/AppKey");
            string AppSecret = XmlTool.GetText("Root/AppSecret");
            string CallBackURI = site.Url + site.Dir + "app/baidu/oauth.aspx?type=logined";
            XmlTool.Dispose();
            switch (_operType)
            {
                case "login"://登陆
                    //引导用户进行授权
                    OAuthClient.GetAuthorizationCode(AppKey, CallBackURI);
                    break;
                case "logined"://登陆后
                    if (q("code") != "")
                    {
                        //用获取到的Authorization Code换取Access Token
                        OAuthMessage msg = OAuthClient.GetAccessTokenByAuthorizationCode(AppKey, AppSecret, q("code"), CallBackURI);
                        BaiduApiClient client = new BaiduApiClient(msg.Session_key, msg.Session_secret);
                        IUsersService userServer = client.UserService;
                        string result = userServer.GetInfo("userid,username,birthday");
                        Dictionary<string, object> newobj = (Dictionary<string, object>)JumboTCMS.Utils.fastJSON.JSON.Instance.ToObject(result);
                        string _Access_Token = (string)newobj["userid"];
                        if (_Access_Token != "")
                        {
                            string _UserName = (string)newobj["username"];
                            string _Birthday = (string)newobj["birthday"];
                            string _Email = "";
                            JumboTCMS.Utils.Cookie.SetObj("OAuth_Info", "{\"code\":\"baidu\",\"token\":\"" + _Access_Token + "\",\"username\":\"" + _UserName + "\",\"email\":\"" + _Email + "\",\"birthday\":\"" + _Birthday + "\"}");
                            Response.Redirect(site.Dir + "passport/register_third.aspx");
                        }
                    }
                    break;
                default: break;
            }
        }
    }
}