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
namespace JumboTCMS.WebFile.Admin
{
    public partial class _special_edittemplate : JumboTCMS.UI.AdminCenter
    {
        private string _Source = string.Empty;
        private string _tempFile = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("master", "html");
            id = Str2Str(q("id"));
            JumboTCMS.Entity.Normal_Special eSpecial = new JumboTCMS.DAL.Normal_SpecialDAL().GetEntity(id);
            _Source = eSpecial.Source;
            _tempFile = site.Dir + "_data/special/_" + _Source;
            if (!IsPostBack)
            {
                if (JumboTCMS.Utils.DirFile.FileExists(_tempFile))
                    this.txtTemplateContent.Text = JumboTCMS.Utils.DirFile.ReadFile(_tempFile);
                else
                    this.txtTemplateContent.Text = "";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string PageStr = this.txtTemplateContent.Text;
            JumboTCMS.Utils.DirFile.SaveFile(PageStr, _tempFile);
            if (this.chkSavaDefault.Checked)
                JumboTCMS.Utils.DirFile.SaveFile(PageStr, site.Dir + "templates/special_index.htm");
            FinalMessage("成功保存", "close.htm", 0);
        }
    }
}
