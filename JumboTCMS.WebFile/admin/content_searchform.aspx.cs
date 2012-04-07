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
namespace JumboTCMS.WebForm.Admin
{
    public partial class _searchform : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ChannelId = Str2Str(q("ccid"));
            id = Str2Str(q("id"));
            Admin_Load("", "html", true);
            string ClassId = Str2Str(q("cid"));
            if (!Page.IsPostBack)
            {
                if (q("k") != "")
                    this.txtKeyword.Text = q("k");
                if (ddlKeyClass.Items.Count < 1)
                {
                    doh.Reset();
                    doh.SqlCmd = "SELECT ID,Title,[Code] FROM [jcms_normal_class] WHERE [IsOut]=0 AND [ChannelId]=" + ChannelId + " ORDER BY code";
                    DataTable dtClass = doh.GetDataTable();
                    this.ddlKeyClass.Items.Clear();
                    this.ddlKeyClass.Items.Add(new ListItem("不指定栏目", "0"));
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
                doh.Reset();
                doh.SqlCmd = "SELECT [SearchFieldValues],[SearchFieldTexts] FROM [jcms_normal_modules] WHERE [Type]='" + ChannelType + "' AND [Enabled]=1 ORDER BY pId";
                DataTable dtModule = doh.GetDataTable();
                this.ddlKeyType.Items.Clear();
                this.ddlKeyType.Items.Add(new ListItem("请选择", ""));
                ListItem li2;
                string _SearchValues = dtModule.Rows[0]["SearchFieldValues"].ToString();
                string _SearchTexts = dtModule.Rows[0]["SearchFieldTexts"].ToString();
                string[] _SearchValue = _SearchValues.Split(',');
                string[] _SearchText = _SearchTexts.Split(',');
                dtModule.Clear();
                dtModule.Dispose();
                for (int i = 0; i < _SearchValue.Length; i++)
                {
                    li2 = new ListItem();
                    li2.Value = _SearchValue[i];
                    li2.Text = _SearchText[i];
                    if (q("f") != "")
                    {
                        if (q("f") == li2.Value)
                            li2.Selected = true;
                        else
                            li2.Selected = false;
                    }
                    this.ddlKeyType.Items.Add(li2);
                }
                ListItem li3;
                string _StateValues = ",-1,0,1";
                string _StateTexts = "全部,待审,新的,已发布";
                string[] _StateValue = _StateValues.Split(',');
                string[] _StateText = _StateTexts.Split(',');
                for (int i = 0; i < _StateValue.Length; i++)
                {
                    li3 = new ListItem();
                    li3.Value = _StateValue[i];
                    li3.Text = _StateText[i];
                    if (q("s") != "")
                    {
                        if (q("s") == li3.Value)
                            li3.Selected = true;
                        else
                            li3.Selected = false;
                    }
                    this.ddlState.Items.Add(li3);
                }
                ListItem li_isimg;
                string _IsImgValues = ",1,2,-1";
                string _IsImgTexts = "全部,有图,外链,无图";
                string[] _IsImgValue = _IsImgValues.Split(',');
                string[] _IsImgText = _IsImgTexts.Split(',');
                for (int i = 0; i < _IsImgValue.Length; i++)
                {
                    li_isimg = new ListItem();
                    li_isimg.Value = _IsImgValue[i];
                    li_isimg.Text = _IsImgText[i];
                    if (q("isimg") != "")
                    {
                        if (q("isimg") == li_isimg.Value)
                            li_isimg.Selected = true;
                        else
                            li_isimg.Selected = false;
                    }
                    this.ddlIsImg.Items.Add(li_isimg);
                }
                ListItem li_istop;
                string _IsTopValues = ",1,-1";
                string _IsTopTexts = "全部,推荐,不推荐";
                string[] _IsTopValue = _IsTopValues.Split(',');
                string[] _IsTopText = _IsTopTexts.Split(',');
                for (int i = 0; i < _IsTopValue.Length; i++)
                {
                    li_istop = new ListItem();
                    li_istop.Value = _IsTopValue[i];
                    li_istop.Text = _IsTopText[i];
                    if (q("istop") != "")
                    {
                        if (q("istop") == li_istop.Value)
                            li_istop.Selected = true;
                        else
                            li_istop.Selected = false;
                    }
                    this.ddlIsTop.Items.Add(li_istop);
                }
                ListItem li_isfocus;
                string _IsFocusValues = ",1,-1";
                string _IsFocusTexts = "全部,焦点,非焦点";
                string[] _IsFocusValue = _IsFocusValues.Split(',');
                string[] _IsFocusText = _IsFocusTexts.Split(',');
                for (int i = 0; i < _IsFocusValue.Length; i++)
                {
                    li_isfocus = new ListItem();
                    li_isfocus.Value = _IsFocusValue[i];
                    li_isfocus.Text = _IsFocusText[i];
                    if (q("isfocus") != "")
                    {
                        if (q("isfocus") == li_isfocus.Value)
                            li_isfocus.Selected = true;
                        else
                            li_isfocus.Selected = false;
                    }
                    this.ddlIsFocus.Items.Add(li_isfocus);
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
