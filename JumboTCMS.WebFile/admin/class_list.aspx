<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="class_list.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._class_list" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>栏目列表</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/jcmsV5.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/jcmsV5.css" />
<link type="text/css" rel="stylesheet" href="css/common.css" />
<script type="text/javascript" src="scripts/common.js"></script>



<script type="text/javascript">
var ccid = joinValue('ccid');
var pagesize=15;
var page=thispage();
$(document).ready(function(){
	$('.tip-r').jtip({gravity: 'r',fade: false});
	ajaxList(page);

});
function ajaxList(currentpage)
{
	if(currentpage!=null) page=currentpage;
	JumboTCMS.Loading.show("正在加载数据,请等待...");
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"page="+currentpage+"&pagesize="+pagesize+"&time="+(new Date().getTime()) + ccid,
		url:		"class_ajax.aspx?oper=ajaxGetList",
		error:		function(XmlHttpRequest,textStatus, errorThrown){JumboTCMS.Loading.hide();alert(XmlHttpRequest.responseText); },
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
				JumboTCMS.Loading.hide();
				$("#ajaxList").setTemplateElement("tplList", null, {filter_data: true});
				$("#ajaxList").processTemplate(d);
				ActiveCoolTable();
				break;
			}
		}
	});
}
function move(id,isUp){
	JumboTCMS.Loading.show("正在处理...");
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"id="+id+"&up="+isUp,
		url:		"class_ajax.aspx?oper=move&time="+(new Date().getTime()) + ccid,
		error:		function(XmlHttpRequest,textStatus, errorThrown){JumboTCMS.Loading.hide();alert(XmlHttpRequest.responseText); },
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
				JumboTCMS.Message(d.returnval, "1");
				ajaxList(page);
				break;
			}
		}
	});
}
function ConfirmDel(id,isout){
	if(isout == 1)
		JumboTCMS.Confirm("确定要删除此栏目吗?", "ajaxDel("+id+","+isout+")");
	else
		JumboTCMS.Confirm("确定要删除此栏目吗?", "ajaxDel("+id+","+isout+")");
}
function ajaxDel(id,isout){
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"id="+id+"&isout="+isout,
		url:		"class_ajax.aspx?oper=ajaxDel&time="+(new Date().getTime()) + ccid,
		error:		function(XmlHttpRequest,textStatus, errorThrown){JumboTCMS.Loading.hide();alert(XmlHttpRequest.responseText); },
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
				ajaxList(page);
				break;
			}
		}
	});
}
function ajaxUpdateFore(){
	JumboTCMS.Loading.show("正在生成...");
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"",
		url:		"class_ajax.aspx?oper=createnav&time="+(new Date().getTime()) + ccid,
		error:		function(XmlHttpRequest,textStatus, errorThrown){JumboTCMS.Loading.hide();alert(XmlHttpRequest.responseText); },
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
				JumboTCMS.Message(d.returnval, "1");
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
		<li class="topmenu"><a href="javascript:void(0);" id="classadd" class="top_link" onclick="JumboTCMS.Popup.show('class_edit.aspx?id=0'+ccid,-1,-1,false)"><span>增加栏目</span></a>
		</li>
	</ul>
	<script>
	topnavbarStuHover();
    </script>
</div>
<table class="formtable">
	<tr>
		<th>更新前台“当前页位置”<input type="button" class="tip-r" tip="<b>当栏目有增减或修改时，需要点击更新</b><br />更新的内容主要是该频道栏目页和内容页的“当前页位置”" /></th>
		<td><input type="button" value="执行" class="btnsubmit" onclick="ajaxUpdateFore();" />
		</td>
	</tr>
</table>
<br />
<textarea id="tplList" style="display:none">
<table class="cooltable">
<thead>
	<tr>
		<th align="center" scope="col" style="width:30px;"><input onclick="checkAllLine()" id="checkedAll" name="checkedAll" type="checkbox" title="全部选择/全部不选" /></th>
		<th scope="col" style="width:40px;">ID</th>
		<th scope="col" width="*">栏目名称</th>
		<th scope="col" style="width:60px;">默认每页</th>
		<th scope="col" style="width:150px;">列表页模板</th>
		<th scope="col" style="width:150px;">内容页模板</th>
		<th scope="col" style="width:40px;">排序</th>
		<th scope="col" style="width:150px;">操作</th>
	</tr>
</thead>
<tbody>
	{#foreach $T.table as record}
	<tr>
		<td align="center"><input class="checkbox" name="selectID" type="checkbox" value='{$T.record.id}' />
		</td>
		<td align="center">{$T.record.id}</td>
		<td align="left">
           		{#if $T.record.codelength < 10}
                	<img src="images/li0{$T.record.codelength}.gif" />
            		{#else}
                	<img src="images/li{$T.record.codelength}.gif" />
			{#/if}
			{$T.record.title}
		</td>
		<td align="center">{$T.record.pagesize}</td>
		<td align="center">{$T.record.templatename}</td>
		<td align="center">{$T.record.contenttempname}</td>
		<td align="center">
			<a href="javascript:void(0)" onclick="move({$T.record.id},1)">↑</a><a style="margin-left:5px" href="javascript:void(0)" onclick="move({$T.record.id},-1)">↓</a>
		</td>
		<td align="center" class="oper">
           		{#if $T.record.codelength < <%=ChannelClassDepth*4%>}
			<a href="javascript:void(0);" onclick="JumboTCMS.Popup.show('class_edit.aspx?id=0&parentid={$T.record.id}'+ ccid,-1,-1,true)">增加子栏</a>
            		{#else}
                	<font color='#cccccc'>增加子栏</font>
			{#/if}
			<a href="javascript:void(0);" onclick="JumboTCMS.Popup.show('class_edit.aspx?id={$T.record.id}'+ ccid,-1,-1,true)">修改</a>
			<a href="javascript:void(0);" onclick="ConfirmDel({$T.record.id},{$T.record.isout})">删除</a>
		</td>
	</tr>
	{#/for}
</tbody>
</table>
</textarea>
<div id="ajaxList"> </div>
<script type="text/javascript">_jcms_SetDialogTitle();</script>
</body>
</html>
