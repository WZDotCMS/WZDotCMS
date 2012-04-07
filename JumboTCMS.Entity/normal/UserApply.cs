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
    /// 用户申请-------表映射实体
    /// </summary>

    public class Normal_UserApply
    {
        public Normal_UserApply()
        { }

        private string _id;
        private int _userid;
        private string _applyinfo;
        private int _applytype;
        private DateTime _applytime;
        private string _applyip;
        private string _usersign;
        private int _applynumber;
        private int _checked;
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
        public string ApplyInfo
        {
            set { _applyinfo = value; }
            get { return _applyinfo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ApplyType
        {
            set { _applytype = value; }
            get { return _applytype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ApplyTime
        {
            set { _applytime = value; }
            get { return _applytime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ApplyIP
        {
            set { _applyip = value; }
            get { return _applyip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserSign
        {
            set { _usersign = value; }
            get { return _usersign; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ApplyNumber
        {
            set { _applynumber = value; }
            get { return _applynumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Checked
        {
            set { _checked = value; }
            get { return _checked; }
        }


    }
}

