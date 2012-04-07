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
using JumboTCMS.DBUtility;

namespace JumboTCMS.DAL
{
    /// <summary>
    /// 广告表信息
    /// </summary>
    public class AdvDAL : Common
    {
        public AdvDAL()
        {
            base.SetupSystemDate();
        }
        public string GetAdvBody(string _id)
        {
            using (DbOperHandler _doh = new Common().Doh())
            {
                string _body = "";
                _doh.Reset();
                _doh.SqlCmd = "select * from [jcms_normal_adv] where id=" + _id;
                DataTable dt = _doh.GetDataTable();
                if (dt.Rows.Count == 0)
                {
                    return null;
                }
                string _advname = dt.Rows[0]["Title"].ToString();
                string _type = dt.Rows[0]["AdvType"].ToString().ToLower();
                string _width = dt.Rows[0]["width"].ToString();
                string _height = dt.Rows[0]["height"].ToString();
                string _content = dt.Rows[0]["Content"].ToString();
                string _url = dt.Rows[0]["Url"].ToString();
                string _picurl = dt.Rows[0]["Picurl"].ToString();
                switch (_type)
                {
                    case "img":
                        if (_url.Length > 10)
                            _body = string.Format("<!--" + _advname + "开始--><a href='{3}' target='_blank'><img src='{0}' width='{1}' height='{2}' border='0' /></a><!--" + _advname + "结束-->", _picurl, _width, _height, _url);
                        else
                            _body = string.Format("<!--" + _advname + "开始--><img src='{0}' width='{1}' height='{2}' border='0' /><!--" + _advname + "结束-->", _picurl, _width, _height);
                        break;
                    case "flash":
                        _body = "<!--" + _advname + "开始--><object classid='clsid:d27cdb6e-ae6d-11cf-96b8-444553540000' codebase='http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0' width='" + _width + "' height='" + _height + "' align='middle'><param name='allowScriptAccess' value='sameDomain' /><param name='movie' value='" + _picurl + "' /><param name='quality' value='high' /><param name='bgcolor' value='#ffffff' /><embed src='" + _picurl + "' quality='high' bgcolor='#ffffff' width='" + _width + "' height='" + _height + "' align='middle' allowScriptAccess='sameDomain' type='application/x-shockwave-flash' pluginspage='http://www.macromedia.com/go/getflashplayer' /></object><!--" + _advname + "结束-->";
                        break;
                    case "iframe":
                        _body = "<!--" + _advname + "开始--><iframe src='" + _content + "' width='" + _width + "' height='" + _height + "' scrolling='auto' frameborder='0' marginheight='0' marginwidth='0'></iframe><!--" + _advname + "结束-->";
                        break;
                    default:
                        _body = "<!--" + _advname + "开始-->" + _content + "<!--" + _advname + "结束-->";
                        break;
                }
                return _body;
            }
        }
        /// <summary>
        /// 生成广告
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_state">0表示暂停；1表示正常</param>
        /// <returns></returns>
        public bool CreateAdv(string _id, string _state)
        {
            string _filename1 = "~/_data/html/more/" + _id + ".htm";
            string _filename2 = "~/_data/shtm/more/" + _id + ".htm";
            string _filename3 = "~/_data/style/more/" + _id + ".js";
            if (_state == "1")
            {
                string _body = GetAdvBody(_id);
                JumboTCMS.Utils.DirFile.SaveFile(_body, _filename1, false);
                JumboTCMS.Utils.DirFile.SaveFile(_body, _filename2, true);
                JumboTCMS.Utils.DirFile.SaveFile(JumboTCMS.Utils.Strings.Html2Js(_body), _filename3, true);
            }
            else
            {
                JumboTCMS.Utils.DirFile.SaveFile("<!---->", _filename1, false);
                JumboTCMS.Utils.DirFile.SaveFile("<!---->", _filename2, true);
                JumboTCMS.Utils.DirFile.SaveFile("//", _filename3, true);
            }
            return true;
        }
    }
}
