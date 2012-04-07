/*
 * ������������: �������ݹ���ϵͳͨ�ð�
 * 
 * ����Ӣ������: JumboTCMS
 * 
 * ����汾: 5.2.X
 * 
 * �����д: ���Ե (���ƿ�������ϵ��jumbot114#126.com,��������ѵļ�������,�����)
 * 
 * �ٷ���վ: http://www.jumbotcms.net/
 * 
 * ��ҵ����: http://www.jumbotcms.net/about/service.html
 * 
 */

using System;
using System.Data;
using System.Web;
using JumboTCMS.Utils;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _emailserver_ajax : JumboTCMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckFormUrl())
            {
                Response.End();
            }
            Admin_Load("0001", "json");
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxGetList":
                    ajaxGetList();
                    break;
                case "ajaxDel":
                    ajaxDel();
                    break;
                case "checkname":
                    ajaxCheckName();
                    break;
                case "ajaxEmailServerExport":
                    ajaxEmailServerExport();
                    break;
                case "ajaxEmailServerImport":
                    ajaxEmailServerImport();
                    break;
                default:
                    DefaultResponse();
                    break;
            }
            Response.Write(this._response);
        }

        private void DefaultResponse()
        {
            this._response = "{result :\"0\",returnval :\"δ֪����\"}";
        }
        private void ajaxCheckName()
        {
            if (q("id") == "0")
            {
                doh.Reset();
                doh.ConditionExpress = "fromaddress=@fromaddress";
                doh.AddConditionParameter("@fromaddress", q("txtFromAddress"));
                if (doh.Exist("jcms_normal_emailserver"))
                    this._response = "{result :\"0\",returnval :\"�������\"}";
                else
                    this._response = "{result :\"1\",returnval :\"�������\"}";
            }
            else
            {
                doh.Reset();
                doh.ConditionExpress = "fromaddress=@fromaddress and id<>" + q("id");
                doh.AddConditionParameter("@fromaddress", q("txtFromAddress"));
                if (doh.Exist("jcms_normal_emailserver"))
                    this._response = "{result :\"0\",returnval :\"�����޸�\"}";
                else
                    this._response = "{result :\"1\",returnval :\"�����޸�\"}";
            }
        }
        private void ajaxGetList()
        {
            doh.Reset();
            doh.SqlCmd = "Select [ID],[fromaddress],[SmtpHost],[Enabled] FROM [jcms_normal_emailserver] ORDER BY id desc";
            DataTable dt = doh.GetDataTable();
            this._response = "{result :\"1\",returnval :\"�����ɹ�\"," + JumboTCMS.Utils.dtHelp.DT2JSON(dt) + "}";
        }
        private void ajaxDel()
        {
            string sId = f("id");
            doh.Reset();
            doh.ConditionExpress = "id=@id";
            doh.AddConditionParameter("@id", sId);
            doh.Delete("jcms_normal_emailserver");
            new JumboTCMS.DAL.Normal_UserMailDAL().ExportEmailServer();
            this._response = "{result :\"1\",returnval :\"�ɹ�ɾ��\"}";
        }
        private void ajaxEmailServerExport()
        {
            if (new JumboTCMS.DAL.Normal_UserMailDAL().ExportEmailServer())
                this._response = "{result :\"1\",returnval :\"�����ɹ�\"}";
            else
                this._response = "{result :\"0\",returnval :\"����ʧ��\"}";
        }
        private void ajaxEmailServerImport()
        {
            if (new JumboTCMS.DAL.Normal_UserMailDAL().ImportEmailServer())
                this._response = "{result :\"1\",returnval :\"����ɹ�\"}";
            else
                this._response = "{result :\"0\",returnval :\"����ʧ��\"}";
        }
    }
}