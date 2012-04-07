<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="article_admin_list.aspx.cs" Inherits="JumboTCMS.WebFile.Modules._article_admin_list" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title></title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/jcmsV5.js"></script>
<link   type="text/css" rel="stylesheet" href="../_data/jcmsV5.css" />
<script type="text/javascript" src="../admin/scripts/common.js"></script>
<link   type="text/css" rel="stylesheet" href="../admin/css/common.css" />
<script type="text/javascript" src="../admin/scripts/content.js"></script>
</head>
<body>

<div class="topnav"> <span class="preload1"></span><span class="preload2"></span>
	<ul id="topnavbar">
		<li class="topmenu"><a href="javascript:void(0);" class="top_link"><span
            class="down">批量操作</span></a>
			<ul class="sub">
				<%if (ChannelIsHtml && IsPower(ChannelId + "-08")){ %><li><a href="javascript:void(0);" onclick="operater('createhtml')">静态生成</a></li><%} %>
				<%if (IsPower(ChannelId + "-04")){ %><li><a href="javascript:void(0);" onclick="operater('pass')">审核内容</a></li><%} %>
				<%if (IsPower(ChannelId + "-04")){ %><li><a href="javascript:void(0);" onclick="operater('nopass')">取消审核</a></li><%} %>
				<%if (IsPower(ChannelId + "-05")){ %><li><a href="javascript:void(0);" onclick="operater('top')">设为推荐</a></li><%} %>
				<%if (IsPower(ChannelId + "-05")){ %><li><a href="javascript:void(0);" onclick="operater('notop')">取消推荐</a></li><%} %>
				<%if (IsPower(ChannelId + "-05")){ %><li><a href="javascript:void(0);" onclick="operater('focus')">设为焦点</a></li><%} %>
				<%if (IsPower(ChannelId + "-05")){ %><li><a href="javascript:void(0);" onclick="operater('nofocus')">取消焦点</a></li><%} %>
				<%if (IsPower(ChannelId + "-02")){ %><li><a href="javascript:void(0);" onclick="operater('sdel')">放入回收站</a></li><%} %>
				<%if (AdminIsFounder){ %><li><a href="javascript:void(0);" onclick="operater('del')">直接删除</a></li><%} %>
			</ul>
		</li>
		<%if (IsPower(ChannelId + "-06") && (ChannelClassDepth > 0)){ %><li class="topmenu"><a href="javascript:void(0);" onclick="move2class();" class="top_link"><span>移动栏目</span></a></li><%} %>
		<li class="topmenu"><a href="javascript:void(0);" onclick="move2special()" class="top_link"><span>加入专题</span></a></li>
		<li class="topmenu"><a href="javascript:void(0);" onclick="ajaxSearch();" class="top_link"><span>过滤检索</span></a></li>
		<%if (IsPower(ChannelId + "-07") && (ChannelClassDepth > 0)){ %><li class="topmenu"><a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('../admin/class_list.aspx?id=0'+ccid,-1,-1,true)" class="top_link"><span>栏目管理</span></a></li><%} %>
		<%if (IsPower(ChannelId + "-01")){ %><li class="topmenu"><a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('../modules/'+mtype+'_admin_edit.aspx?id=0'+ccid,-1,-1,true)" class="top_link"><span>添加<%=ChannelItemName%></span></a></li><%} %>
		<%if (ChannelIsHtml && IsPower(ChannelId + "-08")){ %><li class="topmenu" id="li_createhtml"><a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('../admin/createhtml_default.aspx?oper=null'+ccid,-1,-1,true)" class="top_link"><span>静态生成</span></a></li><%} %>
	</ul>
</div>
<script type="text/javascript">topnavbarStuHover();</script>
<!--#include file="include/quickbar.aspx" -->
<textarea id="tplList" style="display:none">
<table class="cooltable">
<thead>
	<tr>
		<th align="center" scope="col" style="width:30px;"><input onclick="checkAllLine()" id="checkedAll" name="checkedAll" type="checkbox" title="全部选择/全部不选" /></th>
		<th scope="col" style="width:60px;">ID</th>
		<th scope="col" width="*">标题</th>
		<th scope="col" style="width:120px;">所属栏目</th>
		<th scope="col" style="width:150px;">前台发布时间</th>
		<th scope="col" style="width:100px;">状态</th>
		<th scope="col" style="width:120px;">操作</th>
	</tr>
	</thead>
<tbody>
	{#foreach $T.table as record}
	<tr>
		<td align="center"><input class="checkbox" name="selectID" type="checkbox" value='{$T.record.id}' />
		</td>
		<td align="center">{$T.record.id}</td>
		<td align="left">&nbsp;
			{#if $T.record.firstpage != ""}
			<a href="{$T.record.firstpage}" target="_blank" style="color:{$T.record.tcolor}">{$T.record.title}</a>
			{#else}
			<a href="../controls/article.aspx?ChannelId={$T.record.channelid}&id={$T.record.id}&preview=1" target="_blank" style="color:{$T.record.tcolor}">{$T.record.title}</a>
			{#/if}
		</td>
		<td align="center">{$T.record.classname}</td>
		<td align="center">{formatDate($T.record.adddate,'yyyy-MM-dd HH:mm:ss')}</td>
		<td align="center" class="oper">{formatContentOper($T.record.ispass,$T.record.id,'pass')}{formatContentOper($T.record.isimg,$T.record.id,'img')}{formatContentOper($T.record.istop,$T.record.id,'top')}{formatContentOper($T.record.isfocus,$T.record.id,'focus')}
		</td>
		<td align="center" class="oper">
			<%if (IsPower(ChannelId + "-02")) {%>
			<a href="javascript:void(0);" onclick="top.JumboTCMS.Popup.show('../modules/<%=EditFile %>?id={$T.record.id}'+ ccid,-1,-1,true)">修改</a>
			<%}else {%>
			<span style="color:#eeeeee;">修改</span>
			<%}%>
			<%if (IsPower(ChannelId + "-03")) {%>
			<a href="javascript:void(0);" onclick="ConfirmDel({$T.record.id})">删除</a>
			<%}else {%>
			<span style="color:#eeeeee;">删除</span>
			<%}%>
			<a href="javascript:void(0);" onclick="ConfirmCopy({$T.record.id})">克隆</a>
		</td>
	</tr>
	{#/for}
</tbody>
</table></textarea>
<div id="ajaxList"> </div>
<div id="ajaxPageBar" class="pages"> </div>
</body>
</html>
