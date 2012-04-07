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
    /// 管理员日志-------表映射实体
    /// </summary>

    public class Normal_Adminlogs
    {
        public Normal_Adminlogs()
        { }

        private string _id;
        private int _adminid;
        private string _operinfo;
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
        public int AdminId
        {
            set { _adminid = value; }
            get { return _adminid; }
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

