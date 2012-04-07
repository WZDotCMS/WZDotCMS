<%@ Page Language="C#" AutoEventWireup="True" Codebehind="bobi_card2points.aspx.cs" Inherits="JumboTCMS.WebFile.User._bobi_card2points" %>
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
	ajaxBindUserData();//绑定会员数据
	$('#activepointscardForm').ajaxForm({
		beforeSubmit: JumboTCMS.AjaxFormSubmit,
		success :function(data){
			JumboTCMS.Eval(data);
		}
	}); 
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtCardNumber")
		.formValidator({tipid:"tipCardNumber",onshow:"请输入10-16个字符",onfocus:"请输入10-16个字符"})
		.InputValidator({min:10,max:16,onerror:"请输入10-16个字符"});
	$("#txtCardPassword")
		.formValidator({tipid:"tipCardPassword",onshow:"请输入6-12个字符",onfocus:"请输入6-12个字符"})
		.InputValidator({min:6,max:12,onerror:"请输入6-12个字符"});
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
    <script type="text/javascript">$('#bar-bobi-head').addClass('currently');$('#bar-bobi li.small').show();</script>
    <div id="mainarea">
      <div class="nav_two">
        <ul>
          <li><a href="bobi_buypoints.aspx">博币充值</a></li>
          <li class="currently"><a>激活充值卡</a></li>
        </ul>
        <div class="clear"></div>
      </div>
      <div>
        <form id="activepointscardForm" name="form1" method="post" action="ajax.aspx?oper=ajaxCard2Points">
          <table style="width:600px;" border="0" cellspacing="4" cellpadding="4" id="studio" align="center">
            <tr>
              <td height="30" align="right" width="120">充值卡帐号：</td>
              <td width="480"><input type="text" class="inputss" style="width:180px;" name="txtCardNumber" id="txtCardNumber" value="" />
              <span id="tipCardNumber" style="width:200px;"> </span></td>
            </tr>
            <tr>
              <td align="right">充值卡密码：</td>
              <td><input type="text" class="inputss" style="width:180px;" name="txtCardPassword" id="txtCardPassword" value="" />
              <span id="tipCardPassword" style="width:200px;"> </span></td>
            </tr>
            <tr>
              <td colspan="2" align="center" valign="bottom"><input type="submit" id="btnSave" value="提交激活" class="button" />
                <a href="index.aspx">取消</a></td>
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
