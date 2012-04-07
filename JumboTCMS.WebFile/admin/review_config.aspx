<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="review_config.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._review_config" %>
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
	$("#txtPostTimer").formValidator({tipid:"tipPostTimer",onshow:"请填写数字",onfocus:"请填写数字"}).RegexValidator({regexp:"^\([0-9]+)$",onerror:"请填写数字"});
    $("#txtPageSize").formValidator({tipid:"tipPageSize",onshow:"请填写数字",onfocus:"请填写数字"}).RegexValidator({regexp:"^\([0-9]+)$",onerror:"请填写数字"});

});
</script>
</head>
<body>
<form id="form1" runat="server" onsubmit="return $.formValidator.PageIsValid('1')">
			<table class="formtable">
				<tr>
					<th> 游客可以评论 </th>
					<td><asp:RadioButtonList ID="rblGuestPost" runat="server" RepeatDirection="Horizontal">
						<asp:ListItem Value="0" Selected="True">否</asp:ListItem>
						<asp:ListItem Value="1">是</asp:ListItem>
					</asp:RadioButtonList></td>
				</tr>
				<tr>
					<th> 需要管理员审核 </th>
					<td><asp:RadioButtonList ID="rblNeedCheck" runat="server" RepeatDirection="Horizontal">
					    <asp:ListItem Value="0">否</asp:ListItem>
					    <asp:ListItem Value="1" Selected="True">是</asp:ListItem>
					</asp:RadioButtonList></td>
				</tr>
				<tr>
					<th> 留言间隔周期 </th>
					<td><asp:TextBox ID="txtPostTimer" runat="server" Width="60px" CssClass="ipt"></asp:TextBox>秒
					<span id="tipPostTimer" style="width:200px;"> </span></td>
				</tr>
				    <tr>
      <th> 前台每页评论数 </th>
      <td><asp:TextBox ID="txtPageSize" runat="server" Width="60px" CssClass="ipt"></asp:TextBox>
        条 <span id="tipPageSize" style="width:200px;"> </span></td>
    </tr>
			</table>
			<div class="buttonok">
				<asp:Button ID="Button1" runat="server"
                    Text="保存" CssClass="btnsubmit" OnClick="Button1_Click" />
			</div>
</form>
</body>
</html>
