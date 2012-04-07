<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="config.aspx.cs" Inherits="JumboTCMS.WebFile.Extends.QQOnline._config" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>参数设置</title>
<script type="text/javascript" src="../../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../../_data/jcmsV5.js"></script>
<link type="text/css" rel="stylesheet" href="../../_data/jcmsV5.css" />
<link   type="text/css" rel="stylesheet" href="../../admin/css/common.css" />
<link rel="stylesheet" href="../../_libs/jquery.tabs/style.css" type="text/css">
<!--[if lte IE 7]>
<link rel="stylesheet" href="../../_libs/jquery.tabs/style-ie.css" type="text/css">
<![endif]-->
<script type="text/javascript">
$(function() {
	$('#container-1').tabs({ fxFade: true, fxSpeed: 'fast' });
});
</script>

<script type="text/javascript">
$(document).ready(function(){
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtSiteShowX").formValidator({tipid:"tipSiteShowX",onshow:"请填写数字",onfocus:"请填写数字"}).RegexValidator({regexp:"^\([0-9]+)$",onerror:"请填写数字"});
    $("#txtSiteShowY").formValidator({tipid:"tipSiteShowY",onshow:"请填写数字",onfocus:"请填写数字"}).RegexValidator({regexp:"^\([0-9]+)$",onerror:"请填写数字"});

});
</script>
</head>
<body>
<form id="form1" runat="server" onsubmit="return $.formValidator.PageIsValid('1')">
	<br />
    <div id="container-1">
		<ul>
			<li><a href="#fragment-1"><span>参数设置</span></a></li>
		</ul>
		<div id="fragment-1">
			<table class="formtable">
				<tr>
					<th> 显示位置 </th>
					<td><asp:RadioButtonList ID="rblSiteArea" runat="server" RepeatDirection="Horizontal">
						<asp:ListItem Value="0" Selected="True">居左</asp:ListItem>
						<asp:ListItem Value="1">居右</asp:ListItem>
					</asp:RadioButtonList></td>
				</tr>
				<tr>
					<th> 使用皮肤 </th>
					<td><asp:RadioButtonList ID="rblSiteSkin" runat="server" RepeatColumns="3" RepeatDirection="Horizontal">
<asp:ListItem Value="1" Selected="True"><img src='images/qqskin/1/skin.gif' /></asp:ListItem>
<asp:ListItem Value="2"><img src='images/qqskin/2/skin.gif' /></asp:ListItem>
<asp:ListItem Value="3"><img src='images/qqskin/3/skin.gif' /></asp:ListItem>
<asp:ListItem Value="4"><img src='images/qqskin/4/skin.gif' /></asp:ListItem>
<asp:ListItem Value="5"><img src='images/qqskin/5/skin.gif' /></asp:ListItem>
<asp:ListItem Value="6"><img src='images/qqskin/6/skin.gif' /></asp:ListItem>
<asp:ListItem Value="7"><img src='images/qqskin/7/skin.gif' /></asp:ListItem>
<asp:ListItem Value="8"><img src='images/qqskin/8/skin.gif' /></asp:ListItem>
<asp:ListItem Value="9"><img src='images/qqskin/9/skin.gif' /></asp:ListItem>
<asp:ListItem Value="10"><img src='images/qqskin/10/skin.gif' /></asp:ListItem>
<asp:ListItem Value="11"><img src='images/qqskin/11/skin.gif' /></asp:ListItem>
<asp:ListItem Value="12"><img src='images/qqskin/12/skin.gif' /></asp:ListItem>
					</asp:RadioButtonList></td>
				</tr>
				<tr>
					<th> 显示界面X坐标 </th>
					<td><asp:TextBox ID="txtSiteShowX" runat="server" Width="60px" CssClass="ipt"></asp:TextBox>
					<span id="tipSiteShowX" style="width:200px;"> </span></td>
				</tr>
				<tr>
					<th> 显示界面Y坐标 </th>
					<td><asp:TextBox ID="txtSiteShowY" runat="server" Width="60px" CssClass="ipt"></asp:TextBox>
					<span id="tipSiteShowY" style="width:200px;"> </span></td>
				</tr>
			</table>
			<div class="buttonok">
				<asp:Button ID="Button1" runat="server"
                    Text="保存" CssClass="btnsubmit" OnClick="Button1_Click" />
			</div>
		</div>
	</div>
</form>
</body>
</html>
