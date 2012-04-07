<%@ Page Language="C#" AutoEventWireup="true" Codebehind="tougao_home.aspx.cs" Inherits="JumboTCMS.WebFile.User._tougao_home" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title></title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/jcmsV5.js"></script>
<link   type="text/css" rel="stylesheet" href="../_data/jcmsV5.css" />
<link   type="text/css" rel="stylesheet" href="../style/user/common.css" />

<script type="text/javascript">
$(document).ready(function(){
	$('#container-1').tabs({ fxFade: true, fxSpeed: 'fast' });
});
</script>
<link rel="stylesheet" href="../_libs/jquery.tabs/style.css" type="text/css" />
<!--[if lte IE 7]>
<link rel="stylesheet" href="../_libs/jquery.tabs/style-ie.css" type="text/css" />
<![endif]-->
<style>
	table.t1{ font-size:14px; line-height:30px; background:#fff; border-collapse:collapse; border:1px solid #D8EBFF; width:90%;margin:10px auto;}
	table.t1 td,table.t1 th{ font-size:14px; line-height:30px;border:1px solid #D8EBFF;padding:5px;}
	table.t1 table,table.t1 table td{ font-size:14px; line-height:30px; border:0px solid #D8EBFF;}
</style>
</head>
<body>
<form id="form1" runat="server">
<div id="ajaxNotice">
<br />
	<div id="container-1">
		<ul>
			<li><a href="#fragment-1"><span>发布须知</span></a></li>
		</ul>
		<div id="fragment-1">
			<table class="t1">
				<tr>
					<td>随便写点什么吧。</td>
				</tr>
			</table>
		</div>
	</div>
</div>
</form>
</body>
</html>
