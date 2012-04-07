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
    /// QQ在线客服-------表映射实体
    /// </summary>

    public class Extends_QQOnline
    {
        public Extends_QQOnline()
        { }
        public Extends_QQOnline(
            string id,
            string qqid,
            string title,
            string tcolor,
            string face
            )
        {
            this._id = id;
            this._qqid = qqid;
            this._title = title;
            this._tcolor = tcolor;
            this._face = face;
        }
        private string _id;
        private string _qqid;
        private string _title;
        private string _tcolor;
        private string _face;
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
        public string QQID
        {
            set { _qqid = value; }
            get { return _qqid; }
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
        public string TColor
        {
            set { _tcolor = value; }
            get { return _tcolor; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Face
        {
            set { _face = value; }
            get { return _face; }
        }
    }
}

