<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="channel_list.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._channel_list" %>
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
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"page="+currentpage+"&pagesize="+pagesize+"&time="+(new Date().getTime()),
		url:		"channel_ajax.aspx?oper=ajaxGetList",
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
		JumboTCMS.Alert("请先勾选要操作的内容", "0"); 
		return;
	}
	JumboTCMS.Loading.show("正在处理...");
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"ids="+ids,
		url:		"channel_ajax.aspx?oper=ajaxBatchOper&act="+act+"&time="+(new Date().getTime()),
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
				JumboTCMS.Message(d.returnval, "1");
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
		url:		"channel_ajax.aspx?oper=move&time="+(new Date().getTime()),
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
				JumboTCMS.Message(d.returnval, "1");
				ajaxList(page);
				break;
			}
		}
	});
}
function ConfirmDel(id){
	JumboTCMS.Confirm("确定要删除ID为"+id+"的频道吗?", "ajaxDel("+id+")");
}
function ajaxDel(id){
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"id="+id,
		url:		"channel_ajax.aspx?oper=ajaxDel&time="+(new Date().getTime()),
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
				JumboTCMS.Message(d.returnval, "1");
				ajaxList(page);
				break;
			}
		}
	});
}
function ajaxUpdateUrlRewriter(){
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"",
		url:		"channel_ajax.aspx?oper=ajaxUpdateUrlRewriter&time="+(new Date().getTime()),
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
<div class="topnav">
    <span class="preload1"></span><span class="preload2"></span>
    <ul id="topnavbar">
        <li class="topmenu"><a href="javascript:void(0);" id="operater" class="top_link"><span
            class="down">批量操作</span></a>
            <ul class="sub">
                <li><a href="javascript:void(0);" onclick="operater('pass')" title="启用后的频道才会在前台可用">启用频道</a></li>
                <li><a href="javascript:void(0);" onclick="operater('nopass')" title="禁用后的频道不会在前台可用">禁用频道</a></li>
                <li><a href="javascript:void(0);" onclick="operater('nav')" title="">导航显示</a></li>
                <li><a href="javascript:void(0);" onclick="operater('nonav')" title="">导航隐藏</a></li>
                <li><a href="javascript:void(0);" onclick="operater('refresh')" title="一般在频道升级时需要执行">生成目录</a></li>
            </ul>
        </li>
        <li class="topmenu"><a href="javascript:void(0);" id="channeladd" class="top_link"><span
            class="down">增加频道</span></a>
            <ul class="sub">
                {#foreach $T.table as record}
                <li><a href="javascript:void(0);" onclick="JumboTCMS.Popup.show('channel_edit.aspx?cType={$T.record.type}',-1,-1,true)">
                {$T.record.title}模型</a></li>
                {#/for}
            </ul>
        </li>
        <li class="topmenu"><a href="javascript:void(0);" onclick="ajaxUpdateUrlRewriter();" class="top_link"><span>更新二级域名规则</span></a> </li>
    </ul>
    <script>
	topnavbarStuHover();
    </script>
</div>
</textarea>
<div id="ajaxTopNav"></div>
<textarea id="tplList" style="display:none">
<table class="cooltable">
<thead>
	<tr>
		<th align="center" scope="col" style="width:30px;"><input onclick="checkAllLine()" id="checkedAll" name="checkedAll" type="checkbox" title="全部选择/全部不选" /></th>
		<th scope="col" style="width:40px;">ID</th>
		<th scope="col" style="width:120px;">频道名称</th>
		<th scope="col" style="width:80px;">模型</th>
		<th scope="col" style="width:60px;">栏目深度</th>
		<th scope="col" width="*">频道地址</th>
		<th scope="col" style="width:40px;">排序</th>
		<th scope="col" style="width:35px;">状态</th>
		<th scope="col" style="width:35px;">静态</th>
		<th scope="col" style="width:70px;">操作</th>
	</tr>
</thead>
<tbody>
	{#foreach $T.table as record}
	<tr>
		<td align="center"><input class="checkbox" name="selectID" type="checkbox" value='{$T.record.id}' />
		</td>
		<td align="center">{$T.record.id}</td>
		<td align="center">{$T.record.title}</td>
		<td align="center">
			{$T.record.type}
		</td>
		<td align="center">
			{$T.record.classdepth}
		</td>
		<td align="left">
			{#if $T.record.subdomain == ""}
            <a href='{site.Dir}{$T.record.dir}/' target='_blank'>{site.Dir}{$T.record.dir}/</a>
			{#else}
            <a href='{$T.record.subdomain}/' target='_blank'>{$T.record.subdomain}/</a>
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
		<td align="center">
           		{#if $T.record.ishtml == "1"}
                	<font color='blue'>是</font>
            		{#else}
                	<font color='#cccccc'>否</font>
			{#/if}
		</td>
		<td align="center" class="oper">
           	<a href="javascript:void(0);" onclick="JumboTCMS.Popup.show('channel_edit.aspx?ccid={$T.record.id}',-1,-1,true)">修改</a> <a href="javascript:void(0);" onclick="ConfirmDel({$T.record.id})">删除</a>
		</td>
	</tr>
	{#/for}
</tbody>
</table>
</textarea>
<div id="ajaxList"></div>
</body>
</html>
