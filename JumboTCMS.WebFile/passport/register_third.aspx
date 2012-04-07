<%@ Page Language="C#" AutoEventWireup="True" Codebehind="register_third.aspx.cs" Inherits="JumboTCMS.WebFile.Passport._register_third" %>
<!doctype html>
<!--STATUS OK-->
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="X-UA-Compatible" content="IE=7">
<title>第三方登录 <%=site.Name%></title>
<link href="css/<%=base.PassportTheme %>/basic.css" rel="stylesheet" type="text/css" media="screen">
<link rel="stylesheet" type="text/css" href="css/<%=base.PassportTheme %>/add-info.css"/>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/jcmsV5.js"></script>
<link   type="text/css" rel="stylesheet" href="../_data/jcmsV5.css" />
<script type="text/javascript" src="../_libs/my97datepicker4.6/WdatePicker.js"></script>
<style> 
.psp-main .con-psp ul em.c0{display:block;}
</style>
</head>
<body>
<div id="header">
  <div class=""></div>
  <div class="sub-global-header">
    <div class="sub-global-header-inner">
      <div class="logo"><a href="<%=site.Dir %>"><%=site.Name %></a></div>
      <ul class="cate-nav">
      </ul>
      <ul class="login-nav">
        <li><a href="<%=site.Dir %>">返回首页</a>|</li>
        <li class="collect"><a href="javascript:void(0);" onClick="return _jcms_addFavorite('<%=site.Url %>', '<%=site.Name %>')">收藏<%=site.Name2 %></a></li>
      </ul>
    </div>
  </div>
</div>
<div id="wrapper">
  <div id="bd">
    <div class="psp-title">
      <h2>完善个人资料<span class="theme"><a class="default" href="javascript:void(0);" title="蓝色风格" onclick="JumboTCMS.Cookie.set('passport_theme','default');location.reload();"></a><a class="green" href="javascript:void(0);" title="绿色风格" onclick="JumboTCMS.Cookie.set('passport_theme','green');location.reload();"></a></span></h2>
    </div>
    <div class="psp-wrap">
      <div class="psp-main cls">
        <div class="psp-container wi">
          <form action="ajax.aspx?oper=ajaxOAuthNewUser" method="post" class="psp-form" id="newuserForm">
            <ul>
              <li><em class="c0">请先完善个人资料</em></li>
              <li class="error-wraper">
                <label class="k" for="">Email邮箱：</label>
                <span class="v">
                <input type="text" name="txtEmail" id="txtEmail" value="<%=_Email %>" class="psp-text" minlength="4" maxlength="50">
                </span> <em id="tipEmail"></em> </li>
              <li>
                <label class="k" for="">用户名：</label>
                <span class="v">
                <input type="text" name="txtUserName" id="txtUserName" value="<%=_UserName %>" class="psp-text">
                </span> <em id="tipUserName"></em> </li>
              <li>
                <label class="k" for="txtPass1">密&nbsp;&nbsp;码：</label>
                <span class="v">
                <input class="psp-text" style="width:180px" type="password" maxlength="20" name="txtPass1" id="txtPass1" value="" />
                <em id="tipPass1"></em></span> </li>
              <li>
                <label class="k" for="txtPass2">确认密码：</label>
                <span class="v">
                <input class="psp-text" style="width:180px" type="password" maxlength="20" name="txtPass2" id="txtPass2" value="" />
                <em id="tipPass2"></em></span> </li>
              <li class="error-wraper"> <span class="k">&nbsp;性&nbsp;&nbsp;&nbsp;别：</span> <span class="v">
                <input type="radio" class="sex" name="rblSex" value="1" checked="checked">
                <label for=""> 男</label>
                <input type="radio" class="sex" name="rblSex" value="2">
                <label for=""> 女</label>
                </span></li>
              <li class="error-wraper"> <span class="k" for="txtBirthday">出生日期：</span> <span class="v">
                <input class="psp-text Wdate" style="width:90px" type="text" name="txtBirthday" id="txtBirthday" onFocus="WdatePicker({isShowClear:false,readOnly:true,skin:'blue'})" readonly="readonly" value="<%=_Birthday %>" />
                <em id="tipsBirthday"></em></span></li>
            </ul>
            <div class="btn">
              <input type="submit" class="btn-reg" value="完成">
            </div>
          </form>
        </div>
        <div class="psp-sidebar">
          <form action="ajax.aspx?oper=ajaxOAuthBindUser" method="post" class="psp-form con-psp" id="binduserForm">
            <ul>
              <li><em class="c0">已有将博帐号？填写帐号信息完成关联</em></li>
              <li class="error-wraper">
                <label class="k" for="">帐&nbsp;&nbsp;号：</label>
                <span class="v">
                <input type="text" name="txtBindUserName" id="txtBindUserName" class="psp-text login-width" value="">
                </span> <em id="tipBindUserName"></em> </li>
              <li class="error-wraper">
                <label class="k" for="">密&nbsp;&nbsp;码：</label>
                <span class="v">
                <input type="password" name="txtBindUserPass" id="txtBindUserPass" class="psp-text login-width" value="">
                </span> <em id="tipBindUserPass"></em> </li>
            </ul>
            <div class="btn">
              <input type="submit" class="btn-reg" value="关联帐号">
            </div>
          </form>
          <dl class="login-tips">
            <dt>关联将博帐号后，下次你可以：</dt>
            <dd><span>用QQ帐号直接登录</span></dd>
            <dd><span>用关联的将博帐号登录</span></dd>
          </dl>
        </div>
      </div>
    </div>
  </div>
</div>
<div id="footer">
  <div class="copyright">
    <div class="copyright-inner">
      <p class="copyright-url"> <a href="<%=site.Dir%>about/index.html" target="_blank">关于<%=site.Name2%></a>|<a href="<%=site.Dir%>about/contactus.html"  target="_blank">联系<%=site.Name2%></a>|<a href="<%=site.Dir%>about/service.html"  target="_blank">商业服务</a>|<a href="<%=site.Dir%>about/copyright.html"  target="_blank">版权申明</a>|<a href="<%=site.Dir%>sitemap<%=site.StaticExt%>"  target="_blank">网站地图</a> </p>
      <div class="copyright-info">
        <p> <span>Copyright &copy; 2007-2012</span> <span><%=site.Name%>版权所有</span> <a href="http://www.miibeian.gov.cn" target="_blank"><%=site.ICP%></a> </p>
        <p> Powered by <strong><a href="http://www.jumbotcms.net/" target="_blank">JumboTCMS</a></strong> <%=site.Version%></p>
      </div>
    </div>
  </div>
</div>
<script type="text/javascript">
$(document).ready(function(){
	$('#newuserForm').ajaxForm({
		beforeSubmit: JumboTCMS.AjaxFormSubmit,
		success :function(data){
			JumboTCMS.Eval(data);
		}
	}); 
	$('#binduserForm').ajaxForm({
		beforeSubmit: JumboTCMS.AjaxFormSubmit2,
		success :function(data){
			JumboTCMS.Eval(data);
		}
	}); 
	$.formValidator.initConfig({validatorGroup:"1",onError:function(msg){/*alert(msg);*/}});
	$("#txtEmail")
		.formValidator({validatorGroup:"1",tipid:"tipEmail",onfocus:"请输入常用的邮箱"})
		.InputValidator({min:8,max:50,onerror:"邮箱非法,请确认"})
		.RegexValidator({regexp:"email",datatype:"enum",onerror:"格式不正确"})
		.AjaxValidator({
		type : "get",
		url:		"ajax.aspx?oper=checkemail&id=0&time="+(new Date().getTime()),
		datatype : "json",
		success : function(d){	
			if(d.result == "1")
				return true;
			else
				return false;
		},
		buttons: $("#btnSave"),
		error: function(){alert("服务器繁忙，请稍后再试");},
		onerror : "邮箱已被注册",
		onwait : "正在校验邮箱的合法性，请稍候..."
	});
	$("#txtUserName")
		.formValidator({validatorGroup:"1",tipid:"tipUserName",onfocus:"可以是中文、英文、数字、-、_"})
		.InputValidator({min:4,max:20,onerror:"4-20个字符,一个汉字为2个字符"})
		.RegexValidator({regexp:"username",datatype:"enum",onerror:"汉字或字母开头,不支持符号"})
		.AjaxValidator({
		type : "get",
		url:		"ajax.aspx?oper=checkname&id=0&time="+(new Date().getTime()),
		datatype : "json",
		success : function(d){	
			if(d.result == "1")
				return true;
			else
				return false;
		},
		buttons: $("#btnSave"),
		error: function(){alert("服务器繁忙，请稍后再试");},
		onerror : "用户名已被注册",
		onwait : "正在校验用户名的合法性，请稍候..."
	});
	$("#txtPass1").formValidator({validatorGroup:"1",tipid:"tipPass1",onfocus:"请输入6-14位密码"}).InputValidator({min:6,max:14,onerror:"密码6-14位"});
	$("#txtPass2").formValidator({validatorGroup:"1",tipid:"tipPass2",onfocus:"两次密码必须一致"}).InputValidator({min:6,max:14,onerror:"密码6-14位,请确认"}).CompareValidator({desID:"txtPass1",operateor:"=",onerror:"两次密码不一致"});
    $.formValidator.initConfig({validatorGroup:"2",onError:function(msg){/*alert(msg);*/}});
	$("#txtBindUserName").formValidator({validatorGroup:"2",tipid:"tipBindUserName",onshow:"请输入正确的用户名",onfocus:"请输入正确的用户名"})
		.InputValidator({min:4,max:20,onerror:"你输入的用户名非法,请确认"})
		.RegexValidator({regexp:"username",datatype:"enum",onerror:"输入不正确"});
	$("#txtBindUserPass").formValidator({validatorGroup:"2",tipid:"tipBindUserPass",onshow:"请输入正确的密码",onfocus:"请输入正确的密码"}).InputValidator({min:6,max:14,onerror:"密码6-14位"});
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
JumboTCMS.AjaxFormSubmit2=function(item){
	try{
		if($.formValidator.PageIsValid('2'))
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
</body>
</html>
