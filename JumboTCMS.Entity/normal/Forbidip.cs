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
    /// 非法IP-------表映射实体
    /// </summary>

    public class Normal_Forbidip
    {
        public Normal_Forbidip()
        { }

        private string _id;
        private long _startip;
        private string _startip2;
        private long _endip;
        private string _endip2;
        private DateTime _expiredate;
        private int _enabled;
        /// <summary>
        /// 编号
        /// </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 开始IP，已换算成long型
        /// </summary>
        public long StartIP
        {
            set { _startip = value; }
            get { return _startip; }
        }
        /// <summary>
        /// 开始IP，如192.168.1.1
        /// </summary>
        public string StartIP2
        {
            set { _startip2 = value; }
            get { return _startip2; }
        }
        /// <summary>
        /// 结束IP，已换算成long型
        /// </summary>
        public long EndIP
        {
            set { _endip = value; }
            get { return _endip; }
        }
        /// <summary>
        /// 结束IP，如192.168.1.100
        /// </summary>
        public string EndIP2
        {
            set { _endip2 = value; }
            get { return _endip2; }
        }
        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime ExpireDate
        {
            set { _expiredate = value; }
            get { return _expiredate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Enabled
        {
            set { _enabled = value; }
            get { return _enabled; }
        }


    }
}

