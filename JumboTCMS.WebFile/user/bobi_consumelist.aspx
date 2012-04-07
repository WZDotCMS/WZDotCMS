<%@ Page Language="C#" AutoEventWireup="True" Codebehind="bobi_consumelist.aspx.cs" Inherits="JumboTCMS.WebFile.User._bobi_consumelist" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>个人中心 - <%=site.Name%></title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/jcmsV5.js"></script>
<link   type="text/css" rel="stylesheet" href="../_data/jcmsV5.css" />
<script type="text/javascript" src="js/fore.common.js"></script>
<link   type="text/css" rel="stylesheet" href="../style/member/style.css" />
<script type="text/javascript">
var sdate = joinValue('sdate');
var pagesize=10;
var page=thispage();
$(document).ready(function(){
	ajaxBindUserData();//绑定会员数据
	aLoad('');
});
function aLoad(s) {
	page = "1";
	if(s == "1w")
		sdate = "&sdate=1w";
	else if(s == "1m")
		sdate = "&sdate=1m";
	else if(s == "1y")
		sdate = "&sdate=1y";
	else
		sdate = "";
	ajaxList(page);
}
function ajaxList(currentpage)
{
	//设置选项卡
	for(i=1; i<5; i++)
	{
		$i("tab"+i).className = "";
        
	}
	if(sdate == "&sdate=1w")
		$i("tab1").className = "currently";
	else if(sdate == "&sdate=1m")
		$i("tab2").className = "currently";
	else if(sdate == "&sdate=1y")
		$i("tab3").className = "currently";
	else
		$i("tab4").className = "currently";
	if(currentpage!=null) page=currentpage;
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"page="+currentpage+"&pagesize="+pagesize+"&time="+(new Date().getTime()),
		url:		"ajax.aspx?oper=ajaxGetConsumeList"+sdate,
		error:		function(XmlHttpRequest,textStatus, errorThrown){if(XmlHttpRequest.responseText!=""){alert(XmlHttpRequest.responseText);}},
		success:	function(d){
			switch (d.result)
			{
			case '0':
				JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				$("#ajaxList").setTemplateElement("tplList", null, {filter_data: false});
				$("#ajaxList").processTemplate(d);
				$("#ajaxPageBar").html(d.pagerbar);
				break;
			}
		}
	});
}
</script>
</head>
<body>
<!--#include file="include/header.htm"-->
<div id="wrap">
  <div id="main">
    <!--#include file="include/left_menu.htm"-->
    <script type="text/javascript">$('#bar-bobi-head').addClass('currently');$('#bar-bobi li.small').show();</script>
    <div id="mainarea">
      <!--二级菜单-->
      <div class="nav_two">
        <ul>
          <li class="currently"><a>支出明细</a></li>
        </ul>
        <div class="clear"></div>
      </div>
      <!--三级菜单-->
      <div id="nav_box">
        <ul class="nav_three">
          <li id="tab4"><a href="javascript:void(0);" onclick="aLoad('')">全部</a></li>
          <li id="tab3"><a href="javascript:void(0);" onclick="aLoad('1y')">今年</a></li>
          <li id="tab2"><a href="javascript:void(0);" onclick="aLoad('1m')">当月</a></li>
          <li id="tab1" style="display:none"><a href="javascript:void(0);" onclick="aLoad('1w')">本周</a></li>
        </ul>
        <div class="clear"></div>
      </div>
      <!--右侧列表-->
      <div id="list">
        <textarea id="tplList" style="display:none"><table width="100%" cellpadding="0" cellspacing="0">
	<tr>
		<th width="*">详情</th>
		<th scope="col" style="width:120px;">消费时间</th>
	</tr>
	{#foreach $T.table as record}
	<tr>
		<td align="left">&nbsp;{$T.record.operinfo}</td>
		<td align="center">{formatDate($T.record.opertime,"yyyy-MM-dd hh:mm")}</td>
	</tr>
	{#/for}
</table></textarea>
        <div id="ajaxList"></div>
        <div class="clear"></div>
      </div>
      <div id="ajaxPageBar" class="pages"> </div>
      <div class="clear"></div>
    </div>
  </div>
  <div class="clear"></div>
</div>
<!--#include file="include/footer.htm"-->
</body>
</html>
