<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user_edit.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._user_edit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>编辑用户资料</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/jcmsV5.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/jcmsV5.css" />
<link type="text/css" rel="stylesheet" href="css/common.css" />


<script type="text/javascript">
$(document).ready(function(){
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtPoints").formValidator({tipid:"tipPoints",onshow:"请填写数字",onfocus:"请填写数字"}).RegexValidator({regexp:"^\([0-9]+)$",onerror:"请填写数字"});
	$("#txtIntegral")
	.formValidator({tipid:"tipIntegral",onshow:"请填写数字",onfocus:"请填写数字"})
	.RegexValidator({regexp:"^\([0-9]+)$",onerror:"请填写数字"})
	.CompareValidator({desID:"txtMax",operateor:"<=",onerror:"积分没那么多",datatype:"number"});
});
</script>
</head>
<body>
<form id="form1" runat="server" onsubmit="return $.formValidator.PageIsValid('1')">
	<table class="formtable">
		<tr style="display:none;">
			<th> 用户名 </th>
			<td><asp:TextBox ID="txtUserName" runat="server" Width="120px" MaxLength="20" ReadOnly="True" CssClass="ipt"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<th> 密码初始化 </th>
			<td><asp:TextBox ID="txtUserPass" runat="server" TextMode="Password" Width="120px"
                    MaxLength="20" CssClass="ipt" Text="123456"></asp:TextBox>(留空表示不改)
			</td>
		</tr>
		<tr>
			<th> 充博币 </th>
			<td><asp:TextBox ID="txtPoints" runat="server" Width="50px" MaxLength="8" CssClass="ipt">0</asp:TextBox>
			<span id="tipPoints" style="width: 80px"></span>
			</td>
		</tr>
		<tr>
			<th> 扣积分 </th>
			<td><asp:TextBox ID="txtIntegral" runat="server" Width="50px" MaxLength="8" CssClass="ipt">0</asp:TextBox><span style="display:none;"><asp:TextBox ID="txtMax" runat="server" Width="50px" MaxLength="8" CssClass="ipt">0</asp:TextBox></span>
			<span id="tipIntegral" style="width: 80px"></span>
			</td>
		</tr>
		<tr>
			<th> VIP服务</th>
			<td>
                <asp:DropDownList ID="ddlVIPYears" runat="server">
                <asp:ListItem Value="0">服务年份</asp:ListItem>
						<asp:ListItem Value="1">1年</asp:ListItem>
						<asp:ListItem Value="2">2年</asp:ListItem>
						<asp:ListItem Value="2">3年</asp:ListItem>
                </asp:DropDownList>
                不选年份就不予处理
			</td>
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
