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
    /// 投票-------表映射实体
    /// </summary>
    public class Extends_VoteItem
    {
        private string _itemtext;
        private int _itemclicks;
        public Extends_VoteItem()
        { }
        public Extends_VoteItem(string itemtext, int itemclicks)
        {
            this._itemtext = itemtext;
            this._itemclicks = itemclicks;
        }
        public string ItemText
        {
            set { _itemtext = value; }
            get { return _itemtext; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ItemClicks
        {
            set { _itemclicks = value; }
            get { return _itemclicks; }
        }
    }
    public class Extends_Vote
    {
        public Extends_Vote()
        { }
        private string _id;
        private string _title;
        private List<Extends_VoteItem> _item;
        private int _votetotal;
        private int _type;
        private int _lock;
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
        public List<Extends_VoteItem> Item
        {
            set { _item = value; }
            get { return _item; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int VoteTotal
        {
            set { _votetotal = value; }
            get { return _votetotal; }
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
        public int Lock
        {
            set { _lock = value; }
            get { return _lock; }
        }
    }
}

