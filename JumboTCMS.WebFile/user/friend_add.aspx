<%@ Page Language="C#" AutoEventWireup="True" Codebehind="friend_add.aspx.cs" Inherits="JumboTCMS.WebFile.User._friend_add" %>
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
var adduserid = q("adduserid");
var addusername = decodeURIComponent(q("addusername"));
$(document).ready(function(){
	ajaxBindUserData();//绑定会员数据
	$("#txtUserId").val(adduserid);
	$("#txtUserName").val(addusername);
	$('#friendaddForm').ajaxForm({
		beforeSubmit: JumboTCMS.AjaxFormSubmit,
		success :function(data){
			JumboTCMS.Eval(data);
		}
	}); 
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtUserName")
		.formValidator({tipid:"tipUserName",onshow:"",onfocus:"请输入对方用户名"})
		.InputValidator({min:4,max:20,onerror:"用户名非法"})
		.RegexValidator({regexp:"username",datatype:"enum",onerror:"汉字或字母开头,不支持符号"})
		.AjaxValidator({
		type : "get",
		url:		"ajax.aspx?oper=ajaxCheckAddFriend&time="+(new Date().getTime()),
		datatype : "json",
		success : function(d){	
			if(d.result == "1")
			{
				$("#txtUserId").val(d.returnval);
				return true;
			}
			else
			{
				$("#txtUserId").val("0");
				return false;
			}
		},
		buttons: $("#btnSave"),
		error: function(){alert("服务器繁忙，请稍后再试");},
		onerror : "用户名不存在或为你本人",
		onwait : "正在校验用户名的合法性，请稍候..."
	});
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
</script>
</head>
<body>
<!--#include file="include/header.htm"-->
<div id="wrap">
	<div id="main">
		<!--#include file="include/left_menu.htm"-->
		<script type="text/javascript">$('#bar-friend-head').addClass('currently');$('#bar-friend li.small').show();</script>
		<div id="mainarea">
			<form id="friendaddForm" name="form1" method="post" action="ajax.aspx?oper=ajaxAddFriend2" class="ajaxshowdiv">
				<h2 class="title"><img src="../style/app/friend.gif">好友</h2>
				<div class="tabs_header">
					<ul class="tabs">
						<li class="active"><a><span>加好友</span></a></li>
						<li><a href="friend_list.aspx"><span>返回好友列表</span></a></li>
					</ul>
				</div>
				<div class="c_form">
					<table cellspacing="0" cellpadding="3">
						<tr>
							<th><label for="txtUserName">对方用户名：<input type="hidden" id="txtUserId" name="txtUserId" value="0" /></label></th>
							<td><input type="text" id="txtUserName" name="txtUserName" value="" style="width:120px;" class="t_input" tabindex="1" />
								<span id="tipUserName" style="width: 200px"></span>
							</td>
						</tr>
						<tr>
							<th>&nbsp;</th>
							<td><input type="submit" name="btn_submit" id="btn_submit" value="添加" class="submit" />
							</td>
						</tr>
			
					</table>
				</div>
			</form>
		</div>
	</div>
	<div class="clear"></div>
</div>
<!--#include file="include/footer.htm"-->
</body>
</html>

