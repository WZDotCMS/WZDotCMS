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
    /// 模板方案-------表映射实体
    /// </summary>

    public class Normal_TemplateProject
    {
        public Normal_TemplateProject()
        { }

        private string _id;
        private string _title;
        private string _info;
        private string _dir;
        private int _isdefault;
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
        public string Info
        {
            set { _info = value; }
            get { return _info; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Dir
        {
            set { _dir = value; }
            get { return _dir; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int IsDefault
        {
            set { _isdefault = value; }
            get { return _isdefault; }
        }


    }
}

