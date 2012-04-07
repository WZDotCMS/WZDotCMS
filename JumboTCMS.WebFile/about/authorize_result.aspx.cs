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
    public partial class _authorize_result : JumboTCMS.UI.FrontHtml
    {
        public string Domain, WebName,AccreditType, AccreditTypeName, AddTime, UseInBusiness, DeleteCopyright, Validity = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string _domain = q("domain");
            string[] aryReg = { "'", "\"", "<", ">", "%", "?", ",", "=", "_", ";", "|", "[", "]", "&", "/" };
            for (int i = 0; i < aryReg.Length; i++)
            {
                _domain = _domain.Replace(aryReg[i], string.Empty);
            }
            int countNum = 0;
            string sqlStr = "";
            string joinStr = "A.[AccreditType]=B.Id";
            string whereStr1 = "A.[State]=1 and A.[Domain]='" + _domain + "'";
            string whereStr2 = "[State]=1 and [Domain]='" + _domain + "'";
            doh.Reset();
            doh.ConditionExpress = whereStr2;
            countNum = doh.Count("jcms_official_authorization");
            if (countNum == 0)
            {
                Response.Write("<script>alert('此域名未得到官方授权');window.close();</script>");
                Response.End();
            }
            sqlStr = JumboTCMS.Utils.SqlHelp.GetSql("a.*,b.Title as AccreditTypeName,b.UseInBusiness,b.DeleteCopyright", "jcms_official_authorization", "jcms_official_authorization_type", "Id", 1, 1, "desc", joinStr, whereStr1, whereStr2);
            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dt = doh.GetDataTable();
            Domain = dt.Rows[0]["Domain"].ToString();
            WebName = dt.Rows[0]["WebName"].ToString();
            AccreditType = dt.Rows[0]["AccreditType"].ToString();
            AccreditTypeName = dt.Rows[0]["AccreditTypeName"].ToString();
            UseInBusiness = dt.Rows[0]["UseInBusiness"].ToString();
            DeleteCopyright = dt.Rows[0]["DeleteCopyright"].ToString();
            AddTime = Convert.ToDateTime(dt.Rows[0]["AddTime"].ToString()).ToString("yyyy-MM-dd");
            Validity = Convert.ToDateTime(dt.Rows[0]["Validity"].ToString()).ToString("yyyy-MM-dd");

            dt.Clear();
            dt.Dispose();
        }
    }
}
