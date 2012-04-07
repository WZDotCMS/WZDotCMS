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
namespace JumboTCMS.WebFile.Admin
{
    public partial class __authorize_ajax : JumboTCMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckFormUrl())
            {
                Response.End();
            }
            Admin_Load("master", "json");
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxGetList":
                    ajaxGetList();
                    break;
                case "ajaxCheck":
                    ajaxCheck();
                    break;
                case "ajaxRemind":
                    ajaxRemind();
                    break;
                default:
                    DefaultResponse();
                    break;
            }
            Response.Write(this._response);
        }

        private void DefaultResponse()
        {
            this._response = JsonResult(0, "未知操作");
        }
        private void ajaxGetList()
        {
            int page = Int_ThisPage();
            int PSize = Str2Int(q("pagesize"), 20);
            string joinStr = "A.[AccreditType]=B.Id";
            string whereStr1 = "1=1";
            string whereStr2 = "1=1";
            doh.Reset();
            doh.ConditionExpress = whereStr2;
            string sqlStr = "";
            int _countnum = doh.Count("jcms_official_authorization");
            sqlStr = JumboTCMS.Utils.SqlHelp.GetSql("a.*,(select title from [jcms_official_authorization_sitetype] where id=a.sitetype) as SiteTypeName,b.Title as AccreditTypeName,b.UseInBusiness,b.DeleteCopyright", "jcms_official_authorization", "jcms_official_authorization_type", "Id", PSize, page, "desc", joinStr, whereStr1, whereStr2);
            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dt = doh.GetDataTable();
            this._response = "{result :\"1\"," +
                "returnval :\"操作成功\"," +
                "pagerbar :\"" + JumboTCMS.Utils.HtmlPager.GetPageBar(3, "js", 2, _countnum, PSize, page, "javascript:ajaxList(<#page#>);") + "\"," +
                JumboTCMS.Utils.dtHelp.DT2JSON(dt, (PSize * (page - 1))) +
                "}";
            dt.Clear();
            dt.Dispose();
        }
        private void ajaxCheck()
        {
            string aId = f("id");
            doh.Reset();
            doh.ConditionExpress = "id=@id";
            doh.AddConditionParameter("@id", aId);
            object[] _value = doh.GetFields("jcms_official_authorization", "WebName,DefaultPage");
            string _webname = _value[0].ToString();
            string _defaultpage = _value[1].ToString();
            //需要检测首页文件是否含Jumbotcms
            if (JumboTCMS.Utils.Validator.IsFreeSite(_defaultpage, _webname))
            {
                doh.Reset();
                doh.ConditionExpress = "id=@id";
                doh.AddConditionParameter("@id", aId);
                doh.AddFieldItem("State", 1);
                doh.Update("jcms_official_authorization");
                this._response = JsonResult(1, "检测完成");
            }
            else
            {
                doh.Reset();
                doh.ConditionExpress = "id=@id";
                doh.AddConditionParameter("@id", aId);
                doh.AddFieldItem("State", 0);
                doh.Update("jcms_official_authorization");
                this._response = JsonResult(1, "检测完成");
            }
        }
        private void ajaxRemind()
        {
            string aId = f("id");
            doh.Reset();
            doh.ConditionExpress = "id=@id";
            doh.AddConditionParameter("@id", aId);
            object[] _value = doh.GetFields("jcms_official_authorization", "WebName,Domain,LinkEmail,MobileTel");
            string _WebName = _value[0].ToString();
            string _Domain = _value[1].ToString();
            string _LinkEmail = _value[2].ToString();
            string _MobileTel = _value[3].ToString();
            string strXmlFile = HttpContext.Current.Server.MapPath("~/_data/config/_authorize_remind.config");
            JumboTCMS.DBUtility.XmlControl XmlTool = new JumboTCMS.DBUtility.XmlControl(strXmlFile);
            string _Title = XmlTool.GetText("Root/AuthorityRemind/Title", true).Replace("{$WebName}", _WebName).Replace("{$Domain}", _Domain);
            string _EmailBody = XmlTool.GetText("Root/AuthorityRemind/EmailBody", true).Replace("{$WebName}", _WebName).Replace("{$Domain}", _Domain);
            string _MobileBody = XmlTool.GetText("Root/AuthorityRemind/MobileBody", true).Replace("{$WebName}", _WebName).Replace("{$Domain}", _Domain);
            XmlTool.Dispose();
            if (_LinkEmail != "")
            {
                if (new JumboTCMS.DAL.Normal_UserMailDAL().SendMail(_LinkEmail, _Title, _EmailBody))
                {
                    this._response = JsonResult(1, "邮件发送成功");
                }
                else
                {
                    this._response = JsonResult(1, "邮件发送失败");
                }
            }
            else if (_MobileTel != "")
            {
                if (SendMobileMessage(_MobileTel, _MobileBody))
                {
                    this._response = JsonResult(1, "短信发送成功");
                }
                else
                {
                    this._response = JsonResult(1, "短信发送失败");
                }
            }
            else
                this._response = JsonResult(1, "未填邮箱和手机号");
        }
    }
}