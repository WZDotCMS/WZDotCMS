<%@ Page Language="C#" AutoEventWireup="true" Codebehind="database1.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._database1" %>
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
$(function() {
	$('#container-1').tabs({ fxFade: true, fxSpeed: 'fast' });
});
</script>
<link rel="stylesheet" href="../_libs/jquery.tabs/style.css" type="text/css" />
<!--[if lte IE 7]>
<link rel="stylesheet" href="../_libs/jquery.tabs/style-ie.css" type="text/css" />
<![endif]-->
<script type="text/javascript">
function ajaxBackupMssql(){
	if($("#txtSavePath").val()=="")
	{
		alert("请填写目标文件名");
		return;
	}
	top.JumboTCMS.Loading.show("正在备份...");
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"dbname="+$("#txtSavePath").val(),
		url:		"database_ajax.aspx?oper=ajaxBackupMssql&time="+(new Date().getTime()),
		error:		function(XmlHttpRequest,textStatus, errorThrown){top.JumboTCMS.Loading.hide();alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
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
function ajaxRestoreMssql(){
	if($("#txtFromPath").val()=="")
	{
		alert("请填写源文件名");
		return;
	}
	top.JumboTCMS.Loading.show("正在恢复...");
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"dbname="+$("#txtFromPath").val(),
		url:		"database_ajax.aspx?oper=ajaxRestoreMssql&time="+(new Date().getTime()),
		error:		function(XmlHttpRequest,textStatus, errorThrown){top.JumboTCMS.Loading.hide();alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
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
<br />
<div id="container-1">
	<ul>
		<li><a href="#fragment-1"><span>备份数据库</span></a></li>
		<!--<li><a href="#fragment-2"><span>恢复数据库</span></a></li>-->
	</ul>
	<div id="fragment-1">
		<table class="formtable">
			<tr>
				<th> 目标文件名</th>
				<td><input name="txtSavePath" type="text" value="<%=_bakname %>" id="txtSavePath" class="ipt" style="width:220px;" />
				</td>
			</tr>
		</table>
		<div class="buttonok">
			<input type="button" name="btnBackup" value="执行" id="btnBackup" class="btnsubmit" onclick="ajaxBackupMssql();" />
		</div>
	</div>
	<div id="fragment-2">
		<table class="formtable">
			<tr>
				<th> 源文件名 </th>
				<td><input name="txtFromPath" type="text" value="<%=_bakname %>" id="txtFromPath" class="ipt" style="width:220px;" />
				</td>
			</tr>
		</table>
				<div class="buttonok">
			<input type="button" name="btnRestore" value="执行" id="btnRestore" class="btnsubmit" onclick="ajaxRestoreMssql();" />
		</div>
	</div>
</div>
</body>
</html>
