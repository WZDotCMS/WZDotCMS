﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="JumboTCMS.WebFile.Search._index" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" lang="zh-CN">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>站内搜索 -<%= site.Name%></title>
<script type="text/javascript" src="<%= site.Dir%>_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="<%= site.Dir%>_data/jcmsV5.js"></script>
<link rel="stylesheet" type="text/css" href="style/style.css"/>
<script type="text/javascript">
$(document).ready(function() {
	ajaxSearchList(<%= PageSize%>,1);
});
function ajaxSearchList(pagesize,page)
{
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"time="+(new Date().getTime()),
		url:		"ajax.aspx?oper=ajaxGetContentList&type=<%= ChannelType%>&mode=<%= Mode%>&ch=<%= ChannelId%>&k="+encodeURIComponent('<%= Keywords%>')+"&pagesize="+pagesize+"&page="+page,
		error:		function(XmlHttpRequest,textStatus, errorThrown){if(XmlHttpRequest.responseText!=""){alert(XmlHttpRequest.responseText);}},
		success:	function(d){
			if(d.recordcount != -1)
			{
				$("#ajaxTotalCount").text(d.recordcount);
				$("#ajaxEventTime").text(d.eventtime);
				$("#ajaxSearchList").setTemplateElement("tplList", null, {filter_data: false});
				$("#ajaxSearchList").processTemplate(d);
				$("#ajaxPageBar").html(d.pagerbar);
			}
		}
	});
}
</script>
</head>
<body>
<div id="top"><div id="toplogo"><a href="<%= site.Url%><%= site.Dir%>"><img id="logo" alt="<%= site.Name%>" src="style/logo.gif" border="0"></a></div>
  <div id="ss">
    <form action="default.aspx" name="formsearch" method="get">
      <div id="pd"> <span id="ajaxChannelType"></span>
        <script type="text/javascript">BindModuleRadio("ajaxChannelType",q("type"));</script>
      </div>
      <div id="search">
        <input name="k" type="text" maxlength="128" size="128" id="tbKeyWord" class="input" value="<%= Keywords%>" />
        <span id="rfvKeyWord" style="color:Red;display:none;">请输入您要检索的关键字</span>
        <input type="image" class="float-left" src="style/btn_search.gif" alt="" border="0" />
&nbsp; <span id="ajaxMode"></span>
        <script type="text/javascript">BindModeRadio("ajaxMode",q("mode"));</script>
      </div>
      <input type="hidden" name="ch" value="<%= ChannelId%>" />
    </form>
    <!--分词:<%=SplitWords%>-->
  </div>
</div>
<div id="adr"><span class="float-right right10"> 找到相关内容<span id="ajaxTotalCount">0</span>篇，用时<span id="ajaxEventTime">0</span>毫秒</span>&nbsp;&nbsp;&nbsp;&nbsp;<a href="<%= site.Url%><%= site.Dir%>">首页</a>&gt; 搜索 &gt;“<span class="cRed"><%= Keywords%></span>”的搜索结果</div>
<textarea id="tplList" style="display:none">
{#foreach $T.table as record}
<div class="jg">
	<h1><a target='_blank' href='{$T.record.url}'>{$T.record.title}</a></h1>
	{$T.record.summary}
	<br /><span class="cQING"> {$T.record.adddate}  </span> </div>
{#/for}
</textarea>
<div id="ajaxSearchList"></div>
<div id="baidu-union">
  <script type="text/javascript">/*百度联盟300*250*/ var cpro_id = 'u793490';</script>
  <script src="http://cpro.baidu.com/cpro/ui/c.js" type="text/javascript"></script>
</div>
<div class="clear"></div>
<div id="ajaxPageBar" class="pages"></div>
<P></P>
<div id="bottom"> <span id="bq"><%= site.Name%>&nbsp;&nbsp;版权所有</span></div>
</body>
</html>
