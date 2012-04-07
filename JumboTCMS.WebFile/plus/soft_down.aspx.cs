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
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Modules.Soft.Plus
{
    public partial class _down : JumboTCMS.UI.FrontHtml
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 5;//脚本过期时间
            if (q("userkey") != "666666")
            {
                if (q("userkey") != JumboTCMS.Utils.Cookie.GetValue(site.CookiePrev + "user", "password").Substring(4, 8))
                {
                    Response.Redirect("~/errordown.aspx");
                    Response.End();
                }
            }
            string id = Str2Str(q("id"));
            string ChannelId = Str2Str(q("ChannelId"));
            if (id == "0")
            {
                FinalMessage("请不要修改地址栏参数!", site.Dir, 0, 8);
                Response.End();
            }
            int NO = Str2Int(q("NO"));
            doh.Reset();
            doh.ConditionExpress = "ChannelId=" + ChannelId + " and id=" + id;
            object[] _obj = doh.GetFields("jcms_module_soft", "DownUrl,Points,Title");
            string downUrl = _obj[0].ToString().Replace("\r\n", "\r");
            int _Points = Str2Int(_obj[1].ToString(), 0);
            string _SoftTitle = _obj[2].ToString();
            if (downUrl != "")
            {
                if (_Points > 0)//说明是需要扣除博币的，那么肯定要判断当前用户博币够不够
                {
                    if (!CanDownFile(ChannelId, id, _Points, _SoftTitle))
                    {
                        Response.Redirect("~/errordown.aspx");
                        Response.End();
                    }
                }
                doh.Reset();
                doh.ConditionExpress = "ChannelId=" + ChannelId + " and id=" + id;
                doh.Add("jcms_module_soft", "DownNum");
                string[] _DownUrl = downUrl.Split(new string[] { "\r" }, StringSplitOptions.None);
                string _url = _DownUrl[NO];
                if (_url.Contains("|||"))
                    _url = _url.Substring(_url.IndexOf("|||") + 3, (_url.Length - _url.IndexOf("|||") - 3));
                DownloadFile(_url);
            }
            else
                FinalMessage("当前下载地址为空!", site.Dir, 0, 8);
        }
        /// <summary>
        /// 判断附件下载权限
        /// 并扣除相应的博币
        /// </summary>
        /// <param name="_ChannelId">频道ID</param>
        /// <param name="_SoftId">内容ID</param>
        /// <param name="_Points">扣除博币</param>
        /// <param name="_SoftTitle">软件名称</param>
        /// <returns></returns>
        private bool CanDownFile(string _ChannelId, string _SoftId, int _Points, string _SoftTitle)
        {
            string _UserId = "0";
            string _UserName = "";
            if (JumboTCMS.Utils.Cookie.GetValue(site.CookiePrev + "user") != null)
            {
                _UserId = JumboTCMS.Utils.Cookie.GetValue(site.CookiePrev + "user", "id");
                _UserName = JumboTCMS.Utils.Cookie.GetValue(site.CookiePrev + "user", "name");
                bool _IsVIP = new JumboTCMS.DAL.Normal_UserDAL().IsVIPUser(_UserId);
                if (!_IsVIP)//给用户扣除博币,VIP不扣除
                {
                    doh.Reset();
                    //为什么要弄一个DownDegree<50？一个用户连续下载一个文件50次是很可疑的
                    //不是刷分就是恶意下载，做个统计留意一下
                    doh.ConditionExpress = "DownDegree<50 and ChannelId=" + _ChannelId + " and SoftId=" + _SoftId + " and UserId=" + _UserId;
                    if (doh.Exist("jcms_module_soft_downlogs"))//说明已经扣过
                    {
                        doh.Reset();
                        doh.ConditionExpress = "DownDegree<50 and ChannelId=" + _ChannelId + " and SoftId=" + _SoftId + " and UserId=" + _UserId;
                        doh.Add("jcms_module_soft_downlogs", "DownDegree");
                        return true;
                    }
                    doh.Reset();
                    doh.ConditionExpress = "id=" + _UserId;
                    int _myPoints = Str2Int(doh.GetField("jcms_normal_user", "Points").ToString());
                    if (_myPoints < _Points)//说明博币不够
                        return false;
                    //扣除博币
                    new JumboTCMS.DAL.Normal_UserDAL().DeductPoints(_UserId, _Points);
                    string _OperInfo1 = "下载软件:<a href=\"" + Go2View(1, false, _ChannelId, _SoftId, false) + "\" target=\"_blank\">" + _SoftTitle + "</a>，扣除了" + _Points + "个点";
                    new JumboTCMS.DAL.Normal_UserLogsDAL().SaveLog(_UserId, _OperInfo1, 2);
                    //增加一个下载日志记录
                    doh.Reset();
                    doh.AddFieldItem("UserId", _UserId);
                    doh.AddFieldItem("ChannelId", _ChannelId);
                    doh.AddFieldItem("SoftId", _SoftId);
                    doh.AddFieldItem("Points", _Points);
                    doh.AddFieldItem("DownTime", DateTime.Now.ToString());
                    doh.AddFieldItem("DownIP", Const.GetUserIp);
                    doh.AddFieldItem("DownDegree", 1);
                    doh.Insert("jcms_module_soft_downlogs");
                }
                return true;
            }
            else
                return false;
        }
    }
}
