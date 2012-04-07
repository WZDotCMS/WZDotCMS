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
    /// 缩略图表信息
    /// </summary>
    public class Normal_ThumbsDAL : Common
    {
        public Normal_ThumbsDAL()
        {
            base.SetupSystemDate();
        }
        /// <summary>
        /// 得到数据表
        /// </summary>
        /// <param name="_channelid"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string _channelid)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                if (_channelid == "0")
                    _doh.SqlCmd = "SELECT ID,Title,iWidth,iHeight FROM [jcms_normal_thumbs] ORDER BY ChannelID,ID";
                else
                    _doh.SqlCmd = "SELECT ID,Title,iWidth,iHeight FROM [jcms_normal_thumbs] WHERE [ChannelId]=" + _channelid + " OR [ChannelId]=0 ORDER BY ChannelID,ID";
                DataTable dt = _doh.GetDataTable();
                return dt;
            }

        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteByID(string _id)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                _doh.Reset();
                _doh.ConditionExpress = "id=@id";
                _doh.AddConditionParameter("@id", _id);
                int _del = _doh.Delete("jcms_normal_thumbs");
                return (_del == 1);
            }

        }
    }
}
