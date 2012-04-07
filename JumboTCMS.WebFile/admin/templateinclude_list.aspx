<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="templateinclude_list.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._templateinclude_list" %>
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
var pid = joinValue('pid');
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
		data:		"page="+currentpage+"&pagesize="+pagesize+"&time="+(new Date().getTime()) + pid,
		url:		"templateinclude_ajax.aspx?oper=ajaxGetList",
		error:		function(XmlHttpRequest,textStatus, errorThrown){top.JumboTCMS.Loading.hide();alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				top.JumboTCMS.Alert(d.returnval, "0", "top.window.location='" + site.Dir + "admin/login.aspx';");
				break;
			case '0':
				top.JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				top.JumboTCMS.Loading.hide();
				$("#ajaxList").setTemplateElement("tplList", null, {filter_data: true});
				$("#ajaxList").processTemplate(d);
				break;
			}
		}
	});
}
function ConfirmDel(id){
	JumboTCMS.Confirm("确定要删除吗?", "ajaxDel("+id+")");
}
function ajaxDel(id){
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"id="+id,
		url:		"templateinclude_ajax.aspx?oper=ajaxDel&time="+(new Date().getTime()) + pid,
		error:		function(XmlHttpRequest,textStatus, errorThrown){alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				top.JumboTCMS.Alert(d.returnval, "0", "top.window.location='" + site.Dir + "admin/login.aspx';");
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
    </script>

</head>
<body>
    <div class="topnav"> <span class="preload1"></span><span class="preload2"></span>
	<ul id="topnavbar">
		<li class="topmenu"><a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('templateinclude_edit.aspx?id=0'+pid,800,280,true)" id="operateradd" class="top_link"><span>增加文件</span></a>
	</ul>
	<script>
	topnavbarStuHover();
    </script>
</div>
<table cellspacing="0" cellpadding="0" width="100%" class="maintable">
	<tr>
		<th> 前台批量更新</th>
		<td align="left"><input type="button" value="执行" class="btnsubmit" onclick="ajaxTemplateIncludeUpdateFore(q('pid'));" />
		</td>
	</tr>
</table>
<textarea id="tplList" style="display:none">
<table class="listtable" cellspacing="0" cellpadding="0" border="0" style="width:100%;border-collapse:collapse;">
	<tr>
		<th scope="col" style="width:60px;">ID</th>
		<th scope="col" width="*">模块名称</th>
		<th scope="col" style="width:200px;">文件名称</th>
		<th scope="col" style="width:60px;">需要编译</th>
		<th scope="col" style="width:50px;">优先级</th>
		<th scope="col" style="width:200px;">操作</th>
	</tr>
	{#foreach $T.table as record}
	<tr onmouseover="this.style.backgroundColor='F3F7FF'" onmouseout="this.style.backgroundColor='FFFFFF'">
		<td align="center">{$T.record.id}</td>
		<td align="center">{$T.record.title}</td>
		<td align="left">{$T.record.source}</td>
		<td align="center">
			{#if $T.record.needbuild == "1"}
			是
			{#else}
			<font color='red'>否</font>
			{#/if}
		</td>
		<td align="center">{$T.record.sort}</td>
		<td align="center">
			<a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('templateinclude_edit.aspx?id={$T.record.id}'+ pid,800,280,true)">属性设置</a>
			<a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('templateinclude_edittemplate.aspx?source={$T.record.source}{pid}',-1,-1,true)">在线编辑</a>
			<a href="javascript:void(0);" onclick="ajaxTemplateIncludeUpdateFore(q('pid'),'{$T.record.source}')">前台更新</a>
			<a href="javascript:void(0);" onclick="ConfirmDel({$T.record.id})">删除</a>
		</td>
	</tr>
	{#/for}
</table>
</textarea>
<div id="ajaxList"></div>

</body>
</html>
