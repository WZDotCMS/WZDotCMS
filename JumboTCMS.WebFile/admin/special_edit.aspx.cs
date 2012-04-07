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
    public partial class _special_edit : JumboTCMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Admin_Load("master", "html");
            id = Str2Str(q("id"));
            JumboTCMS.DBUtility.WebFormHandler wh = new JumboTCMS.DBUtility.WebFormHandler(doh, "jcms_normal_special", btnSave);
            wh.AddBind(txtTitle, "Title", true);
            wh.AddBind(txtSource, "Source", true);
            wh.AddBind(txtInfo, "Info", true);
            if (id == "0")
            {
                wh.Mode = JumboTCMS.DBUtility.OperationType.Add;
            }
            else
            {
                wh.ConditionExpress = "id=" + id;
                this.txtSource.Enabled = false;
                wh.Mode = JumboTCMS.DBUtility.OperationType.Modify;
            }
            wh.validator = chkForm;
            wh.AddOk += new EventHandler(save_ok);
            wh.ModifyOk += new EventHandler(save_ok);
        }
        protected void bind_ok(object sender, EventArgs e)
        {
        }
        protected bool chkForm()
        {
            if (!CheckFormUrl())
                return false;
            if (!Page.IsValid)
                return false;
            //判断重复性
            JumboTCMS.DAL.Normal_SpecialDAL dal = new JumboTCMS.DAL.Normal_SpecialDAL();
            if (dal.ExistTitle(this.txtTitle.Text, id, ""))
            {
                FinalMessage("专题名重复!", "", 1);
                return false;
            }
            if (dal.ExistSource(this.txtSource.Text, id, ""))
            {
                FinalMessage("文件名重复!", "", 1);
                return false;
            }
            return true;

        }
        protected void save_ok(object sender, EventArgs e)
        {
            string _tempFile = site.Dir + "_data/special/_" + this.txtSource.Text;
            if (!JumboTCMS.Utils.DirFile.FileExists(_tempFile))
            {
                string _defaultTemplate = JumboTCMS.Utils.DirFile.ReadFile("~/templates/special_index.htm");
                JumboTCMS.Utils.DirFile.SaveFile(_defaultTemplate, _tempFile);
            }
            FinalMessage("成功保存", "close.htm", 0);
        }
    }
}
