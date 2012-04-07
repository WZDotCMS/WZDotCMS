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
using System.Collections.Generic;
namespace JumboTCMS.Entity
{
    /// <summary>
    /// 栏目树实体
    /// </summary>
    public class Normal_ClassTree
    {
        public Normal_ClassTree()
        { }
        private string _id;
        private string _name = string.Empty;
        private string _link = string.Empty;
        private string _rssurl = string.Empty;
        private bool _haschild = false;
        private List<Normal_ClassTree> _subchild;
        /// <summary>
        /// 栏目编号
        /// </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 栏目名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 栏目链接
        /// </summary>
        public string Link
        {
            set { _link = value; }
            get { return _link; }
        }
        /// <summary>
        /// RSS地址
        /// </summary>
        public string RssUrl
        {
            set { _rssurl = value; }
            get { return _rssurl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool HasChild
        {
            set { _haschild = value; }
            get { return _haschild; }
        }
        public List<Normal_ClassTree> SubChild
        {
            set { _subchild = value; }
            get { return _subchild; }
        }
    }
}

