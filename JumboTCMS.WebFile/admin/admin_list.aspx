<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="admin_list.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._admin_list" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title></title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/jcmsV5.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/jcmsV5.css" />
<link type="text/css" rel="stylesheet" href="css/common.css" />
<script type="text/javascript" src="scripts/common.js"></script>

<script type="text/javascript">
var pagesize=15;
var page=thispage();
$(document).ready(function(){
	ajaxList(page);
});
function ajaxList(currentpage)
{
	if(currentpage!=null) page=currentpage;
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"page="+currentpage+"&pagesize="+pagesize+"&time="+(new Date().getTime()),
		url:		"admin_ajax.aspx?oper=ajaxGetList",
		error:		function(XmlHttpRequest,textStatus, errorThrown){alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				top.JumboTCMS.Alert(d.returnval, "0", "top.window.location='login.aspx';");
				break;
			case '0':
				window.JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				$("#ajaxList").setTemplateElement("tplList", null, {filter_data: true});
				$("#ajaxList").processTemplate(d);
				ActiveCoolTable();
				$("#ajaxPageBar").html(d.pagerbar);
				break;
			}
		}
	});
}
function ConfirmDel(id){
	window.JumboTCMS.Confirm("确定要删除吗?", "ajaxDel("+id+")");
}
function ajaxDel(id){
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"id="+id,
		url:		"admin_ajax.aspx?oper=ajaxDel&time="+(new Date().getTime()),
		error:		function(XmlHttpRequest,textStatus, errorThrown){alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				window.JumboTCMS.Alert(d.returnval, "0", "top.window.location='login.aspx';");
				break;
			case '0':
				window.JumboTCMS.Alert(d.returnval, "0");
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
<table class="helptable">
	<tr>
		<td>
			<ul>
				<li>此处添加的管理员无权操作“系统管理”和“用户管理”</li>
				<li>只有site.config中Founders节点上的用户才是超级管理员</li>
			</ul>
		</td>
	</tr>
</table>
<div class="topnav"> <span class="preload1"></span><span class="preload2"></span>
	<ul id="topnavbar">
		<li class="topmenu"><a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('admin_add.aspx?id=0',650,320,false)" class="top_link"><span>添加管理员</span></a></li>
	</ul>
	<script>
	topnavbarStuHover();
        </script>
</div>
<textarea id="tplList" style="display:none">
<table class="cooltable">
<thead>
	<tr>
		<th scope="col" style="width:40px;">ID</th>
		<th scope="col" width="*">登录名</th>
		<th scope="col" style="width:150px;">上次登陆时间</th>
		<th scope="col" style="width:140px;">上次登陆IP</th>
		<th scope="col" style="width:50px;">状态</th>
		<th scope="col" style="width:150px;">操作</th>
	</tr>
</thead>
<tbody>
	{#foreach $T.table as record}
	<tr>
		<td align="center">{$T.record.adminid}</td>
		<td align="center">{$T.record.adminname}</td>
		<td align="left">{$T.record.lasttime2}</td>
		<td align="center">{$T.record.lastip2}</td>
		<td align="center">
			{#if $T.record.adminstate == "1"}
			<font color='blue'>启用</font>
			{#else}
			<font color='red'>已禁</font>
			{#/if}
		</td>
		<td align="center" class="oper">
           		<a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('admin_edit.aspx?id={$T.record.id}',450,230,true)">修改</a>
			<a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('admin_editpower.aspx?id={$T.record.id}',-1,-1,true)">编辑权限</a>
			<a href="javascript:void(0);" onclick="ConfirmDel({$T.record.id})">删除</a>
		</td>
	</tr>
	{#/for}
</tbody>
</table>
</textarea>
    <div id="ajaxList">
    </div>
    <div id="ajaxPageBar" class="pages">
    </div>
</body>
</html>
