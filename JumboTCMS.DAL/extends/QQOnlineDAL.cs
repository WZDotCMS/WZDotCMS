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
using System.Collections.Generic;
using System.Web;
using JumboTCMS.Utils;
using JumboTCMS.Entity;
using JumboTCMS.DBUtility;

namespace JumboTCMS.DAL
{
    /// <summary>
    /// 友情链接插件
    /// </summary>
    public class Extends_QQOnlineDAL : Common
    {
        public Extends_QQOnlineDAL()
        {
            base.SetupSystemDate();
        }
        /// <summary>
        /// 得到列表
        /// </summary>
        public List<Extends_QQOnline> QQOnlineList()
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                List<Extends_QQOnline> qqonlines;
                qqonlines = new List<Extends_QQOnline>();
                _doh.Reset();
                _doh.SqlCmd = "SELECT * FROM [jcms_extends_qqonline] Where State=1 ORDER BY OrderNum Desc,Id Desc";
                DataTable dtQQOnline = _doh.GetDataTable();
                if (dtQQOnline.Rows.Count > 0)
                {
                    for (int i = 0; i < dtQQOnline.Rows.Count; i++)
                    {
                        qqonlines.Add(new Extends_QQOnline(dtQQOnline.Rows[i]["Id"].ToString(),
                            dtQQOnline.Rows[i]["QQID"].ToString(),
                            dtQQOnline.Rows[i]["Title"].ToString(),
                            dtQQOnline.Rows[i]["TColor"].ToString(),
                            dtQQOnline.Rows[i]["face"].ToString()
                            ));
                    }
                }
                dtQQOnline.Clear();
                dtQQOnline.Dispose();
                return qqonlines;
            }
        }
    }
}
