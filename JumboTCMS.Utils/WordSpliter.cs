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
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace JumboTCMS.Utils
{
    /// <summary>
    /// 分词类
    /// </summary>
    public static class WordSpliter
    {
        /// <summary>
        /// 得到分词关键字
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetKeyword(string key, string splitchar)
        {
            JumboTCMS.Utils.ShootSeg.Segment seg = new JumboTCMS.Utils.ShootSeg.Segment();
            seg.InitWordDics();
            seg.EnablePrefix = true;
            seg.Separator = splitchar;
            return seg.SegmentText(key, false).Trim();
        }
        public static string GetKeyword(string key)
        {
            return GetKeyword(key," ");
        }
    }
}