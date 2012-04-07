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
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Admin
{
    public partial class _ajax : JumboTCMS.UI.AdminCenter
    {
        private string _operType = string.Empty;
        private string _response = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ChannelId = Str2Str(q("ccid"));
            Admin_Load("", "json", true);
            this._operType = q("oper");
            switch (this._operType)
            {
                case "ajaxMove2Special":
                    ajaxMove2Special();
                    break;
                default:
                    DefaultResponse();
                    break;
            }
            Response.Write(this._response);
        }

        private void DefaultResponse()
        {
            this._response = JsonResult(0, "未知操作");
        }
        /// <summary>
        /// 加入专题
        /// </summary>
        private void ajaxMove2Special()
        {
            if (new JumboTCMS.DAL.Normal_SpecialContentDAL().Move2Special(Str2Int(f("tosid")), ChannelId, ChannelType, f("ids")))
                this._response = JsonResult(1, "操作成功");
            else
                this._response = JsonResult(0, "操作失败");
        }
    }
}