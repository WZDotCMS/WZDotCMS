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
using System.Text.RegularExpressions;
namespace JumboTCMS.DBUtility
{
    /// <summary>
    /// 枚举，作为Web中常用的用户操作类型。常用于权限相关的判断。
    /// </summary>
    public enum OperationType : byte { Add, Modify, Delete, Audit, Enable };
}