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
namespace JumboTCMS.WebFile.About
{
    public partial class _authorize_submit : JumboTCMS.UI.FrontHtml
    {
        public string Domain, WebName, AccreditType, AccreditTypeName, AddTime, UseInBusiness, DeleteCopyright, Validity = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string _domain = f("domain");
            string _defaultpage = f("defaultpage");
            string _webname = f("webname");

            string[] aryReg = { "'", "\"", "<", ">", "%", "?", ",", "=", "_", ";", "|", "[", "]", "&", "/" };
            for (int i = 0; i < aryReg.Length; i++)
            {
                _domain = _domain.Replace(aryReg[i], string.Empty);
            }
            int countNum = 0;
            doh.Reset();
            doh.ConditionExpress = "[State]=1 and [Domain]='" + _domain + "'";
            countNum = doh.Count("jcms_official_authorization");
            if (countNum > 0)
            {
                Response.Write("<script>alert('此域名已得到官方授权，不需要再申请');window.close();</script>");
                Response.End();
            }
            //需要检测首页文件是否含Jumbotcms
            if (JumboTCMS.Utils.Validator.IsFreeSite(_defaultpage, _webname))
            {
                doh.Reset();
                doh.AddFieldItem("Domain", _domain);
                doh.AddFieldItem("WebName", _webname);
                doh.AddFieldItem("DefaultPage", _defaultpage);
                doh.AddFieldItem("AccreditType", 1);
                doh.AddFieldItem("State", 1);
                doh.AddFieldItem("AddTime", System.DateTime.Now.ToString("yyyy-MM-dd"));
                doh.AddFieldItem("Validity", "2099-12-31");
                doh.Insert("jcms_official_authorization");
                Response.Redirect("authorize_result.aspx?domain=" + _domain);
            }
            else
            {
                Response.Write("<script>alert('此网站信息不正确，不能进行申请');window.close();</script>");
                Response.End();
            }
        }

    }
}
