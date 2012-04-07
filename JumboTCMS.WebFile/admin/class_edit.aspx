<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="class_edit.aspx.cs" Inherits="JumboTCMS.WebForm.Admin._class_edit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>栏目编辑</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/jcmsV5.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/jcmsV5.css" />
<link type="text/css" rel="stylesheet" href="css/common.css" />
<script type="text/javascript" src="../_libs/pinyin.js"></script>
<script type="text/javascript">
var ccid = joinValue('ccid');
$(document).ready(function(){
	if(q('parentid')!='') $('#ddlClass').val(q('parentid'));
	$('.tip-r').jtip({gravity: 'r',fade: false});
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtTitle").formValidator({tipid:"tipTitle",onshow:"请输入栏目名称",onfocus:"推荐使用4个汉字"}).InputValidator({min:4,max:30,onerror:"请输入4-30个字符(2-15个汉字)"});
	$("#txtPageSize").formValidator({tipid:"tipPageSize",onshow:"请填写5-50的数字,推荐20",onfocus:"请填写5-100的数字,推荐20"}).RegexValidator({regexp:"^\([5-9]{1}|[1-9]{1}[0-9]{1}|100)$",onerror:"请填写5-100的数字"});
	$("#txtFolder").formValidator({tipid:"tipFolder",onshow:"只支持英文字母、数字和短线",onfocus:"一旦保存将无法修改"}).RegexValidator({regexp:"^\([0-9a-zA-Z\-]+)$",onerror:"只支持英文字母、数字和短线"});
	$("#txtAliasPage").formValidator({empty:true,tipid:"tipAliasPage",onshow:"静态文件路径(第1页)，不输入即为默认。如/aa/bb/cc.html",onfocus:"这里不作合法性的验证，请慎重输入"}).RegexValidator({regexp:"^\(/[_\-a-zA-Z0-9\.]+(/[_\-a-zA-Z0-9\.]+)*\.(aspx|htm(l)?|shtm(l)?))$",onerror:"格式错误"});
	$("#txtSortRank").formValidator({tipid:"tipSortRank",onshow:"请填写数字",onfocus:"请填写数字"}).RegexValidator({regexp:"^\([0-9]+)$",onerror:"请填写数字"});
});
function ajaxChinese2Pinyin(chinese,t)
{
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"chinese="+encodeURIComponent(chinese)+"&t="+t+"&time="+(new Date().getTime()),
		url:		"ajax.aspx?oper=ajaxChinese2Pinyin",
		error:		function(XmlHttpRequest,textStatus, errorThrown){JumboTCMS.Loading.hide();alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				JumboTCMS.Alert(d.returnval, "0", "top.window.location='" + site.Dir + "admin/login.aspx';");
				break;
			case '0':
				JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				$("#txtFolder").val(d.returnval);
				break;
			}
		}
	});
}
</script>
</head>
<body>
<form id="form1" runat="server" onsubmit="return $.formValidator.PageIsValid('1')">
	<table class="formtable">
		<tr>
			<th> 栏目名称 </th>
			<td><asp:TextBox ID="txtTitle" runat="server" MaxLength="40" Width="200px" CssClass="ipt"></asp:TextBox>
				<span id="tipTitle" style="width:200px;"> </span></td>
		</tr>
		<tr style="display:none;">
			<th>序号</th>
			<td><asp:TextBox ID="txtSortRank" runat="server" MaxLength="3" Width="40px" CssClass="ipt">0</asp:TextBox>
				<span id="tipSortRank" style="width:200px;"> </span><br /><span class="red">在同级栏目下请勿重复</span></td>
		</tr>
		<tr>
			<th> 目录名称</th>
			<td><asp:TextBox ID="txtFolder" runat="server" MaxLength="40" Width="200px" CssClass="ipt"></asp:TextBox> 
                <span id="tipFolder" style="width:200px;"> </span></td>
		</tr>
		<tr>
			<th> 所属分类 </th>
			<td><asp:DropDownList ID="ddlClass" runat="server"> </asp:DropDownList>
				(该频道只支持<%=ChannelClassDepth %>级分类) </td>
		</tr>
		<tr style="display:none;">
			<th> 最低浏览权限 </th>
			<td><asp:DropDownList ID="ddlReadGroup" runat="server"> </asp:DropDownList>
			(此功能在静态生成的频道无效) 
			</td>
		</tr>
		<tr>
			<th> 栏目简介</th>
			<td><asp:TextBox ID="txtInfo" runat="server" Height="97px" TextMode="MultiLine" Width="97%" CssClass="ipt"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<th> 列表页模板 </th>
			<td><asp:DropDownList ID="ddlTemplateId" runat="server"> </asp:DropDownList>
			</td>
		</tr>
		<tr>
			<th> 内容页模板 </th>
			<td><asp:DropDownList ID="ddlContentTemp" runat="server"> </asp:DropDownList>
			</td>
		</tr>
		<tr>
			<th> 默认每页记录数 </th>
			<td><asp:TextBox ID="txtPageSize" runat="server" Width="39px" CssClass="ipt">20</asp:TextBox>
				<span id="tipPageSize" style="width:200px;"> </span></td>
		</tr>
		<tr style="display:<%=MainChannel.IsPost? "" : "none"%>;">
			<th> 允许会员投稿</th>
			<td><asp:RadioButtonList ID="rblIsPost" runat="server" EnableViewState="False" RepeatColumns="2">
					<Items>
						<asp:ListItem Value="1">是</asp:ListItem>
						<asp:ListItem Value="0" Selected="True">否</asp:ListItem>
					</Items>
				</asp:RadioButtonList></td>
		</tr>
		<tr>
			<th> 允许在“当前页位置”显示</th>
			<td><asp:RadioButtonList ID="rblIsTop" runat="server" EnableViewState="False" RepeatColumns="2">
					<Items>
						<asp:ListItem Value="1" Selected="True">是</asp:ListItem>
						<asp:ListItem Value="0">否</asp:ListItem>
					</Items>
				</asp:RadioButtonList></td>
		</tr>
		<tr style="display:none">
			<th> 静态文件路径(第1页)(功能不支持) </th>
			<td><asp:TextBox ID="txtAliasPage" runat="server" Width="97%" CssClass="ipt"></asp:TextBox>
				<br /><span id="tipAliasPage" style="width:400px;"></span></td>
		</tr>
	</table>
	<div class="buttonok">
		<asp:Button ID="btnSave" runat="server" Text="确定" CssClass="btnsubmit" 
            onclick="btnSave_Click" />
		<input id="btnReset" type="button" value="取消" class="btncancel" onclick="parent.JumboTCMS.Popup.hide();" />
	</div>
</form>
<script type="text/javascript">_jcms_SetDialogTitle();</script>
</body>
</html>
