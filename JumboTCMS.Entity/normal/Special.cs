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
    /// 专题-------表映射实体
    /// </summary>

    public class Normal_Special
    {
        public Normal_Special()
        { }
        public Normal_Special(string id, string title, string info, string source)
        {
            this._id = id;
            this._title = title;
            this._info = info;
            this._source = source;
        }
        private string _id;
        private string _title;
        private string _info;
        private string _source;
        /// <summary>
        /// 编号
        /// </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 专题标题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 专题简介
        /// </summary>
        public string Info
        {
            set { _info = value; }
            get { return _info; }
        }
        /// <summary>
        /// 专题文件名
        /// </summary>
        public string Source
        {
            set { _source = value; }
            get { return _source; }
        }


    }
}

