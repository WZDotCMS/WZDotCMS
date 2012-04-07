<%@ Page Language="C#" AutoEventWireup="True" Codebehind="message_read.aspx.cs" Inherits="JumboTCMS.WebFile.User._message_read" %>
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
var id = joinValue('id');
var mode = joinValue('mode');
var pagesize=5;
var page=thispage();
$(document).ready(function(){
	ajaxBindUserData();//绑定会员数据
	if(q("mode")=="")
		aLoad('inbox');
	else
		aLoad(q("mode"));
	$('#pmsendForm').ajaxForm({
		beforeSubmit: JumboTCMS.AjaxFormSubmit,
		success :function(data){
			JumboTCMS.Eval(data);
		}
	}); 
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtContent")
		.formValidator({tipid:"tipContent",onshow:"必填项!",onfocus:"必须填写"})
		.InputValidator({min:10,max:200,onerror: "字符数10-200!"});
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
}
function aLoad(s) {
	page = "1";
	if(s == "new")
		mode = "&mode=new";
	else if(s == "inbox")
		mode = "&mode=inbox";
	else if(s == "outbox")
		mode = "&mode=outbox";
	else
		mode = "";
	ajaxList(page);
}
function ajaxList(currentpage)
{
	//设置选项卡
	for(i=1; i<4; i++)
	{
		$i("tab"+i).className = "";
        
	}
	if(mode == "&mode=new")
		$i("tab1").className = "current";
	else if(mode == "&mode=inbox")
		$i("tab2").className = "current";
	else if(mode == "&mode=outbox")
		$i("tab3").className = "current";
	if(currentpage!=null) page=currentpage;
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"page="+currentpage+"&pagesize="+pagesize+"&time="+(new Date().getTime())+id,
		url:		"ajax.aspx?oper=ajaxGetMessageList"+mode,
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
				$("#txtUserId").val(d.table[0].senduserid);
				$("#txtUserName").val(d.table[0].sendusername);
				$("#txtTitle").val("re:"+d.table[0].title);
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
		<script type="text/javascript">$('#bar-message-head').addClass('currently');$('#bar-message li.small').show();</script>
<div id="mainarea">
			<h2 class="title"><img src="../style/app/pm.gif">消息</h2>
			<div class="tabs_header">
				<ul class="tabs">
					<li class="active"><a href="message_list.aspx"><span>短消息</span></a></li>
					<li><a href="notice_list.aspx"><span>通知</span></a></li>
					<li class="null"><a href="message_send.aspx">发短消息</a></li>
				</ul>
			</div>
			<div id="content" style="width:640px;">
            <textarea id="tplList" style="display:none"><li>
	<div class="avatar48"> <a><img src="../_data/avatar/{$T.table[0].senduserid}_m.jpg" onerror="this.src='../_data/avatar/0_m.jpg'" width="48" height="48" /></a> </div>
	<div class="pm_body">
		<div class="pm_h">
			<div class="pm_f">
				<p><a>{$T.table[0].sendusername}</a> <span class="time">{$T.table[0].adddate}</span></p>
				<div class="pm_c"> {$T.table[0].title}<br>
					{$T.table[0].content}
				</div>
				<a href="javascript:void(0);" class="float_del" onclick="ajaxDelMessage({$T.table[0].id})">删除</a>
			</div>
		</div>
	</div>
</li>
</textarea>
				<ol class="pm_list" id="ajaxList"></ol>
			<form id="pmsendForm" name="form1" method="post" action="ajax.aspx?oper=ajaxReplyMessage" class="ajaxshowdiv">
				<div class="c_form">
					<table cellspacing="0" cellpadding="3">
						<tr style="display:none;">
							<th><input type="hidden" id="txtUserId" name="txtUserId" value="0" /></th>
							<td><input type="text" id="txtUserName" name="txtUserName" value="" style="width:396px;" class="t_input" tabindex="1" />
							<input type="hidden" id="txtTitle" name="txtTitle" value="" />
							</td>
						</tr>
						<tr>
							<th style="vertical-align: top;"><label for="message">回复内容：</label></th>
							<td><textarea id="txtContent" name="txtContent" cols="40" rows="4" style="width:400px; height: 150px;"></textarea>
								<br /><span id="tipContent" style="width: 200px"></span>
							</td>
						</tr>
						<tr>
							<th>&nbsp;</th>
							<td><input type="submit" name="btn_submit" id="btn_submit" value="回复" class="submit" />
							</td>
						</tr>
			
					</table>
				</div>
			</form>
			</div>
			<div id="sidebar" style="width:150px;">
				<div class="cat">
					<h3>文件夹</h3>
					<ul class="post_list line_list">
						<li id="tab1"><a href="message_list.aspx?mode=new">未读消息</a></li>
						<li id="tab2"><a href="message_list.aspx?mode=inbox">收件箱</a></li>
						<li id="tab3" style="display:none;"><a href="javascript:void(0);" onclick="aLoad('outbox')">发件箱</a></li>
					</ul>
				</div>
			</div>
		</div>
	</div>
	<div class="clear"></div>
</div>
<!--#include file="include/footer.htm"-->
</body>
</html>
