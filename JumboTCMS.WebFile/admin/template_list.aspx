<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="template_list.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._template_list" %>
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
var pid = joinValue('pid');
function ajaxTopNav()
{
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"time="+(new Date().getTime()),
		url:		"modules_ajax.aspx?oper=ajaxGetList&enabled=1",
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
				$("#ajaxTopNav").setTemplateElement("tplTopNav", null, {filter_data: true});
				$("#ajaxTopNav").processTemplate(d);
				break;
			}
		}
	});
}
var pagesize=15;
var page=thispage();
$(document).ready(function(){
	ajaxTopNav();
	ajaxList(page);
});
function ajaxList(currentpage)
{
	if(currentpage!=null) page=currentpage;
	JumboTCMS.Loading.show("正在加载数据,请等待...");
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"page="+currentpage+"&pagesize="+pagesize+"&time="+(new Date().getTime()) + pid,
		url:		"template_ajax.aspx?oper=ajaxGetList",
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
		url:		"template_ajax.aspx?oper=ajaxDel&time="+(new Date().getTime()),
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
function ConfirmDef(id){
	JumboTCMS.Confirm("设为默认,确定吗?", "ajaxDef("+id+")");
}
function ajaxDef(id){
	JumboTCMS.Loading.show("正在处理...");
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"id="+id,
		url:		"template_ajax.aspx?oper=ajaxDef&time="+(new Date().getTime()),
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
    </script>

</head>
<body>
<textarea id="tplTopNav" style="display:none">
<div class="topnav"> <span class="preload1"></span><span class="preload2"></span>
	<ul id="topnavbar">
		<li class="topmenu"><a href="javascript:void(0);" id="templateadd" class="top_link"><span
            class="down">增加模板</span></a>
			<ul class="sub">
				<li><a class="fly" href='javascript:void(0);'>系统类</a>
					<ul>
						<li><a href="javascript:void(0);" onclick="JumboTCMS.Popup.show('template_edit.aspx?type=system&stype=index&id=0'+pid,500,280,false)"> 系统首页</a></li>
					</ul>
				</li>
				{#foreach $T.table as record}
				<li><a class="fly" href='javascript:void(0);'>{$T.record.title}类</a>
					<ul>
						<li><a href="javascript:void(0);" onclick="JumboTCMS.Popup.show('template_edit.aspx?type={$T.record.type}&stype=channel&id=0'+pid,500,280,false)">频道页</a></li>
						<li><a href="javascript:void(0);" onclick="JumboTCMS.Popup.show('template_edit.aspx?type={$T.record.type}&stype=class&id=0'+pid,500,280,false)">列表页</a></li>
						<li><a href="javascript:void(0);" onclick="JumboTCMS.Popup.show('template_edit.aspx?type={$T.record.type}&stype=content&id=0'+pid,500,280,false)">内容页</a></li>
					</ul>
				</li>
				{#/for}
			</ul>
		</li>
	</ul>
	<script>
	topnavbarStuHover();
    </script>
</div>
</textarea>
<div id="ajaxTopNav"></div>
<textarea id="tplList" style="display:none">
<table class="listtable" cellspacing="0" cellpadding="0" border="0" style="width:100%;border-collapse:collapse;">
	<tr>
		<th scope="col" style="width:40px;">ID</th>
		<th scope="col" width="*">模板名称</th>
		<th scope="col" style="width:170px;">模板文件名</th>
		<th scope="col" style="width:140px;">模板类型</th>
		<th scope="col" style="width:220px;">操作</th>
	</tr>
	{#foreach $T.table as record}
	<tr onMouseOver="this.style.backgroundColor='F3F7FF'" onMouseOut="this.style.backgroundColor='FFFFFF'">
		<td align="center">{$T.record.id}</td>
		<td align="left">{$T.record.title}</td>
		<td align="left">{$T.record.source}</td>
		<td align="left">{$T.record.type}-{$T.record.stype}</td>
		<td align="center">
			<a href="javascript:void(0);" onclick="JumboTCMS.Popup.show('template_edit.aspx?id={$T.record.id}{pid}',500,280,false)">属性设置</a>
			<a href="javascript:void(0);" onclick="JumboTCMS.Popup.show('template_edittemplate.aspx?id={$T.record.id}{pid}',-1,-1,true)">在线编辑</a>
			{#if $T.record.isdefault == "1"}
			<font color='#cccccc'>设为默认</font>
			<font color='#cccccc'>删除</font>
			{#else}
			<a href="javascript:void(0);" onclick="ConfirmDef({$T.record.id})">设为默认</a>
			<a href="javascript:void(0);" onclick="ConfirmDel({$T.record.id})">删除</a>
			{#/if}
		</td>
	</tr>
	{#/for}
</table>
</textarea>
<div id="ajaxList"></div>
</body>
</html>
