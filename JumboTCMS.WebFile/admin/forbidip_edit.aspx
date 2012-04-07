<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="forbidip_edit.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._forbidip_edit" %>
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


<script type="text/javascript" src="../_libs/my97datepicker4.6/WdatePicker.js"></script>
<script type="text/javascript">
$(document).ready(function(){
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtStartIP").formValidator({tipid:"tipStartIP",onshow:"输入标准IP格式",onfocus:"例如：96.48.1.1"}).RegexValidator({regexp:"^(((2[0-4]{1}[0-9]{1})|(25[0-5]{1}))|(1[0-9]{2})|([1-9]{1}[0-9]{1})|([0-9]{1})).(((2[0-4]{1}[0-9]{1})|(25[0-5]{1}))|(1[0-9]{2})|([1-9]{1}[0-9]{1})|([0-9]{1})).(((2[0-4]{1}[0-9]{1})|(25[0-5]{1}))|(1[0-9]{2})|([1-9]{1}[0-9]{1})|([0-9]{1})).(((2[0-4]{1}[0-9]{1})|(25[0-5]{1}))|(1[0-9]{2})|([1-9]{1}[0-9]{1})|([0-9]{1}))$",onerror:"IP输入有误"});
	$("#txtEndIP").formValidator({tipid:"tipEndIP",onshow:"输入标准IP格式",onfocus:"例如：96.48.1.255"}).RegexValidator({regexp:"^(((2[0-4]{1}[0-9]{1})|(25[0-5]{1}))|(1[0-9]{2})|([1-9]{1}[0-9]{1})|([0-9]{1})).(((2[0-4]{1}[0-9]{1})|(25[0-5]{1}))|(1[0-9]{2})|([1-9]{1}[0-9]{1})|([0-9]{1})).(((2[0-4]{1}[0-9]{1})|(25[0-5]{1}))|(1[0-9]{2})|([1-9]{1}[0-9]{1})|([0-9]{1})).(((2[0-4]{1}[0-9]{1})|(25[0-5]{1}))|(1[0-9]{2})|([1-9]{1}[0-9]{1})|([0-9]{1}))$",onerror:"IP输入有误"});
});
    </script>
</head>
<body>
<form id="form1" runat="server" onsubmit="return $.formValidator.PageIsValid('1')">
	<table class="formtable">
		<tr>
			<th> 截至日期 </th>
			<td><asp:TextBox ID="txtExpireDate" runat="server" MaxLength="20" CssClass="Wdate ipt"></asp:TextBox>
				<span id="tipExpireDate" style="width:200px;"> </span></td>
		</tr>
		<tr>
			<th> 开始IP </th>
			<td><asp:TextBox ID="txtStartIP" runat="server" Width="120px" MaxLength="20" CssClass="ipt"></asp:TextBox>
				<span id="tipStartIP" style="width:200px;"> </span></td>
		</tr>
		<tr>
			<th> 结束IP </th>
			<td><asp:TextBox ID="txtEndIP" runat="server" Width="120px" MaxLength="20" CssClass="ipt"></asp:TextBox>
				<span id="tipEndIP" style="width:200px;"> </span></td>
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
