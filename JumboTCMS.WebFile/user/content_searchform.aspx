<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="content_searchform.aspx.cs" Inherits="JumboTCMS.WebFile.User._content_searchform" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title></title>
<link   type="text/css" rel="stylesheet" href="../style/user/common.css" />
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/jcmsV5.js"></script>
<link   type="text/css" rel="stylesheet" href="../_data/jcmsV5.css" />
<script type="text/javascript">
var ctype = joinValue('ctype');//频道类别
var ccid = joinValue('ccid');//频道ID
var cid = joinValue('cid');//栏目ID
var k=joinValue('k');//关键字
var f=joinValue('f');//检索字段
var s=joinValue('s');//检索状态
var d=joinValue('d');//检索时间
function go2Search(){
    var _cid = $("#ddlKeyClass").val();
    var _k = $("#txtKeyword").val();
    var _f = $("#ddlKeyType").val();
    var _d = $("#ddlAddDate").val();
    if(( _k!="" ) && ( _f=="" ))
    {
        alert("输入关键字后一定要选择匹配字段");
        return;
    }
    parent.location.href=site.Dir + "modules/"+q('ctype')+"/user/list.htm?ccid="+q('ccid')+"&ctype="+q('ctype')+"&cid="+_cid+"&k="+encodeURIComponent(_k)+"&f="+_f+"&d="+_d;
}
    </script>
</head>
<body>
<form id="form1" runat="server">
	<table class="formtable">
		<tr>
			<th> 所属栏目 </th>
			<td><asp:DropDownList ID="ddlKeyClass" runat="server"> </asp:DropDownList>
			</td>
		</tr>
		<tr>
			<th> 关键字 </th>
			<td><asp:TextBox ID="txtKeyword" runat="server" Width="264px" CssClass="ipt"></asp:TextBox>
				<br />
				不支持单引、双引等特殊符号 </td>
		</tr>
		<tr>
			<th> 匹配字段 </th>
			<td><asp:DropDownList ID="ddlKeyType" runat="server">
					<asp:ListItem Value="">请选择</asp:ListItem>
					<asp:ListItem Value="title">标题</asp:ListItem>
					<asp:ListItem Value="summary">简介</asp:ListItem>
				</asp:DropDownList>
			</td>
		</tr>
		<tr>
			<th> 发布时间 </th>
			<td><asp:DropDownList ID="ddlAddDate" runat="server">
					<asp:ListItem Value="">不限时间</asp:ListItem>
					<asp:ListItem Value="1d">今天</asp:ListItem>
					<asp:ListItem Value="1w">本周</asp:ListItem>
					<asp:ListItem Value="1m">本月</asp:ListItem>
				</asp:DropDownList>
			</td>
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
