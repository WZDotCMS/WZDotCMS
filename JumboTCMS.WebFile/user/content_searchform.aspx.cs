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
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.User
{
    public partial class _content_searchform : JumboTCMS.UI.UserCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ChannelId = Str2Str(q("ccid"));
            id = Str2Str(q("id"));
            User_Load("", "html", true);
            string ClassId = Str2Str(q("cid"));
            if (!Page.IsPostBack)
            {
                if (q("k") != "")
                    this.txtKeyword.Text = q("k");
                if (ddlKeyClass.Items.Count < 1)
                {
                    doh.Reset();
                    doh.SqlCmd = "SELECT ID,Title,[Code] FROM [jcms_normal_class] WHERE [IsOut]=0 AND [IsPost]=0 AND [ChannelId]=" + ChannelId + " ORDER BY code";
                    DataTable dtClass = doh.GetDataTable();
                    this.ddlKeyClass.Items.Clear();
                    this.ddlKeyClass.Items.Add(new System.Web.UI.WebControls.ListItem("不指定栏目", "0"));
                    ListItem li;
                    for (int i = 0; i < dtClass.Rows.Count; i++)
                    {
                        li = new ListItem();
                        li.Value = dtClass.Rows[i]["Id"].ToString();
                        li.Text = getListName(dtClass.Rows[i]["Title"].ToString(), dtClass.Rows[i]["code"].ToString());
                        if (ClassId == li.Value)
                            li.Selected = true;
                        else
                            li.Selected = false;
                        this.ddlKeyClass.Items.Add(li);
                    }
                    dtClass.Clear();
                    dtClass.Dispose();
                }
                if (q("f") != "")
                {
                    for (int i = 0; i < this.ddlKeyType.Items.Count; i++)
                    {
                        if (this.ddlKeyType.Items[i].Value == q("f"))
                            this.ddlKeyType.Items[i].Selected = true;
                    }
                }
                if (q("d") != "")
                {
                    for (int i = 0; i < this.ddlAddDate.Items.Count; i++)
                    {
                        if (this.ddlAddDate.Items[i].Value == q("d"))
                            this.ddlAddDate.Items[i].Selected = true;
                    }
                }
            }

        }

    }
}
