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
    /// 缩略图尺寸-------表映射实体
    /// </summary>

    public class Normal_Thumbs
    {
        public Normal_Thumbs()
        { }

        private string _id;
        private int _channelid;
        private string _title;
        private int _iwidth;
        private int _iheight;
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
        public int ChannelId
        {
            set { _channelid = value; }
            get { return _channelid; }
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
        public int iWidth
        {
            set { _iwidth = value; }
            get { return _iwidth; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int iHeight
        {
            set { _iheight = value; }
            get { return _iheight; }
        }


    }
}

