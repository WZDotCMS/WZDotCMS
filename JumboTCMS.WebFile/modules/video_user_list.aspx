<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="video_user_list.aspx.cs" Inherits="JumboTCMS.WebFile.Modules._video_user_list" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title></title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/jcmsV5.js"></script>
<link   type="text/css" rel="stylesheet" href="../_data/jcmsV5.css" />
<script type="text/javascript" src="../style/user/common.js"></script>
<link   type="text/css" rel="stylesheet" href="../style/user/common.css" />
<script type="text/javascript">
var mtype = q('ctype');//模型
var ctype = joinValue('ctype');//频道类型
var ccid = joinValue('ccid');//频道ID
var cid = joinValue('cid');//栏目ID
var k=joinValue('k');//关键字
var f=joinValue('f');//检索字段
var s=joinValue('s');//检索状态
var d=joinValue('d');//检索时间
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
		url:		mtype + "_user_ajax.aspx?oper=ajaxGetList"+ccid+cid+k+f+s+d,
        error:		function(XmlHttpRequest,textStatus, errorThrown){if(XmlHttpRequest.responseText!=""){alert(XmlHttpRequest.responseText);}},
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				JumboTCMS.Alert(d.returnval, "0", "top.window.location='../passport/login.aspx';");
				break;
			case '0':
				JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				$("#ajaxList").setTemplateElement("tplList", null, {filter_data: true});
				$("#ajaxList").processTemplate(d);
				$("#ajaxPageBar").html(d.pagerbar);
				break;
			}
		}
	});
}
function operater(act,classid){
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
		data:		"ids="+ids+"&tocid="+classid,
		url:		mtype + "_user_ajax.aspx?oper=ajaxBatchOper&act="+act+"&time="+(new Date().getTime()) + ccid,
		error:		function(XmlHttpRequest,textStatus, errorThrown){JumboTCMS.Loading.hide();alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				JumboTCMS.Loading.hide();
				JumboTCMS.Alert(d.returnval, "0", "top.window.location='../passport/login.aspx';");
				break;
			case '0':
				JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				JumboTCMS.Loading.hide();
				ajaxList(page);
				break;
			}
		}
	});
}
function ConfirmDel(id){
	JumboTCMS.Confirm("确定要删除吗？", "ajaxDel("+id+")");
}
function ajaxDel(id){
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"id="+id,
		url:		mtype + "_user_ajax.aspx?oper=ajaxDel&time="+(new Date().getTime()) + ccid,
		error:		function(XmlHttpRequest,textStatus, errorThrown){JumboTCMS.Loading.hide();alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				JumboTCMS.Alert(d.returnval, "0", "top.window.location='../passport/login.aspx';");
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
function ajaxSearch(){
	JumboTCMS.Popup.show("../user/content_searchform.aspx?ctype="+mtype+ccid+cid+k+f+s+d,500,280,false);
}
    </script>
</head>
<body>
<div class="topnav"> <span class="preload1"></span><span class="preload2"></span>
	<ul id="topnavbar">
		<li class="topmenu"><a href="javascript:void(0);" onclick="JumboTCMS.Popup.show(mtype + '_user_edit.aspx?id=0'+ccid,-1,-1,true)" id="operadd" class="top_link"><span>添加<%=ChannelItemName%></span></a></li>
		<li class="topmenu"><a href="javascript:void(0);" onclick="ajaxSearch();" id="opersearch" class="top_link"><span>过滤检索</span></a></li>
	</ul>
	<script>
	topnavbarStuHover();
    </script>
</div>
<textarea id="tplList" style="display:none"><table class="listtable" cellspacing="0" cellpadding="0" border="0" style="width:100%;border-collapse:collapse;">
	<tr>
		<th align="center" scope="col" style="width:30px;"><input onclick="checkAll()" id="checkedAll" name="checkedAll" type="checkbox" /></th>
		<th scope="col" style="width:60px;">ID</th>
		<th scope="col" width="*">标题</th>
		<th scope="col" style="width:120px;">所属栏目</th>
		<th scope="col" style="width:100px;">状态</th>
		<th scope="col" style="width:70px;">操作</th>
	</tr>
	{#foreach $T.table as record}
	<tr onMouseOver="this.style.backgroundColor='F3F7FF'" onMouseOut="this.style.backgroundColor='FFFFFF'">
		<td align="center"><input class="checkbox" name="selectID" type="checkbox" value='{$T.record.id}' />
		</td>
		<td align="center">{$T.record.id}</td>
		<td align="left">&nbsp;&nbsp;&nbsp;<a>{$T.record.title}</a></td>
		<td align="center">{$T.record.classname}</td>
		<td align="center"><img title="{formatIsPass($T.record.ispass)}" src="../admin/images/ico_ispass{$T.record.ispass}.gif" border="0" /><img title="{formatIsImg($T.record.isimg)}" src="../admin/images/ico_isimg{$T.record.isimg}.gif" border="0" /><img title="{formatIsTop($T.record.istop)}" src="../admin/images/ico_istop{$T.record.istop}.gif" border="0" /><img title="{formatIsFocus($T.record.isfocus)}" src="../admin/images/ico_isfocus{$T.record.isfocus}.gif" border="0" /></td>
		<td align="center">
			<a href="javascript:void(0);" onclick="JumboTCMS.Popup.show(mtype + '_user_edit.aspx?id={$T.record.id}'+ ccid,-1,-1,true)">修改</a>
			<a href="javascript:void(0);" onclick="ConfirmDel({$T.record.id})">删除</a>
		</td>
	</tr>
	{#/for}
</table></textarea>
<div id="ajaxList"> </div>
<div id="ajaxPageBar" class="pages"> </div>
</body>
</html>
