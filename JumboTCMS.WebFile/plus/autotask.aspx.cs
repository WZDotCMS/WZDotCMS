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
using JumboTCMS.Utils;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Plus
{
    public partial class _autotask : JumboTCMS.UI.BasicPage
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;
        private string _spliter = "'";
        protected void Page_Load(object sender, EventArgs e)
        {
            this._operType = q("oper");
            if (base.DBType == "0") _spliter = "#";
            switch (this._operType)
            {
                case "DeleteNotice":
                    DeleteNotice();
                    break;
                case "DeleteUnactivedUser":
                    DeleteUnactivedUser();
                    break;
                default:
                    DefaultResponse();
                    break;
            }
            Response.Write(this._response);
        }
        private void DefaultResponse()
        {
            this._response = "未知操作";
        }
        private void DeleteNotice()
        {
            string _password = q("password");
            if (_password != System.Configuration.ConfigurationManager.AppSettings["AutoTask:Password"])
            {
                this._response = "密码错误";
                return;
            }
            doh.Reset();
            doh.ConditionExpress = "[State]=1 AND [ReadTime]<=" + _spliter + System.DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") + _spliter;
            int _doCount = doh.Delete("jcms_normal_user_notice");
            doh.Reset();
            doh.ConditionExpress = "[State]=1 AND [ReadTime]<=" + _spliter + System.DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd") + _spliter;
            int _doCount2 = doh.Delete("jcms_normal_user_message");
            this._response = "有" + _doCount + "条已阅读的站内信被删除；有" + _doCount2 + "条已阅读的通知被删除";
        }
        private void DeleteUnactivedUser()
        {
            string _password = q("password");
            if (_password != System.Configuration.ConfigurationManager.AppSettings["AutoTask:Password"])
            {
                this._response = "密码错误";
                return;
            }
            doh.Reset();
            doh.ConditionExpress = "[State]=0 AND [RegTime]<=" + _spliter + System.DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd") + _spliter;
            int _doCount = doh.Delete("jcms_normal_user");
            this._response = "有" + _doCount + "个未激活的会员被删除";
        }
    }
}