﻿<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="user_list.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._user_list" %>
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
function ajaxGroupList()
{
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"time="+(new Date().getTime()),
		url:		"usergroup_ajax.aspx?oper=ajaxGetList",
		error:		function(XmlHttpRequest,textStatus, errorThrown) { alert(XmlHttpRequest.responseText);},
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				top.JumboTCMS.Alert(d.returnval, "0", "top.window.location='login.aspx';");
				break;
			case '0':
				top.JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				$("#ajaxGroupList").setTemplateElement("tplGroupList", null, {filter_data: true});
				$("#ajaxGroupList").processTemplate(d);
				break;
			}
		}
	});
}
var gid = joinValue('gid');//用户组ID
var keys=joinValue('keys');//关键字
var pagesize=15;
var page=thispage();
$(document).ready(function(){
	ajaxGroupList();
	ajaxList(page);

});
function ajaxList(currentpage)
{
	if(currentpage!=null) page=currentpage;
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"page="+currentpage+"&pagesize="+pagesize+"&time="+(new Date().getTime()),
		url:		"user_ajax.aspx?oper=ajaxGetList"+gid+keys,
		error:		function(XmlHttpRequest,textStatus, errorThrown) { alert(XmlHttpRequest.responseText);},
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				top.JumboTCMS.Alert(d.returnval, "0", "top.window.location='login.aspx';");
				break;
			case '0':
				top.JumboTCMS.Alert(d.returnval, "0");
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
function operater(act,groupid){
	var ids = JoinSelect("selectID");
	if(ids=="")
	{
		top.JumboTCMS.Alert("请先勾选要操作的内容", "0"); 
		return;
	}
	top.JumboTCMS.Loading.show("正在处理...");
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"ids="+ids+"&togid="+groupid,
		url:		"user_ajax.aspx?oper=ajaxBatchOper&act="+act+"&time="+(new Date().getTime()),
		error:		function(XmlHttpRequest,textStatus, errorThrown){top.JumboTCMS.Loading.hide();alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				top.JumboTCMS.Alert(d.returnval, "0", "top.window.location='login.aspx';");
				break;
			case '0':
				top.JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				top.JumboTCMS.Message(d.returnval, "1");
				ajaxList(page);
				break;
			}
		}
	});
}
function ConfirmDel(id){
	top.top.JumboTCMS.Confirm("删除会员将同时删除与其相关的信息，确定吗?", "IframeOper.ajaxDel("+id+")");
}
function ajaxDel(id){
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"id="+id,
		url:		"user_ajax.aspx?oper=ajaxDel&time="+(new Date().getTime()),
		error:		function(XmlHttpRequest,textStatus, errorThrown){alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				top.JumboTCMS.Alert(d.returnval, "0", "top.window.location='login.aspx';");
				break;
			case '0':
				top.JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				ajaxList(page);
				break;
			}
		}
	});
}
function move2group(groupid){
	operater("move2group",groupid);
}
function ajaxSearch(){
	top.JumboTCMS.Popup.show("user_searchform.aspx?state="+gid+keys,500,280,true);
}
</script>
</head>
<body>
<textarea id="tplGroupList" style="display:none">
{#foreach $T.table as record}
<li><a href="javascript:void(0);" onclick="move2group({$T.record.id})"> {$T.record.groupname}</a></li>
{#/for}
</textarea>
<div class="topnav"> <span class="preload1"></span><span class="preload2"></span>
	<ul id="topnavbar">
		<li class="topmenu"><a href="javascript:void(0);" class="top_link"><span
            class="down">批量操作</span></a>
			<ul class="sub">
				<li><a href="javascript:void(0);" onclick="operater('pass')">启用</a></li>
				<li><a href="javascript:void(0);" onclick="operater('nopass')">禁用</a></li>
				<li><a class="fly" href="javascript:void(0);" id="move2group">转移分组</a>
					<ul id="ajaxGroupList"></ul>
				</li>
			</ul>
		</li>
		<li class="topmenu"><a href="javascript:void(0);" onclick="ajaxSearch();" class="top_link"><span>过滤检索</span></a></li>
	</ul>
	<script>topnavbarStuHover();</script>
</div>
<textarea id="tplList" style="display:none">
<table class="cooltable">
<thead>
	<tr>
		<th align="center" scope="col" style="width:30px;"><input onclick="checkAllLine()" id="checkedAll" name="checkedAll" type="checkbox" title="全部选择/全部不选" /></th>
		<th scope="col" style="width:60px;">ID</th>
		<th width="*">用户名</th>
		<th style="width:75px;">用户组</th>
		<th scope="col" style="width:90px;">VIP有效期</th>
		<th scope="col" style="width:60px;">博币</th>
		<th scope="col" style="width:60px;">积分</th>
		<th scope="col" style="width:40px;">状态</th>
		<th scope="col" style="width:290px;">操作</th>
	</tr>
</thead>
<tbody>
	{#foreach $T.table as record}
	<tr>
		<td align="center"><input class="checkbox" name="selectID" type="checkbox" value='{$T.record.id}' /></td>
		<td align="center">{$T.record.id}</td>
		<td align="center">{$T.record.username}</td>
		<td align="center">{$T.record.groupname}</td>
		<td align="left">
			{#if $T.record.isvip == "1"}
			{formatDate($T.record.viptime,"yyyy-MM-dd")}
			{#/if}
			
		</td>
		<td align="center">{$T.record.points}</td>
		<td align="center">{$T.record.integral}</td>
		<td align="center">
			{#if $T.record.state == "1"}
			启用
			{#else}
			<font color='red'>已禁</font>
			{#/if}
		</td>
		<td align="center" class="oper">
			<a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('user_preview.aspx?id={$T.record.id}',-1,-1,true)">详细信息</a>
           		<a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('user_edit.aspx?id={$T.record.id}',450,280,true)">特殊处理</a>
			{#if $T.record.adminid == "0"}
          		<a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('admin_add.aspx?id={$T.record.id}',500,320,false)">升为管理员</a>
			{#else}
			<font color='#cccccc'>升为管理员</font>
			{#/if}
			{#if $T.record.serviceid == "0" || $T.record.serviceid == ""}
          		<a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('/admin/service_add.aspx?id={$T.record.id}',450,170,false)">升为客服</a>
			{#else}
			<font color='#cccccc'>升为客服</font>
			{#/if}
			<a href="javascript:void(0);" onclick="ConfirmDel({$T.record.id})">删除会员</a>
		</td>
	</tr>
	{#/for}
</tbody>
</table>
</textarea>
    <div id="ajaxList"></div>
    <div id="ajaxPageBar" class="pages"></div>
</body>
</html>
