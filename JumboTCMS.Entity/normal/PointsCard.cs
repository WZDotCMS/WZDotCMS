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
    /// 充值卡-------表映射实体
    /// </summary>

    public class Normal_PointsCard
    {
        public Normal_PointsCard()
        { }

        private string _id;
        private string _cardnumber;
        private string _cardpassword;
        private int _userid;
        private int _points;
        private DateTime _limiteddate;
        private DateTime _activetime;
        private string _activeip;
        private int _state;
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
        public string CardNumber
        {
            set { _cardnumber = value; }
            get { return _cardnumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CardPassword
        {
            set { _cardpassword = value; }
            get { return _cardpassword; }
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
        public int Points
        {
            set { _points = value; }
            get { return _points; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime LimitedDate
        {
            set { _limiteddate = value; }
            get { return _limiteddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ActiveTime
        {
            set { _activetime = value; }
            get { return _activetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ActiveIP
        {
            set { _activeip = value; }
            get { return _activeip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int State
        {
            set { _state = value; }
            get { return _state; }
        }


    }
}

