<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="default.aspx.cs" Inherits="JumboTCMS.WebFile.Install._default" %>
<html>
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>欢迎安装JumboTCMS</title>
<style type="text/css" media="screen"> 
html, body, #swfDiv { height:100%; } 
body { margin:0; padding:0; overflow:hidden; }
</style>
<script type="text/javascript" src="../_libs/swfobject.js"></script>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/jcmsV5.js"></script>
<script type="text/javascript">
function InstallSuccess(){
	location.href = '../default.aspx';
}
</script>
</head>
<body scroll="no">
<div id="swfDiv"></div>
<script type="text/javascript">
var flashvars = {};
var params = {};
params.quality = "high";
params.bgcolor = "#869ca7";
params.allowScriptAccess = "sameDomain";
params.allowfullscreen = "true";
var attributes = {};
attributes.id="InstallFlexApp";
attributes.name="InstallFlexApp";
swfobject.embedSWF("install.swf", "swfDiv", "100%", "100%", "9.0.0", "../style/flex3/expressInstall.swf", flashvars, params, attributes); 
</script>
</body>
</html>
