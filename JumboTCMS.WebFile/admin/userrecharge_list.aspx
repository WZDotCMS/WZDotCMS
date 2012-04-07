<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="userrecharge_list.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._userrecharge_list" %>
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
		url:		"userrecharge_ajax.aspx?oper=ajaxGetList",
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
</script>

</head>
<body>
<textarea id="tplList" style="display:none">
<table class="listtable" cellspacing="0" cellpadding="0" border="0" style="width:100%;border-collapse:collapse;">
	<tr>
		<th scope="col" style="width:40px;">ID</th>
		<th scope="col" style="width:160px;">订单号</th>
		<th scope="col" style="width:120px;">充值金额</th>
		<th scope="col" style="width:120px;">状态</th>
		<th scope="col" style="width:120px;">目标账户</th>
		<th scope="col" width="*">会员名</th>
		<th scope="col" style="width:160px;">订单生成时间</th>
	</tr>
	{#foreach $T.table as record}
	<tr onMouseOver="this.style.backgroundColor='F3F7FF'" onMouseOut="this.style.backgroundColor='FFFFFF'">
		<td align="center">{$T.record.id}</td>
		<td align="center">{$T.record.ordernum}</td>
		<td align="center">{$T.record.money}</td>
		<td align="center">
           		{#if $T.record.state == "0"}<font color="red">未付款</font>{#/if}
           		{#if $T.record.state == "1"}<font color="green">已付款</font>{#/if}
		</td>
		<td align="center">{$T.record.paymentway}</td>
		<td align="center">{$T.record.username}</td>
		<td align="center">{$T.record.ordertime}</td>
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
