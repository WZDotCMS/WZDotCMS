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
    /// 评论-------表映射实体
    /// </summary>

    public class Normal_Review
    {
        public Normal_Review()
        { }

        private string _id = "0";
        private int _channelid;
        private int _parentid;
        private int _contentid;
        private DateTime _adddate;
        private string _content;
        private string _ip;
        private string _username;
        private int _ispass;
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
        /// 父级ID
        /// </summary>
        public int ParentId
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 内容ID
        /// </summary>
        public int ContentId
        {
            set { _contentid = value; }
            get { return _contentid; }
        }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime AddDate
        {
            set { _adddate = value; }
            get { return _adddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Content
        {
            set { _content = value; }
            get { return _content; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IP
        {
            set { _ip = value; }
            get { return _ip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 状态:0表示未审;1表示审核
        /// </summary>
        public int IsPass
        {
            set { _ispass = value; }
            get { return _ispass; }
        }


    }
}

