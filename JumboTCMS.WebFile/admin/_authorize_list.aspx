<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="_authorize_list.aspx.cs" Inherits="JumboTCMS.WebFile.Admin.__authorize_list" %>
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
	top.JumboTCMS.Loading.show("正在加载数据,请等待...");
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"page="+currentpage+"&pagesize="+pagesize+"&time="+(new Date().getTime()),
		url:		"_authorize_ajax.aspx?oper=ajaxGetList",
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
				top.JumboTCMS.Loading.hide();
				$("#ajaxList").setTemplateElement("tplList", null, {filter_data: true});
				$("#ajaxList").processTemplate(d);
				ActiveCoolTable();
				$("#ajaxPageBar").html(d.pagerbar);
				break;
			}
		}
	});
}
function ajaxCheck(id){
	top.JumboTCMS.Loading.show("正在检测,请等待...");
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"id="+id,
		url:		"_authorize_ajax.aspx?oper=ajaxCheck&time="+(new Date().getTime()),
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
				ajaxList(page);
				break;
			}
		}
	});
}
function ajaxRemind(id){
	top.JumboTCMS.Loading.show("正在发送邮件,请等待...");
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"id="+id,
		url:		"_authorize_ajax.aspx?oper=ajaxRemind&time="+(new Date().getTime()),
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
		<li class="topmenu"><a href="javascript:void(0);" onclick="JumboTCMS.Popup.show('_authorize_edit.aspx',450,-80,true)" id="operater2" class="top_link"><span>添加网站</span></a> </li>
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
		<th scope="col" width="*">域名</th>
		<th scope="col" style="width:180px;">网站名称</th>
		<th scope="col" style="width:80px;">网站类型</th>
		<th scope="col" style="width:80px;">授权类型</th>
		<th scope="col" style="width:120px;">授权起始时间</th>
		<th scope="col" style="width:40px;">状态</th>
		<th scope="col" style="width:125px;">操作</th>
	</tr>
</thead>
<tbody>
	{#foreach $T.table as record}
	<tr>
		<td align="center">{$T.record.id}</td>
		<td align="center"><a href="{$T.record.defaultpage}" target="_blank">{$T.record.domain}</a></td>
		<td align="center">{$T.record.webname}</td>
		<td align="center">{$T.record.sitetypename}</td>
		<td align="center">{$T.record.accredittypename}</td>
		<td align="center">{formatDate($T.record.addtime,'yyyy-MM-dd')}</td>
		<td align="center">
			{#if $T.record.state == "1"}
			正常
			{#else}
			<font color='red'>可疑</font>
			{#/if}
		</td>
		<td align="center" class="oper">
			{#if $T.record.accredittype == "1"}
			<a href="javascript:void(0);" onclick="ajaxCheck({$T.record.id})">检测</a>
			{#else}
			<font color='#eeeeee'>检测</font>
			{#/if}
           		<a href="javascript:void(0);" onclick="JumboTCMS.Popup.show('_authorize_edit.aspx?id={$T.record.id}',450,-80,true)">修改</a>
			{#if $T.record.state == "1"}
			<font color='#eeeeee'>违规提醒</font>
			{#else}
			    {#if $T.record.linkemail == "" && $T.record.mobiletel == ""}
			    <font color='#eeeeee'>违规提醒</font>
			    {#else}
			    <a href="javascript:void(0);" onclick="ajaxRemind({$T.record.id})">违规提醒</a>
			    {#/if}
			{#/if}
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
