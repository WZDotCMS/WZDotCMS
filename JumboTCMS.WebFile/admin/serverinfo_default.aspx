<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="serverinfo_default.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._serverinfo_index" %>
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
$(function() {
    $('#container-1').tabs({ fxFade: true, fxSpeed: 'fast' });
    //$('#container-1').tabs({ remote: true });
});
</script>
<link rel="stylesheet" href="../_libs/jquery.tabs/style.css" type="text/css">
<!--[if lte IE 7]>
<link rel="stylesheet" href="../_libs/jquery.tabs/style-ie.css" type="text/css">
<![endif]-->
</head>
<body>
<form id="form1" runat="server">
	<br />
	<div id="container-1">
		<ul>
			<li><a href="#fragment-1"><span>常规属性</span></a></li>
			<li><a href="#fragment-2"><span>其他属性</span></a></li>
		</ul>
		<div id="fragment-1">
			<table class="formtable">
				<tr>
					<th> 服务器名 </th>
					<td><asp:Label ID="lbServerName" runat="server"></asp:Label>
					</td>
				</tr>
				<tr>
					<th> IP地址 </th>
					<td><asp:Label ID="lbIp" runat="server"></asp:Label>
					</td>
				</tr>
				<tr>
					<th> 当前域名 </th>
					<td><asp:Label ID="lbDomain" runat="server"></asp:Label>
					</td>
				</tr>
				<tr>
					<th> WEB端口 </th>
					<td><asp:Label ID="lbPort" runat="server"></asp:Label>
					</td>
				</tr>
				<tr>
					<th> IIS版本 </th>
					<td align="left"><asp:Label ID="lbIISVer" runat="server"></asp:Label>
					</td>
				</tr>
				<tr>
					<th> 当前目录 </th>
					<td><asp:Label ID="lbPhPath" runat="server"></asp:Label>
					</td>
				</tr>
				<tr>
					<th> 服务器操作系统 </th>
					<td><asp:Label ID="lbOperat" runat="server"></asp:Label>
					</td>
				</tr>
				<tr>
					<th> 系统所在文件夹 </th>
					<td><asp:Label ID="lbSystemPath" runat="server"></asp:Label>
					</td>
				</tr>
				<tr>
					<th> 脚本超时时间 </th>
					<td><asp:Label ID="lbTimeOut" runat="server"></asp:Label>
					</td>
				</tr>
				<tr>
					<th> 服务器的语言种类 </th>
					<td><asp:Label ID="lbLan" runat="server"></asp:Label>
					</td>
				</tr>
				<tr>
					<th> .NET Framework 版本 </th>
					<td><asp:Label ID="lbAspnetVer" runat="server"></asp:Label>
					</td>
				</tr>
				<tr>
					<th> 服务器当前时间 </th>
					<td><asp:Label ID="lbCurrentTime" runat="server"></asp:Label>
					</td>
				</tr>
				<tr>
					<th> 服务器IE版本 </th>
					<td><asp:Label ID="lbIEVer" runat="server"></asp:Label>
					</td>
				</tr>
				<tr>
					<th> 上次启动到现在已运行 </th>
					<td><asp:Label ID="lbServerLastStartToNow" runat="server"></asp:Label>
					</td>
				</tr>
			</table>
		</div>
		<div id="fragment-2">
			<table class="formtable">
				<tr>
					<th> 逻辑驱动器 </th>
					<td><asp:Label ID="lbLogicDriver" runat="server"></asp:Label>
					</td>
				</tr>
				<tr>
					<th> CPU 总数 </th>
					<td><asp:Label ID="lbCpuNum" runat="server"></asp:Label>
					</td>
				</tr>
				<tr>
					<th> CPU 类型 </th>
					<td><asp:Label ID="lbCpuType" runat="server"></asp:Label>
					</td>
				</tr>
				<tr>
					<th> 虚拟内存 </th>
					<td><asp:Label ID="lbMemory" runat="server"></asp:Label>
					</td>
				</tr>
				<tr>
					<th> 当前程序占用内存 </th>
					<td><asp:Label ID="lbMemoryPro" runat="server"></asp:Label>
					</td>
				</tr>
				<tr>
					<th> Asp.net所占内存 </th>
					<td><asp:Label ID="lbMemoryNet" runat="server"></asp:Label>
					</td>
				</tr>
				<tr>
					<th> Asp.net所占CPU </th>
					<td><asp:Label ID="lbCpuNet" runat="server"></asp:Label>
					</td>
				</tr>
				<tr>
					<th> 当前Session数量 </th>
					<td><asp:Label ID="lbSessionNum" runat="server"></asp:Label>
					</td>
				</tr>
				<tr>
					<th> 当前SessionID </th>
					<td><asp:Label ID="lbSession" runat="server"></asp:Label>
					</td>
				</tr>
				<tr>
					<th> 当前系统用户名 </th>
					<td><asp:Label ID="lbUser" runat="server"></asp:Label>
					</td>
				</tr>
			</table>
		</div>
	</div>
</form>
</body>
</html>
