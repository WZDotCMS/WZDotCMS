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
using System.Text;
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.User
{
    public partial class _tougao_menulist : JumboTCMS.UI.UserCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User_Load("", "html");
            int menuId = Str2Int(q("m"));
            int minId = 0;
            int maxId = 0;
            string[,] menu = leftMenu();
            string tmpHtml = "\r\n";
            tmpHtml += "<div class=\"box\" id=\"box\">\r\n";
            tmpHtml += "    <ul id=\"navBox\" class='navBox'>\r\n";
            if (menuId < publicMenu)
            {
                minId = 0;
                maxId = menuId;
            }
            else
            {
                minId = menuId;
                maxId = menu.GetLength(0) - 1;
            }
            for (int i = minId; i < maxId + 1; i++)
            {
                if (menu[i, 0] == null) break;
                tmpHtml += "        <li><a href=\"javascript:void(0)\" class=\"folder\" onclick=\"toggleExpand(this)\" name=\"mj" + i + "\" id=\"mj" + i + "\">" + menu[i, 0].Split('$')[0] + "</a>\r\n";
                tmpHtml += "            <ul>" + "\r\n";
                for (int j = 1; j < menu.GetLength(1); j++)
                {
                    if (menu[i, j] == null)
                    {
                        break;
                    }
                    tmpHtml += "            <li><a href='" + menu[i, j].Split('|')[0] + "' id=\"_" + (i * 100 + j) + "\" target=\"content\">" + menu[i, j].Split('|')[1] + "</a></li>\r\n";
                }
                tmpHtml += "            </ul>" + "\r\n";
                tmpHtml += "        </li>" + "\r\n";
            }
            tmpHtml += "<script type=\"text/javascript\">\r\n$(\"#box a[target=content]\").each(function(a){a.onclick=makeTab;})\r\nwinResize();\r\n</script>";
            this.side.InnerHtml = tmpHtml;
        }
    }
}
