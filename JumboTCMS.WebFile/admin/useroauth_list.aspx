<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="useroauth_list.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._useroauth_list" %>
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
		url:		"useroauth_ajax.aspx?oper=ajaxGetList",
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
		url:		"useroauth_ajax.aspx?oper=ajaxBatchOper&act="+act+"&time="+(new Date().getTime()),
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
		url:		"useroauth_ajax.aspx?oper=move&time="+(new Date().getTime()),
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
function ajaxOAuthUpdateFore()
{
	top.JumboTCMS.Loading.show("正在更新，请等待...");
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"time="+(new Date().getTime()),
		url:		"useroauth_ajax.aspx?oper=updatefore",
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
		<li class="topmenu"><a href="javascript:void(0);" class="top_link"><span
            class="down">批量操作</span></a>
			<ul class="sub">
				<li><a href="javascript:void(0);" onclick="operater('pass')">启用</a></li>
				<li><a href="javascript:void(0);" onclick="operater('nopass')">禁用</a></li>
			</ul>
		</li>
	</ul>
	<script>
	topnavbarStuHover();
        </script>
</div>
<table cellspacing="0" cellpadding="0" width="100%" class="maintable">
	<tr>
		<th> 前台更新</th>
		<td align="left"><input type="button" value="执行" class="btnsubmit" onclick="ajaxOAuthUpdateFore();" />
		</td>
	</tr>
</table>
<textarea id="tplList" style="display:none">
<table class="cooltable">
<thead>
	<tr>
		<th align="center" scope="col" style="width:30px;"><input onclick="checkAllLine()" id="checkedAll" name="checkedAll" type="checkbox" title="全部选择/全部不选" /></th>
		<th scope="col" style="width:40px;">ID</th>
		<th scope="col" style="width:140px;">名称</th>
		<th scope="col" width="*">路径</th>
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
		<td align="center">{$T.record.title}</td>
		<td align="left">
                    	../app/{$T.record.code}/
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
			<a href='javascript:void(0);' onclick="top.JumboTCMS.Alert('请手工修改如下文件：<br /><%=site.Dir %>_data/config/oauth_{$T.record.code}.config', '1');">配置</a>

		</td>
	</tr>
	{#/for}
</tbody>
</table>
</textarea>
<div id="ajaxList"> </div>
</body>
</html>
