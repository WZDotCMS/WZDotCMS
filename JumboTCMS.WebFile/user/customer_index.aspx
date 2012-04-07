<%@ Page Language="C#" AutoEventWireup="True" Codebehind="customer_index.aspx.cs" Inherits="JumboTCMS.WebFile.User._customer_index" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-

transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>个人中心 - <%=site.Name%></title>
<script type="text/javascript" src="../../_data/json/_systemcount.js"></script>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/jcmsV5.js"></script>
<link   type="text/css" rel="stylesheet" href="../_data/jcmsV5.css" />
<script type="text/javascript" src="js/fore.common.js"></script>
<link   type="text/css" rel="stylesheet" href="../style/member/style.css" />
<script type="text/javascript">
$(document).ready(function(){
	ajaxBindUserData('BindOtherData(_jcms_UserData)');//绑定会员数据
	ajaxTopFriendList();
});
function BindOtherData(data){
	$(".index_name").html(data.username+"（"+data.groupname+"）");
}
function ajaxTopFriendList()
{
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"page=1&pagesize=6&time="+(new Date().getTime()),
		url:		"../user/ajax.aspx?oper=ajaxGetFriendList",
		success:	function(d){
			switch (d.result)
			{
			case '0':
				JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				$("#ajaxTopFriendList").setTemplateElement("tplRightMenu", null, {filter_data: 

false});
				$("#ajaxTopFriendList").processTemplate(d);
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
		<script type="text/javascript">$('#bar-customer-head').addClass('currently');$('#bar-customer li.small').show();</script>
		<div id="mainarea">
			<div id="content">
				<div class="composer_header">
					<div style="float:left;width:137px;">
						<div class="ar_r_t">
							<div class="ar_l_t">
								<div class="ar_r_b">
									<div class="ar_l_b"><img id="u_myface_l" src="../_data/avatar/<%=UserId%>_l.jpg" onerror="this.src='../_data/avatar/0_l.jpg'" width="120" height="120" /></div>
								</div>
							</div>
						</div>
					</div>
					<div class="composer" style="float:left">
						<h3 class="index_name"><span class="u_username"></span>（<span class="u_groupname"></span>）</h3>
						<p>绑定邮箱：<span class="u_email"></span></p>
						<p>当前博币 <span class="u_points">0</span></p>
						<p><span class="u_vipdate" style="color:red;"></span></p>
					</div>
				</div>
				<div class="clear"></div>
				<div class="nav_two">
					<ul>
						<li><a>用户等级说明</a></li>
					</ul>
					<div class="clear"></div>
				</div>
				<div class="inportant_remind">
					<ul>
						<li><span class="b">游客：</span><span>可浏览下载本站不扣博币的内容。</span></li>
						<li><span class="b">普通用户：</span><span>除拥有游客权限外，根据每篇内容的提示的应扣博币下载资料（注：重复下载不会重复扣分）。可通过<a href="bobi_buypoints.aspx" target="_top">博币充值</a>获得相应博币。</span></li>
						<li><span class="b">VIP用户：</span><span>在VIP有效期内，可浏览下载本站所有内容。普通用户可通过包年服务升级到VIP用户。</span></li>
					</ul>
				</div>
				<div class="clear"></div>
			</div>
			<div id="sidebar">
 				<div class="sidebox">
					<h2 class="title">
						<p class="r_option"> <a href="friend_list.aspx">全部</a> </p>
						我的好友 </h2>
                    <textarea id="tplRightMenu" style="display:none">{#foreach $T.table as record}
<li>
	<div class="avatar48"><a href="javascript:void(0);" onclick="ShowUserPage({$T.record.frienduserid});" 

title="{$T.record.friendusername}"><img src="../_data/avatar/{$T.record.frienduserid}_m.jpg" 

onerror="this.src='../_data/avatar/0_m.jpg'" alt="{$T.record.friendusername}" width="48" height="48" /></a></div>
	<p><a href="javascript:void(0);" onclick="ShowUserPage({$T.record.frienduserid});" 

title="{$T.record.friendusername}">{$T.record.friendusername}</a></p>
</li>
{#/for}</textarea>
					<ul class="avatar_list" id="ajaxTopFriendList">
					</ul>
				</div>
			</div>
		</div>
		<div id="bottom"></div>
	</div>
	<div class="clear"></div>
</div>
<!--#include file="include/footer.htm"-->
</body>
</html>
