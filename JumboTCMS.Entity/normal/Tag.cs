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
    /// 标签-------表映射实体
    /// </summary>

    public class Normal_Tag
    {
        public Normal_Tag()
        { }

        private string _id;
        private int _channelid;
        private string _title;
        private int _clicktimes;
        /// <summary>
        /// 编号
        /// </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 频道ID
        /// </summary>
        public int ChannelId
        {
            set { _channelid = value; }
            get { return _channelid; }
        }
        /// <summary>
        /// 标签名
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 点击数
        /// </summary>
        public int ClickTimes
        {
            set { _clicktimes = value; }
            get { return _clicktimes; }
        }


    }
}

