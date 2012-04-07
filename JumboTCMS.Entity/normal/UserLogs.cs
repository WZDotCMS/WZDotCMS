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
    /// 会员日志-------表映射实体
    /// </summary>

    public class Normal_UserLogs
    {
        public Normal_UserLogs()
        { }

        private string _id;
        private int _userid;
        private string _operinfo;
        private int _opertype;
        private DateTime _opertime;
        private string _operip;
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
        public int UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OperInfo
        {
            set { _operinfo = value; }
            get { return _operinfo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int OperType
        {
            set { _opertype = value; }
            get { return _opertype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime OperTime
        {
            set { _opertime = value; }
            get { return _opertime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OperIP
        {
            set { _operip = value; }
            get { return _operip; }
        }


    }
}

