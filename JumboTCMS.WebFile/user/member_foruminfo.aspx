<%@ Page Language="C#" AutoEventWireup="True" Codebehind="member_foruminfo.aspx.cs" Inherits="JumboTCMS.WebFile.User._member_foruminfo" %>
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
$(document).ready(function(){
	ajaxBindUserData('BindOtherData(_jcms_UserData)');//绑定会员数据
	$('#forumForm').ajaxForm({
		beforeSubmit: JumboTCMS.AjaxFormSubmit,
		success :function(data){
			JumboTCMS.Eval(data);
		}
	}); 
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtForumName")
		.formValidator({tipid:"tipForumName",onshow:"请输入论坛的登录名",onfocus:"请输入论坛的登录名"})
		.InputValidator({min:1,onerror:"请输入论坛的登录名"})
		.RegexValidator({regexp:"username",datatype:"enum",onerror:"汉字或字母开头,不支持符号"});
	$("#txtForumPass1").formValidator({tipid:"tipForumPass1",onshow:"请输入6-14位密码",onfocus:"请输入6-14位密码"}).InputValidator({min:6,max:14,onerror:"密码6-14位"});
	$("#txtForumPass2").formValidator({tipid:"tipForumPass2",onshow:"两次密码必须一致",onfocus:"两次密码必须一致"}).InputValidator({min:6,max:14,onerror:"密码6-14位,请确认"}).CompareValidator({desID:"txtForumPass1",operateor:"=",onerror:"两次密码不一致"});
});
function BindOtherData(data){
	$("#txtForumName").val(data.forumname);
}
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
    <script type="text/javascript">$('#bar-member-head').addClass('currently');$('#bar-member li.small').show();</script>
    <div id="mainarea">
      <div class="nav_two">
        <ul>
          <li class="currently">论坛信息</li>
        </ul>
        <div class="clear"></div>
      </div>
      <div>
        <form id="forumForm" name="form1" method="post" action="ajax.aspx?oper=ajaxChangeForumInfo">
          <table align="center" id="studio">
            <tr>
              <td width="120" height="30" align="right">绑定帐号：</td>
              <td width="410"><input type="text" class="inputss" style="width:180px;" name="txtForumName" id="txtForumName" />
                <span id="tipForumName" style="width: 200px"></span></td>
            </tr>
            <tr>
              <td align="right">帐号密码：</td>
              <td><input type="password" class="inputss" style="width:180px;" name="txtForumPass1" id="txtForumPass1" />
                <span id="tipForumPass1" style="width: 200px"></span></td>
            </tr>
            <tr>
              <td align="right">确认密码：</td>
              <td><input type="password" class="inputss" style="width:180px;" name="txtForumPass2" id="txtForumPass2" />
                <span id="tipForumPass2" style="width: 200px"></span></td>
            </tr>
            <tr>
              <td colspan="2" align="center" valign="bottom"><input type="submit" id="btnSave" value="确定修改" class="button" /></td>
            </tr>
          </table>
        </form>
        <div class="clear"></div>
      </div>
    </div>
  </div>
  <div class="clear"></div>
</div>
<!--#include file="include/footer.htm"-->
</body>
</html>
