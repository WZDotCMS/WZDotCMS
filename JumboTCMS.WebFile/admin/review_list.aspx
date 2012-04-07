<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="review_list.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._review_list" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>评论列表</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/jcmsV5.js"></script>
<link   type="text/css" rel="stylesheet" href="../_data/jcmsV5.css" />
<script type="text/javascript" src="scripts/common.js"></script>
<link   type="text/css" rel="stylesheet" href="css/common.css" />
<script type="text/javascript">
var pagesize=15;
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
		url:		"review_ajax.aspx?oper=ajaxGetList",
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
		url:		"review_ajax.aspx?oper=ajaxBatchOper&act="+act+"&time="+(new Date().getTime()),
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
</script>
</head>
<body>
<div class="topnav"> <span class="preload1"></span><span class="preload2"></span>
  <ul id="topnavbar">
    <li class="topmenu"><a href="javascript:void(0);" class="top_link"><span
            class="down">批量操作</span></a>
      <ul class="sub">
        <li><a href="javascript:void(0);" onclick="operater('pass')">审核评论</a></li>
        <li><a href="javascript:void(0);" onclick="operater('nopass')">取消审核</a></li>
        <li><a href="javascript:void(0);" onclick="operater('del')">直接删除</a></li>
      </ul>
    </li>
    <li class="topmenu"><a href="javascript:void(0);" onclick="JumboTCMS.Popup.show('review_config.aspx',620,-40,true)" class="top_link"><span>参数设置</span></a></li>
  </ul>
  <script>
	topnavbarStuHover();
    </script>
</div>
<textarea id="tplList" style="display:none">
<table class="cooltable">
<thead>
	<tr>
		<th align="center" scope="col" style="width:30px;"><input onclick="checkAllLine()" id="checkedAll" name="checkedAll" type="checkbox" title="全部选择/全部不选" /></th>
		<th scope="col" style="width:60px;">ID</th>
		<th scope="col" width="*">内容</th>
		<th scope="col" style="width:200px;">发表时间</th>
		<th scope="col" style="width:50px;">状态</th>
	</tr>
	</thead>
<tbody>
	{#foreach $T.table as record}
	<tr>
		<td align="center"><input class="checkbox" name="selectID" type="checkbox" value='{$T.record.id}' />
		</td>
		<td align="center">{$T.record.id}</td>
		<td align="left">{$T.record.content}</td>
		<td align="center">{$T.record.adddate}</td>
		<td align="center">
			{#if $T.record.ispass == "1"}
			已审
			{#/if}
			{#if $T.record.ispass == "0"}
			<font color='blue'>未审</font>
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
