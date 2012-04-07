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
using JumboTCMS.Common;

namespace JumboTCMS.WebFile.User
{
    public partial class _message_read : JumboTCMS.UI.UserCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User_Load("", "html");
            id = Str2Str(q("id"));
            //标识为已读
            doh.Reset();
            doh.ConditionExpress = "id=@id and [State]=0 and ReceiveUserId=" + UserId;
            doh.AddConditionParameter("@id", id);
            doh.AddFieldItem("State", 1);
            doh.AddFieldItem("ReadTime", System.DateTime.Now.ToString());
            doh.Update("jcms_normal_user_message");
        }
    }
}
