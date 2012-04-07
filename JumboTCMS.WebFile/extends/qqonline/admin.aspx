<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>QQ在线客服</title>
<script type="text/javascript" src="../../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../../_data/jcmsV5.js"></script>
<link   type="text/css" rel="stylesheet" href="../../_data/jcmsV5.css" />
<script type="text/javascript" src="../../admin/scripts/common.js"></script>
<link   type="text/css" rel="stylesheet" href="../../admin/css/common.css" />

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
		url:		"ajax.aspx?oper=ajaxGetList",
		error:		function(XmlHttpRequest,textStatus, errorThrown) { alert(XmlHttpRequest.responseText);},
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				JumboTCMS.Alert(d.returnval, "0", "top.window.location='" + site.Dir + "admin/login.aspx';");
				break;
			case '0':
				JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				$("#ajaxList").setTemplateElement("tplList", null, {filter_data: false});
				$("#ajaxList").processTemplate(d);
				$("#ajaxPageBar").html(d.pagerbar);
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
		url:		"ajax.aspx?oper=ajaxBatchOper&act="+act+"&time="+(new Date().getTime()),
		error:		function(XmlHttpRequest,textStatus, errorThrown){JumboTCMS.Loading.hide();alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				JumboTCMS.Alert(d.returnval, "0", "top.window.location='" + site.Dir + "admin/login.aspx';");
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
function ajaxUpdateFore()
{
	JumboTCMS.Loading.show("正在更新，请等待...");
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"time="+(new Date().getTime()),
		url:		"ajax.aspx?oper=updatefore",
		error:		function(XmlHttpRequest,textStatus, errorThrown){JumboTCMS.Loading.hide();alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				JumboTCMS.Alert(d.returnval, "0", "top.window.location='" + site.Dir + "admin/login.aspx';");
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
<div id="temporarydiv" style="display:none;"></div>
<div class="topnav">
	<span class="preload1"></span><span class="preload2"></span>
	<ul id="topnavbar">
		<li class="topmenu"><a href="javascript:void(0);" class="top_link"><span
            class="down">批量操作</span></a>
			<ul class="sub">
				<li><a href="javascript:void(0);" onclick="operater('pass')">审核链接</a></li>
				<li><a href="javascript:void(0);" onclick="operater('nopass')">取消审核</a></li>
				<li><a href="javascript:void(0);" onclick="operater('del')">直接删除</a></li>
			</ul>
		</li>
		<li class="topmenu"><a href="javascript:void(0);" onclick="JumboTCMS.Popup.show('edit.aspx?id=0',620,-1,true)" class="top_link"><span>添加新的QQ号</span></a></li>
		<li class="topmenu"><a href="javascript:void(0);" onclick="JumboTCMS.Popup.show('config.aspx',620,-40,true)" class="top_link"><span>参数设置</span></a></li>
	</ul>
	<script>
	topnavbarStuHover();
    </script>
</div>
<table class="formtable">
	<tr>
		<th>更新前台<input type="button" class="tip-r" tip="当QQ数据与配置有更新时，需要点击此按钮" /></th>
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
		<th scope="col" style="width:60px;">ID</th>
		<th scope="col" width="*">QQ号码</th>
		<th scope="col" style="width:150px;">显示昵称</th>
		<th scope="col" style="width:70px;">显示头像</th>
		<th scope="col" style="width:50px;">状态</th>
		<th scope="col" style="width:50px;">操作</th>
	</tr>
	</thead>
<tbody>
	{#foreach $T.table as record}
	<tr>
		<td align="center"><input class="checkbox" name="selectID" type="checkbox" value='{$T.record.id}' />
		</td>
		<td align="center">{$T.record.id}</td>
		<td align="center">{$T.record.qqid}</td>
		<td align="center">{$T.record.title}</td>
		<td align="center"><img src='images/qqface/{$T.record.face}_m.gif'></td>
		<td align="center">
			{#if $T.record.state == "1"}
			显示
			{#/if}
			{#if $T.record.state == "0"}
			<font color='blue'>不显示</font>
			{#/if}
		</td>
		<td align="center" class="oper">
			<a href="javascript:void(0);" onclick="JumboTCMS.Popup.show('edit.aspx?id={$T.record.id}',620,-1,true)">修改</a>
		</td>
	</tr>
	{#/for}
</tbody>
</table></textarea>
<div id="ajaxList"> </div>
<div id="ajaxPageBar" class="pages"> </div>
</body>
</html>
