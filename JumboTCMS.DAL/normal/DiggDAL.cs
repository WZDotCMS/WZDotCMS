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
using System.Collections.Generic;
using System.Web;
using JumboTCMS.Utils;
using JumboTCMS.Entity;
using JumboTCMS.DBUtility;

namespace JumboTCMS.DAL
{
    /// <summary>
    /// 内容顶客
    /// </summary>
    public class Normal_DiggDAL : Common
    {
        public Normal_DiggDAL()
        {
            base.SetupSystemDate();
        }
        /// <summary>
        /// 得到内容
        /// </summary>
        /// <param name="_channeltype"></param>
        /// <param name="_contentid"></param>
        /// <returns></returns>
        public Normal_Digg GetDigg(string _channeltype, string _contentid)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                Normal_Digg digg = new Normal_Digg();
                digg.ChannelType = _channeltype;
                digg.ContentId = Str2Int(_contentid);
                _doh.Reset();
                _doh.ConditionExpress = "channeltype=@channeltype and contentid=@contentid";
                _doh.AddConditionParameter("@channeltype", _channeltype);
                _doh.AddConditionParameter("@contentid", _contentid);
                if (!_doh.Exist("jcms_normal_digg"))
                {
                    _doh.Reset();
                    _doh.AddFieldItem("ChannelType", _channeltype);
                    _doh.AddFieldItem("ContentId", _contentid);

                    _doh.AddFieldItem("DiggNum", 0);
                    _doh.Insert("jcms_normal_digg");
                }
                _doh.Reset();
                _doh.ConditionExpress = "channeltype=@channeltype and contentid=@contentid";
                _doh.AddConditionParameter("@channeltype", _channeltype);
                _doh.AddConditionParameter("@contentid", _contentid);
                digg.DiggNum = Str2Int(_doh.GetField("jcms_normal_digg", "DiggNum").ToString());
                return digg;
            }
        }
    }
}
