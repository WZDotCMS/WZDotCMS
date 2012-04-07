<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="authorize_result.aspx.cs" Inherits="JumboTCMS.WebFile.About._authorize_result" %>
<!DOCTYPE html PUBliC "-//W3C//DTD html 4.01 Transitional//EN" "http://www.w3c.org/TR/1999/REC-html401-19991224/loose.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>授权查询 - JumboTCMS - 将博内容管理系统通用版</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<link type="text/css" rel="stylesheet" href="css/style.css" />
<style type="text/css">
.table{
margin-bottom:20px;
border:1px solid #ccc;border-collapse:collapse;
}
#licenseArea{
	margin:auto;
	width:632px;height:396px;
	background:url(images/license_bg<%= AccreditType%>.png) no-repeat scroll 0 0 #FFFFFF;
	position:relative;
}
#licenseInfo{
	position:absolute;
	top:120px;
	left:150px;
	font-size:13px;
	color:#111;
}
#licenseInfo p{margin:8px 0;font-size:12px;}
#licenseInfo span{
	display:inline-block;
	width:100px;
	padding-right:10px;
	text-align:right;
}
</style>
</head>
<body id="ad">
<div class="top">
  <div class="top-logo">
    <div class="top-nav"> <a href="http://www.jumbotcms.net">Back JumboTCMS</a> </div>
  </div>
</div>
<div id="mainbg">
  <div id="right">
    <div id="main" class="text">
      <div id="licenseArea">
        <div id="licenseInfo">
          <p><span>授权域名：</span><%= Domain %>（<%= WebName %>）</p>
          <p><span>授权类型：</span><%=AccreditTypeName %></p>
          <p><span>商业环境使用：</span><%= UseInBusiness%></p>
          <p><span>去除前台版权：</span><%= DeleteCopyright%></p>
          <p><span>服务有效期限：</span><%= Validity%></p>
          <p><span>授权起始时间：</span><%= AddTime%></p>
        </div>
      </div>
    </div>
    <div id="footer"> (C) Copyright, JumboTCMS.Net. All rights Reserved.<br>
      <a href="http://www.miibeian.gov.cn" target="_blank">京ICP备10038310号-1</a></div>
  </div>
  <div id="left">
    <div class="left-menu">
      <ul>
        <li><a href="http://www.jumbotcms.net/about/index.html">关于将博</a></li>
        <li><a href="http://www.jumbotcms.net/about/service.html">商业服务</a></li>
        <li class="currently"><a>授权中心</a></li>
        <li><a href="http://www.jumbotcms.net/about/contactus.html">联系我们</a></li>
        <li><a href="http://www.jumbotcms.net/about/copyright.html">版权申明</a></li>
        <li><a href="http://www.jumbotcms.net/about/support.html">赞助我们</a></li>
      </ul>
    </div>
  </div>
</div>
</body>
</html>
