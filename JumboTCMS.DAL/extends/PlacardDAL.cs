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
    public class Extends_PlacardDAL : Common
    {
        public Extends_PlacardDAL()
        {
            base.SetupSystemDate();
        }
        /// <summary>
        /// 得到列表
        /// </summary>
        public List<Extends_Placard> PlacardList()
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                List<Extends_Placard> placards;
                placards = new List<Extends_Placard>();
                _doh.Reset();
                _doh.SqlCmd = "SELECT TOP 10 [Id],[Title],[AddTime] FROM [jcms_extends_placard] WHERE [State]=1 ORDER BY Id Desc";
                DataTable dtPlacard = _doh.GetDataTable();
                if (dtPlacard.Rows.Count > 0)
                {
                    for (int i = 0; i < dtPlacard.Rows.Count; i++)
                    {
                        placards.Add(new Extends_Placard(dtPlacard.Rows[i]["Id"].ToString(),
                            dtPlacard.Rows[i]["Title"].ToString(),
                            "",
                            Convert.ToDateTime(dtPlacard.Rows[i]["AddTime"].ToString()),
                            1
                            ));
                    }
                }
                dtPlacard.Clear();
                dtPlacard.Dispose();
                return placards;
            }
        }
    }
}
