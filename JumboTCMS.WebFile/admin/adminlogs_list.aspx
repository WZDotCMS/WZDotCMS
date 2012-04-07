<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="adminlogs_list.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._adminlogs_list" %>
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
		url:		"adminlogs_ajax.aspx?oper=ajaxGetList",
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
function Confirmclear(){
	top.JumboTCMS.Confirm("确定要清空吗?", "IframeOper.ajaxClear()");
}
function ajaxClear(){
	top.JumboTCMS.Loading.show("正在清空...");
	$.ajax({
		type:		"get",
		dataType:	"json",
		url:		"adminlogs_ajax.aspx?oper=clear&time="+(new Date().getTime()),
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
    <div class="topnav">
        <span class="preload1"></span><span class="preload2"></span>
        <ul id="topnavbar">
            <li class="topmenu"><a href="javascript:void(0);" onclick="Confirmclear();" class="top_link"><span>清空日志</span></a>
            </li>
        </ul>

        <script>
	topnavbarStuHover();
        </script>

    </div>
<textarea id="tplList" style="display:none">
<table class="listtable" cellspacing="0" cellpadding="0" border="0" style="width:100%;border-collapse:collapse;">
	<tr>
		<th scope="col" style="width:40px;">ID</th>
		<th scope="col" width="80">操作员</th>
		<th scope="col" width="*">操作行为</th>
		<th scope="col" style="width:140px;">操作IP</th>
		<th scope="col" style="width:140px;">操作时间</th>
	</tr>
	{#foreach $T.table as record}
	<tr onMouseOver="this.style.backgroundColor='F3F7FF'" onMouseOut="this.style.backgroundColor='FFFFFF'">
		<td align="center">{$T.record.id}</td>
		<td align="center">{$T.record.adminname}</td>
		<td align="left">{$T.record.operinfo}</td>
		<td align="left">{$T.record.operip}</td>
		<td align="left">{$T.record.opertime}</td>
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
