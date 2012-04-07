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
using JumboTCMS.Common;
namespace JumboTCMS.WebFile.Modules.Video.Plus
{
    public partial class _play : JumboTCMS.UI.FrontHtml
    {
        protected void Page_Unload(object sender, EventArgs e)
        {
            SavePageLog(1);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 5;//脚本过期时间
            string id = Str2Str(q("id"));
            int NO = Str2Int(q("NO"));
            int vWidth = Str2Int(q("w")) == 0 ? 400 : Str2Int(q("w"));
            int vHeight = Str2Int(q("h")) == 0 ? 300 : Str2Int(q("h"));
            string vAutoStart = Str2Int(q("auto")) != 0 ? "true" : "false";
            string vAutoPlay = Str2Int(q("auto")) != 0 ? "1" : "0";
            bool vFull = Str2Int(q("full")) != 0 ? true : false;
            if (vFull)
                vHeight -= 55;
            string _html = string.Empty;
            if (id == "0")
            {
                _html = "<img src=\"" + site.Dir + "style/common/video_play.jpg\" border=\"0\" width=\"" + vWidth + "\" height=\"" + vHeight + "\" />";
            }
            else
            {
                doh.Reset();
                doh.ConditionExpress = "id=" + id;
                object[] value = doh.GetFields("jcms_module_video", "Img,VideoUrl");
                string videoImg = value[0].ToString();
                string videoUrl = value[1].ToString().Replace("\r\n", "\r");
                string previewImage = videoImg == "" ? site.Dir + "style/flash/videoPlayer/Evanescence1.jpg" : videoImg;

                if (videoUrl != "")
                {
                    string[] _VideoUrl = videoUrl.Split(new string[] { "\r" }, StringSplitOptions.None);
                    string _txt = "片段[" + (NO + 1) + "]";
                    string _url = _VideoUrl[NO];
                    if (_url.Contains("|||"))
                    {
                        _txt = _url.Substring(0, _url.IndexOf("|||"));
                        _url = _url.Substring(_url.IndexOf("|||") + 3, (_url.Length - _url.IndexOf("|||") - 3));
                    }
                    string _ext = JumboTCMS.Utils.DirFile.GetFileExt(_url);
                    if (!_url.Contains("http://"))
                        _url = site.Url + _url;
                    switch (_ext)
                    {
                        case "asf":
                            _html = "<object classid=\"clsid:22D6F312-B0F6-11D0-94AB-0080C74C7E95\" codebase=\"http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=6,0,02,902\" type=\"application/x-oleobject\" standby=\"Loading...\" width=\"" + vWidth + "\" height=\"" + vHeight + "\"><param name=\"FileName\" VALUE=\"" + _url + "\" /><param name=\"ShowStatusBar\" value=\"-1\" /><param name=\"AutoStart\" value=\"" + vAutoStart + "\" /><embed type=\"application/x-mplayer2\" pluginspage=\"http://www.microsoft.com/Windows/MediaPlayer/\" src=\"" + _url + "\" autostart=\"" + vAutoStart + "\" width=\"" + vWidth + "\" height=\"" + vHeight + "\" /></object>";
                            break;
                        case "avi":
                            _html = "<object classid=\"clsid:22D6F312-B0F6-11D0-94AB-0080C74C7E95\" codebase=\"http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=6,0,02,902\" type=\"application/x-oleobject\" standby=\"Loading...\" width=\"" + vWidth + "\" height=\"" + vHeight + "\"><param name=\"FileName\" VALUE=\"" + _url + "\" /><param name=\"ShowStatusBar\" value=\"-1\" /><param name=\"AutoStart\" value=\"" + vAutoStart + "\" /><embed type=\"application/x-mplayer2\" pluginspage=\"http://www.microsoft.com/Windows/MediaPlayer/\" src=\"" + _url + "\" autostart=\"" + vAutoStart + "\" width=\"" + vWidth + "\" height=\"" + vHeight + "\" /></object>";
                            break;
                        case "wmv":
                            _html = "<object classid=\"clsid:22D6F312-B0F6-11D0-94AB-0080C74C7E95\" codebase=\"http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=6,0,02,902\" type=\"application/x-oleobject\" standby=\"Loading...\" width=\"" + vWidth + "\" height=\"" + vHeight + "\"><param name=\"FileName\" VALUE=\"" + _url + "\" /><param name=\"ShowStatusBar\" value=\"-1\" /><param name=\"AutoStart\" value=\"" + vAutoStart + "\" /><embed type=\"application/x-mplayer2\" pluginspage=\"http://www.microsoft.com/Windows/MediaPlayer/\" src=\"" + _url + "\" autostart=\"" + vAutoStart + "\" width=\"" + vWidth + "\" height=\"" + vHeight + "\" /></object>";
                            break;
                        case "swf":
                            _html = "<object codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,0,0\" classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" width=\"" + vWidth + "\" height=\"" + vHeight + "\"><param name=\"movie\" value=\"" + _url + "\" /><param name=\"quality\" value=\"high\" /><param name=\"AllowScriptAccess\" value=\"never\" /><embed src=\"" + _url + "\" quality=\"high\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" type=\"application/x-shockwave-flash\" width=\"" + vWidth + "\" height=\"" + vHeight + "\" /></object>";
                            break;
                        default:
                            _html = "<object type=\"application/x-shockwave-flash\" data=\"" + site.Dir + "style/flash/videoPlayer/videoPlayer.swf\" width=\"" + vWidth + "\" height=\"" + vHeight + "\" id=\"videoPlayer\"><param name=\"movie\" value=\"" + site.Dir + "style/flash/videoPlayer/videoPlayer.swf\"/><param name=\"play\" value=\"true\" /><param name=\"loop\" value=\"true\" /><param name=\"allowFullScreen\" value=\"true\" /><param name=\"wmode\" value=\"window\" /><param name=\"FlashVars\" value=\"htmlPage=index.html&settingsFile=" + site.Dir + "style/flash/videoPlayer/setting" + vAutoPlay + ".xml&flvFile=" + _url + "&previewImage=" + previewImage + "\"/></object>";
                            break;
                    }
                }
                else
                    _html = "<img src=\"" + site.Dir + "style/common/video_play.jpg\" border=\"0\" width=\"" + vWidth + "\" height=\"" + vHeight + "\" />";
            }
            if (vFull)
                Response.Write("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\"><html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\"/><title>视频播放器</title><link rel=\"stylesheet\" href=\"" + site.Dir + "style/flash/videoPlayer/css/css.css\" type=\"text/css\" /><script type=\"text/javascript\" src=\"" + site.Dir + "_libs/jquery.tools.pack.js\"></script></head><body><ul id=\"navigation\"><li id='li_light' class='off'><a style=\"cursor:pointer;\"></a></li><li class='help'><a style=\"cursor:pointer;\"></a></li></ul><div style=\"margin:0px;padding-top:55px;\">" + _html + "</div><script type=\"text/javascript\">$(function() {$('#navigation a').stop().animate({'marginTop':'-33px'},1000);$('#navigation > li').hover(function () {$('a',$(this)).stop().animate({'marginTop':'-2px'},200);},function () {$('a',$(this)).stop().animate({'marginTop':'-33px'},200);});});$('#li_light').click(function() {parent.LightOpenOff();/*if ($(this).hasClass('off')){$(this).removeClass('off');$(this).addClass('open');}else{$(this).removeClass('open');$(this).addClass('off');}*/});$('.help').click(function() {parent.ShowHelpInfo();});</script></body></html>");
            else
                Response.Write("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\"><html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\"/><title>视频播放器</title><link rel=\"stylesheet\" href=\"" + site.Dir + "style/flash/videoPlayer/css/css.css\" type=\"text/css\" /></head><body><div style=\"margin:0px;padding-top:0px;\">" + _html + "</div></body></html>");

        }
    }
}