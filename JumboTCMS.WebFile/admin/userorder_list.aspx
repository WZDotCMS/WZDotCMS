<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="userorder_list.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._userorder_list" %>
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
		url:		"userorder_ajax.aspx?oper=ajaxGetList",
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
				$("#ajaxPageBar").html(d.pagerbar);
				break;
			}
		}
	});
}
function ajaxCheck(ordernum){
	top.JumboTCMS.Loading.show("正在处理...");
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"ordernum="+ordernum,
		url:		"userorder_ajax.aspx?oper=ajaxCheck&time="+(new Date().getTime()),
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
function ajaxDel(ordernum){
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"ordernum="+ordernum,
		url:		"userorder_ajax.aspx?oper=ajaxDel&time="+(new Date().getTime()),
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
</script>

</head>
<body>
<textarea id="tplList" style="display:none">
<table class="listtable" cellspacing="0" cellpadding="0" border="0" style="width:100%;border-collapse:collapse;">
	<tr>
		<th scope="col" style="width:40px;">ID</th>
		<th scope="col" style="width:160px;">订单号</th>
		<th scope="col" style="width:100px;">订单总金额</th>
		<th scope="col" style="width:70px;">状态</th>
		<th scope="col" width="*">会员名</th>
		<th scope="col" style="width:160px;">订单生成时间</th>
		<th scope="col" style="width:180px;">操作</th>
	</tr>
	{#foreach $T.table as record}
	<tr onMouseOver="this.style.backgroundColor='F3F7FF'" onMouseOut="this.style.backgroundColor='FFFFFF'">
		<td align="center">{$T.record.id}</td>
		<td align="center">{$T.record.ordernum}</td>
		<td align="center">{formatCurrency($T.record.money)}</td>
		<td align="center">
           		{#if $T.record.state == "0"}<font color="red">未付款</font>{#/if}
           		{#if $T.record.state == "1"}<font color="green">已付款</font>{#/if}
           		{#if $T.record.state == "2"}<font color="blue">已发货</font>{#/if}
           		{#if $T.record.state == "3"}<font color="black">已成交</font>{#/if}
		</td>
		<td align="center">{$T.record.username}</td>
		<td align="center">{$T.record.ordertime}</td>
		<td align="center">
			<a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('userorder_view.aspx?ordernum={$T.record.ordernum}',800,500,true)">订单详情</a>
			{#if $T.record.state == "0"}
			<a href="javascript:void(0);" onclick="top.JumboTCMS.Confirm('确定要作废该订单?', 'IframeOper.ajaxDel(\'{$T.record.ordernum}\')');">作废订单</a>
			{#else}
			<font color='#cccccc'>作废订单</font>
			{#/if}
			{#if $T.record.state == "0" || $T.record.state == "1"}
           		<a href="javascript:void(0);" onclick="top.JumboTCMS.Confirm('你确定已收到对方汇款/转账，并准备发货吗?', 'IframeOper.ajaxCheck(\'{$T.record.ordernum}\')');">准备发货</a>
			{#else}
			<font color='#cccccc'>准备发货</font>
			{#/if}
		</td>
	</tr>
	{#/for}
</table>
</textarea>
    <div id="ajaxList">
    </div>
    <div id="ajaxPageBar" class="pages">
    </div>
</body>
</html>
