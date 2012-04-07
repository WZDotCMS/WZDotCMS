<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="home.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._home" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>后台首页</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/jcmsV5.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/jcmsV5.css" />
<link type="text/css" rel="stylesheet" href="css/common.css" />
<script type="text/javascript" src="scripts/admin.js"></script>
<script type="text/javascript">
$(document).ready(function(){
	$('.tip-t').jtip({gravity: 't',fade: false});
	$('.tip-r').jtip({gravity: 'r',fade: false});
	$('.tip-b').jtip({gravity: 'b',fade: false});
	$('.tip-l').jtip({gravity: 'l',fade: false});
	ajaxIncludeList();
});
function ajaxIncludeList()
{
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"page=1&pagesize=100&pid=1&time="+(new Date().getTime()),
		url:		"templateinclude_ajax.aspx?oper=ajaxGetList",
		error:		function(XmlHttpRequest,textStatus, errorThrown){top.JumboTCMS.Loading.hide();alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				top.JumboTCMS.Alert(d.returnval, "0", "top.window.location='" + site.Dir + "admin/login.aspx';");
				break;
			case '0':
				top.JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				$("#ajaxIncludeList").setTemplateElement("tplIncludeList", null, {filter_data: true});
				$("#ajaxIncludeList").processTemplate(d);
				break;
			}
		}
	});
}
</script>
</head>
<body>
<div id="temporarydiv" style="display:none;"></div>
<div style="margin:10px;">
  <div>
    <table class="form_head">
      <tr>
        <td class="actions"><table cellpadding="0" cellspacing="0" border="0">
            <tr>
              <td class="active"><span>模块管理</span></td>
          </table></td>
      </tr>
    </table>
<textarea id="tplIncludeList" style="display:none">
<table cellspacing="0" cellpadding="0" width="100%" class="form_table">
    <tr>
	{#foreach $T.table as record}
        <th>{$T.record.no}.{$T.record.title}</th>
        <td><input type="button" value="模板编辑" class="btnsubmit" onclick="top.JumboTCMS.Popup.show('/admin/templateinclude_edittemplate.aspx?pid=1&source={$T.record.source}',-1,-1,true)" />
          <input type="button" value="更新" class="btnsubmit" onclick="ajaxTemplateIncludeUpdateFore('1','{$T.record.source}')" />
        </td>
	{#if $T.record.no%2 == 0}</tr><tr>{#/if}
	{#/for}
	{#if $T.recordcount%2 == 0}</tr><tr>{#/if}
        <th>批量更新上述模块</th>
        <td><input type="button" value="执行" class="btnsubmit" onclick="ajaxTemplateIncludeUpdateFore('1','')" />
        </td>
      </tr>
</table>
</textarea>
<div id="ajaxIncludeList"></div>
  </div>
  <br />
	<div>
	<table class="form_head">
		<tr>
			<td class="actions"><table cellpadding="0" cellspacing="0" border="0">
					<tr>
						<td class="active"><span>站内搜索</span></td>
				</table></td>
		</tr>
	</table>
	<table cellspacing="0" cellpadding="0" width="100%" class="form_table">
		<tr>
			<th>追加索引<input type="button" class="tip-r" tip="如果有新的内容，需要更新" /></th>
			<td><input type="button" value="更新" class="btnsubmit" onclick="ajaxCreateSearchIndex(0);" />
			</td>
			<th>全新索引<input type="button" class="tip-l" tip="如果对旧的内容进行了修改或删除，需要更新" /></th>
			<td><input type="button" value="更新" class="btnsubmit" onclick="JumboTCMS.Confirm('全新索引将删除所有旧的索引，过程可能比较慢，确定要执行吗?', 'ajaxCreateSearchIndex(1)');" />
			</td>
		</tr>
	</table>
	</div>
	<br />
	<div>
	<table class="form_head">
		<tr>
			<td class="actions"><table cellpadding="0" cellspacing="0" border="0">
					<tr>
						<td class="active"><span>其他</span></td>
				</table></td>
		</tr>
	</table>
	<table cellspacing="0" cellpadding="0" width="100%" class="form_table">
		<tr>
			<th> 网站首页面<input type="button" class="tip-r" tip="一般更新频道首页时网站首页自动会更新，此操作只需要在单独修改模板文件时执行" /></th>
			<td><input type="button" value="更新" class="btnsubmit" onclick="ajaxCreateIndexPage();" />	
			</td>
			<th>栏目数据统计<input type="button" class="tip-l" tip="只统计频道和一级栏目" /></th>
			<td><input type="button" value="更新" class="btnsubmit" onclick="ajaxCreateSystemCount();" />	
			</td>
		</tr>
	</table>
	</div>
</div>
</body>
</html>
