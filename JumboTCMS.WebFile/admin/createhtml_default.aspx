<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="createhtml_default.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._createhtml" %>
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
<script type="text/javascript" src="../_libs/jquery.tree2.js"></script>
<link   rel="stylesheet" type="text/css" href="../_libs/jquery.tree/style.css" />
<script type="text/javascript">
$(function() {
	$('#container-1').tabs({ fxFade: true, fxSpeed: 'fast' });
});
</script>
<link rel="stylesheet" href="../_libs/jquery.tabs/style.css" type="text/css">
<!--[if lte IE 7]>
<link rel="stylesheet" href="../_libs/jquery.tabs/style-ie.css" type="text/css">
<![endif]-->
<script type="text/javascript">
$(document).ready(function(){
	if("<%=_Go2Create %>" == "false")
	{
		$("#table2").hide();
		$("#button2").attr("disabled",true);
		$("#button3").attr("disabled",true);
	}
	getTreeJSON();
	//$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	//$("#txtId1").formValidator({tipid:"tipId1",onshow:"请填写数字",onfocus:"请填写数字"}).RegexValidator({regexp:"^\[0-9]+$",onerror:"请填写数字"});
	//$("#txtId2").formValidator({tipid:"tipId2",onshow:"请填写数字",onfocus:"请填写数字"}).RegexValidator({regexp:"^\[0-9]+$",onerror:"请填写数字"});
});
function getTreeJSON(){
	$.getJSON("class_ajax.aspx?oper=ajaxTreeJson&ccid=<%=ChannelId%>&time="+(new Date().getTime()), {}, function(treedata) {
		var o = {
			showcheck: true,
			cbiconpath: site.Dir + "_libs/jquery.tree/images/",
			oncount: function() {
				$('#ClassList').html('<ul></ul>');
				var _val = $("#tree").getTSVs();//value
				var _txt = $("#tree").getTSTs();//Text
				if (_val != null){
					var e1 = [];
					var e2 = [];
                			var l = _val.length;
                			for (var i = 0; i < l; i++) {
						if(_val[i]!="0"){e1[e1.length] = _val[i];
							e2[e2.length] = _txt[i];
            						$('#ClassList ul').append('<li><a href="javascript:void(0);" class="group_link" key="' + _val[i] + '"><span style="float:left;">' + e2.length + ': ' + _txt[i] + '</span><div class="icon_ready">!</div></a></li>');
						}
                			}
					$('#txtClassList').val(e1.join(","));
					$('#ClassCount').html('选择数量：' + e1.length);
				}
				else{
					$('#txtClassList').val('');
					$('#ClassList ul').html('');
					$('#ClassCount').html('选择数量：0');

				}
			},
                        url: "class_ajax.aspx?oper=ajaxTreeJson&&ccid=<%=ChannelId%>&time="+(new Date().getTime())
		};
		o.data = treedata;
		$("#tree").treeview(o);//使用
	}); 
}
</script>
<script type="text/javascript">
var ccid = joinValue('ccid');//频道ID
function ajaxCreateChannel()
{
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"time="+(new Date().getTime()),
		url:		"createhtml_ajax.aspx?oper=createchannel"+ccid,
		error:		function(XmlHttpRequest,textStatus, errorThrown) { alert(XmlHttpRequest.responseText);},
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				JumboTCMS.Alert(d.returnval, "0", "top.window.location='login.aspx';");
				break;
			case '0':
				JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				JumboTCMS.Message(d.returnval, "1");
				break;
			}
		}
	});
}
function CreateByClass(act){
	var Classs = $('#txtClassList').val();
	if(Classs.length<1)
		alert("请至少选择一个栏目");
	else
	{
		$('#ClassList').find('a').each(function() {
			var n = $(this).parent().html();
			n = ReplaceAll(n, 'icon_ignore', 'icon_ready');
			n = ReplaceAll(n, 'icon_success', 'icon_ready');
			n = ReplaceAll(n, 'icon_failure', 'icon_ready');
			$(this).parent().html(n);
		})
		ajaxCreateByClass(Classs,act);
	}
}
function ajaxCreateByClass(Classs,act){
	var isLast = true;//是否最后一次
	var thisclasss = Classs;
	var lastclasss = "";
	var classcount = Classs.replace(/[^,]/g,   "").length + 1;
	if(classcount==0) return;
	if ( classcount > 1 ){
		var e = [];
                var s = Classs.split(',');
                for (var i = 0; i < 1; i++)
			e[e.length] = s[i];
		thisclasss = e.join(",");
		lastclasss = Classs.substring(thisclasss.length+1);
		isLast = false;
	}
	if(act=="class")
		JumboTCMS.Loading.show("正在生成ID:"+thisclasss+"的栏目页,请等待...");
	else
		JumboTCMS.Loading.show("正在生成ID:"+thisclasss+"的内容页,请等待...");
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"classid="+thisclasss+"&time="+(new Date().getTime()),
		url:		"createhtml_ajax.aspx?oper=createbyclass&act="+act+ccid,
		error:		function(XmlHttpRequest,textStatus, errorThrown){JumboTCMS.Loading.hide();alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				JumboTCMS.Alert(d.returnval, "0", "top.window.location='login.aspx';");
				break;
			case '0':
				JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				JumboTCMS.Loading.hide();
				returnResult(thisclasss,d.returnval);
				if(!isLast) ajaxCreateByClass(lastclasss,act);
				break;
			}
		}
	});
}
var ReplaceAll = function(strOrg, strFind, strReplace) {
    var index = 0;
    while (strOrg.indexOf(strFind, index) != -1) {
        strOrg = strOrg.replace(strFind, strReplace);
        index = strOrg.indexOf(strFind, index);
    }
    return strOrg;
} 
//返回结果
var returnResult = function(keys,result) {
	$('#ClassList').find('a').each(function() {
		if ( (","+keys+",").indexOf(","+$(this).attr('key')+",") > -1 ){
			var n = $(this).parent().html();
			n = ReplaceAll(n, 'icon_ignore', 'icon_ready');
			n = ReplaceAll(n, 'icon_success', 'icon_ready');
			n = ReplaceAll(n, 'icon_failure', 'icon_ready');
			n = ReplaceAll(n, 'icon_ready', 'icon_'+result);
			$(this).parent().html(n);
		}
	})
}
function ajaxCreateById()
{
	JumboTCMS.Loading.show("正在更新,请等待...");
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"id1="+$("#txtId1").val()+"&id2="+$("#txtId2").val()+"&time="+(new Date().getTime()),
		url:		"createhtml_ajax.aspx?oper=createbyid"+ccid,
		error:		function(XmlHttpRequest,textStatus, errorThrown){JumboTCMS.Loading.hide();alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				JumboTCMS.Alert(d.returnval, "0", "top.window.location='login.aspx';");
				break;
			case '0':
				JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				JumboTCMS.Message(d.returnval, "1");
				break;
			}
		}
	});
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
  <br />
  <div id="container-1">
    <ul>
      <li><a href="#fragment-1"><span>更新静态页面</span></a></li>
    </ul>
    <div id="fragment-1">
      <table class="helptable">
        <tr>
          <td><ul>
              <li>选择某一栏目后，其父级栏目将自动被选择</li>
            </ul></td>
        </tr>
      </table>
      <table cellspacing="0" cellpadding="0" width="100%" class="formtable">
        <tr>
          <td width="400" valign="top"><div style="width:398px;"><a name="grouplist"></a>请在左侧选择要更新的栏目</div>
            <div style="border-bottom: #c3daf9 1px solid; border-left: #c3daf9 1px solid; width: 398px; height: 320px; overflow: auto; border-top: #c3daf9 1px solid; border-right: #c3daf9 1px solid;">
              <div id="tree"></div>
            </div>
            <textarea name="txtClassList" id="txtClassList" style="display:none;height:97px;width:97%;"></textarea>
          </td>
          <td valign="top"><div id="ClassCount">选择数量：0</div>
            <div id="ClassList" style="height: 320px; overflow: auto;">
              <ul>
              </ul>
            </div></td>
        </tr>
      </table>
      <div class="buttonok">
        <input id="button2" type="button" title="更新栏目页" value="更新栏目页" class="btnsubmit2" onclick="CreateByClass('class')" />
        <input id="button3" type="button" title="更新内容页" value="更新内容页" class="btnsubmit2" onclick="CreateByClass('content')" />
        <input id="button1" type="button" value="更新频道首页" class="btnsubmit2" onclick="ajaxCreateChannel()" />
      </div>
    </div>
  </div>
</form>
</body>
</html>
