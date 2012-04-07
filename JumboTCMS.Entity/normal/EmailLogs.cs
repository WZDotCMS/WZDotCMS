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
    /// 发信日志-------表映射实体
    /// </summary>

    public class Normal_Emaillogs
    {
        public Normal_Emaillogs()
        { }

        private string _id;
        private int _adminid;
        private string _sendtitle;
        private string _sendusers;
        private DateTime _sendtime;
        private string _sendip;
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
        public int AdminId
        {
            set { _adminid = value; }
            get { return _adminid; }
        }
        /// <summary>
        /// 发信标题
        /// </summary>
        public string SendTitle
        {
            set { _sendtitle = value; }
            get { return _sendtitle; }
        }
        /// <summary>
        /// 发信收件人
        /// </summary>
        public string SendUsers
        {
            set { _sendusers = value; }
            get { return _sendusers; }
        }
        /// <summary>
        /// 发信时间
        /// </summary>
        public DateTime SendTime
        {
            set { _sendtime = value; }
            get { return _sendtime; }
        }
        /// <summary>
        /// 发信IP
        /// </summary>
        public string SendIP
        {
            set { _sendip = value; }
            get { return _sendip; }
        }


    }
}

