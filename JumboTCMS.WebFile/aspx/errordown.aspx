<%@ Page Language="C#" AutoEventWireup="True" Codebehind="errordown.aspx.cs" Inherits="JumboTCMS.WebFile._errordown" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>下载错误</title>
<style type="text/css">
body{margin:0px;font-size:12px;font: 12px Tahoma,Arial, sans-serif;}
*{margin:0px;padding:0px;}

dl.message{margin:60px auto;width:600px;}
dl.message dt{height:36px;background:url(style/message/tbg.gif) repeat-x;line-height:36px;font-size:14px;font-weight:bold;color:#143C80;width:600px;}
dl.message dt span.tl{background:url(style/message/tl.gif) no-repeat left top;width:6px;height:36px;float:left;}
dl.message dt span.tr{background:url(style/message/tr.gif) no-repeat right top;width:6px;height:36px;float:right;}
dl.message dd{background:url(style/message/message.gif) no-repeat 35px 20px;width:448px;border:1px solid #C6C6C6;border-top:0;border-bottom:0;font-weight:bold;color:#143C80;font-size:14px;padding:25px 0 10px 150px;line-height:30px;}
dl.message dd .em{color:#FF5400;font-weight:bold;font-size:14px;}
dl.message dd a{color:#464646;text-decoration:underline;font-weight:normal;font-size:12px;background:url(style/message/return.gif) no-repeat left;padding-left:20px;padding-top:1px;}
dl.message dd.message_b{background:url(style/message/bbg.gif) repeat-x;height:19px;width:600px;border:0;padding:0;}
dl.message dd.message_b span.bl{background:url(style/message/bl.gif) no-repeat left top;width:7px;height:19px;float:left;padding:0;}
dl.message dd.message_b span.br{background:url(style/message/br.gif) no-repeat right top;width:7px;height:19px;float:right;padding:0;}
</style>
</head>
<body>
<div style="margin:120px auto 0; width:620px">
		<dl class="message">
			<dt><span class="tl"></span><span class="tr"></span>友情提醒</dt>
			<dd><span class="em">您当前不能下载，原因可能如下：</span><br>
				1、您还未登录会员，请先<a href="<%= site.Url%><%= site.Dir%>passport/login.aspx">登录</a>！<br />
				2、您当前的账户的博币不足，请进行<a href="<%= site.Url%><%= site.Dir%>user/bobi_buypoints.aspx">博币充值</a>！<br />
				<br>
			</dd>
			<dd class="message_b"><span class="bl"></span><span class="br"></span></dd>
		</dl>
            </div>
</body>
</html>
