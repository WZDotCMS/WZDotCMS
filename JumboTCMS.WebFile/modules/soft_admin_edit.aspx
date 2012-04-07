<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="soft_admin_edit.aspx.cs" Inherits="JumboTCMS.WebFile.Modules._Soft_admin_edit" %>
<%@ Register Assembly="JumboTCMS.FCKeditorV2" Namespace="JumboTCMS.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>软件编辑</title>
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
		url:		"soft_admin_ajax.aspx?oper=checkname&time="+(new Date().getTime()),
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
	$("#txtPoints").formValidator({tipid:"tipPoints",onshow:"请填写数字",onfocus:"请填写数字"}).RegexValidator({regexp:"^\([0-9]+)$",onerror:"请填写数字"});
	$("#txtAliasPage").formValidator({empty:true,tipid:"tipAliasPage",onshow:"静态文件路径(第1页)，不输入即为默认。如/aa/bb/cc.html",onfocus:"这里不作合法性的验证，请慎重输入"}).RegexValidator({regexp:"^\(/[_\-a-zA-Z0-9\.]+(/[_\-a-zA-Z0-9\.]+)*\.(aspx|htm(l)?|shtm(l)?))$",onerror:"格式错误"});
});
//插入预览附件地址
function AttachmentSelected(path,elementid)
{
	$("#"+elementid).val(path);
}
//插入上传软件
function AttachmentOperater(path,type,size){
    var currUrl=$("#txtDownUrl").val();
    if(currUrl!="")
    {
		$("#txtDownUrl").val(currUrl + "\r本站地址|||" + path);
		$("#txtSSize").val(size);
	}
	else
	{
	    $("#txtDownUrl").val("本站地址|||" + path);
	    $("#txtSSize").val(size);
	}
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
			<th> 作者/开发商 </th>
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
			<th> <%=ChannelItemName+"版本" %> </th>
			<td><asp:TextBox ID="txtVersion" runat="server" CssClass="ipt"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<th> <%=ChannelItemName+"平台" %> </th>
			<td><asp:TextBox ID="txtOperatingSystem" runat="server" Width="97%" CssClass="ipt">Win9x/2000/XP/2003</asp:TextBox>
				&nbsp; <br />
				<script type="text/javascript">
	function oslist() {
		var OSList = "Win9x|2000|XP|2003|Vista|Unix|Linux";
		var arrOS = OSList.split("|");
		for (i=0;i<=arrOS.length-1;i++) {
			document.write ("<input type=\"checkbox\" onclick=\"setos(this)\" value=\""+arrOS[i]+"\">"+arrOS[i]+" ");
		}
	}
	oslist();
	function setos(obj) {
		var value = obj.value;
		var OSList = document.form1.txtOperatingSystem.value;
		var arrOS = OSList.split("/");
		var l= "/" + OSList ;
		var s,b;
		if (obj.checked==true) {
			for (i=0;i<=arrOS.length-1;i++) {
				if (arrOS[i]==value) {
					b=true;
					break;
				}
			}
			if (b!=true) {
				s = l + "/" + value;
				if (s.substr(0,1)=="/") {
					s=s.substr(1);
				}
				document.form1.txtOperatingSystem.value = s;
			}
		}
		else {
			for (i=0;i<=arrOS.length-1;i++) {
				if (arrOS[i]==value) {
					eval("var re=/\\/?"+value+"/g");
					s=l.replace(re,"");
					if (s.substr(0,1)=="/") {
						s=s.substr(1)
					}
					document.form1.txtOperatingSystem.value = s ;
					break;
				}
			}
		}
	}
                </script>
			</td>
		</tr>
		<tr>
			<th> 解压密码 </th>
			<td><asp:TextBox ID="txtUnZipPass" runat="server" Width="97%" CssClass="ipt">无</asp:TextBox>
			</td>
		</tr>
		<tr>
			<th> 演示地址 </th>
			<td><asp:TextBox ID="txtDemoUrl" runat="server" Width="97%" CssClass="ipt">http://</asp:TextBox>
			</td>
		</tr>
		<tr>
			<th> 官方/注册地址 </th>
			<td><asp:TextBox ID="txtRegUrl" runat="server" Width="97%" CssClass="ipt">http://</asp:TextBox>
			</td>
		</tr>
		<tr>
			<th>简介<input type="button" class="tip-t" tip="html代码会被自动过滤，并只保留前200个字符" /></th>
			<td><asp:TextBox ID="txtSummary" runat="server" Height="97px" TextMode="MultiLine" Width="97%" CssClass="ipt"></asp:TextBox></td>
		</tr>
		<tr>
			<th>详细介绍</th>
			<td><FCKeditorV2:FCKeditor ID="FCKeditor1" runat="server" ToolbarSet="Simple" Height="350px" Width="100%"> </FCKeditorV2:FCKeditor>
				<br />
				<asp:Label ID="txtContentMsg" runat="server" ForeColor="Red"></asp:Label>
			</td>
		</tr>
		<tr>
			<th> 下载需博币 </th>
			<td><asp:TextBox ID="txtPoints" runat="server" MaxLength="3" Width="30px" CssClass="ipt">0</asp:TextBox>
				<span id="tipPoints" style="width:200px;"> </span></td>
		</tr>
		<tr>
			<th> 大小 </th>
			<td><asp:TextBox ID="txtSSize" runat="server" Width="80px" CssClass="ipt">未知</asp:TextBox>
			</td>
		</tr>
		<tr>
			<th> 附件地址<input type="button" class="tip-r" tip="多个地址之间请用换行分割" /></th>
			<td colspan="3"><asp:TextBox ID="txtDownUrl" runat="server" Height="97px" TextMode="MultiLine" Width="97%" CssClass="ipt"></asp:TextBox>
				<br />
				<asp:Label ID="lbDownUrlMsg" runat="server" ForeColor="Red"> </asp:Label>
				</td>
		</tr>
		<tr>
			<th> 附件上传 </th>
			<td><iframe id="frm_upload" src="../admin/attachment_default.aspx?ccid=<%=ChannelId %>" width="100%"
                    height="30" scrolling="no" frameborder="0"></iframe></td>
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
