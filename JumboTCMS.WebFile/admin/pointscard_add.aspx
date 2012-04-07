<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pointscard_add.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._pointscard_add" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>批量添加充值卡</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/jcmsV5.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/jcmsV5.css" />
<link type="text/css" rel="stylesheet" href="css/common.css" />
<script type="text/javascript" src="../_libs/my97datepicker4.6/WdatePicker.js"></script>
<script type="text/javascript">
(function(){
	$(document).ready(function(){
		$.ajax({
			type:		"get",
			dataType:	"json",
			data:		"time="+(new Date().getTime()),
			url:		"pointscard_ajax.aspx?oper=ajaxSortList",
			success:	function(data){
				if(data.result == "1"){
					$("#ddlPoints").empty();
					for (i=0;i<data.table.length;i++) {
						$("#ddlPoints").addOption(data.table[i].title,data.table[i].points);
					}   
				}
			}
		});
		$('#form1').ajaxForm({
			beforeSubmit: JumboTCMS.AjaxFormSubmit,
			success :function(data){
				JumboTCMS.Eval(data);
			}
		}); 
		$.formValidator.initConfig({onError:function(msg){alert(msg);}});
		$("#txtCardCount")
			.formValidator({tipid:"tipCardCount",onshow:"请输入50以内的整数",onfocus:"请输入50以内的整数"})
			.InputValidator({min:1,max:50,type:"value",onerror:"请输入50以内的整数"});
		$("#txtCardNumberLen")
			.formValidator({tipid:"tipCardNumberLen",onshow:"请输入10-16的整数",onfocus:"请输入10-16的整数"})
			.InputValidator({min:10,max:16,type:"value",onerror:"请输入10-16的整数"});
		$("#txtCardPasswordLen")
			.formValidator({tipid:"tipCardPasswordLen",onshow:"请输入6-12的整数",onfocus:"请输入6-12的整数"})
			.InputValidator({min:6,max:12,type:"value",onerror:"请输入6-12的整数"});
	});
	//Form验证
	JumboTCMS.AjaxFormSubmit=function(item){
		try{
			if($.formValidator.PageIsValid('1'))
			{
				JumboTCMS.Loading.show("正在处理，请稍等...");
				return true;
			}else{
				return false;
			}
		}catch(e){
			return false;
		}
	};
})();
    </script>
</head>
<body>
<form id="form1" name="form1" method="post" action="pointscard_ajax.aspx?oper=ajaxBatchAdd">
	<table class="formtable">
		<tr>
			<th> 批量生成充值卡数量 </th>
			<td><input name="txtCardCount" type="text" maxlength="40" id="txtCardCount" style="width:60px;" />
				<span id="tipCardCount" style="width:200px;"> </span></td>
		</tr>
		<tr>
			<th> 充值卡帐号位数 </th>
			<td><input name="txtCardNumberLen" type="text" maxlength="40" id="txtCardNumberLen" style="width:60px;" />
				<span id="tipCardNumberLen" style="width:200px;"> </span></td>
		</tr>
		<tr>
			<th> 充值卡密码位数 </th>
			<td><input name="txtCardPasswordLen" type="text" maxlength="40" id="txtCardPasswordLen" style="width:60px;" />
				<span id="tipCardPasswordLen" style="width:200px;"> </span></td>
		</tr>
		<tr>
			<th> 充值卡含博币 </th>
			<td><select name="ddlPoints" id="ddlPoints"></select></td>
		</tr>
		<tr style="display:none;">
			<th> 到期日期 </th>
			<td><input name="txtCardLimitedDate" type="text" style="width:80px;" value="<%=defaultLimitedDate%>" maxlength="20" id="txtCardLimitedDate" class="Wdate" onfocus="WdatePicker({isShowClear:false,readOnly:true,skin:'blue'})" readonly="readonly" />
				<span id="tipCardLimitedDate" style="width:200px;"> </span></td>
		</tr>
	</table>
	<div class="buttonok">
		<input type="submit" name="btnSave" value="确认" id="btnSave" class="btnsubmit" />
		<input id="btnReset" type="button" value="取消" class="btncancel" onclick="parent.JumboTCMS.Popup.hide();" />
	</div>
</form>
<script type="text/javascript">_jcms_SetDialogTitle();</script>
</body>
</html>
