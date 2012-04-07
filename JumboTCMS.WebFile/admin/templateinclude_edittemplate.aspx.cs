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
    public partial class _templateinclude_edittemplate : JumboTCMS.UI.AdminCenter
    {
        public string tpPath = string.Empty;
        private string _Source = string.Empty;
        private string _tempFile = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("master", "html");
            _Source = q("source");
            string pid = Str2Str(q("pid"));
            doh.Reset();
            doh.ConditionExpress = "id=@id";
            doh.AddConditionParameter("@id", pid);
            tpPath = doh.GetField("jcms_normal_templateproject", "Dir").ToString();
            if (tpPath.Length == 0)
            {
                Response.Write("HTML模板方案选择有误!");
                Response.End();
                return;
            }
            //JumboTCMS.DAL.Normal_TemplateIncludeDAL sTempate = new JumboTCMS.DAL.Normal_TemplateIncludeDAL();
            //_Source = sTempate.GetSource(id);
            _tempFile = site.Dir + "templates/" + tpPath + "/include/" + _Source;
            this.lblTemplateFile.Text = _tempFile;
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
            FinalMessage("成功保存", "close.htm", 0);
        }
    }
}
