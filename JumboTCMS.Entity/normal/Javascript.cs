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
    /// 外站调用-------表映射实体
    /// </summary>

    public class Normal_Javascript
    {
        public Normal_Javascript()
        { }

        private string _id;
        private string _title;
        private string _code;
        private int _channelid;
        private int _classid;
        private int _selectnumber;
        private int _titlelen;
        private string _jumbotlicode;
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
        public string Code
        {
            set { _code = value; }
            get { return _code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ChannelId
        {
            set { _channelid = value; }
            get { return _channelid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ClassId
        {
            set { _classid = value; }
            get { return _classid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int SelectNumber
        {
            set { _selectnumber = value; }
            get { return _selectnumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int TitleLen
        {
            set { _titlelen = value; }
            get { return _titlelen; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JumbotLiCode
        {
            set { _jumbotlicode = value; }
            get { return _jumbotlicode; }
        }


    }
}

