﻿/*
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
    /// 插件-------表映射实体
    /// </summary>

    public class Normal_Extends
    {
        public Normal_Extends()
        { }

        private string _id;
        private string _title;
        private string _name;
        private string _author;
        private string _info;
        private int _type;
        private int _pid;
        private string _basetable;
        private string _manageurl;
        private int _enabled;
        private int _locked;
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
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Author
        {
            set { _author = value; }
            get { return _author; }
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
        public int Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int pId
        {
            set { _pid = value; }
            get { return _pid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BaseTable
        {
            set { _basetable = value; }
            get { return _basetable; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ManageUrl
        {
            set { _manageurl = value; }
            get { return _manageurl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Enabled
        {
            set { _enabled = value; }
            get { return _enabled; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Locked
        {
            set { _locked = value; }
            get { return _locked; }
        }


    }
}

