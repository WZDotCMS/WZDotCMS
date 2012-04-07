<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="photo_user_edit.aspx.cs" Inherits="JumboTCMS.WebFile.Modules._photo_user_edit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>图片编辑</title>
<link   type="text/css" rel="stylesheet" href="../admin/css/common.css" />
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/jcmsV5.js"></script>
<link   type="text/css" rel="stylesheet" href="../_data/jcmsV5.css" />



<script type="text/javascript">
	var ccid = joinValue('ccid');
$(document).ready(function(){
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtTitle").formValidator({tipid:"tipTitle",onshow:"请输入标题，单引号之类的将自动过滤",onfocus:"至少输入6个字符"}).InputValidator({min:6,onerror:"至少输入6个字符,请确认"})
		.AjaxValidator({
		type : "get",
		data:		"id=<%=id%>" + ccid,
		url:		"photo_user_ajax.aspx?oper=checkname&time="+(new Date().getTime()),
		datatype : "json",
		success : function(d){	
			if(d.result == "1")
				return true;
			else
				return false;
		},
		buttons: $("#btnSave"),
		error: function(){alert("服务器繁忙，请稍后再试");},
		onerror : "该标题已经存在",
		onwait : "正在校验标题的合法性，请稍候..."
	})<%if (id!="0") {%>.DefaultPassed()<%} %>;
});
</script>
</head>
<body>
<form id="form1" runat="server" onsubmit="return $.formValidator.PageIsValid('1')">
	<table class="formtable">
		<tr>
			<th> <%=ChannelItemName + "标题" %> </th>
			<td><asp:TextBox ID="txtAddDate" runat="server" Visible="false"></asp:TextBox>
				<asp:TextBox ID="txtTitle" runat="server" Width="400px" MaxLength="150" CssClass="ipt"></asp:TextBox>
				<span id="tipTitle" style="width:200px;"> </span>
				<br />
			</td>
		</tr>
		<tr>
			<th> 所属栏目 </th>
			<td><asp:DropDownList ID="ddlClassId" runat="server"> </asp:DropDownList>
			</td>
		</tr>
		<tr>
			<th> 来源 </th>
			<td><asp:TextBox ID="txtSourceFrom" runat="server" Width="180px" CssClass="ipt"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<th> 作者 </th>
			<td><asp:TextBox ID="txtAuthor" runat="server" MaxLength="20" Width="80px" CssClass="ipt">佚名</asp:TextBox>
				<span style="display:none;"><asp:TextBox ID="txtEditor" runat="server"></asp:TextBox>
				<asp:TextBox ID="txtUserId" runat="server">0</asp:TextBox></span>
			</td>
		</tr>
		<tr>
			<th> 缩略图 </th>
			<td><asp:TextBox ID="txtImg" runat="server" Width="420px" MaxLength="230" CssClass="ipt"></asp:TextBox></td>
		</tr>
		<tr>
			<th> 推荐 </th>
			<td><asp:RadioButtonList ID="rblIsTop" runat="server" RepeatColumns="2">
					<Items>
						<asp:ListItem Value="0" Selected="True">否</asp:ListItem>
						<asp:ListItem Value="1">是</asp:ListItem>
					</Items>
				</asp:RadioButtonList>
			</td>
		</tr>
		<tr>
			<th>标签<input type="button" class="tip-t" tip="多个标签之间请用&quot;,&quot;分割" /></th>
			<td><asp:TextBox ID="txtTags" runat="server" MaxLength="150" Width="300px" CssClass="ipt" onblur="FormatListValue(this.id);"></asp:TextBox></td>
		</tr>
		<tr>
			<th>简介<input type="button" class="tip-t" tip="html代码会被自动过滤，并只保留前200个字符" /></th>
			<td><asp:TextBox ID="txtSummary" runat="server" Height="97px" TextMode="MultiLine" Width="97%" CssClass="ipt"></asp:TextBox></td>
		</tr>
		<tr>
			<th> 图片地址<input type="button" class="tip-r" tip="多个地址之间请用换行分割" /></th>
			<td colspan="3"><asp:TextBox ID="txtPageSize" runat="server" Visible="false">1</asp:TextBox>
				<asp:TextBox ID="txtPhotoUrl" runat="server" Height="97px" TextMode="MultiLine" Width="97%" CssClass="ipt"></asp:TextBox>
				<br />
				<asp:Label ID="lbPhotoUrlMsg" runat="server" ForeColor="Red"></asp:Label>
				</td>
		</tr>
	</table>
	<div class="buttonok">
	<asp:CheckBox ID="chkIsEdit" runat="server" Text="立即发布" Visible="false" />
		<asp:Button ID="btnSave" runat="server" Text="确定" CssClass="btnsubmit" OnClick="btnSave_Click" />
		<input id="btnReset" type="button" value="取消" class="btncancel" onclick="parent.JumboTCMS.Popup.hide();" />
	</div>
</form>
<script type="text/javascript">_jcms_SetDialogTitle();</script>
</body>
</html>
