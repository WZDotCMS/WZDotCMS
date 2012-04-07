<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="page_list.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._page_list" %>
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
	JumboTCMS.Loading.show("正在加载数据,请等待...");
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"page="+currentpage+"&pagesize="+pagesize+"&time="+(new Date().getTime()),
		url:		"page_ajax.aspx?oper=ajaxGetList",
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
				$("#ajaxPageBar").html(d.pagerbar);
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
		url:		"page_ajax.aspx?oper=ajaxDel&time="+(new Date().getTime()),
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
function ajaxCreatePage(id){
	JumboTCMS.Loading.show("正在生成...");
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"id="+id,
		url:		"page_ajax.aspx?oper=ajaxCreatePage&time="+(new Date().getTime()),
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
		<li class="topmenu"><a href="javascript:void(0);" id="pageadd" class="top_link" onclick="JumboTCMS.Popup.show('page_edit.aspx?id=0',-1,-1,false)"><span>增加单页</span></a></li>
	</ul>
	<script>
	topnavbarStuHover();
    </script>
</div>
<textarea id="tplList" style="display:none">
<table class="listtable" cellspacing="0" cellpadding="0" border="0" style="width:100%;border-collapse:collapse;">
	<tr>
		<th scope="col" style="width:40px;">ID</th>
		<th scope="col" style="width:120px;">单页名称</th>
		<th scope="col" style="width:300px;">模板文件</th>
		<th scope="col" width="*">生成文件</th>
		<th scope="col" style="width:200px;">操作</th>
	</tr>
	{#foreach $T.table as record}
	<tr onMouseOver="this.style.backgroundColor='F3F7FF'" onMouseOut="this.style.backgroundColor='FFFFFF'">
		<td align="center">{$T.record.id}</td>
		<td align="center"><a href="{$T.record.outurl}" target="_blank">{$T.record.title}</a></td>
		<td align="left"><%=site.Dir%>templates/{$T.record.source}</td>
		<td align="left"><a href="{$T.record.outurl}" target="_blank">{$T.record.outurl}</a></td>
		<td align="center">
			<a href="javascript:void(0);" onclick="JumboTCMS.Popup.show('page_edit.aspx?id={$T.record.id}',-1,-1,false)">属性设置</a>
			<a href="javascript:void(0);" onclick="JumboTCMS.Popup.show('page_edittemplate.aspx?id={$T.record.id}',760,420,true)">模板编辑</a>
			<a href="javascript:void(0);" onclick="ajaxCreatePage({$T.record.id})">静态生成</a>
			<a href="javascript:void(0);" onclick="ConfirmDel({$T.record.id})">删除</a>
		</td>
	</tr>
	{#/for}
</table>
</textarea>
<div id="ajaxList"></div>
<div id="ajaxPageBar" class="pages"> </div>
</body>
</html>
