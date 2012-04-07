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
    /// 留言-------表映射实体
    /// </summary>

    public class Normal_Question
    {
        public Normal_Question()
        { }
        public Normal_Question(
            string id,
            int parentid,
            DateTime adddate,
            string title,
            string content,
            string ip,
            string username,
            int userid,
            int classid,
            int ispass
            )
        {
            this._id = id;
            this._parentid = parentid;
            this._adddate = adddate;
            this._title = title;
            this._content = content;
            this._ip = ip;
            this._username = username;
            this._userid = userid;
            this._classid = classid;
            this._ispass = ispass;
        }
        private string _id;
        private int _parentid;
        private DateTime _adddate;
        private string _title;
        private string _content;
        private string _ip;
        private string _username;
        private int _userid;
        private int _classid;
        private int _ispass;
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
        public int ParentId
        {
            set { _parentid = value; }
            get { return _parentid; }
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
        public int ClassId
        {
            set { _classid = value; }
            get { return _classid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int IsPass
        {
            set { _ispass = value; }
            get { return _ispass; }
        }


    }
}

