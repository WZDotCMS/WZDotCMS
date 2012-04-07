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
    /// 友情链接-------表映射实体
    /// </summary>

    public class Normal_Link
    {
        public Normal_Link()
        { }
        public Normal_Link(
            string id,
            string title,
            string url,
            string imgpath,
            string info,
            int style
            )
        {
            this._id = id;
            this._title = title;
            this._url = url;
            this._imgpath = imgpath;
            this._info = info;
            this._style = style;
        }
        private string _id;
        private string _title;
        private string _url;
        private string _imgpath;
        private string _info;
        private int _style;
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
        public string Url
        {
            set { _url = value; }
            get { return _url; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ImgPath
        {
            set { _imgpath = value; }
            get { return _imgpath; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Info
        {
            set { _info = value; }
            get { return _info; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Style
        {
            set { _style = value; }
            get { return _style; }
        }
    }
}

