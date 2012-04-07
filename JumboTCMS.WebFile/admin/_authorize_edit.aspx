<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="_authorize_edit.aspx.cs" Inherits="JumboTCMS.WebFile.Admin.__authorize__edit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>授权信息编辑</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/jcmsV5.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/jcmsV5.css" />
<link type="text/css" rel="stylesheet" href="css/common.css" />


<script type="text/javascript" src="../_libs/my97datepicker4.6/WdatePicker.js"></script>
<script type="text/javascript">
$(document).ready(function(){
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
});
    </script>
</head>
<body>
<form id="form1" runat="server" onsubmit="return $.formValidator.PageIsValid('1')">
	<table class="formtable">
		<tr>
			<th> 网站域名 </th>
			<td><asp:TextBox ID="txtDomain" runat="server" Width="300px" MaxLength="80" CssClass="ipt"></asp:TextBox>
				<span id="tipDomain" style="width:200px;"> </span></td>
		</tr>
		<tr>
			<th> 首页地址 </th>
			<td><asp:TextBox ID="txtDefaultPage" runat="server" Width="300px" MaxLength="200" CssClass="ipt"></asp:TextBox>
				<span id="tipDefaultPage" style="width:200px;"> </span></td>
		</tr>
		<tr>
			<th> 网站名称 </th>
			<td><asp:TextBox ID="txtWebName" runat="server" Width="300px" MaxLength="30" CssClass="ipt"></asp:TextBox>
				<span id="tipWebName" style="width:200px;"> </span></td>
		</tr>
		<tr>
			<th> 联系邮件 </th>
			<td><asp:TextBox ID="txtLinkEmail" runat="server" Width="300px" MaxLength="80" CssClass="ipt"></asp:TextBox>
				<span id="tipLinkEmail" style="width:200px;"> </span></td>
		</tr>
		<tr>
			<th> 联系手机 </th>
			<td><asp:TextBox ID="txtMobileTel" runat="server" Width="200px" MaxLength="11" CssClass="ipt"></asp:TextBox>
				<span id="tipMobileTel" style="width:200px;"> </span></td>
		</tr>
		<tr>
			<th> 授权类型</th>
			<td>
                <asp:DropDownList ID="ddlAccreditType" runat="server">
						<asp:ListItem Value="1">免费授权</asp:ListItem>
						<asp:ListItem Value="2">标准商业授权</asp:ListItem>
						<asp:ListItem Value="3">企业商业授权</asp:ListItem>
                </asp:DropDownList>
			</td>
		</tr>
		<tr>
			<th> 网站类型</th>
			<td>
                <asp:DropDownList ID="ddlSiteType" runat="server"></asp:DropDownList>
			</td>
		</tr>
		<tr>
			<th> 授权日期 </th>
			<td><asp:TextBox ID="txtAddTime" runat="server" MaxLength="20" CssClass="Wdate ipt"></asp:TextBox>
				<span id="tipAddTime" style="width:200px;"> </span></td>
		</tr>
		<tr>
			<th> 有效日期 </th>
			<td><asp:TextBox ID="txtValidity" runat="server" MaxLength="20" CssClass="Wdate ipt"></asp:TextBox>
				<span id="tipValidity" style="width:200px;"> </span></td>
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
