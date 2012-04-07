<%@ Page Language="C#" AutoEventWireup="true" Codebehind="attachment_preview.aspx.cs" Inherits="JumboTCMS.WebFile.Admin.Attachment._preview" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>附件预览</title>
<meta   http-equiv="Content-Type" content="text/html;charset=utf-8" />
<style type="text/css">
BODY,
HTML {
	padding: 0px;
	margin: 0px;
}
BODY {
	font-family: Verdana, Arial, Helvetica, sans-serif;
	font-size: 12px;
	background: #EEE;
	padding: 0px;
}			
.example {
	float: left;
	width: 100%;
	margin: 0px;
}		
.demo {
	width: 100%;
	height: 400px;
	border:0;
	background: #FFF;
	overflow: scroll;
	padding: 0px;
}
</style>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/jcmsV5.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/jcmsV5.css" />
<link type="text/css" rel="stylesheet" href="css/common.css" />


<script type="text/javascript">
$(document).ready(function(){
	$('#fileTree').fileTree({ root: '<%=RootPath%>', script: 'attachment_tree.aspx', folderEvent: 'dblclick', expandSpeed: 1, collapseSpeed: 1 }, function(file) { 
		parent.AttachmentSelected(file,'<%=ElementID%>');
		parent.Popup.hide();
	});
});
</script>
</head>
<body>
    <form id="form1" runat="server">
		<div class="example">
			<div id="fileTree" class="demo"></div>
		</div>
    </form>
</body>
</html>
