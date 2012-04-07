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
    /// 模型内容业务类
    /// </summary>
    public class ModuleContentDAL : Common
    {
        public ModuleContentDAL()
        {
            base.SetupSystemDate();
        }
        /// <summary>
        /// 获得内容的某些属性(第一个是时间，第二个是内容页另名)
        /// </summary>
        /// <param name="_channelid">频道ID</param>
        /// <param name="_channeltype">频道模型</param>
        /// <param name="_contentid">内容ID</param>
        /// <returns></returns>
        public object[] GetSome(string _channelid, string _channeltype, string _contentid)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "ChannelId=" + _channelid + " and Id=" + _contentid;
                return _doh.GetFields("jcms_module_" + _channeltype, "AddDate,FirstPage,AliasPage");
            }
        }
    }
}
