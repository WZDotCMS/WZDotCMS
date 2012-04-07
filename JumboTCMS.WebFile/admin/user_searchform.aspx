<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user_searchform.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._user_searchform" %>
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
var gid = joinValue('gid');//栏目ID
var keys=joinValue('keys');//关键字
function go2Search(){
	var _gid = $("#ddlUserGroup").val();
	var _keys = $("#txtKeyword").val();
	top.IframeOper.location.href="user_list.aspx?gid=" + _gid + "&keys=" + encodeURIComponent(_keys);
	top.JumboTCMS.Popup.hide();
}
    </script>
</head>
<body>
<form id="form1" runat="server">
	<table class="formtable">
		<tr>
			<th> 所属分组 </th>
			<td><asp:DropDownList ID="ddlUserGroup" runat="server"> </asp:DropDownList>
			</td>
		</tr>
		<tr>
			<th> 用户名 </th>
			<td><asp:TextBox ID="txtKeyword" runat="server" Width="264px" CssClass="ipt"></asp:TextBox>
				<br />
				不支持符号 </td>
		</tr>
	</table>
	<div class="buttonok">
		<input id="btnSearch" type="button" value="确定" class="btnsubmit" onclick="go2Search();" />
		<input id="btnReset" type="button" value="取消" class="btncancel" onclick="parent.JumboTCMS.Popup.hide();" />
	</div>
</form>
<script type="text/javascript">_jcms_SetDialogTitle();</script>
</body>
</html>
