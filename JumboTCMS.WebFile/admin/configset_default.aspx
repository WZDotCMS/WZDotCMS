<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="configset_default.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._config_index" %>
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


<link rel="stylesheet" href="../_libs/jquery.tabs/style.css" type="text/css">
<!--[if lte IE 7]>
<link rel="stylesheet" href="../_libs/jquery.tabs/style-ie.css" type="text/css">
<![endif]-->



<script type="text/javascript">
$(document).ready(function(){
	$('.tip-r').jtip({gravity: 'r',fade: false});
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtName").formValidator({tipid:"tipName",onshow:"请输入网站全称",onfocus:"建议输入4-6个汉字"}).InputValidator({min:4,max:30,onerror:"请输入4-30个字符或2-15个汉字,请确认"});
	$("#txtName2").formValidator({tipid:"tipName2",onshow:"请输入网站简称",onfocus:"2-6个字符"}).InputValidator({min:2,max:6,onerror:"请输入2-6个字符或1-3个汉字,请确认"});
<%if (base.Edition == "All")
    { %>
	$("#txtUrl").formValidator({empty:true,tipid:"tipUrl",onshow:"如果不用多个域名请留空！",onfocus:"以http://开头，结尾不要加/。如:http://www.jumbotcms.net",onempty:"如果不用多个域名请留空！"}).RegexValidator({regexp:"domain",datatype:"enum",onerror:"以http://开头，结尾不要加/"});
<%} %>
});
</script>
</head>
<body>
<form id="form1" runat="server" onsubmit="return $.formValidator.PageIsValid('1')">
	<br />
	<div>
		<div>
			<ul class="tabs-nav">
				<li class="tabs-selected"><a href="configset_default.aspx"><span>基本参数</span></a></li>
			</ul>
		</div>
		<div class="tabs-container">
			<table class="formtable">
				<tr>
					<th> 网站全称 </th>
					<td><asp:TextBox ID="txtName" runat="server" MaxLength="50" Width="300px" CssClass="ipt"></asp:TextBox>
						<span id="tipName" style="width:200px;"> </span></td>
				</tr>
				<tr>
					<th> 网站简称 </th>
					<td><asp:TextBox ID="txtName2" runat="server" MaxLength="4" Width="80px" CssClass="ipt"></asp:TextBox>
						<span id="tipName2" style="width:200px;"> </span></td>
				</tr>
				<tr style="display:<%=(base.Edition == "All")?"":"none"%>">
					<th> 网站主域名 </th>
					<td><asp:TextBox ID="txtUrl" runat="server" MaxLength="60" Width="300px" CssClass="ipt"></asp:TextBox>
						<span id="tipUrl" style="width:200px;"> </span></td>
				</tr>
				<tr>
					<th> 网站关键字 </th>
					<td><asp:TextBox ID="txtKeywords" runat="server" MaxLength="50" Width="97%" CssClass="ipt"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<th> 网站描述 </th>
					<td><asp:TextBox ID="txtDescription" runat="server" MaxLength="50" Width="97%" CssClass="ipt"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<th> 备案号 </th>
					<td><asp:TextBox ID="txtICP" runat="server" MaxLength="100" Width="200px" CssClass="ipt"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<th> 网站版式<input type="button" class="tip-r" tip="<b>请勿随意改动该参数</b><br/>选择实时动态，所有频道都不会生成静态" /></th>
					<td><asp:RadioButtonList ID="rblIsHtml" runat="server" RepeatDirection="Horizontal">
							<asp:ListItem Value="1">静态生成</asp:ListItem>
							<asp:ListItem Value="0">实时动态</asp:ListItem>
						</asp:RadioButtonList>
					</td>
				</tr>
				<tr style="display:none;">
					<th> 静态后缀名 </th>
					<td><asp:RadioButtonList ID="rbStaticExt" runat="server" RepeatDirection="Horizontal">
							<asp:ListItem Value=".htm">.htm</asp:ListItem>
							<asp:ListItem Value=".html">.html</asp:ListItem>
							<asp:ListItem Value=".shtml">.shtml</asp:ListItem>
							<asp:ListItem Value=".aspx">.aspx</asp:ListItem>
						</asp:RadioButtonList>
					</td>
				</tr>
				<tr>
					<th> 会员注册 </th>
					<td><asp:RadioButtonList ID="rblAllowReg" runat="server" RepeatDirection="Horizontal">
							<asp:ListItem Value="1">允许</asp:ListItem>
							<asp:ListItem Value="0">不允许</asp:ListItem>
						</asp:RadioButtonList>
					</td>
				</tr>
				<tr>
					<th> 需要邮件激活<input type="button" class="tip-r" tip="如果选择是，那么需要在&quot;邮箱系统&quot;中配置系统邮箱" /></th>
					<td><asp:RadioButtonList ID="rblCheckReg" runat="server" RepeatDirection="Horizontal">
							<asp:ListItem Value="1">是</asp:ListItem>
							<asp:ListItem Value="0">否</asp:ListItem>
						</asp:RadioButtonList>
					</td>
				</tr>
				<tr>
					<th> 通行证皮肤 </th>
					<td><asp:RadioButtonList ID="rblPassportTheme" runat="server" RepeatDirection="Horizontal">
							<asp:ListItem Value="default">蓝色</asp:ListItem>
							<asp:ListItem Value="green">绿色</asp:ListItem>
						</asp:RadioButtonList>
					</td>
				</tr>
			</table>
		</div>
	</div>
	<div class="buttonok">
		<asp:Button ID="Button1" runat="server" Text="保存" CssClass="btnsubmit" OnClick="Button1_Click" />
	</div>
</form>
</body>
</html>
