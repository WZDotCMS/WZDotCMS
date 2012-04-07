<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="photo_admin_edit.aspx.cs" Inherits="JumboTCMS.WebFile.Modules._photo_admin_edit" %>
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
	$('.tip-t').jtip({gravity: 't',fade: false});
	$('.tip-r').jtip({gravity: 'r',fade: false});
	$('.tip-b').jtip({gravity: 'b',fade: false});
	$('.tip-l').jtip({gravity: 'l',fade: false});
	$("#txtTColor").attachColorPicker();
	var color = $("#txtTColor").getValue();
	if(color!="")
		$("#txtTitle").css("color",color);
	$("#txtTColor").change(function() {
		var color2 = $("#txtTColor").getValue();
		$("#txtTitle").css("color","#000");
		$("#txtTitle").css("color",color2);
	});
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtTitle").formValidator({tipid:"tipTitle",onshow:"请输入标题，单引号之类的将自动过滤",onfocus:"至少输入6个字符"}).InputValidator({min:6,onerror:"至少输入6个字符,请确认"})
		.AjaxValidator({
		type : "get",
		data:		"id=<%=id%>" + ccid,
		url:		"photo_admin_ajax.aspx?oper=checkname&time="+(new Date().getTime()),
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
	$("#txtAliasPage").formValidator({empty:true,tipid:"tipAliasPage",onshow:"静态文件路径(第1页)，不输入即为默认。如/aa/bb/cc.html",onfocus:"这里不作合法性的验证，请慎重输入"}).RegexValidator({regexp:"^\(/[_\-a-zA-Z0-9\.]+(/[_\-a-zA-Z0-9\.]+)*\.(aspx|htm(l)?|shtm(l)?))$",onerror:"格式错误"});
});
//插入预览附件地址
function AttachmentSelected(path,elementid)
{
	$("#"+elementid).val(path);
}
//插入上传图片
function AttachmentOperater(path,type,size){
	path = path.toLowerCase().replace(/(.jpg|.gif|.bmp|.png)[\|]{3}/g, '|||').replace(/[$]{3}/g, '\r');
	var currUrl=$("#txtPhotoUrl").val();
	if(currUrl!="")
		$("#txtPhotoUrl").val(currUrl + "\r" + path);
	else
		$("#txtPhotoUrl").val(path);
}
var PhotoInput = 'txtImg';
function FillPhoto(photo){
	$('#'+PhotoInput).val(photo);
}
    </script>
</head>
<body>
<form id="form1" runat="server" onsubmit="return $.formValidator.PageIsValid('1')">
	<table class="formtable">
		<tr>
			<th> <%=ChannelItemName + "标题" %> </th>
			<td><asp:TextBox ID="txtTitle" runat="server"  MaxLength="150" Width="97%" CssClass="ipt"></asp:TextBox>
				<br />
				<img alt="标题颜色" src="../admin/images/color.gif" width="21" height="21" align="absbottom" />
				<asp:TextBox ID="txtTColor" runat="server" Width="80px" CssClass="ipt"></asp:TextBox><span id="tipTitle" style="width:200px;"> </span>
			</td>
		</tr>
		<tr>
			<th> 发布时间 </th>
			<td><asp:TextBox ID="txtAddDate" runat="server" CssClass="ipt" Width="150px"></asp:TextBox>
			</td>
		</tr>
		<tr style="display:<%=(ChannelClassDepth > 0)?"":"none"%>">
			<th> 所属栏目 </th>
			<td><asp:DropDownList ID="ddlClassId" runat="server"> </asp:DropDownList>
			</td>
		</tr>
		<tr>
			<th> 最低浏览权限 </th>
			<td><asp:DropDownList ID="ddlReadGroup" runat="server"> </asp:DropDownList>
			(此功能在静态生成的频道无效)
			</td>
		</tr>
		<tr>
			<th> 来源 </th>
			<td><asp:TextBox ID="txtSourceFrom" runat="server" Width="300px" CssClass="ipt"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<th> 作者 </th>
			<td><asp:TextBox ID="txtAuthor" runat="server" MaxLength="20" Width="120px" CssClass="ipt"></asp:TextBox>
				<span style="display:none;">
				<asp:TextBox ID="txtEditor" runat="server" CssClass="ipt"></asp:TextBox>
				<asp:TextBox ID="txtUserId" runat="server" CssClass="ipt">0</asp:TextBox>
				</span> </td>
		</tr>
		<tr>
			<th> 缩略图 </th>
			<td><asp:TextBox ID="txtImg" runat="server" MaxLength="100" Width="97%" CssClass="ipt"></asp:TextBox>
				<a href="javascript:void(0);" onclick="PhotoInput = 'txtImg';WinFullOpen('../admin/cut2thumbs_upload.aspx?ccid=<%=ChannelId %>&photo='+encodeURIComponent($('#txtImg').val()));">
                <img src="../admin/images/createthumbs.png" align="absmiddle" style="border:0px;" />
				</a> </td>
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
		<tr style="display:none;">
			<th> 缩略图地址<input type="button" class="tip-r" tip="多个地址之间请用换行分割" /></th>
			<td colspan="3"><asp:TextBox ID="txtThumbsUrl" runat="server" Height="97px" TextMode="MultiLine" Width="97%" CssClass="ipt"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<th> 大图地址<input type="button" class="tip-r" tip="多个地址之间请用换行分割" /></th>
			<td colspan="3"><asp:CheckBox ID="chkSaveRemoteThumbs" runat="server" Text="远程图片自动缩略" Checked="true" />
			<br /><asp:TextBox ID="txtPageSize" runat="server" Visible="false" CssClass="ipt">1</asp:TextBox>
				<asp:TextBox ID="txtPhotoUrl" runat="server" Height="97px" TextMode="MultiLine" Width="97%" CssClass="ipt"></asp:TextBox>
				<br />
				<asp:Label ID="lbPhotoUrlMsg" runat="server" ForeColor="Red"></asp:Label>
			</td>
		</tr>
		<tr>
			<th> 图片上传 </th>
			<td><iframe id="frm_upload" src="../admin/attachment_default2.aspx?ccid=<%=ChannelId %>" width="100%"
                    height="345" scrolling="no" frameborder="0"></iframe></td>
		</tr>
		<tr style="display:<%=(base.Edition == "All" && ChannelIsHtml)?"":"none"%>">
			<th> 静态文件路径(第1页) </th>
			<td><asp:TextBox ID="txtAliasPage" runat="server" Width="97%" CssClass="ipt"></asp:TextBox>
				<br /><span id="tipAliasPage" style="width:400px;"></span></td>
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
