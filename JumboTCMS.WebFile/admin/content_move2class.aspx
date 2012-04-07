<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="content_move2class.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._content_move2class" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>请慎重选择目标栏目</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/jcmsV5.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/jcmsV5.css" />
<link type="text/css" rel="stylesheet" href="css/common.css" />
<script type="text/javascript" src="../_libs/jquery.tree2.js"></script>
<link   rel="stylesheet" type="text/css" href="../_libs/jquery.tree/style.css" />
<script type="text/javascript">
$(document).ready(function(){
	getTreeJSON();
});
function getTreeJSON(){
	$.getJSON("class_ajax.aspx?oper=ajaxTreeJson&ccid=<%=ChannelId%>&time="+(new Date().getTime()), {}, function(treedata) {
		var o = {
			showcheck:false,
			cbiconpath: site.Dir + "_libs/jquery.tree/images/",
			onnodeclick: function() {
			    var d = $("#tree").getTCT().value;
				$('#txtClass').val(d);
			},
			url: "class_ajax.aspx?oper=ajaxTreeJson&&ccid=<%=ChannelId%>&time="+(new Date().getTime())
		};
		o.data = treedata;
		$("#tree").treeview(o);//使用
	}); 
}
function Move2Class(){
    var _classid = $('#txtClass').val();
    if(_classid=="0"){
        alert('请选择一个目标栏目');
        return;
    }
    top.IframeOper.ajaxMove2Class(_classid);
}

</script>
<style>
#ClassList ul { list-style:none; padding:0px; margin:0px; }
#ClassList ul li { list-style:none; padding:0px; margin:0px; }
a.group_link{
	display:block;
	height:22px;
	line-height:22px;
}
.icon_ready{
	float:left;
	width:24px;
	height:22px;
	overflow:hidden;
	text-indent:-999em;	
	background:url(images/ready.gif) no-repeat;
}
.icon_loading{
	float:left;
	width:24px;
	height:22px;
	overflow:hidden;
	text-indent:-999em;	
	background:url(images/loading.gif) no-repeat;
}
.icon_ignore{
	float:left;
	width:24px;
	height:22px;
	overflow:hidden;
	text-indent:-999em;	
	background:url(images/ignore.gif) no-repeat;
}
.icon_failure{
	float:left;
	width:24px;
	height:22px;
	overflow:hidden;
	text-indent:-999em;	
	background:url(images/failure.gif) no-repeat;
}
.icon_success{
	float:left;
	width:24px;
	height:22px;
	overflow:hidden;
	text-indent:-999em;	
	background:url(images/success.gif) no-repeat;
}
</style>
</head>
<body>
<form id="form1" runat="server">
        <div style="border-bottom: #c3daf9 1px solid; border-left: #c3daf9 1px solid; width: 100%; height: 280px; overflow: auto; border-top: #c3daf9 1px solid; border-right: #c3daf9 1px solid;">
          <div id="tree"></div>
        </div>
        <input name="txtClass" id="txtClass" type="hidden" value="0" />
  <div class="buttonok">
    <input id="button1" type="button" value="转移" class="btnsubmit" onclick="Move2Class()" />
    <input id="btnReset" type="button" value="取消" class="btncancel" onclick="parent.JumboTCMS.Popup.hide();" />
  </div>
</form>
<script type="text/javascript">_jcms_SetDialogTitle();</script>
</body>
</html>
