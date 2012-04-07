<%@ Page Language="C#" AutoEventWireup="true" Codebehind="tougao_menulist.aspx.cs" Inherits="JumboTCMS.WebFile.User._tougao_menulist" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head><meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<script src="../_libs/myjsframe0.21/myjsframe.js" type="text/javascript"></script>
<style type="text/css">
	* { padding:0; margin:0}
	html,body { overflow:hidden; height:100%; width:100%;}
	body { font-size:12px; font-family:Arial, Helvetica, sans-serif}
	.box { height:100%; overflow:auto; width:100%; 
		scrollbar-face-color: #f2f2f2;
		scrollbar-highlight-color: #ffffff;
		scrollbar-shadow-color: #cccccc;
		scrollbar-3dlight-color: #dddddd;
		scrollbar-arrow-color: #336000;
		scrollbar-track-color: #fff;
		scrollbar-darkshadow-color: #ffffff;	
	}	
	ul { list-style:none}
	ul ul { margin-left:1.4em; display:none}
	.navBox { padding:0.4em}
	a {color:#333; text-decoration:none;}
	a:hover { text-decoration:underline}
	li { margin:0.26em 0; zoom:1}
	.navBox a { background:url(../_libs/myjsframe0.21/helprecord.gif) no-repeat 0 0; padding:1px 2px 1px 18px; display:block; line-height:1.2em; zoom:1}
	.navBox a:visited {color:#666;}
	.navBox a.folder { background:url(../_libs/myjsframe0.21/pkg-closed.gif) no-repeat 0 0; padding-left:34px; }
	.navBox a.folder:visited {color:#333; font-style:normal}
	.navBox a.open { background:url(../_libs/myjsframe0.21/pkg-open.gif) no-repeat 0 0;}
</style>
<script type="text/javascript">
if($.Browse.isIE() && $.Browse.IEVer()==6){
    document.execCommand("BackgroundImageCache", false, true); // 强制缓存css背景图
}
function winResize(){
    $("box").style.height = (document.documentElement.clientHeight-30) + "px";
}
if(window.attachEvent){
	window.attachEvent("onresize",winResize);
}else{
	window.addEventListener("resize",winResize,false);
}
function toggleExpand(obj){
	var ul = $(obj).nextElement();
	if(!ul || ul.tagName!="UL"){return;}	
	ul.style.display = ul.getStyle("display")=="none" ? "block" : "none";
	$(obj).toggleClassName("open");
}
function makeTab(){
	var wp = window.parent;
	wp.$("#tabList a[target=content]").each(function(a){a.removeClassName("cur")});
	var tabTxt = this.innerHTML.stripTags();
	var tabHref = this.href;
	var tabID = this.id.substring(1);
	var tab = wp.tabObject[tabID];
	if(!tab){
		wp.tabObject[tabID] = true;
		//setTimeout(function(){			
			var tab = wp.document.createElement("li");			
			tab.innerHTML = "<a href='javascript:void(0)' class='closeTab' onclick='closeTab(this)'></a><a href='"+tabHref+"' id='tab"+tabID+"' target='content' class='cur' ondblclick='closeTab(this)' onclick='selectThisTab(this)' title='"+tabTxt+"'><em><span>"+tabTxt+"</span></em></a>";
			wp.$("tabList").appendChild(tab);
			var oneTabWidth =  132; //window.parent.$("#tabList li")[0].clientWidth;
			var TabBoxWidthCanBeUse = wp.$("tabList").clientWidth - (wp.$("#tabList li").length)*132-27;
			var MaxTab = Math.ceil(TabBoxWidthCanBeUse/oneTabWidth);
			if(MaxTab<1){
				wp.$("otherTabHandle").addClassName("tabArrowHigh");
				wp.$("tabArrow").onmouseover = wp.showOtherTab;
				wp.$("tabArrow").onmouseout = wp.hideOtherTab;
				wp.$("otherTabBox").appendChild( wp.$("#tabList li")[0].remove() );
			}
		//},5);
	}else{
		if(document.fireEvent){
			wp.$("tab"+tabID).fireEvent("onclick");
		}else{
			wp.$("tab"+tabID).onclick();
		}
	}
}

</script>
<title>
</title></head>
<body id="side" runat="server">
</body>
</html>
