<%@ Page Language="C#" AutoEventWireup="True" Codebehind="default.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._index" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>管理中心 - JumboTCMS <%=site.Version %></title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/jcmsV5.js"></script>
<link   type="text/css" rel="stylesheet" href="../_data/jcmsV5.css" />
<link   type="text/css" rel="stylesheet" href="css/index.css" />
<!--后台首页-->
<script type="text/javascript" src="scripts/admin.js"></script>
<script type="text/javascript">
var _height_top = 98;
var _height_bottom = 31;
var _menuid = (q("menuid") != "") ? q("menuid") : "3";
var _menuNum=0;//初始信息
$(document).ready(function(){
	ShowTopTabs(_menuid);
	ShowLeftMenus(_menuid);
	setInterval("setTime()",1000);//当前时间
	JumboTCMS.Event.add(window,"scroll",resizeHeight);
	JumboTCMS.Event.add(window,"resize",resizeHeight);
});
function resizeHeight()
{
	//可判断的类型有msie、mozilla、opera、safa
	//if($.browser.msie) {
		$i("IframeOper").style.height =  (_jcms_GetViewportHeight() - _height_top - _height_bottom -4) + "px";
		$i("ajaxMenuBody").style.height =  (_jcms_GetViewportHeight() - _height_top - _height_bottom - 28) + "px";
	//}
}
function ShowLeftMenus(n){
	$('#ajaxMenuBody').html("<br/><br/><img src='images/index-loading.gif' />");
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"m="+n+"&time="+(new Date().getTime()),
		url:		"ajax.aspx?oper=leftmenu",
		error:		function(XmlHttpRequest,textStatus, errorThrown){alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				JumboTCMS.Alert(d.returnval, "0", "window.location='login.aspx';");
				break;
			case '0':
				JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				_menuNum = d.recordcount;
				ShowPage(d.firstlink);
				$("#ajaxMenuBody").setTemplateElement("tplMenuBody", null, {filter_data: false});
				$("#ajaxMenuBody").processTemplate(d);
				showsubmenu(1);
				AddContextMenu();//右键菜单
				break;
			}
		}
	});
} 
function ShowTopTabs(n){
	$('#coolnav td:even').addClass('mouseout');
	$('#coolnav td#toptab'+n).addClass('selected');
	$('#coolnav td').hover(
		function() {if (!$(this).hasClass('blank')) $(this).addClass('mouseover');},
		function() {if (!$(this).hasClass('blank')) $(this).removeClass('mouseover');}
	);
	$('#coolnav td').click(
		function() {
        		$('#coolnav td').each(function() { 
           			$(this).removeClass('selected');
        		}); 
			$(this).addClass('selected');
		}
	);
}
function setTime(){
	var dt=new Date();
	var arr_week=new Array("星期日","星期一","星期二","星期三","星期四","星期五","星期六");
	var strWeek=arr_week[dt.getDay()];
	var strHour=dt.getHours();
	var strMinutes=dt.getMinutes();
	var strSeconds=dt.getSeconds();
	if (strMinutes<10) strMinutes="0"+strMinutes;
	if (strSeconds<10) strSeconds="0"+strSeconds;
	var strYear=dt.getFullYear()+"年";
	var strMonth=(dt.getMonth()+1)+"月";
	var strDay=dt.getDate()+"日";
	var strTime=strHour+":"+strMinutes+":"+strSeconds;
	$i('time').innerHTML="<span>现在是："+strYear+strMonth+strDay+"&nbsp;"+strTime+"&nbsp;&nbsp;"+strWeek+"</span>";
}
function ShowPage(url){ 
	IframeOper.location.href=url;
} 

function showsubmenu(sid)
{
	for (var i=1; i < ( _menuNum + 1 );i++)
	{
		if (sid != i){
			$i("submenu" + i).style.display="none";
			$i("imgmenu" + i).className = "menu-title0";
		}
		else
		{
			$i("submenu" + i).style.display="";
			$i("imgmenu" + i).className = "menu-title1";
		}
	}

	$('#submenu' + sid + ' td[@name=menu-content]').click(
		function() {

			$('td[@name=menu-content]').removeClass('menu-content1').addClass('menu-content0');
        		$(this).addClass('menu-content1').removeClass('menu-content0');
			ShowPage($(this).attr('url'));
		}
	);
	$('#submenu' + sid + ' td[@name=menu-content]:first').trigger("click");
}
function AddContextMenu(){
	$('span.MenuElement1').each(
		function() {
			$(this).contextMenu('myMenu2', {
				bindings: {
					'channelsetting': function(t) {
						JumboTCMS.Popup.show('channel_edit.aspx?ccid='+(t.id).split('_')[1],-1,-1,true);
					},
					'classmanager': function(t) {
						JumboTCMS.Popup.show('class_list.aspx?ccid='+(t.id).split('_')[1],-1,-1,true);
					},
					'clearcache': function(t) {
						JumboTCMS.Popup.show('createhtml_default.aspx?ccid='+(t.id).split('_')[1],-1,-1,true);
					}
				}
			});
		}
	);
}
</script>
</head>
<body onload="resizeHeight()">
<div class="top">
	<table width="100%" border="0" cellspacing="0" cellpadding="0">
		<tr>
			<td width="8" background="images/index-top-l.gif"><img src="images/index-top-l.gif" width="8" height="98" /></td>
			<td height="98" background="images/index-top-c.gif"><table width="100%" border="0" cellspacing="0" cellpadding="0">
					<tr>
						<td height="30"><table width="100%" height="30" border="0" cellspacing="0" cellpadding="0">
								<tr>
									<td align="left"><strong>将博内容管理系统通用版</strong></td>
									<td align="right" valign="top"><input type="button" title="安全退出" value="" class="close-0" onclick="chkLogout();" onmouseover="this.className='close-1'" onmouseout="this.className='close-0'" /></td>
								</tr>
							</table></td>
					</tr>
					<tr>
						<td height="60">
							<div class="floatleft"><a href="../" target="_blank"><img src="images/index-logo.jpg" height="56" /></a></div>
							<div id="coolnav" class="floatleft" style="overflow:hidden;">
								<table border="0" cellpadding="0" cellspacing="0">
									<tr>
										<td id="toptab3"><div style="cursor:pointer" onclick="ShowLeftMenus(3);"><img src="images/index-nav-img-1.gif" /><br/>后台首页</div></td>

										<td id="toptab5"><div style="cursor:pointer" onclick="ShowLeftMenus('6');"><img src="images/index-nav-img-7.gif" /><br/>内容管理</div></td>
										<%if (AdminIsFounder) { %>
										<td id="toptab1"><div style="cursor:pointer" onclick="ShowLeftMenus('1');"><img src="images/index-nav-img-3.gif" /><br/>用户管理</div></td>
										<td id="toptab2"><div style="cursor:pointer" onclick="ShowLeftMenus('2');"><img src="images/index-nav-img-4.gif" /><br/>模板管理</div></td>
										<td id="toptab2"><div style="cursor:pointer" onclick="ShowLeftMenus('5');"><img src="images/index-nav-img-6.gif" /><br/>插件管理</div></td>
										<td id="toptab4" class="hide"><div style="cursor:pointer" onclick="ShowLeftMenus('4');"><img src="images/index-nav-img-5.gif" /><br/>邮件群发</div></td>
										<td id="toptab0"><div style="cursor:pointer" onclick="ShowLeftMenus('0');"><img src="images/index-nav-img-2.gif" /><br/>全局管理</div></td>
										<%} %>
									</tr></table>
							</div>
						</td>
					</tr>
					<tr>
						<td height="8"></td>
					</tr>
				</table></td>
			<td width="8" background="images/index-top-r.gif"><img src="images/index-top-r.gif" width="8" height="98" /></td>
		</tr>
	</table>
</div>
<div class="contextMenu" id="myMenu2" style="display:none;">
	<ul>
		<li id="channelsetting"><img src="images/index-contextmenu-setting.gif" />频道设置</li>
		<li id="classmanager"><img src="images/index-contextmenu-tree.gif" />栏目管理</li>
		<li id="clearcache"><img src="images/index-contextmenu-refresh.gif" />静态生成</li>
	</ul>
</div>
<textarea id="tplMenuBody" style="display:none">
<table width="151" border="0" align="center" cellpadding="0" cellspacing="0">
{#foreach $T.table as record}
	<tr>
		<td><table width="100%" border="0" cellspacing="0" cellpadding="0">
			<tr>
				<td height="27" id="imgmenu{$T.record.no}" class="menu-title1" onclick="showsubmenu({$T.record.no});">{$T.record.title}</td>
			</tr>
			<tr>
				<td><div name="SubMenu" id="submenu{$T.record.no}" style="display: none;">
					<table width="100%" border="0" cellspacing="0" cellpadding="0">
					{#foreach $T.record.table as record2}
					{#if $T.record2.ischannel != '1'}
						{#if $T.record2.no == 1}
						<tr><td height="27" name="menu-content" class="menu-content1" url="{$T.record2.url}"><span class="MenuElement0">{$T.record2.title}</span></td></tr>
						{#else}
						<tr><td height="27" name="menu-content" class="menu-content0" url="{$T.record2.url}"><span class="MenuElement0">{$T.record2.title}</span></td></tr>
						{#/if}
					{#else}
						{#if $T.record2.no == 1}
						<tr><td height="27" name="menu-content" class="menu-content1" url="{$T.record2.url}"><span class="MenuElement1" id="channel_{$T.record2.channelid}">{$T.record2.title}</span></td></tr>
						{#else}
						<tr><td height="27" name="menu-content" class="menu-content0" url="{$T.record2.url}"><span class="MenuElement1" id="channel_{$T.record2.channelid}">{$T.record2.title}</span></td></tr>
						{#/if}
					{#/if}
					{#/for}
					</table>
				</div></td>
			</tr>
		</table></td>
	</tr>
{#/for}
</table>
</textarea>
<div class="side">
	<table width="155" height="100%" border="0" cellpadding="0" cellspacing="0">
		<tr>
			<td bgcolor="#4B6995" style="width:1px;"></td>
			<td width="153" height="100%" valign="top">
				<div id="menuDiv">
					<table width="151" height="100%" border="0" cellpadding="0" cellspacing="0" align="right" class="menu-box">
						<tr><td height="14" class="menu-top-0"></td></tr>
						<tr><td valign="top" id="ajaxMenuBody" height="*" align="center"></td></tr>
						<tr><td height="14" class="menu-bottom-0"></td></tr>
					</table>
				</div>
			</td>
			<td bgcolor="#FFFFFF" style="width:1px;"></td>
		</tr>
	</table>
</div>
<div class="main">
	<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
		<tr>
			<td width="*" height="100%" valign="top"><table width="100%" height="100%" border="0" cellpadding="0" cellspacing="2">
					<tr>
						<td width="100%" height="100%" valign="top"><iframe name="IframeOper" id="IframeOper" height="100%" width="100%" frameborder="0" marginheight="0" marginwidth="0" src=""></iframe></td>
					</tr>
				</table></td>
			<td bgcolor="#4B6995" style="width:1px;"></td>
		</tr>
	</table>
</div>
<div class="bottom">
	<table width="100%" border="0" cellspacing="0" cellpadding="0">
		<tr>
			<td width="8" background="images/index-bottom-l.gif" style="line-height:31px;"><img src="images/index-bottom-l.gif" width="8" height="31" /></td>
			<td height="31" background="images/index-bottom-c.gif" style="line-height:31px;">
				<div class="floatleft">当前登录：<%=AdminName%></div>
				<div class="floatright" id="time"></div>
			</td>
			<td width="8" background="images/index-bottom-r.gif" style="line-height:31px;"><img src="images/index-bottom-r.gif" width="8" height="31" /></td>
		</tr>
	</table>
</div>
</body>
</html>
