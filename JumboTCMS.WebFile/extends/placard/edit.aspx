<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit.aspx.cs" Inherits="JumboTCMS.WebFile.Extends.Placard._edit" %>
<%@ Register Assembly="JumboTCMS.FCKeditorV2" Namespace="JumboTCMS.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title></title>
<link   type="text/css" rel="stylesheet" href="../../admin/css/common.css" />
<script type="text/javascript" src="../../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../../_data/jcmsV5.js"></script>
<link   type="text/css" rel="stylesheet" href="../../_data/jcmsV5.css" />

<script type="text/javascript">
$(document).ready(function(){
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtTitle").formValidator({tipid:"tipTitle",onshow:"请输入公告标题",onfocus:"请输入公告标题"}).InputValidator({min:4,max:50,onerror:"4-50个字符(2个字符=1个汉字)"});
});
</script>
</head>
<body>
<form id="form1" runat="server" onsubmit="return $.formValidator.PageIsValid('1')">
		<table class="formtable">
			<tr>
				<th> 标题 </th>
				<td><asp:TextBox ID="txtTitle" runat="server" Width="220px" CssClass="ipt"></asp:TextBox>
					<span id="tipTitle" style="width:200px;"> </span></td>
			</tr>
			<tr>
				<th> 内容 </th>
				<td><FCKeditorV2:FCKeditor ID="FCKeditor1" runat="server" ToolbarSet="Simple" Height="290px" Width="100%"> </FCKeditorV2:FCKeditor>
				<span id="tipContent" style="width:200px;"> </span></td>
			</tr>
		</table>
	<div class="buttonok">
		<asp:Button ID="btnSave" runat="server" Text="确定" CssClass="btnsubmit" />
		<input id="btnReset" type="button" value="取消" class="btncancel" onclick="parent.JumboTCMS.Popup.hide();" />
	</div>
</form>
</body>
</html>
