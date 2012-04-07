<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="template_edit.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._template_edit" %>
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


<script type="text/javascript">
$(document).ready(function(){
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtTitle").formValidator({tipid:"tipTitle",onshow:"请输入模板名称",onfocus:"请输入4-30个字符(2-15个汉字)"}).InputValidator({min:4,max:30,onerror:"请输入4-30个字符(2-15个汉字)"})
		.AjaxValidator({
		type : "get",
		data:		"id=<%=id%>",
		url:		"template_ajax.aspx?oper=checkname&time="+(new Date().getTime()),
		datatype : "json",
		success : function(d){	
			if(d.result == "1")
				return true;
			else
				return false;
		},
		buttons: $("#btnSave"),
		error: function(){alert("服务器繁忙，请稍后再试");},
		onerror : "该模板名称已经存在",
		onwait : "正在校验模板名称的合法性，请稍候..."
	})<%if (id!="0") {%>.DefaultPassed()<%} %>;
	$("#txtSource").formValidator({tipid:"tipSource",onshow:"文件名可包含字母、数字和下划线",onfocus:"请把文件放相应的方案目录下"}).RegexValidator({regexp:"^[_a-zA-Z0-9]+\.htm$",onerror:"后缀为.htm"});
});
    </script>
</head>
<body>
<form id="form1" runat="server" onsubmit="return $.formValidator.PageIsValid('1')">
	<table class="formtable">
		<tr>
			<th> 模板名称 </th>
			<td><asp:TextBox ID="txtTitle" runat="server" MaxLength="20" Width="225px" CssClass="ipt"></asp:TextBox>
				<span id="tipTitle" style="width:200px;"> </span></td>
		</tr>
		<tr>
			<th> 模板文件 </th>
			<td> <% =site.Dir%>templates/<% = tpPath%>/
				<asp:TextBox ID="txtSource" runat="server" Width="120px" CssClass="ipt"></asp:TextBox>
				<span id="tipSource" style="width:200px;"> </span></td>
		</tr>
	</table>
	<div class="buttonok">
		<asp:Button ID="btnSave" runat="server" Text="确定" CssClass="btnsubmit" />
		<input id="btnReset" type="button" value="取消" class="btncancel" onclick="parent.JumboTCMS.Popup.hide();" />
	</div>
</form>
<script type="text/javascript">_jcms_SetDialogTitle();</script>
</body>
</html>
