<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit.aspx.cs" Inherits="JumboTCMS.WebFile.Extends.Vote._edit" %>
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
	$("#txtTitle").formValidator({tipid:"tipTitle",onshow:"请输入标题",onfocus:"请输入4-30个字符(2-15个汉字)"}).InputValidator({min:4,max:30,onerror:"请输入4-30个字符(2-15个汉字)"});
	$("#txtContent").formValidator({tipid:"tipContent",onshow:"每个选项之间用&quot;|&quot;隔开",onfocus:"每个选项之间用&quot;|&quot;隔开"}).InputValidator({min:1,onerror:"请输入投票项"});
});
function FormatList(id)
{
	var _val = $('#txtContent').val();
	_val =_val.replace(/\r\n/g,"\n");
	_val =_val.replace(/[|\n]+/g,"|");
	_val =_val.replace(/[\n]+/g,"|");
	_val =_val.replace(/[|]+/g,"|");
	_val =_val.replace(/ /g,"");
	$('#txtContent').val(_val);
}
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
				<th> 投票选项 </th>
				<td><asp:TextBox ID="txtContent" onblur="FormatList('txtContent');" runat="server" Height="120px" TextMode="MultiLine" Width="97%" CssClass="ipt"></asp:TextBox>
				<span id="tipContent" style="width:200px;"> </span></td>
			</tr>
			<tr>
				<th> 类别 </th>
				<td><asp:RadioButtonList ID="rblType" runat="server" RepeatDirection="Horizontal">
						<asp:ListItem Value="0" Selected="True">单选</asp:ListItem>
						<asp:ListItem Value="1">多选</asp:ListItem>
					</asp:RadioButtonList>
				</td>
			</tr>
		</table>
	<div class="buttonok">
		<asp:Button ID="btnSave" runat="server" Text="确定" CssClass="btnsubmit" 
            OnClick="btnSave_Click" />
		<input id="btnReset" type="button" value="取消" class="btncancel" onclick="parent.JumboTCMS.Popup.hide();" />
	</div>
</form>
</body>
</html>
