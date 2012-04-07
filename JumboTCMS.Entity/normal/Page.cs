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
    /// 单页内容-------表映射实体
    /// </summary>

    public class Normal_Page
    {
        public Normal_Page()
        { }

        private string _id;
        private string _title;
        private string _source;
        private string _outurl;
        /// <summary>
        /// 编号
        /// </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 模板文件名
        /// </summary>
        public string Source
        {
            set { _source = value; }
            get { return _source; }
        }
        /// <summary>
        /// 静态文件路径
        /// </summary>
        public string OutUrl
        {
            set { _outurl = value; }
            get { return _outurl; }
        }

    }
}

