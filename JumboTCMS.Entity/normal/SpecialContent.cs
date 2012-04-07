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
    /// 专题内容-------表映射实体
    /// </summary>

    public class Normal_SpecialContent
    {
        public Normal_SpecialContent()
        { }

        private string _id;
        private string _title;
        private int _sid;
        private int _channelid;
        private int _contentid;
        /// <summary>
        /// 编号
        /// </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 内容标题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 所属专题ID
        /// </summary>
        public int sId
        {
            set { _sid = value; }
            get { return _sid; }
        }
        /// <summary>
        /// 所属频道ID
        /// </summary>
        public int ChannelId
        {
            set { _channelid = value; }
            get { return _channelid; }
        }
        /// <summary>
        /// 指向内容的ID
        /// </summary>
        public int ContentId
        {
            set { _contentid = value; }
            get { return _contentid; }
        }


    }
}

