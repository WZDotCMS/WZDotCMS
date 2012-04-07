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
using System.Web.UI.WebControls;
using JumboTCMS.Utils;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _user_ajax : JumboTCMS.UI.AdminCenter
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
                case "ajaxUserInfo":
                    GetUserInfo();
                    break;
                case "ajaxBatchOper":
                    ajaxBatchOper();
                    break;
                case "ajaxDel":
                    ajaxDel();
                    break;
                case "checkname":
                    ajaxCheckName();
                    break;
                default:
                    DefaultResponse();
                    break;
            }
            Response.Write(this._response);
        }

        private void ajaxCheckName()
        {
            if (q("id") == "0")
            {
                doh.Reset();
                doh.ConditionExpress = "username=@username";
                doh.AddConditionParameter("@username", q("txtUserName"));
                if (doh.Exist("jcms_normal_user"))
                    this._response = JsonResult(0, "不可添加");
                else
                    this._response = JsonResult(1, "可以添加");
            }
            else
                this._response = JsonResult(1, "可以修改");
        }
        private void DefaultResponse()
        {
            this._response = JsonResult(0, "未知操作");
        }
        private void ajaxGetList()
        {
            string keys = q("keys");
            int gId = Str2Int(q("gId"), 0);
            int page = Int_ThisPage();
            int PSize = Str2Int(q("pagesize"), 20);
            int countNum = 0;
            string sqlStr = "";
            string joinStr = "A.[Group]=B.Id";
            string whereStr1 = "1=1";//外围条件(带A.)
            string whereStr2 = "1=1";//分页条件(不带A.)
            if (keys.Trim().Length > 0)
            {
                whereStr1 += " and A.UserName LIKE '%" + keys + "%'";
                whereStr2 += " and UserName LIKE '%" + keys + "%'";
            }
            if (gId > 0)
            {
                whereStr1 += " and a.[Group]=" + gId.ToString();
                whereStr2 += " and [Group]=" + gId.ToString();
            }
            doh.Reset();
            doh.ConditionExpress = whereStr2;
            countNum = doh.Count("jcms_normal_user");
            sqlStr = JumboTCMS.Utils.SqlHelp.GetSql("A.id as id,A.UserName as UserName,B.GroupName as GroupName,A.Email as email,A.Points as points,A.Integral as integral,A.IsVIP as isvip,A.VIPTime as viptime,A.state as state,A.TrueName as truename,A.AdminId as adminid,A.ServiceId as serviceid", "jcms_normal_user", "jcms_normal_usergroup", "Id", PSize, page, "desc", joinStr, whereStr1, whereStr2);
            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dt = doh.GetDataTable();
            this._response = "{result :\"1\"," +
                "returnval :\"操作成功\"," +
                "pagerbar :\"" + JumboTCMS.Utils.HtmlPager.GetPageBar(3, "js", 2, countNum, PSize, page, "javascript:ajaxList(<#page#>);") + "\"," +
                JumboTCMS.Utils.dtHelp.DT2JSON(dt) +
                "}";
            dt.Clear();
            dt.Dispose();
        }
        private void GetUserInfo()
        {
            string _userid = Str2Str(q("id"));
            int page = 1;
            int PSize = 1;
            int countNum = 0;
            string sqlStr = "";
            string joinStr = "A.[Group]=B.Id";
            string whereStr1 = "A.Id=" + _userid;
            string whereStr2 = "Id=" + _userid;
            doh.Reset();
            doh.ConditionExpress = whereStr2;
            countNum = doh.Count("jcms_normal_user");
            sqlStr = JumboTCMS.Utils.SqlHelp.GetSql("A.*,B.GroupName", "jcms_normal_user", "jcms_normal_usergroup", "Id", PSize, page, "desc", joinStr, whereStr1, whereStr2);
            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dt = doh.GetDataTable();
            this._response = "{result :\"" + countNum + "\"," + JumboTCMS.Utils.dtHelp.DT2JSON(dt) + "}";
            dt.Clear();
            dt.Dispose();
        }
        private void ajaxDel()
        {
            string uId = f("id");
            doh.Reset();
            doh.ConditionExpress = "id=@id and adminid=0";
            doh.AddConditionParameter("@id", uId);
            int _delCount = doh.Delete("jcms_normal_user");
            UserGroupCount("0");
            if (_delCount > 0)
                this._response = JsonResult(1, "成功删除");
            else
                this._response = JsonResult(0, "管理人员不能直接删除");
        }
        /// <summary>
        /// 执行批量操作
        /// </summary>
        /// <param name="oper"></param>
        /// <param name="ids"></param>
        private void ajaxBatchOper()
        {
            string act = q("act");
            string togid = f("togid");
            string ids = f("ids");
            BatchUser(act, togid, ids, "json");
            UserGroupCount("0");
            this._response = JsonResult(1, "操作成功");
        }
        /// <summary>
        /// 执行用户的审核,用户组转移等操作
        /// </summary>
        /// <param name="_act">操作类型{pass=审核,nopass=未审,move2group=转移用户组}</param>
        /// <param name="_ids">id字符串,以","串联起来</param>
        /// <param name="pageType">页面分为html和json</param>
        public void BatchUser(string _act, string _togid, string _ids, string pageType)
        {
            string[] idValue;
            idValue = _ids.Split(',');
            if (_act == "pass")
            {
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "id=" + idValue[i];
                    doh.AddFieldItem("State", 1);
                    doh.Update("jcms_normal_user");
                }
                return;
            }
            if (_act == "nopass")
            {
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "id=" + idValue[i];
                    doh.AddFieldItem("State", 0);
                    doh.Update("jcms_normal_user");
                }
                return;
            }
            if (_act == "move2group")
            {
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "id=" + idValue[i];
                    doh.AddFieldItem("Group", _togid);
                    doh.Update("jcms_normal_user");
                }
                return;
            }
        }
    }
}