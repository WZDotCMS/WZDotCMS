﻿<%@ Page Language="C#" AutoEventWireup="True" Codebehind="tougao_default.aspx.cs" Inherits="JumboTCMS.WebFile.User._tougao_default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>我的投稿 - <%=site.Name%></title>
<style type="text/css">
@import url(../_libs/myjsframe0.21/help.css);
</style>
<script src="../_libs/myjsframe0.21/myjsframe.js" type="text/javascript"></script>
<script type="text/javascript">
function closeTab(obj){		
	if(obj.target){obj=$(obj).previousElement()}	
	window.tabObject[$(obj).nextElement().id.substring(3)] = false;
	if($(obj).nextElement().hasClassName("cur")){
		if($(obj.parentNode).nextElement()){
			var a = $(obj.parentNode).nextElement().subTag("a")[1];
			if(a.fireEvent){
				a.fireEvent("onclick");
			}else{
				a.onclick();
			}
			$("content").src = a.href;		
		}else if($(obj.parentNode).previousElement()){
			var arr = $(obj.parentNode).previousElement().subTag("a");
			var a = arr.length==1 ? arr[0] : arr[1];
			a.fireEvent ? a.fireEvent("onclick") : a.onclick();
			$("content").src = a.href;
		}
	}
	$(obj.parentNode).remove();
	
	if($("#otherTab li").length > 0){	
		//把列表中最后一个tab还原到tabList中去
		$("tabList").insertBefore( $("#otherTab li").pop().remove(), $("#tabList li")[0]);
		if($("#otherTab li").length==0){
			$("otherTabHandle").removeClassName("tabArrowHigh");
			$("tabArrow").onmouseover = $("tabArrow").onmouseout = null;
		}
	}		
}
function selectThisTab(obj){
	if($(obj).parentIndex($("otherTab"))!=-1){	
		window.hideOtherTab();
		$("otherTabBox").appendChild($("#tabList li")[0]);
		$("tabList").appendChild(obj.parentNode);
		setTimeout(function(){obj.fireEvent ? obj.fireEvent("onclick") : obj.onclick();},5);
	}else{
		$(obj.parentNode.parentNode).subTag("a").each(function(t){t.removeClassName("cur")});
		$(obj).addClassName("cur");
	}
	if(obj.id.substring(0,3)=="tab")return;
	var index = $(obj.parentNode.parentNode).subTag("a").indexOf(obj);
	if(index==0){
		$("expandcollapse").style.visibility = "visible";
	}else{
		$("expandcollapse").style.visibility = "hidden";
	}
	$("expandcollapse").setAttribute("status",false);
	var btnImgExp = new Image();
	btnImgExp.src = "../_libs/myjsframe0.21/expand-all.gif";
	btnImgExp.width = "16";
	btnImgExp.height = "16";
	$("expandcollapse").removeChild($("expandcollapse").firstChild);
	$("expandcollapse").appendChild(btnImgExp);
	
	obj.blur();
}
function initTab(){
	var tabLen = Math.floor(($("tabList").clientWidth - 27)/134) - $("#tabList li").length;
	if(tabLen>0){ 
		$("otherTabHandle").removeClassName("tabArrowHigh");
		$("tabArrow").onmouseover = $("tabArrow").onmouseout = null;
		if(tabLen==0 || $("#otherTab li").length==0)return;
		for(var i=0; i<tabLen; i++){
			$("tabList").insertBefore($("#otherTab li").pop().remove(), $("#tabList li")[0]);
		}
	}else if(tabLen==0 && $("#otherTab li").length==0){
		$("otherTabHandle").removeClassName("tabArrowHigh");
		$("tabArrow").onmouseover = $("tabArrow").onmouseout = null;	
	}else{
		var j = -tabLen;
		for(var i=0; i<j; i++){
			$("otherTabBox").appendChild( $("#tabList li")[i] );
		}
		$("otherTabHandle").addClassName("tabArrowHigh");
		$("tabArrow").onmouseover = window.showOtherTab;
		$("tabArrow").onmouseout = window.hideOtherTab;
	}
}
function initHeight(){
	var h = document.documentElement.clientHeight;
	var h2 = h-57;
	$("lefterIframe").style.height = $("lefter").style.height = $("splitLineV").style.height = $("main").style.height = (h2-8) + "px";
	$("lefterIframe").style.height = (h2-6)+"px";
	$("content").style.height = (h2 - 36) + "px";
	initTab();
}
function beginDrag(me,evt){
	evt = evt?evt:window.event;		
	var d = document.createElement("div");
	d.style.position = "absolute";	
	d.style.width = "5px";
	d.style.background = "url(../_libs/myjsframe0.21/multidashed.gif) repeat";
	d.style.zIndex = 9999;
	d.style.cursor = "e-resize";
	var detlaX = (evt.clientX || evt.pageX)-me.offsetLeft;
	
	d.style.top = "63px";
	d.style.left = me.offsetLeft+"px";
	d.style.height = $("lefter").style.height;
	
	document.body.appendChild(d);
	
	if(document.all){
	    d.attachEvent("onmousemove",move);
		d.attachEvent("onmouseup",up);
		d.setCapture();
	}else{
		document.addEventListener("mousemove",move,true);
		document.addEventListener("mouseup",up,true);
		evt.stopPropagation();
		evt.preventDefault();
	}
			
	function move(evt){
	if(!document.all){evt.stopPropagation();}
		var k = (evt.clientX || evt.pageX)-detlaX;
		if(k<190 || k>400){return;}
		d.style.left = k + "px";
		detlaX=k=null;
		
	}
	
	function up(evt){
		d.style.width = "0px";
		d.style.height = "0px";
		var leftBarWidth = parseInt(d.style.left)-4;
		if(document.all){
			d.detachEvent("onmousemove",move);
			d.detachEvent("onmouseup",up);
			d.releaseCapture();
		}else{
			document.removeEventListener("mousemove",move,true);
			document.removeEventListener("mouseup",up,true);
			evt.stopPropagation();
		}	
		d.parentNode.removeChild(d);		
		d=null;
		if(leftBarWidth>-1){
			$("lefterIframe").width = $("lefter").style.width = (leftBarWidth+1) + "px";				
		}
		$("splitLineV").style.left = (leftBarWidth+4)+"px";		
		$("mainPaddingLeft").style.paddingLeft = (leftBarWidth+5)+"px";		
		initTab();
	}
}
function toggleHandle(obj){
	if(!obj.getAttribute("leftbar") || obj.getAttribute("leftbar")=="on"){
		obj.setAttribute("leftbar","off");
		obj.setAttribute("startLeft",parseInt($("splitLineV").style.left));
		$("lefterIframe").width = $("lefter").style.width = "0px";				
		$("splitLineV").style.left = "2px";		
		$("mainPaddingLeft").style.paddingLeft = "3px";
		$(obj).style.backgroundPositionX = "-21px";		
	}else{
		obj.setAttribute("leftbar","on");
		var startLeft = obj.getAttribute("startLeft");
		$("lefterIframe").width = $("lefter").style.width = (startLeft-3)+"px";
		$("splitLineV").style.left = startLeft+"px";		
		$("mainPaddingLeft").style.paddingLeft = (startLeft+1)+"px";	
		$(obj).style.backgroundPositionX = "-11px";			
	}
}
function expandcollapse(btn){
	btn.removeChild(btn.firstChild);
	var btnImg = new Image();
	if(arguments.callee.status=="expa"){
		window.frames["lefterIframe"].$("#box a.folder").each(function(a){a.removeClassName("open");})
		window.frames["lefterIframe"].$("#box ul ul").each(function(ul){ul.style.display="none";})		
		btnImg.src = "../_libs/myjsframe0.21/expand-all.gif";		
		arguments.callee.status = "coll";		
	}else{
		window.frames["lefterIframe"].$("#box a.folder").each(function(a){a.addClassName("open");})
		window.frames["lefterIframe"].$("#box ul").each(function(ul){ul.style.display="block";})
		btnImg.src = "../_libs/myjsframe0.21/collapse-all.gif";	
		arguments.callee.status = "expa";
	}
	btnImg.width = "16";
	btnImg.height = "16";	
    btn.appendChild(btnImg);		
}
window.tabObject = {0:"系统首页"};
window.showOtherTab = function(){
	$('otherTab').show();
	$('otherTabHandle').addClassName('tabArrowHigh2');
}
window.hideOtherTab = function(){
	$('otherTab').hide();
	$('otherTabHandle').removeClassName('tabArrowHigh2');
}
</script>
</head>
<body>
<div class="box" id="box" style="zoom:1">
	<div class="header">
		<h1>我的投稿</h1>
		<div class="version"><a href="index.aspx" target="_top">个人中心</a> | <a href="../" target="_blank">网站首页</a><div id="CurrentVesion" runat="server"></div></div>
	</div>
	<div class="nav">
		<input type="text" id="keyword" class="ipt keyword" value="搜索..." onfocus="if(this.value=='搜索...'){this.value='';}" onblur="if(this.value==''){this.value='搜索...'}" style="color:#878787">
	</div>
	<div class="mainBox">
		<div class="lefter" id="lefter">
			<ul class="indexTab tabList" id="indexTab">
				<li><a href="tougao_menulist.aspx?m=2" target="lefterIframe" onclick="selectThisTab(this)" class="cur"><em><span>频道</span></em></a></li>
			</ul>
            <button class="expandcollapse" id="expandcollapse" onclick="expandcollapse(this)"><img src="../_libs/myjsframe0.21/expand-all.gif" height="16" width="16" /></button>
			<div style="background:#deecfd; height:2px; overflow:hidden;border-top:1px solid #8db2e3; border-bottom:1px solid #8db2e3;"></div>
			<iframe src="about:blank" frameborder="0" id="lefterIframe" name="lefterIframe" height="100%" width="200px"></iframe>
		</div>
		<div class="mainPaddingLeft" id="mainPaddingLeft">
			<div class="splitLineV" id="splitLineV" onmousedown="beginDrag(this,event)" ondblclick="toggleHandle(this)"></div>
			<div class="main" id="main">
				<ul class="tabList" id="tabList">                	
					<li><a href='javascript:void(0)' class='closeTab' style="visibility:hidden"></a><a href="tougao_home.aspx" class="cur" target="content" ondblclick='return false' onclick="selectThisTab(this)" id="tab0"><em><span>首页</span></em></a></li>
				</ul>
                <div class="tabArrow" id="tabArrow">
                	<div id="otherTabHandle" class="otherTabHandle"></div>
                    <div class="otherTab" id="otherTab" onmouseover="$(this).show()" onmouseout="$(this).hide()" style="display:none">
                    	<ul id="otherTabBox"></ul>
                    </div>
                </div>
				<div style="background:#deecfd; height:2px; overflow:hidden;border-top:1px solid #8db2e3; border-bottom:1px solid #8db2e3;"></div>
				<iframe src="tougao_home.aspx" width="100%" height="100%" frameborder="0" name="content" id="content" onmousemove=""></iframe>
			</div>
		</div>
	</div>
</div>
<script type="text/javascript">
$("box").onresize = initHeight;
$.DOMReady(initHeight);
$("lefterIframe").src = "tougao_menulist.aspx?m=2";
</script>
</body>
</html>
