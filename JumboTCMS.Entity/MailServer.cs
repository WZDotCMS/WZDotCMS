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
using System.Data;
using System.Collections;
namespace JumboTCMS.Entity
{
    public class MailServer
    {
        private IList m_FromAddresss;
        private IList m_FromNames;
        private IList m_FromPwds;
        private IList m_SmtpHosts;
        private IList m_SmtpPorts;
        private IList m_Useds;
        public MailServer()
        {
        }
        /// <summary>
        /// 发件人地址
        /// </summary>
        public IList FromAddresss
        {
            get { return m_FromAddresss; }
            set { m_FromAddresss = value; }
        }
        /// <summary>
        /// 发件人称呼
        /// </summary>
        public IList FromNames
        {
            get { return m_FromNames; }
            set { m_FromNames = value; }
        }
        /// <summary>
        /// 发件人密码
        /// </summary>
        public IList FromPwds
        {
            get { return m_FromPwds; }
            set { m_FromPwds = value; }
        }
        /// <summary>
        /// 发件人smtp
        /// </summary>
        public IList SmtpHosts
        {
            get { return m_SmtpHosts; }
            set { m_SmtpHosts = value; }
        }
        public IList SmtpPorts
        {
            get { return m_SmtpPorts; }
            set { m_SmtpPorts = value; }
        }
        /// <summary>
        /// 成功发送次数
        /// </summary>
        public IList Useds
        {
            get { return m_Useds; }
            set { m_Useds = value; }
        }
    }
}
