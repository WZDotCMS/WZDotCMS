<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="modules_list.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._modules_list" %>
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
});
function ajaxList(currentpage)
{
	if(currentpage!=null) page=currentpage;
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"page="+currentpage+"&pagesize="+pagesize+"&time="+(new Date().getTime()),
		url:		"modules_ajax.aspx?oper=ajaxGetList",
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
function move(id,isUp){
	top.JumboTCMS.Loading.show("正在处理...");
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"id="+id+"&up="+isUp,
		url:		"modules_ajax.aspx?oper=move&time="+(new Date().getTime()),
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
	JumboTCMS.Confirm("确定要删除ID为"+id+"的模型吗?", "ajaxDel("+id+")");
}
function ajaxDel(id){
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"id="+id,
		url:		"modules_ajax.aspx?oper=ajaxDel&time="+(new Date().getTime()),
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
</script>
</head>
<body>
<table cellspacing="0" cellpadding="0" width="100%" class="maintable">
	<tr>
		<th> 前台更新<input type="button" class="tip-r" tip="只当模型有增减时才需要更新" /></th>
		<td align="left"><input type="button" value="执行" class="btnsubmit" onclick="ajaxModuleUpdateFore();" />
		</td>
	</tr>
</table>
<textarea id="tplList" style="display:none">
<table class="cooltable">
<thead>
	<tr>
		<th scope="col" style="width:40px;">ID</th>
		<th scope="col" width="*">模型名称</th>
		<th scope="col" style="width:70px;">模型标识</th>
		<th scope="col" style="width:40px;">排序</th>
		<th scope="col" style="width:35px;">状态</th>
		<th scope="col" style="width:80px;">操作</th>
	</tr>
</thead>
<tbody>
	{#foreach $T.table as record}
	<tr>
		<td align="center">{$T.record.id}</td>
		<td align="center">{$T.record.title}</td>
		<td align="center">
			{#if $T.record.locked == "1"}
			<font color='blue'>系统模型</font>
			{#else}
			<font color='red'>外部模型</font>
			{#/if}
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
			<a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('modules_setting.aspx?type={$T.record.type}',600,480,true)">设置</a>
			<a href="javascript:void(0);" onclick="ConfirmDel({$T.record.id})">删除</a>
		</td>
	</tr>
	{#/for}
</tbody>
</table>
</textarea>
<div id="ajaxList"> </div>
</body>
</html>
