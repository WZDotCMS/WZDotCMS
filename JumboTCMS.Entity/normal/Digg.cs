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
namespace JumboTCMS.Entity
{
    /// <summary>
    /// 顶客-------表映射实体
    /// </summary>

    public class Normal_Digg
    {
        public Normal_Digg()
        { }

        private string _id;
        private int _contentid;
        private string _channeltype;
        private int _diggnum;
        /// <summary>
        /// 
        /// </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ContentId
        {
            set { _contentid = value; }
            get { return _contentid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ChannelType
        {
            set { _channeltype = value; }
            get { return _channeltype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int DiggNum
        {
            set { _diggnum = value; }
            get { return _diggnum; }
        }


    }
}

