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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JumboTCMS.Utils;
using JumboTCMS.Common;

namespace JumboTCMS.WebFile.Admin.Cut2Thumbs
{
    public partial class _upload : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ChannelId = Str2Str(q("ccid"));
            Admin_Load("", "html", true);
            this.ThumbsSize.Items.Clear();
            ListItem li;
            doh.Reset();
            doh.ConditionExpress = "id=@id";
            doh.AddConditionParameter("@id", ChannelId);
            string _defaultThumbs = doh.GetField("jcms_normal_channel", "DefaultThumbs").ToString();
            DataTable dtThumbs = new JumboTCMS.DAL.Normal_ThumbsDAL().GetDataTable(ChannelId);
            for (int i = 0; i < dtThumbs.Rows.Count; i++)
            {
                li = new ListItem();
                li.Value = dtThumbs.Rows[i]["iWidth"].ToString() + "|" + dtThumbs.Rows[i]["iHeight"].ToString();
                li.Text = dtThumbs.Rows[i]["Title"].ToString();
                if (_defaultThumbs == dtThumbs.Rows[i]["ID"].ToString())
                    li.Selected = true;
                else
                    li.Selected = false;
                this.ThumbsSize.Items.Add(li);
            }
            dtThumbs.Clear();
            dtThumbs.Dispose();
            if (q("photo") != "")
            {
                NewsCollection nc = new NewsCollection();
                this.Image1.ImageUrl = nc.LocalFileUrl(site.Url, site.MainSite, q("photo"), ChannelUploadPath, true, 0, 0);
            }
        }
    }
}

