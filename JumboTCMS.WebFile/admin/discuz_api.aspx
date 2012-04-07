<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="discuz_api.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._discuz_api" %>
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
	$.formValidator.initConfig({validatorGroup:"1",onError:function(msg){/*alert(msg)*/}});
	$("#txtForumAPIKey").formValidator({validatorGroup:"1",empty:true,tipid:"tipForumAPIKey",onshow:"留空表示不整合",onfocus:"留空表示不整合"}).InputValidator({min:1,onerror:"请输入"});
});
function Button1_onclick() {
    return $.formValidator.PageIsValid('1');
}
</script>
</head>
<body>
<form id="form1" runat="server">
	<br />
	<div>
		<div>
			<ul class="tabs-nav">
				<li class="tabs-selected"><a><span>整合接口</span></a></li>
			</ul>
		</div>

		<div class="tabs-container">
			<table class="helptable">
				<tr>
					<td><ul>
							<li>开源版只做了Discuz!NT2.6及以上版本的单向整合，即：CMS同步注册/登录/注销论坛。</li>
							<li>若要做更深的耦合，请自行研究DiscuzToolkit。</li>
						</ul></td>
				</tr>
			</table>
			<table class="formtable">
				<tr>
					<th> API Key </th>
					<td><asp:TextBox ID="txtForumAPIKey" runat="server" Width="350px" CssClass="ipt"></asp:TextBox>
						<span id="tipForumAPIKey" style="width:200px;"> </span></td>
				</tr>
				<tr>
					<th> 密钥 </th>
					<td><asp:TextBox ID="txtForumSecret" runat="server" Width="350px" CssClass="ipt"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<th> 论坛访问地址 </th>
					<td><asp:TextBox ID="txtForumUrl" runat="server" Width="97%" CssClass="ipt"></asp:TextBox>
					</td>
				</tr>
			</table>
			<div class="buttonok">
				<asp:Button ID="Button1" onclientclick="return Button1_onclick()" runat="server" Text="保存" CssClass="btnsubmit" OnClick="Button1_Click" />
			</div>
		</div>
	</div>
</form>
</body>
</html>
