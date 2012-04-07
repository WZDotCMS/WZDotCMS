using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Collections.Specialized;

namespace JumboTCMS.OAuth.Sina
{
    public class SinaConfig
    {
        /// <summary>
        /// 得到Sina的AppKey
        /// </summary>
        /// <returns>string AppKey</returns>
        public static string AppKey
        {
            get
            {
                return JumboTCMS.Utils.Session.Get("OAuth_Sina.AppKey");
            }

            set
            {
                JumboTCMS.Utils.Session.Add("OAuth_Sina.AppKey", value);
            }
        }

        /// <summary>
        /// 得到Sina的AppSecret
        /// </summary>
        /// <returns>string AppSecret</returns>
        public static string AppSecret
        {
            get
            {
                return JumboTCMS.Utils.Session.Get("OAuth_Sina.AppSecret");
            }

            set
            {
                JumboTCMS.Utils.Session.Add("OAuth_Sina.AppSecret", value);
            }
        }

        /// <summary>
        /// 得到回调地址
        /// </summary>
        /// <returns></returns>
        public static string CallBackURI
        {
            get
            {
                return JumboTCMS.Utils.Session.Get("OAuth_Sina.CallBackURI");
            }

            set
            {
                JumboTCMS.Utils.Session.Add("OAuth_Sina.CallBackURI", value);
            }
        }

    }
}
