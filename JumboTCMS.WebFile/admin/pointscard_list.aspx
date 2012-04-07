<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="pointscard_list.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._pointscard_list" %>
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
var pagesize=10;
var page=thispage();
$(document).ready(function(){
	ajaxList(page);
});
function ajaxList(currentpage)
{
	if(currentpage!=null) page=currentpage;
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"page="+currentpage+"&pagesize="+pagesize+"&time="+(new Date().getTime()),
		url:		"pointscard_ajax.aspx?oper=ajaxGetList",
		error:		function(XmlHttpRequest,textStatus, errorThrown) { alert(XmlHttpRequest.responseText);},
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
				$("#ajaxList").setTemplateElement("tplList", null, {filter_data: true});
				$("#ajaxList").processTemplate(d);
				ActiveCoolTable();
				$("#ajaxPageBar").html(d.pagerbar);
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
		url:		"pointscard_ajax.aspx?oper=ajaxBatchOper&act="+act+"&time="+(new Date().getTime()),
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
function ConfirmDel(id){
	top.JumboTCMS.Confirm("确定要删除吗?", "IframeOper.ajaxDel("+id+")");
}
function ajaxDel(id){
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"id="+id,
		url:		"pointscard_ajax.aspx?oper=ajaxDel&time="+(new Date().getTime()),
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
function ConfirmOffer(id){
	top.JumboTCMS.Confirm("确定要标识此充值卡并让它生效吗?", "IframeOper.ajaxOffer("+id+")");
}
function ConfirmBindUser(id){
	top.JumboTCMS.Confirm("确定要创建一个新用户并捆绑充值卡吗?", "IframeOper.ajaxBindUser("+id+")");
}
function ajaxOffer(id){
	top.JumboTCMS.Loading.show("正在处理...");
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"id="+id,
		url:		"pointscard_ajax.aspx?oper=ajaxOffer&time="+(new Date().getTime()),
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
function ajaxBindUser(id){
	top.JumboTCMS.Loading.show("正在处理...");
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"id="+id,
		url:		"pointscard_ajax.aspx?oper=ajaxBindUser&time="+(new Date().getTime()),
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
    </script>
</head>
<body>
<div id="ajaxTopNav">
	<div class="topnav"> <span class="preload1"></span><span class="preload2"></span>
		<ul id="topnavbar">
			<li class="topmenu"><a href="javascript:void(0);" class="top_link"><span
            class="down">批量操作</span></a>
				<ul class="sub">
					<li><a href="javascript:void(0);" onclick="operater('pass')" title="只有状态为锁定中的才能激活">激活</a></li>
					<li><a href="javascript:void(0);" onclick="operater('nopass')" title="只有状态为库存的才能锁定">锁定</a></li>
					<li><a href="javascript:void(0);" onclick="operater('del')">删除</a></li>
				</ul>
			</li>
		<li class="topmenu"><a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('pointscard_add.aspx',500,350,true)" id="operater2" class="top_link"><span>批量添加充值卡</span></a> </li>

		</ul>
		<script>
	topnavbarStuHover();
        </script>
	</div>
</div>
<table class="helptable">
	<tr>
		<td>
			<ul>
				<li>只有标识为“已售”的充值卡才能使用</li>
				<li>注册会员可以将充值卡中的博币冲入自己账户</li>
			</ul>
		</td>
	</tr>
</table>
<textarea id="tplList" style="display:none">
<table class="cooltable">
<thead>
	<tr>
		<th align="center" scope="col" style="width:30px;"><input onclick="checkAllLine()" id="checkedAll" name="checkedAll" type="checkbox" title="全部选择/全部不选" /></th>
		<th scope="col" style="width:60px;">ID</th>
		<th width="*">卡号</th>
		<th scope="col" style="width:120px;">密码</th>
		<th scope="col" style="width:60px;">博币</th>
		<th scope="col" style="width:60px;">状态</th>
		<th scope="col" style="width:80px;">使用人</th>
		<th scope="col" style="width:150px;">操作</th>
	</tr>
</thead>
<tbody>
	{#foreach $T.table as record}
	<tr>
		<td align="center">
			<input class="checkbox" name="selectID" type="checkbox" value='{$T.record.id}' />
		</td>
		<td align="center">{$T.record.id}</td>
		<td align="left">
			{$T.record.cardnumber}
		</td>
		<td align="left">
			{$T.record.cardpassword}
		</td>
		<td align="center">{$T.record.points}</td>
		<td align="center">
			{#if $T.record.state == "3"}
			<font color='black'>已用</font>
			{#elseif $T.record.state == "2"}
			<font color='blue'>已售</font>
			{#elseif $T.record.state == "1"}
			<font color='green'>库存</font>
			{#else}
			<font color='red'>锁定</font>
			{#/if}
		</td>
		<td align="center">{$T.record.username}</td>
		<td align="center" class="oper">
			{#if $T.record.state != "2"}
			<a href="javascript:void(0);" onclick="ConfirmDel({$T.record.id})">删除</a>
			{#else}
			<font color='#cccccc'>删除</font>
			{#/if}
			{#if $T.record.state == "1"}
			<a title="标识已售后充值卡可以由用户自由冲入自己的帐号(适用多帐号用户)" href='javascript:void(0);' onclick="ConfirmOffer({$T.record.id})">标识已售</a>
			<a title="捆绑会员是指创建一个与充值卡号、密码相同的前台用户(适用未有帐号的用户)" href='javascript:void(0);' onclick="ConfirmBindUser({$T.record.id})">捆绑会员</a>
			{#else}
			<font color='#cccccc'>标识已售</font>
			<font color='#cccccc'>捆绑会员</font>
			{#/if}
		</td>
	</tr>
	{#/for}
</tbody>
</table>
</textarea>
<div id="ajaxList"> </div>
<div id="ajaxPageBar" class="pages"> </div>
</body>
</html>
