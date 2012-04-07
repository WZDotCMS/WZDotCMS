﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="special_edittemplate.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._special_edittemplate" %>
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
	$("#txtTemplateContent").formValidator({tipid:"tipTemplateContent",onshow:"请输入内容",onfocus:"请输入内容"}).InputValidator({min:5,onerror:"模板内容太少了吧"});
});
    </script>
</head>
<body>
<form id="form1" runat="server" onsubmit="return $.formValidator.PageIsValid('1')">
	<table class="formtable">
		<tr>
			<td>支持标签：
				[<a onclick="InsertUnit('{$SpecialId}', 'txtTemplateContent')" href="javascript:void(0)" title="专题编号">{$SpecialId}</a>] 
				[<a onclick="InsertUnit('{$SpecialName}', 'txtTemplateContent')" href="javascript:void(0)" title="专题名称">{$SpecialName}</a>] 
				[<a onclick="InsertUnit('{$SpecialInfo}', 'txtTemplateContent')" href="javascript:void(0)" title="专题简介">{$SpecialInfo}</a>] </td>
		</tr>
		<tr>
			<td><asp:TextBox ID="txtTemplateContent" runat="server" Width="100%" TextMode="MultiLine"
                    Height="230px"></asp:TextBox>
				<span id="tipTemplateContent" style="width:200px;"> </span></td>
		</tr>
		<tr>
			<td><asp:CheckBox ID="chkSavaDefault" runat="server" Text="存为默认模板" Enabled="false" /></td>
		</tr>
	</table>
	<div class="buttonok">
		<asp:Button ID="btnSave" runat="server" Text="确定" CssClass="btnsubmit" OnClick="btnSave_Click" />
		<input id="btnReset" type="button" value="取消" class="btncancel" onclick="parent.JumboTCMS.Popup.hide();" />
	</div>
</form>
<script type="text/javascript">_jcms_SetDialogTitle();</script>
</body>
</html>
