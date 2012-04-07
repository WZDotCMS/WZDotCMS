<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="extends_list.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._extends_list" %>
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
<script type="text/javascript" src="scripts/admin.js"></script>
<script type="text/javascript">
var pagesize=15;
var page=thispage();
$(document).ready(function(){
	$('.tip-r').jtip({gravity: 'r',fade: false});
	ajaxList(page);
    ajaxNew();
});
function ajaxList(currentpage)
{
	if(currentpage!=null) page=currentpage;
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"page="+currentpage+"&pagesize="+pagesize+"&time="+(new Date().getTime()),
		url:		"extends_ajax.aspx?oper=ajaxGetList",
		error:		function(XmlHttpRequest,textStatus, errorThrown){alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				JumboTCMS.Alert(d.returnval, "0", "top.window.location='login.aspx';");
				break;
			case '0':
				JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				$("#ajaxList").setTemplateElement("tplList", null, {filter_data: true});
				$("#ajaxList").processTemplate(d);
				ActiveCoolTable();
				break;
			}
		}
	});
}
function ajaxNew()
{
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"time="+(new Date().getTime()),
		url:		"extends_ajax.aspx?oper=new",
        	error:		function(XmlHttpRequest,textStatus, errorThrown) { alert(XmlHttpRequest.responseText);},
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				JumboTCMS.Alert(d.returnval, "0", "top.window.location='login.aspx';");
				break;
			case '0':
				JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				$("#ajaxNew").setTemplateElement("tplNew", null, {filter_data: true});
				$("#ajaxNew").processTemplate(d);
				break;
			}
		}
	});
}
function operater(act){
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
		data:		"ids="+ids,
		url:		"extends_ajax.aspx?oper=ajaxBatchOper&act="+act+"&time="+(new Date().getTime()),
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
function move(id,isUp){
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"id="+id+"&up="+isUp,
		url:		"extends_ajax.aspx?oper=move&time="+(new Date().getTime()),
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
				top.JumboTCMS.Message(d.returnval, "1");
				ajaxList(page);
				break;
			}
		}
	});
}
function ConfirmDel(id){
	JumboTCMS.Confirm("卸载后插件的数据和目录会自动被删除，确定吗?", "ajaxDel("+id+")");
}
function ajaxDel(id){
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"id="+id,
		url:		"extends_ajax.aspx?oper=ajaxDel&time="+(new Date().getTime()),
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
				top.JumboTCMS.Message(d.returnval, "1");
				ajaxList(page);
				break;
			}
		}
	});
}
function ajaxInstall(extendname)
{
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"name="+extendname,
		url:		"extends_ajax.aspx?oper=install&time="+(new Date().getTime()),
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
				ajaxList(page);
				ajaxNew();
				break;
			}
		}
	});
}
</script>
</head>
<body>
<div class="topnav"> <span class="preload1"></span><span class="preload2"></span>
	<ul id="topnavbar">
		<li class="topmenu"><a href="javascript:void(0);" class="top_link"><span
            class="down">批量操作</span></a>
			<ul class="sub">
				<li><a href="javascript:void(0);" onclick="operater('pass')">启用</a></li>
				<li><a href="javascript:void(0);" onclick="operater('nopass')">禁用</a></li>
			</ul>
		</li>
		<li class="topmenu"><a href="javascript:void(0);" onclick="ajaxNew();" id="operater2" class="top_link"><span>搜索新插件</span></a> </li>
	</ul>
	<script>
	topnavbarStuHover();
        </script>
</div>
<table class="helptable">
	<tr>
		<td><ul>
				<li>插件分为:页面嵌入式 、外部链接式</li>
			</ul></td>
	</tr>
</table>
<textarea id="tplList" style="display:none">
<table class="cooltable">
<thead>
	<tr>
		<th align="center" scope="col" style="width:30px;"><input onclick="checkAllLine()" id="checkedAll" name="checkedAll" type="checkbox" title="全部选择/全部不选" /></th>
		<th scope="col" style="width:40px;">ID</th>
		<th scope="col" style="width:140px;">插件名称</th>
		<th scope="col" style="width:80px;">插件类型</th>
		<th scope="col" width="*">插件路径</th>
		<th style="width:100px;">插件简介</th>
		<th scope="col" style="width:40px;">排序</th>
		<th scope="col" style="width:35px;">状态</th>
		<th scope="col" style="width:35px;">操作</th>
	</tr>
</thead>
<tbody>
	{#foreach $T.table as record}
	<tr>
		<td align="center"><input class="checkbox" name="selectID" type="checkbox" value='{$T.record.id}' />
		</td>
		<td align="center">{$T.record.id}</td>
		<td align="center"><a title="插件作者:{$T.record.author}">{$T.record.title}</a></td>
		<td align="center">
			{#if $T.record.type == "1"}
			<font color='red'>外部链接式</font>
			{#else}
			<font color='blue'>页面嵌入式</font>
			{#/if}
		</td>
		<td align="left">
                    	../extends/{$T.record.name}/
		</td>
		<td align="center">
                    	{$T.record.info}
		</td>
		<td align="center">
			<a href="javascript:void(0)" onclick="move({$T.record.id},1)">↑</a><a style="margin-left:5px" href="javascript:void(0)" onclick="move({$T.record.id},-1)">↓</a>
		</td>
		<td align="center">
			{#if $T.record.enabled == "1"}
			启用
			{#else}
			<font color='red'>已禁</font>
			{#/if}
		</td>
		<td align="center" class="oper">
			{#if $T.record.locked == "0" && $T.record.enabled == "0"}
				<a title="卸载将自动删除数据表" href='javascript:void(0);' onclick="ConfirmDel({$T.record.id})">卸载</a>
			{#else}
			<font color='#cccccc' title="启用的插件不能被卸载，请先禁用">卸载</font>
			{#/if}
		</td>
	</tr>
	{#/for}
</tbody>
</table>
</textarea>
<div id="ajaxList"> </div>
<br />
<table class="helptable">
	<tr>
		<td><ul>
				<li>以下是未安装的插件</li>
			</ul></td>
	</tr>
</table>
<textarea id="tplNew" style="display:none">
<table class="listtable" cellspacing="0" cellpadding="0" border="0" style="width:100%;border-collapse:collapse;">
	<tr>
		<td class="th" align="center" width="150">插件名称</td>
		<td class="th" align="center" width="120">插件类型</td>
		<td class="th" align="center" width="*">插件路径</td>
		<td class="th" align="center" width="70">插件作者</td>
		<td class="th" align="center" width="40">操作</td>
	</tr>
	{#foreach $T.table as record}
	<tr onMouseOver="this.style.backgroundColor='F3F7FF'" onMouseOut="this.style.backgroundColor='FFFFFF'">
		<td align="center">{$T.record.title}</td>
		<td align="center">
			{#if $T.record.type == "1"}
			<font color='blue'>外部链接式</font>
			{#else}
			<font color='red'>页面嵌入式</font>
			{#/if}
		</td>
		<td align="left">
                    	../extends/{$T.record.name}/
		</td>
		<td align="center">
			{$T.record.author}
		</td>
		<td align="center">
			<a href="javascript:void(0);" onclick="ajaxInstall('{$T.record.name}');">安装</a>
		</td>
	</tr>
	{#/for}
</table>
</textarea>
<div id="ajaxNew"> </div>
</body>
</html>
