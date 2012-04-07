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
    /// 文章实体列表
    /// </summary>
    public class Module_Articles
    {
        public Module_Articles()
        { }
        public List<Module_Article> DT2List(DataTable _dt)
        {
            if (_dt == null) return null;
            return JumboTCMS.Utils.dtHelp.DT2List<Module_Article>(_dt);
        }
    }
    /// <summary>
    /// 文章-------表映射实体
    /// </summary>
    public class Module_Article
    {
        public Module_Article()
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
        private string _content;
        private string _firstpage;
        private string _aliaspage;
        /// <summary>
        /// 编号
        /// </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 频道编号
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
        /// 栏目编号
        /// </summary>
        public string ClassId
        {
            set { _classid = value; }
            get { return _classid; }
        }
        /// <summary>
        /// 文章标题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 标题颜色
        /// </summary>
        public string TColor
        {
            set { _tcolor = value; }
            get { return _tcolor; }
        }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime AddDate
        {
            set { _adddate = value; }
            get { return _adddate; }
        }
        /// <summary>
        /// 简介
        /// </summary>
        public string Summary
        {
            set { _summary = value; }
            get { return _summary; }
        }
        /// <summary>
        /// 编辑(默认为录入内容的用户名)
        /// </summary>
        public string Editor
        {
            set { _editor = value; }
            get { return _editor; }
        }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author
        {
            set { _author = value; }
            get { return _author; }
        }
        /// <summary>
        /// 标签
        /// </summary>
        public string Tags
        {
            set { _tags = value; }
            get { return _tags; }
        }
        /// <summary>
        /// 浏览次数
        /// </summary>
        public int ViewNum
        {
            set { _viewnum = value; }
            get { return _viewnum; }
        }
        /// <summary>
        /// 状态(0表示未审,1表示审核)
        /// </summary>
        public int IsPass
        {
            set { _ispass = value; }
            get { return _ispass; }
        }
        /// <summary>
        /// 是否有图片
        /// </summary>
        public int IsImg
        {
            set { _isimg = value; }
            get { return _isimg; }
        }
        /// <summary>
        /// 缩略图
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
        /// 投稿者ID(0表示管理员发布)
        /// </summary>
        public int UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 最低阅读权限
        /// </summary>
        public int ReadGroup
        {
            set { _readgroup = value; }
            get { return _readgroup; }
        }
        /// <summary>
        /// 内容来源/出处
        /// </summary>
        public string SourceFrom
        {
            set { _sourcefrom = value; }
            get { return _sourcefrom; }
        }
        /// <summary>
        /// 文章内容
        /// </summary>
        public string Content
        {
            set { _content = value; }
            get { return _content; }
        }
        /// <summary>
        /// 链接地址
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

