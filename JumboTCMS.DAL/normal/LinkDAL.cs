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
    /// 友情链接
    /// </summary>
    public class Normal_LinkDAL : Common
    {
        public Normal_LinkDAL()
        {
            base.SetupSystemDate();
        }
        /// <summary>
        /// 得到列表
        /// </summary>
        public List<Normal_Link> LinkList()
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                List<Normal_Link> links;
                links = new List<Normal_Link>();
                _doh.Reset();
                _doh.SqlCmd = "SELECT [Id],[Title],[Url],[ImgPath],[Info],[Style] FROM [jcms_normal_link] WHERE [State]=1 ORDER BY Style Desc,OrderNum Desc,Id Desc";
                DataTable dtLink = _doh.GetDataTable();
                if (dtLink.Rows.Count > 0)
                {
                    for (int i = 0; i < dtLink.Rows.Count; i++)
                    {
                        links.Add(new Normal_Link(dtLink.Rows[i]["Id"].ToString(),
                            dtLink.Rows[i]["Title"].ToString(),
                            dtLink.Rows[i]["Url"].ToString(),
                            dtLink.Rows[i]["ImgPath"].ToString(),
                            dtLink.Rows[i]["Info"].ToString(),
                            Str2Int(dtLink.Rows[i]["Style"].ToString())
                            ));
                    }
                }
                dtLink.Clear();
                dtLink.Dispose();
                return links;
            }
        }
    }
}
