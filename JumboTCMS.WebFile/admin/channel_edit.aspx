<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="channel_edit.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._channel_edit" %>
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
$(document).ready(function(){
	$('.tip-r').jtip({gravity: 'r',fade: false});
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtTitle").formValidator({tipid:"tipTitle",onshow:"请输入频道名称",onfocus:"建议输入4个汉字"}).InputValidator({min:4,max:10,onerror:"请输入4-10个字符或2-5个汉字,请确认"});
	$("#txtDir").formValidator({tipid:"tipDir",onshow:"添加之后不能修改",onfocus:"包含字母、数字、短线(-)和下划线"}).RegexValidator({regexp:"^[_\-a-zA-Z0-9\.]+(/{1}[_\-a-zA-Z0-9\.]+)?$",onerror:"输入有误"});
	$("#txtSubDomain").formValidator({empty:true,tipid:"tipSubDomain",onshow:"一旦添加请勿随意修改，如果不用二级域名请留空！",onfocus:"以http://开头，结尾不要加/。如:http://video.x.com",onempty:"如果不用二级域名请留空！"}).RegexValidator({regexp:"domain",datatype:"enum",onerror:"以http://开头，结尾不要加/"});
	$("#txtItemName").formValidator({tipid:"tipItemName",onshow:"2个汉字",onfocus:"2个汉字"}).RegexValidator({regexp:"^[\u4E00-\u9FA5]{2}$",onerror:"请输入2个汉字"});
	$("#txtItemUnit").formValidator({tipid:"tipItemUnit",onshow:"1个汉字",onfocus:"1个汉字"}).RegexValidator({regexp:"^[\u4E00-\u9FA5]{1}$",onerror:"请输入1个汉字"});
	$("#ddlLanguageCode").formValidator({empty:true,tipid:"tipLanguageCode",onshow:"选择之后不能修改",onfocus:"选择之后不能修改"}).InputValidator({min:1,onerror:"请选择语言包"});

});
    </script>
</head>
<body>
<form id="form1" runat="server" onsubmit="return $.formValidator.PageIsValid('1')">
	<table class="formtable">
		<tr>
			<th> 频道名称 </th>
			<td><asp:TextBox ID="txtTitle" runat="server" MaxLength="20" Width="225px" CssClass="ipt"></asp:TextBox>
				<span id="tipTitle" style="width:200px;"> </span></td>
		</tr>
		<tr>
			<th> 频道简介<input type="button" class="tip-r" tip="使用一句话概括，主要便于搜索引擎优化(SEO)" /></th>
			<td><asp:TextBox ID="txtInfo" runat="server" Width="97%" CssClass="ipt"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<th> 频道实际目录 </th>
			<td><asp:TextBox ID="txtDir" runat="server" MaxLength="30" Width="120px" CssClass="ipt"></asp:TextBox>
				<span id="tipDir" style="width:200px;"> </span></td>
		</tr>
		<tr style="display:<%=(base.Edition == "All")?"":"none"%>">
			<th> 频道访问域名 </th>
			<td><asp:TextBox ID="txtSubDomain" runat="server" MaxLength="100" Width="300px" CssClass="ipt"></asp:TextBox>
				<br /><span id="tipSubDomain" style="width:400px;"> </span></td>
		</tr>
		<tr>
			<th> 栏目最大深度<input type="button" class="tip-r" tip="0表示无分类" /></th>
			<td>
                <asp:DropDownList ID="ddlClassDepth" runat="server">
                <asp:ListItem Selected="True">4</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>0</asp:ListItem>
                </asp:DropDownList>
			</td>
		</tr>
		<tr>
			<th> 项目名称<input type="button" class="tip-r" tip="在频道的侧边有最新新闻/推荐新闻，其中的“新闻”就是该频道的项目名称。<br />范例：“新闻”、“图片”、“软件”" /></th>
			<td><asp:TextBox ID="txtItemName" runat="server" MaxLength="20" CssClass="ipt">内容</asp:TextBox>
				<span id="tipItemName" style="width:200px;"> </span></td>
		</tr>
		<tr>
			<th> 项目单位<input type="button" class="tip-r" tip="在内容页有上一篇/下一篇，其中的“篇”就是该频道的项目单位。<br />范例：“篇”、“副”、“个”" /></th>
			<td><asp:TextBox ID="txtItemUnit" runat="server" MaxLength="8" Width="60px" CssClass="ipt">篇</asp:TextBox>
				<span id="tipItemUnit" style="width:200px;"> </span></td>
		</tr>
		<tr>
			<th> 频道页模板 </th>
			<td><asp:DropDownList ID="ddlTemplate" runat="server"> </asp:DropDownList>
			</td>
		</tr>
		<tr>
			<th> 是否静态生成<input type="button" class="tip-r" tip="1.网站不启用静态，频道将不能静态<br/>2.采用静态的频道，若要前台同步则需要手动去更新<br/>3.频道一旦创建请勿随意改动该参数<br/>4.静态化的频道不支持列表页和内容页的权限控制" /></th>
			<td><asp:RadioButtonList ID="rblIsHtml" runat="server" EnableViewState="False" RepeatColumns="2">
					<asp:ListItem Value="0" Selected="True">否</asp:ListItem>
					<asp:ListItem Value="1">是</asp:ListItem>
				</asp:RadioButtonList>(请勿随意改动)
			</td>
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
		<tr>
			<th> 禁用本频道 </th>
			<td><asp:RadioButtonList ID="rblEnabled" runat="server" EnableViewState="False" RepeatColumns="2">
					<asp:ListItem Selected="True" Value="1">否</asp:ListItem>
					<asp:ListItem Value="0">是</asp:ListItem>
				</asp:RadioButtonList>
			</td>
		</tr>
		<tr>
			<th> 允许会员投稿</th>
			<td><asp:RadioButtonList ID="rblIsPost" runat="server" EnableViewState="False" RepeatColumns="2">
					<Items>
						<asp:ListItem Value="0" Selected="True">否</asp:ListItem>
						<asp:ListItem Value="1">是</asp:ListItem>
					</Items>
				</asp:RadioButtonList></td>
		</tr>
		<tr>
			<th> 默认缩略图尺寸 </th>
			<td>
                <asp:DropDownList ID="ddlDefaultThumbs" runat="server">
                </asp:DropDownList>
			</td>
		</tr>
		<tr>
			<th> 附件存放目录 </th>
			<td><asp:TextBox ID="txtUploadPath" runat="server" Width="400px" CssClass="ipt"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<th> 附件上传后缀 </th>
			<td><asp:TextBox ID="txtUploadType" runat="server" Width="400px" CssClass="ipt"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<th> 附件大小限制 </th>
			<td><asp:TextBox ID="txtUploadSize" runat="server" Width="60px" CssClass="ipt"></asp:TextBox> K
			<span id="tipUploadSize" style="width:200px;"> </span></td>
		</tr>
		<tr style="display:none;">
			<th> 语言包 </th>
			<td><asp:DropDownList ID="ddlLanguageCode" runat="server"> </asp:DropDownList>
			<span id="tipLanguageCode" style="width:200px;"> </span></td>
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
