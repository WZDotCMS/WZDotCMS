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
using JumboTCMS.Utils;
namespace JumboTCMS.WebFile.Digg
{
    public partial class _ajax : JumboTCMS.UI.FrontAjax
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 8;//脚本过期时间
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxDiggAdd":
                    GetDiggAdd();
                    break;
                default:
                    DefaultResponse();
                    break;
            }

            Response.Write(this._response);
        }

        private void DefaultResponse()
        {
            this._response = "{result :\"未知操作\"}";
        }
        private void GetDiggAdd()
        {
            string ChannelType = q("cType");
            if (!JumboTCMS.Utils.Validator.IsNumeric(q("id")))//ID有误
            {
                this._response = "{count :\"0\", msg:\"ID有误\"}";
                return;
            }
            if (JumboTCMS.Utils.Cookie.GetValue(q("cType") + "DiggNum" + q("id")) == null)
            {
                doh.Reset();
                doh.ConditionExpress = "contentid=" + q("id") + " and channeltype='" + q("cType") + "'";
                doh.Add("jcms_normal_digg", "DiggNum");
                JumboTCMS.Utils.Cookie.SetObj(q("cType") + "DiggNum" + q("id"), "ok");
            }
            doh.Reset();
            doh.ConditionExpress = "contentid=" + q("id") + " and channeltype='" + q("cType") + "'";
            this._response = "{count :\"" + Validator.IntStr(doh.GetField("jcms_normal_digg", "DiggNum").ToString()) + "\"}";
        }
    }
}
