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
    public partial class _pointscard_ajax : JumboTCMS.UI.AdminCenter
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
                case "ajaxSortList":
                    ajaxSortList();
                    break;
                case "ajaxBatchOper":
                    ajaxBatchOper();
                    break;
                case "ajaxBatchAdd":
                    ajaxBatchAdd();
                    break;
                case "ajaxOffer":
                    ajaxOffer();
                    break;
                case "ajaxBindUser":
                    ajaxBindUser();
                    break;
                case "ajaxDel":
                    ajaxDel();
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
            string keys = q("keys");
            int gId = Str2Int(q("gId"), 0);
            int page = Int_ThisPage();
            int PSize = Str2Int(q("pagesize"), 20);
            int countNum = 0;
            string sqlStr = "";
            string joinStr = "A.[UserId]=B.Id";
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
            countNum = doh.Count("jcms_normal_pointscard");
            sqlStr = JumboTCMS.Utils.SqlHelp.GetSql("A.id as id,A.CardNumber as CardNumber,A.CardPassword as CardPassword,B.UserName as UserName,A.Points as points,A.LimitedDate as LimitedDate,A.State as State,A.ActiveTime as ActiveTime,A.ActiveIP as ActiveIP", "jcms_normal_pointscard", "jcms_normal_user", "Id", PSize, page, "desc", joinStr, whereStr1, whereStr2);
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
        private void ajaxSortList()
        {
            doh.Reset();
            doh.SqlCmd = "SELECT [Points],[Title] FROM [jcms_normal_pointscard_sort] ORDER BY Id asc";
            DataTable dt = doh.GetDataTable();
            this._response = "{result :\"1\"," +
                "returnval :\"操作成功\"," +
                JumboTCMS.Utils.dtHelp.DT2JSON(dt) +
                "}";
            dt.Clear();
            dt.Dispose();
        }
        private void ajaxDel()
        {
            string cId = f("id");
            doh.Reset();
            doh.ConditionExpress = "id=@id and State<2";//只有状态小于2的记录才可以删除
            doh.AddConditionParameter("@id", cId);
            int _delCount = doh.Delete("jcms_normal_pointscard");
            if (_delCount > 0)
                this._response = JsonResult(1, "删除成功");
            else
                this._response = JsonResult(0, "删除有误");
        }
        private void ajaxOffer()
        {
            string cId = f("id");
            doh.Reset();
            doh.ConditionExpress = "id=@id and State=1";//只有状态为库存的才能标识
            doh.AddConditionParameter("@id", cId);
            doh.AddFieldItem("State", 2);
            int _updateCount = doh.Update("jcms_normal_pointscard");
            if (_updateCount > 0)
                this._response = JsonResult(1, "标识成功");
            else
                this._response = JsonResult(0, "标识有误");
        }
        private void ajaxBindUser()
        {
            string cId = f("id");
            doh.Reset();
            doh.ConditionExpress = "id=@id and State=1";//只有状态为库存的才能标识
            doh.AddConditionParameter("@id", cId);
            doh.AddFieldItem("State", 3);
            int _updateCount = doh.Update("jcms_normal_pointscard");
            if (_updateCount > 0)
            {
                doh.Reset();
                doh.ConditionExpress = "id=@id and State=3";
                doh.AddConditionParameter("@id", cId);
                object[] _value = doh.GetFields("jcms_normal_pointscard", "CardNumber,CardPassword,Points");
                string _CardNumber = _value[0].ToString();
                string _CardPassword = _value[1].ToString();
                int _Points = Str2Int(_value[2].ToString());
                //创建一个新用户
                int dPoints = Str2Int(JumboTCMS.Utils.XmlCOM.ReadConfig("~/_data/config/site", "DefaultPoints"), 0);
                doh.Reset();
                doh.AddFieldItem("UserName", _CardNumber);
                doh.AddFieldItem("UserPass", JumboTCMS.Utils.MD5.Lower32(_CardPassword));
                doh.AddFieldItem("Sex", 0);
                doh.AddFieldItem("Email", "@");
                doh.AddFieldItem("Birthday", "1980-1-1");
                doh.AddFieldItem("Group", 1);
                doh.AddFieldItem("Points", _Points);
                doh.AddFieldItem("Login", 0);
                doh.AddFieldItem("State", 1);
                doh.AddFieldItem("AdminId", 0);
                doh.AddFieldItem("Setting", ",,");
                doh.AddFieldItem("AdminState", 0);
                doh.AddFieldItem("IsVIP", 0);
                doh.AddFieldItem("Integral", 0);
                doh.AddFieldItem("RegTime", DateTime.Now.ToString());
                doh.AddFieldItem("RegIp", Const.GetUserIp);
                doh.Insert("jcms_normal_user");
                doh.Reset();
                doh.ConditionExpress = "id=1";
                doh.Add("jcms_normal_usergroup", "UserTotal");
                this._response = JsonResult(1, "绑定成功");
            }
            else
                this._response = JsonResult(0, "绑定有误");

        }
        /// <summary>
        /// 执行增加充值卡
        /// </summary>
        private void ajaxBatchAdd()
        {
            if (!CheckFormUrl())
                Response.End();
            int _cardCount = Str2Int(f("txtCardCount"));
            int _cardNumberLen = Str2Int(f("txtCardNumberLen"));
            int _cardPasswordLen = Str2Int(f("txtCardPasswordLen"));
            int _cardPoints = Str2Int(f("ddlPoints"));
            DateTime _cardLimitedDate = DateTime.Parse(f("txtCardLimitedDate"));
            if (_cardCount > 0 && _cardCount <= 100)
            {
                Random random = new Random();
                for (int i = 0; i < _cardCount; i++)
                {
                    string _cardNumber = GetRandomNumberString(_cardNumberLen, true, random);
                    string _cardPassword = GetRandomNumberString(_cardPasswordLen, true, random);
                    doh.Reset();
                    doh.AddFieldItem("CardNumber", _cardNumber);
                    doh.AddFieldItem("CardPassword", _cardPassword);
                    doh.AddFieldItem("State", 1);
                    doh.AddFieldItem("LimitedDate", _cardLimitedDate);
                    doh.AddFieldItem("UserId", 0);
                    doh.AddFieldItem("Points", _cardPoints);
                    doh.AddFieldItem("ActiveIP", "0.0.0.0");
                    doh.AddFieldItem("ActiveTime", DateTime.Now.ToString());
                    doh.Insert("jcms_normal_pointscard");
                }
                this._response = "alert('" + _cardCount + "张充值卡添加成功。');top.IframeOper.ajaxList(1);top.JumboTCMS.Popup.hide();";
            }
            else
            {
                this._response = "alert('表单提交有误。');";
            }
        }
        /// <summary>
        /// 执行批量操作
        /// </summary>
        /// <param name="oper"></param>
        /// <param name="ids"></param>
        private void ajaxBatchOper()
        {
            string act = q("act");
            string ids = f("ids");
            BatchPointsCard(act, ids);
            this._response = JsonResult(1, "操作成功");
        }
        /// <summary>
        /// 执行用户的审核,用户组转移等操作
        /// </summary>
        /// <param name="_act">操作类型{pass=审核,nopass=未审}</param>
        /// <param name="_ids">id字符串,以","串联起来</param>
        public void BatchPointsCard(string _act, string _ids)
        {
            string[] idValue;
            idValue = _ids.Split(',');
            if (_act == "pass")
            {
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "State=0 AND id=" + idValue[i];//只有状态为锁定中的才能激活
                    doh.AddFieldItem("State", 1);
                    doh.Update("jcms_normal_pointscard");
                }
                return;
            }
            if (_act == "nopass")
            {
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "State=1 AND id=" + idValue[i];//只有状态为库存的才能锁定
                    doh.AddFieldItem("State", 0);
                    doh.Update("jcms_normal_pointscard");
                }
                return;
            }
            if (_act == "del")
            {
                for (int i = 0; i < idValue.Length; i++)
                {
                    doh.Reset();
                    doh.ConditionExpress = "State<2 AND id=" + idValue[i];//只有状态小于2的记录才可以删除
                    doh.AddFieldItem("State", 1);
                    doh.Delete("jcms_normal_pointscard");
                }
                return;
            }
        }
    }
}