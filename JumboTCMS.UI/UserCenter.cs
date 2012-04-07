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
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using System.Text;
using JumboTCMS.Utils;
namespace JumboTCMS.UI
{
    /// <summary>
    /// </summary>
    public class UserCenter : FrontHtml
    {
        public int publicMenu = 2;
        public string ChannelId = "0";
        public string ChannelName = string.Empty;
        public string ChannelType = "system";
        public string ChannelDir = string.Empty;
        public string ChannelItemName = string.Empty;
        public string ChannelItemUnit = string.Empty;
        public int ChannelClassDepth = 0;
        public bool ChannelIsHtml = false;
        public string id = "0";
        protected string UserId = "0";
        protected string UserName = string.Empty;
        protected int UserPoints = 0;
        protected string UserNickName = string.Empty;
        protected string UserPass = string.Empty;
        protected string UserKey = string.Empty;
        protected string UserEmail = string.Empty;
        protected string UserGroupId = "0";
        protected string UserPower = string.Empty;
        protected string UserSetting = string.Empty;
        protected string UserCookies = string.Empty;
        protected bool UserIsLogin = false;

        /// <summary>
        /// 验证登陆
        /// </summary>
        private void chkLogin()
        {
            if (Cookie.GetValue(site.CookiePrev + "user") != null)
            {
                UserId = Cookie.GetValue(site.CookiePrev + "user", "id");
                UserGroupId = Cookie.GetValue(site.CookiePrev + "user", "groupid");
                UserName = Cookie.GetValue(site.CookiePrev + "user", "name");
                UserNickName = Cookie.GetValue(site.CookiePrev + "user", "nickname");
                UserPass = Cookie.GetValue(site.CookiePrev + "user", "password");
                UserEmail = Cookie.GetValue(site.CookiePrev + "user", "email");
                UserKey = UserPass.Substring(4, 8);
                UserSetting = Cookie.GetValue(site.CookiePrev + "user", "setting");
                UserCookies = Cookie.GetValue(site.CookiePrev + "user", "cookies");
                if (UserId.Length != 0 && UserName.Length != 0)
                {
                    JumboTCMS.Entity.Normal_User _User = new JumboTCMS.DAL.Normal_UserDAL().GetEntity(UserId);
                    if (_User.UserName.Length > 0)
                    {
                        this.UserIsLogin = true;
                        UserPoints = _User.Points;//需要实时获取
                    }
                }
            }
        }
        /// <summary>
        /// 验证权限
        /// </summary>
        /// <param name="s">空时只要求登录</param>
        private bool IsPower(string s)
        {
            if (s == "ok") return true;
            if (!this.UserIsLogin)//验证一次本地信息
                chkLogin();
            if (s == "") return (this.UserIsLogin);
            return (this.UserPower.Contains("," + s + ","));
        }

        /// <summary>
        /// 验证权限
        /// </summary>
        /// <param name="s"></param>
        /// <param name="pageType">页面分为html和json</param>
        private void chkPower(string s, string pageType)
        {
            if (!IsPower(s))
            {
                showErrMsg("权限不足或未登录", pageType);
            }
        }
        /// <summary>
        /// 输出错误信息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="pageType">页面分为html和json</param>
        private void showErrMsg(string msg, string pageType)
        {
            if (pageType != "json")
                FinalMessage(msg, site.Dir + "passport/login.aspx", 0);
            else
            {
                HttpContext.Current.Response.Clear();
                if (!this.UserIsLogin)
                    HttpContext.Current.Response.Write(JsonResult(-1, msg));
                else
                    HttpContext.Current.Response.Write(JsonResult(0, msg));
                HttpContext.Current.Response.End();
            }
        }
        /// <summary>
        /// 管理中心初始
        /// </summary>
        /// <param name="powerNum">权限,为空表示验证是否登录</param>
        /// <param name="pageType">页面分为html和json</param>
        protected void User_Load(string powerNum, string pageType)
        {
            chkPower(powerNum, pageType);
        }

        /// <summary>
        /// 管理中心初始,并获得频道的各项参数值
        /// </summary>
        /// <param name="powerNum">权限</param>
        /// <param name="isChannel">如果为false就表示ChannelId可以为0</param>
        protected void User_Load(string powerNum, string pageType, bool isChannel)
        {
            chkPower(powerNum, pageType);
            if (isChannel && ChannelId == "0")
            {
                showErrMsg("参数错误,请不要在外部提交数据", pageType);
                return;
            }
            if (ChannelId != "0")
            {
                JumboTCMS.Entity.Normal_Channel _Channel = new JumboTCMS.DAL.Normal_ChannelDAL().GetEntity(ChannelId);
                ChannelName = _Channel.Title;
                ChannelDir = _Channel.Dir;
                ChannelType = _Channel.Type;
                ChannelItemName = _Channel.ItemName;
                ChannelItemUnit = _Channel.ItemUnit;
                ChannelClassDepth = _Channel.ClassDepth;
                ChannelIsHtml = _Channel.IsHtml;
            }
        }
        /// <summary>
        /// 管理菜单
        /// </summary>
        /// <returns></returns>
        protected string[,] leftMenu()
        {
            doh.Reset();
            doh.SqlCmd = "SELECT Id,Title,[Type] FROM [jcms_normal_modules] WHERE [Enabled]=1 AND [Type] in('article','photo','soft','video') ORDER BY pId";
            DataTable dtModule = doh.GetDataTable();
            //下面的'$'后面的'0'表示系统
            string[,] menu = new string[publicMenu + dtModule.Rows.Count, 30];
            menu[0, 0] = "个人面板$0$config";
            menu[0, 1] = "|密码修改";
            menu[0, 2] = "|资料修改";

            menu[1, 0] = "收藏夹$0$fav";

            string mModuleId = string.Empty;
            string mModuleName = string.Empty;
            string mModuleType = string.Empty;
            int j = 0;
            for (int i = 0; i < dtModule.Rows.Count; i++)
            {
                j = i + publicMenu;
                mModuleId = dtModule.Rows[i]["Id"].ToString();
                mModuleName = dtModule.Rows[i]["Title"].ToString();
                mModuleType = dtModule.Rows[i]["Type"].ToString().ToLower();
                menu[j, 0] = mModuleName + "$1$m_" + mModuleType;
                doh.Reset();
                doh.SqlCmd = "SELECT Id,Title FROM [jcms_normal_channel] WHERE [Enabled]=1 AND [IsPost]=1 AND [Type]='" + mModuleType + "' ORDER BY pId";
                DataTable dtChannel = doh.GetDataTable();
                string mChannelId = string.Empty;
                string mChannelName = string.Empty;
                for (int m = 0; m < dtChannel.Rows.Count; m++)
                {
                    mChannelId = dtChannel.Rows[m]["Id"].ToString();
                    mChannelName = dtChannel.Rows[m]["Title"].ToString();
                    menu[j, m + 1] = "../modules/" + mModuleType + "_user_list.aspx?ccid=" + mChannelId + "&ctype=" + mModuleType + "|" + mChannelName;
                }
                dtChannel.Clear();
                dtChannel.Dispose();
            }
            dtModule.Clear();
            dtModule.Dispose();
            return menu;
        }
        /// <summary>
        /// 编辑内容时,向栏目专题标题颜色等DropDownList中添加内容
        /// </summary>
        /// <param name="ddlClassId">栏目ID</param>
        /// <param name="ClassDepth">栏目深度</param>
        protected void getEditDropDownList(ref DropDownList ddlClassId, int ClassDepth)
        {
            if (!Page.IsPostBack)
            {
                doh.Reset();
                doh.SqlCmd = "SELECT [ID],[Title],[code] FROM [jcms_normal_class] WHERE [IsOut]=0 AND [IsPost]=1 AND [ChannelId]=" + ChannelId;
                if (ClassDepth > 0)
                    doh.SqlCmd += " AND left(code)<=" + (4 * ClassDepth).ToString();
                doh.SqlCmd += " ORDER BY code";
                DataTable dtClass = doh.GetDataTable();
                if (dtClass.Rows.Count == 0)
                {
                    dtClass.Clear();
                    dtClass.Dispose();
                    FinalMessage("此频道没有可以发表内容的栏目", site.Dir + "user/close.htm", 0);
                    return;
                }
                for (int i = 0; i < dtClass.Rows.Count; i++)
                {
                    ddlClassId.Items.Add(new ListItem(getListName(dtClass.Rows[i]["Title"].ToString(), dtClass.Rows[i]["code"].ToString()), dtClass.Rows[i]["Id"].ToString()));
                }
                dtClass.Clear();
                dtClass.Dispose();
            }
        }
        /// <summary>
        /// 取得内容列表
        /// </summary>
        /// <param name="_classId">栏目Id</param>
        /// <param name="keyType">搜索关键字类型{Author,title,summary}</param>
        /// <param name="keyWord">搜索关键字</param>
        /// <param name="sDate">日期{1d=今天,1w=本周,1m=本月}</param>
        /// <param name="isPass">是否审核{0=未审,1=已审,-1=已删,否则=全部}</param>
        /// <param name="isTop">是否推荐{0=不推荐,1=推荐,否则=全部}</param>
        /// <param name="PSize">每页记录数</param>
        /// <param name="page">页码</param>
        protected string GetContentList(string _ctype, string _classId, string keyType, string keyWord, string sDate, string isPass, string isTop, int PSize, int page)
        {
            _classId = JumboTCMS.Utils.Strings.SafetyStr(_classId);
            keyType = JumboTCMS.Utils.Strings.SafetyStr(keyType);
            keyWord = JumboTCMS.Utils.Strings.SafetyStr(keyWord);
            sDate = JumboTCMS.Utils.Strings.SafetyStr(sDate);
            int countNum = 0;
            string sqlStr = "";
            string joinStr = "A.[ClassId]=B.Id";
            string whereStr1 = "A.UserId=" + UserId + " AND A.ChannelId=" + ChannelId;//外围条件(带A.)
            string whereStr2 = "UserId=" + UserId + " AND ChannelId=" + ChannelId;//分页条件(不带A.)
            if (_classId != "0")
            {
                whereStr1 += " AND A.ClassId=" + _classId;
                whereStr2 += " AND ClassId=" + _classId;
            }
            if ((keyType.Length > 0) && (keyWord.Length > 0))
            {
                whereStr1 += " and A." + keyType + " LIKE '%" + keyWord + "%'";
                whereStr2 += " and " + keyType + " LIKE '%" + keyWord + "%'";
            }
            switch (isPass)
            {
                case "0":
                    whereStr1 += " AND A.IsPass=0";
                    whereStr2 += " AND IsPass=0";
                    break;
                case "1":
                    whereStr1 += " AND A.IsPass=1";
                    whereStr2 += " AND IsPass=1";
                    break;
                case "-1":
                    whereStr1 += " AND A.IsPass=-1";
                    whereStr2 += " AND IsPass=-1";
                    break;
                default:
                    break;
            }
            switch (isTop)
            {
                case "0":
                    whereStr1 += " AND A.IsTop=0";
                    whereStr2 += " AND IsTop=0";
                    break;
                case "1":
                    whereStr1 += " AND A.IsTop=1";
                    whereStr2 += " AND IsTop=1";
                    break;
                default:
                    break;
            }
            if (sDate != "")
            {
                if (DBType == "0")
                {
                    switch (sDate)
                    {
                        case "1d":
                            whereStr1 += " AND datediff('d',A.adddate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            whereStr2 += " AND datediff('d',adddate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                        case "1w":
                            whereStr1 += " AND datediff('ww',A.adddate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            whereStr2 += " AND datediff('ww',adddate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                        case "1m":
                            whereStr1 += " AND datediff('m',A.adddate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            whereStr2 += " AND datediff('m',adddate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                    }
                }
                else
                {
                    switch (sDate)
                    {
                        case "1d":
                            whereStr1 += " AND datediff(d,A.adddate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            whereStr2 += " AND datediff(d,adddate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                        case "1w":
                            whereStr1 += " AND datediff(ww,A.adddate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            whereStr2 += " AND datediff(ww,adddate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                        case "1m":
                            whereStr1 += " AND datediff(m,A.adddate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            whereStr2 += " AND datediff(m,adddate,'" + DateTime.Now.ToShortDateString() + "')=0";
                            break;
                    }
                }
            }
            doh.Reset();
            //doh.SqlCmd = "SELECT count(Id) as CC FROM [jcms_module_" + _ctype + "] WHERE " + whereStr2;
            //countNum = Convert.ToInt32(doh.GetDataTable().Rows[0]["cc"].ToString());
            doh.ConditionExpress = whereStr2;
            countNum = doh.Count("jcms_module_" + _ctype);
            sqlStr = JumboTCMS.Utils.SqlHelp.GetSql("A.id as id,A.Title as title,B.Title as classname,A.adddate as adddate,A.IsPass as IsPass,A.IsImg as IsImg,A.IsTop as IsTop,A.IsFocus as IsFocus,A.FirstPage", "jcms_module_" + _ctype, "jcms_normal_class", "id", PSize, page, "desc", joinStr, whereStr1, whereStr2);
            doh.Reset();
            doh.SqlCmd = sqlStr;
            DataTable dt = doh.GetDataTable();
            return "{result :\"1\"," +
                "returnval :\"操作成功\"," +
                "pagerbar :\"" + JumboTCMS.Utils.HtmlPager.GetPageBar(3, "js", 2, countNum, PSize, page, "javascript:ajaxList(<#page#>);") + "\"," +
                JumboTCMS.Utils.dtHelp.DT2JSON(dt, (PSize * (page - 1))) +
                "}";
        }
        /// <summary>
        /// 判断编辑权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="_cType"></param>
        /// <returns></returns>
        protected void checkEdit(string id, string _cType)
        {
            string[] setting = (string[])UserSetting.Split(',');
            bool _CanEdit = (setting[9] == "1");
            if (!_CanEdit)
                FinalMessage("您所在的组无权发布", site.Dir + "user/close.htm", 0);
            else
                if (id != "0")
                {
                    doh.Reset();
                    doh.ConditionExpress = "id=" + id + " and UserId=" + UserId + " and IsPass=0";
                    if (!doh.Exist("jcms_module_" + _cType))
                        FinalMessage("内容已经审核或不是你发表的", site.Dir + "user/close.htm", 0);
                }
        }
        /// <summary>
        /// 判断删除权限
        /// </summary>
        /// <param name="id"></param>
        /// <param name="_cType"></param>
        /// <returns></returns>
        protected string checkDel(string id, string _cType)
        {
            doh.Reset();
            doh.ConditionExpress = "id=" + id + " and UserId=" + UserId + " and IsPass=0";
            if (!doh.Exist("jcms_module_" + _cType))
                return JsonResult(0, "内容已经审核或不是你发表的");
            else
            {
                doh.ConditionExpress = "UserId=" + UserId + " AND id=" + id;
                doh.Delete("jcms_module_" + _cType);
                return JsonResult(1, "成功删除");
            }
        }
    }
}