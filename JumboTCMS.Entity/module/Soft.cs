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
using System.Data;
namespace JumboTCMS.Entity
{
    /// <summary>
    /// 软件实体列表
    /// </summary>
    public class Module_Softs
    {
        public Module_Softs()
        { }
        public List<Module_Soft> DT2List(DataTable _dt)
        {
            if (_dt == null) return null;
            return JumboTCMS.Utils.dtHelp.DT2List<Module_Soft>(_dt);
        }
    }
    /// <summary>
    /// 软件-------表映射实体
    /// </summary>

    public class Module_Soft
    {
        public Module_Soft()
        { }

        private string _id;
        private string _channelid;
        private string _channelishtml = "0";
        private string _classid;
        private string _title;
        private string _tcolor;
        private DateTime _adddate;
        private string _summary;
        private string _editor;
        private string _author;
        private string _tags;
        private int _viewnum;
        private int _ispass;
        private int _isimg;
        private string _img;
        private int _istop;
        private int _isfocus;
        private int _userid;
        private int _readgroup;
        private string _sourcefrom;
        private string _version;
        private string _operatingsystem;
        private string _unzippass;
        private string _demourl;
        private string _regurl;
        private string _ssize;
        private int _points;
        private string _downurl;
        private int _downnum;
        private string _firstpage;
        private string _aliaspage;
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
        public string ChannelId
        {
            set { _channelid = value; }
            get { return _channelid; }
        }
        public string ChannelIsHtml
        {
            set { _channelishtml = value; }
            get { return _channelishtml; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ClassId
        {
            set { _classid = value; }
            get { return _classid; }
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
        public DateTime AddDate
        {
            set { _adddate = value; }
            get { return _adddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Summary
        {
            set { _summary = value; }
            get { return _summary; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Editor
        {
            set { _editor = value; }
            get { return _editor; }
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
        public string Tags
        {
            set { _tags = value; }
            get { return _tags; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ViewNum
        {
            set { _viewnum = value; }
            get { return _viewnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int IsPass
        {
            set { _ispass = value; }
            get { return _ispass; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int IsImg
        {
            set { _isimg = value; }
            get { return _isimg; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Img
        {
            set { _img = value; }
            get { return _img; }
        }
        /// <summary>
        /// 是否推荐
        /// </summary>
        public int IsTop
        {
            set { _istop = value; }
            get { return _istop; }
        }
        /// <summary>
        /// 是否焦点
        /// </summary>
        public int IsFocus
        {
            set { _isfocus = value; }
            get { return _isfocus; }
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
        public int ReadGroup
        {
            set { _readgroup = value; }
            get { return _readgroup; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SourceFrom
        {
            set { _sourcefrom = value; }
            get { return _sourcefrom; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Version
        {
            set { _version = value; }
            get { return _version; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OperatingSystem
        {
            set { _operatingsystem = value; }
            get { return _operatingsystem; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UnZipPass
        {
            set { _unzippass = value; }
            get { return _unzippass; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DemoUrl
        {
            set { _demourl = value; }
            get { return _demourl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RegUrl
        {
            set { _regurl = value; }
            get { return _regurl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SSize
        {
            set { _ssize = value; }
            get { return _ssize; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Points
        {
            set { _points = value; }
            get { return _points; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DownUrl
        {
            set { _downurl = value; }
            get { return _downurl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int DownNum
        {
            set { _downnum = value; }
            get { return _downnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FirstPage
        {
            set { _firstpage = value; }
            get { return _firstpage; }
        }
        public string AliasPage
        {
            set { _aliaspage = value; }
            get { return _aliaspage; }
        }
    }
}

