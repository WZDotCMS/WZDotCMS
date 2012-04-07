<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="urlrewriter_default.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._urlrewriter_index" %>
<%@ Register assembly="JumboTCMS.WebControls" namespace="JumboTCMS.WebControls" tagprefix="Jumbot" %>
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

<script type="text/javascript">
(function(){
	$(document).ready(function(){
		$('#urlrewriterForm').ajaxForm({
			beforeSubmit: JumboTCMS.AjaxFormSubmit,
			success :function(data){
				JumboTCMS.Eval(data);
			}
		}); 
	});
	//Form验证
	JumboTCMS.AjaxFormSubmit=function(item){
		JumboTCMS.Loading.show("正在处理，请稍等...");
		return true;
	};
})();
    </script>
</head>
<body>
<table class="helptable">
	<tr>
		<td><ul>
				<li>如果不是很熟悉正则，请勿随便改动</li>
			</ul></td>
	</tr>
</table>
<form id="urlrewriterForm" name="form1" method="post" action="urlrewriter_ajax.aspx?oper=ajaxSaveRules">
	<table class="formtable">
		<tr>
			<th> 伪静态规则 </th>
			<td><textarea name="txtRulesContent" id="txtRulesContent" style="height:280px;width:97%;"><%=_rules%></textarea></td>
		</tr>
	</table>
	<div class="buttonok">
		<input type="submit" value="修改" id="btnSaveContent" class="btnsubmit" />
	</div>
</form>
</body>
</html>
