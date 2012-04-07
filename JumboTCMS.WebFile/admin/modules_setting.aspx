<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="modules_setting.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._modules_setting" %>
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
	$.formValidator.initConfig({validatorGroup:"1",onError:function(msg){alert(msg);}});
	$("#txtAdminUploadType").formValidator({validatorGroup:"1",tipid:"tipAdminUploadType",onshow:"半角分号隔开",onfocus:"半角分号隔开"});
	$("#txtAdminUploadSize").formValidator({validatorGroup:"1",tipid:"tipAdminUploadSize",onshow:"单位为KB",onfocus:"单位为KB",onfocus:"单位为KB"}).RegexValidator({regexp:"num1",datatype:"enum",onerror:"格式不正确"});
    $.formValidator.initConfig({validatorGroup:"2",onError:function(msg){/*alert(msg)*/}});
	$("#txtUserUploadType").formValidator({validatorGroup:"2",tipid:"tipUserUploadType",onshow:"半角分号隔开",onfocus:"半角分号隔开"});
    $("#txtUserUploadSize").formValidator({validatorGroup:"2",tipid:"tipUserUploadSize",onshow:"单位为KB",onfocus:"单位为KB"}).RegexValidator({regexp:"num1",datatype:"enum",onerror:"格式不正确"});
});
function Button1_onclick() {
    return $.formValidator.PageIsValid('1');
}

function Button2_onclick() {
    return $.formValidator.PageIsValid('2');
}
    </script>
</head>
<body>
<form id="form1" runat="server">
	<br />
	<div>
		<div>
			<ul class="tabs-nav">
				<li class="tabs-selected"><a><span>附件上传设置</span></a></li>
			</ul>
		</div>
		<div class="tabs-container">
			<table class="formtable">
				<tr>
					<th> 后台上传格式限制 </th>
					<td><asp:TextBox ID="txtAdminUploadType" runat="server" MaxLength="80" Width="300px" CssClass="ipt"></asp:TextBox>
						<br /><span id="tipAdminUploadType" style="width: 250px"></span></td>
				</tr>
				<tr>
					<th> 后台上传大小限制 </th>
					<td><asp:TextBox ID="txtAdminUploadSize" runat="server" MaxLength="20" Width="300px" CssClass="ipt"></asp:TextBox>
						<br /><span id="tipAdminUploadSize" style="width: 250px"></span></td>
				</tr>
			</table>
			<div class="buttonok">
				<asp:Button ID="Button1" onclientclick="return Button1_onclick()" runat="server"
                    Text="保存" CssClass="btnsubmit" OnClick="Button1_Click" />
			</div>
			<br />
			<table class="formtable" style="display:<%=UserDisplay%>">
				<tr>
					<th> 投稿上传格式限制 </th>
					<td><asp:TextBox ID="txtUserUploadType" runat="server" MaxLength="20" Width="300px" CssClass="ipt"></asp:TextBox>
						<br /><span id="tipUserUploadType" style="width: 250px"></span></td>
				</tr>
				<tr>
					<th> 投稿上传大小限制 </th>
					<td><asp:TextBox ID="txtUserUploadSize" runat="server" MaxLength="80" Width="300px" CssClass="ipt"></asp:TextBox>
						<br /><span id="tipUserUploadSize" style="width: 250px"></span></td>
				</tr>
			</table>
			<div class="buttonok" style="display:<%=UserDisplay%>">
				<asp:Button ID="Button2" onclientclick="return Button2_onclick()" runat="server"
                    Text="保存" CssClass="btnsubmit" OnClick="Button2_Click" />
			</div>
		</div>
	</div>
</form>
</body>
</html>
