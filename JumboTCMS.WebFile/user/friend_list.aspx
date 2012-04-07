<%@ Page Language="C#" AutoEventWireup="True" Codebehind="friend_list.aspx.cs" Inherits="JumboTCMS.WebFile.User._friend_list" %>
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
var pagesize=20;
var page=thispage();
$(document).ready(function(){
	ajaxBindUserData();//绑定会员数据
});
function ajaxList(currentpage)
{
	if(currentpage!=null) page=currentpage;
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"page="+currentpage+"&pagesize="+pagesize+"&time="+(new Date().getTime()),
		url:		"ajax.aspx?oper=ajaxGetFriendList",
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
function ConfirmDel(friendid){
	JumboTCMS.Confirm("确定要解除好友关系吗？", "ajaxDel("+friendid+")");
}
function ajaxDel(friendid){
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"friendid="+friendid,
		url:		"ajax.aspx?oper=ajaxDelFriend&time="+(new Date().getTime()),
		error:		function(XmlHttpRequest,textStatus, errorThrown){JumboTCMS.Loading.hide();alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				JumboTCMS.Alert(d.returnval, "0", "top.window.location='../passport/login.aspx';");
				break;
			case '0':
				JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				ajaxList(page);
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
		<script type="text/javascript">$('#bar-friend-head').addClass('currently');$('#bar-friend li.small').show();</script>

		<div id="mainarea">
			<div class="tabs_header">
				<ul class="tabs">
					<li class="active"><a><span>好友列表</span></a></li>
					<li><a href="friend_add.aspx"><span>添加好友</span></a></li>
					<li><a><span>邀请好友</span></a></li>
				</ul>
			</div>
			<div class="c_form">
            <textarea id="tplList" style="display:none"><ul class="avatar_list">
	{#foreach $T.table as record}
	<li>
		<div class="avatar48"><a href="javascript:void(0);" onclick="ShowUserPage({$T.record.frienduserid});" title="{$T.record.friendusername}"><img src="../_data/avatar/{$T.record.frienduserid}_m.jpg" onerror="this.src='../_data/avatar/0_m.jpg'" alt="{$T.record.friendusername}" width="48" height="48" /></a></div>
		<p><a href="javascript:void(0);" onclick="ShowUserPage({$T.record.frienduserid});" title="{$T.record.friendusername}">{$T.record.friendusername}</a></p>
		<p><a href="javascript:void(0);" title="将{$T.record.friendusername}从你的好友列表中删除" id="a_online_friend_{$T.record.frienduserid}" onclick="ConfirmDel({$T.record.frienduserid})">解除好友</a></p>
	</li>
	{#/for}
</ul></textarea>
				<div id="ajaxList"></div>
				<div class="clear"></div>
				<div id="ajaxPageBar" class="pages"> </div>
			</div>
		</div>
	</div>
	<div class="clear"></div>
</div>
<!--#include file="include/footer.htm"-->
</body>
</html>

