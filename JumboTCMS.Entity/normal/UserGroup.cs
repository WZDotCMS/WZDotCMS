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
    /// 会员组-------表映射实体
    /// </summary>

    public class Normal_UserGroup
    {
        public Normal_UserGroup()
        { }

        private string _id;
        private string _groupname;
        private string _setting;
        private int _islogin;
        private int _usertotal;
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
        public string GroupName
        {
            set { _groupname = value; }
            get { return _groupname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Setting
        {
            set { _setting = value; }
            get { return _setting; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int IsLogin
        {
            set { _islogin = value; }
            get { return _islogin; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int UserTotal
        {
            set { _usertotal = value; }
            get { return _usertotal; }
        }


    }
}

