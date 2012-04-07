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
    /// 频道-------表映射实体
    /// </summary>

    public class Normal_Channel
    {
        public Normal_Channel()
        { }

        private string _id = "0";
        private string _title = string.Empty;
        private string _info = string.Empty;
        private int _classdepth = 0;
        private string _dir = string.Empty;
        private string _subdomain = string.Empty;
        private int _pid = 0;
        private string _itemname = string.Empty;
        private string _itemunit = string.Empty;
        private int _templateid = 0;
        private string _type = "system";
        private bool _enabled = false;
        private int _defaultthumbs = 0;
        private bool _ispost = false;
        private bool _ishtml = false;
        private bool _istop = false;
        private string _uploadpath;
        private string _uploadtype;
        private int _uploadsize;
        private string _languagecode;

        /// <summary>
        /// 编号
        /// </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 频道名称
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 频道简介
        /// </summary>
        public string Info
        {
            set { _info = value; }
            get { return _info; }
        }
        /// <summary>
        /// 栏目深度
        /// </summary>
        public int ClassDepth
        {
            set { _classdepth = value; }
            get { return _classdepth; }
        }
        /// <summary>
        /// 路径
        /// </summary>
        public string Dir
        {
            set { _dir = value; }
            get { return _dir; }
        }
        public string SubDomain
        {
            set { _subdomain = value; }
            get { return _subdomain; }
        }
        /// <summary>
        /// 权值
        /// </summary>
        public int pId
        {
            set { _pid = value; }
            get { return _pid; }
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ItemName
        {
            set { _itemname = value; }
            get { return _itemname; }
        }
        /// <summary>
        /// 项目单位
        /// </summary>
        public string ItemUnit
        {
            set { _itemunit = value; }
            get { return _itemunit; }
        }
        /// <summary>
        /// 模板ID
        /// </summary>
        public int TemplateId
        {
            set { _templateid = value; }
            get { return _templateid; }
        }
        /// <summary>
        /// 模板模型：article/soft/photo/video，system表示外部频道
        /// </summary>
        public string Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled
        {
            set { _enabled = value; }
            get { return _enabled; }
        }
        /// <summary>
        /// 默认缩略图Id
        /// </summary>
        public int DefaultThumbs
        {
            set { _defaultthumbs = value; }
            get { return _defaultthumbs; }
        }
        /// <summary>
        /// 是否会员可投稿
        /// </summary>
        public bool IsPost
        {
            set { _ispost = value; }
            get { return _ispost; }
        }
        /// <summary>
        /// 是否静态
        /// </summary>
        public bool IsHtml
        {
            set { _ishtml = value; }
            get { return _ishtml; }
        }
        /// <summary>
        /// 是否导航
        /// </summary>
        public bool IsTop
        {
            set { _istop = value; }
            get { return _istop; }
        }
        /// <summary>
        /// 附件存放目录(已经过滤标签)
        /// </summary>
        public string UploadPath
        {
            set { _uploadpath = value; }
            get { return _uploadpath; }
        }
        /// <summary>
        /// 附件上传类型
        /// </summary>
        public string UploadType
        {
            set { _uploadtype = value; }
            get { return _uploadtype; }
        }
        /// <summary>
        /// 附件大小限制
        /// </summary>
        public int UploadSize
        {
            set { _uploadsize = value; }
            get { return _uploadsize; }
        }
        /// <summary>
        /// 语言包
        /// </summary>
        public string LanguageCode
        {
            set { _languagecode = value; }
            get { return _languagecode; }
        }
    }
}

