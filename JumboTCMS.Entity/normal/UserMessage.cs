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
    /// 会员短信-------表映射实体
    /// </summary>

    public class Normal_UserMessage
    {
        public Normal_UserMessage()
        { }

        private string _id;
        private string _title;
        private string _content;
        private string _sendip;
        private int _senduserid;
        private int _receiveuserid;
        private string _receiveusername;
        private DateTime _adddate;
        private int _state;
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
        public string Title
        {
            set { _title = value; }
            get { return _title; }
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
        public string SendIP
        {
            set { _sendip = value; }
            get { return _sendip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int SendUserId
        {
            set { _senduserid = value; }
            get { return _senduserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ReceiveUserId
        {
            set { _receiveuserid = value; }
            get { return _receiveuserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReceiveUserName
        {
            set { _receiveusername = value; }
            get { return _receiveusername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime AddDate
        {
            set { _adddate = value; }
            get { return _adddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int State
        {
            set { _state = value; }
            get { return _state; }
        }


    }
}

