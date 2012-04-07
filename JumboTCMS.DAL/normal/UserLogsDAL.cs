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
using JumboTCMS.DBUtility;

namespace JumboTCMS.DAL
{
    /// <summary>
    /// 会员日志表信息
    /// </summary>
    public class Normal_UserLogsDAL : Common
    {
        public Normal_UserLogsDAL()
        {
            base.SetupSystemDate();
        }
        /// <summary>
        /// 保存用户日志
        /// </summary>
        /// <param name="_uid">会员ID</param>
        /// <param name="_info">保存信息</param>
        /// <param name="_type">操作类型,1=分组移动,2=扣除博币,3=积分增加(2,3为系统操作),4=增加博币,5=VIP升级,6积分扣除(4,5,6为管理员操作)</param>
        public void SaveLog(string _uid, string _info, int _type)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.AddFieldItem("UserId", _uid);
                _doh.AddFieldItem("OperInfo", _info);
                _doh.AddFieldItem("OperType", _type);
                _doh.AddFieldItem("OperTime", DateTime.Now.ToString());
                _doh.AddFieldItem("OperIP", IPHelp.ClientIP);
                _doh.Insert("jcms_normal_user_logs");
            }
        }
    }
}
