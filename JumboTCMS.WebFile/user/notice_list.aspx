<%@ Page Language="C#" AutoEventWireup="True" Codebehind="notice_list.aspx.cs" Inherits="JumboTCMS.WebFile.User._notice_list" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>个人中心 - <%=site.Name%></title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/jcmsV5.js"></script>
<link   type="text/css" rel="stylesheet" href="../_data/jcmsV5.css" />
<script type="text/javascript" src="js/fore.common.js"></script>
<link   type="text/css" rel="stylesheet" href="../style/member/style.css" />
<script type="text/javascript">
var id = "";
var type = joinValue('type');
var pagesize=10;
var page=thispage();
$(document).ready(function(){
	ajaxBindUserData();//绑定会员数据
	if(q("type")=="")
		aLoad('');
	else
		aLoad(q("type"));
});
function aLoad(s) {
	page = "1";
	if(s != "")
		type = "&type=" + s;
	else
		type = "";
	ajaxList(page);
}
function ajaxList(currentpage)
{
	if(currentpage!=null) page=currentpage;
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"page="+currentpage+"&pagesize="+pagesize+"&time="+(new Date().getTime())+id,
		url:		"ajax.aspx?oper=ajaxGetNoticeList"+type,
		error:		function(XmlHttpRequest,textStatus, errorThrown){if(XmlHttpRequest.responseText!=""){alert(XmlHttpRequest.responseText);}},
		success:	function(d){
			switch (d.result)
			{
			case '0':
				JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				$("#ajaxList").setTemplateElement("tplList", null, {filter_data: false});
				$("#ajaxList").processTemplate(d);
				$("#ajaxPageBar").html(d.pagerbar);
				break;
			}
		}
	});
}
</script>
</head>
<body>
<!--#include file="include/header.htm"-->
<div id="wrap">
	<div id="main">
		<!--#include file="include/left_menu.htm"-->
		<script type="text/javascript">$('#bar-message-head').addClass('currently');$('#bar-message li.small').show();</script>
<div id="mainarea">
			<h2 class="title"><img src="../style/app/pm.gif">消息</h2>
			<div class="tabs_header">
				<ul class="tabs">
					<li><a href="message_list.aspx"><span>短消息</span></a></li>
					<li class="active"><a href="notice_list.aspx"><span>通知</span></a></li>
				</ul>
			</div>
			<div id="content">
            <textarea id="tplList" style="display:none">{#foreach $T.table as record}
<li>
	{$T.record.content}
	&nbsp;
	<span class="time">{$T.record.adddate}</span>
</li>
{#/for}</textarea>
				<ul class="line_list" id="ajaxList"></ul>
				<div class="page" id="ajaxPageBar"></div>
			</div>
			<div id="sidebar">
				<div class="sidebox">
					<h2 class="title">通知分类</h2>
					<ul class="line_list">
						<li><a href="notice_list.aspx">查看全部通知</a></li>
						<li><a href="notice_list.aspx?type=service">网站客服</a></li>
						<li><a href="notice_list.aspx?type=friend">好友</a></li>
					</ul>
				</div>
			</div>
		</div>
		<!--/mainarea-->
	</div>
	<div class="clear"></div>
</div>
<!--#include file="include/footer.htm"-->
</body>
</html>

