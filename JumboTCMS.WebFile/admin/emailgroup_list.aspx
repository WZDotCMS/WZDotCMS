<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="emailgroup_list.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._emailgroup_list" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title></title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/jcmsV5.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/jcmsV5.css" />
<link type="text/css" rel="stylesheet" href="css/common.css" />
<script type="text/javascript" src="scripts/common.js"></script>

<script language="javascript">
var pagesize=15;
var page=thispage();
$(document).ready(function(){
	ajaxList(page);
});
function ajaxList(currentpage)
{
	if(currentpage!=null) page=currentpage;
	top.JumboTCMS.Loading.show("正在加载数据,请等待...",260,80);
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"page="+currentpage+"&pagesize="+pagesize+"&time="+(new Date().getTime()),
		url:		"emailgroup_ajax.aspx?oper=ajaxGetList",
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
				break;
			}
		}
	});
}
function ConfirmDel(id){
	top.JumboTCMS.Confirm("确定要删除吗?", "IframeOper.ajaxDel("+id+")");
}
function ajaxDel(id){
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"id="+id,
		url:		"emailgroup_ajax.aspx?oper=ajaxDel&time="+(new Date().getTime()),
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
function ajaxEmailGroupCount(){
	top.JumboTCMS.Loading.show("正在统计...",260,80);
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"time="+(new Date().getTime()),
		url:		"emailgroup_ajax.aspx?oper=ajaxEmailGroupCount",
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
    </script>
</head>
<body>
<div class="topnav"> <span class="preload1"></span><span class="preload2"></span>
	<ul id="topnavbar">
		<li class="topmenu"><a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('emailgroup_edit.aspx',-1,-1,true)" id="operater2" class="top_link"><span>添加邮箱组</span></a> </li>
	</ul>
	<script>
	topnavbarStuHover();
        </script>
</div>
<table cellspacing="0" cellpadding="0" width="100%" class="maintable">
	<tr>
		<th>邮件统计</th>
		<td align="left"><input type="button" value="执行" class="btnsubmit" onclick="ajaxEmailGroupCount();" />
		</td>
	</tr>
</table>
<textarea id="tplList" style="display:none">
<table class="cooltable">
<thead>
	<tr>
		<th align="center" scope="col" style="width:30px;"><input onclick="checkAllLine()" id="checkedAll" name="checkedAll" type="checkbox" title="全部选择/全部不选" /></th>
		<th scope="col" style="width:40px;">ID</th>
		<th scope="col" width="*">组名</th>
		<th scope="col" style="width:55px;">邮箱数</th>
		<th scope="col" style="width:80px;">操作</th>
	</tr>
</thead>
<tbody>
	{#foreach $T.table as record}
	<tr>
		<td align="center"><input class="checkbox" name="selectID" type="checkbox" value='{$T.record.id}' />
		</td>
		<td align="center">{$T.record.id}</td>
		<td align="center">{$T.record.groupname}</td>
		<td align="center">{$T.record.emailtotal}</td>
		<td align="center" class="oper">
           		<a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('emailgroup_edit.aspx?id={$T.record.id}',-1,-1,true)">编辑</a>
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
