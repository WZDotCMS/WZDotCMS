<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="user_preview.aspx.cs" Inherits="JumboTCMS.WebFile.Admin._user_preview" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>用户详细资料</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/jcmsV5.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/jcmsV5.css" />
<link type="text/css" rel="stylesheet" href="css/common.css" />

<script type="text/javascript">
            $(function() {
                $('#container-1').tabs({ fxFade: true, fxSpeed: 'fast' });
            });
    </script>
<link rel="stylesheet" href="../_libs/jquery.tabs/style.css" type="text/css" />
<!--[if lte IE 7]>
<link rel="stylesheet" href="../_libs/jquery.tabs/style-ie.css" type="text/css" />
<![endif]-->
<script type="text/javascript">
(function(){
	$(document).ready(function() {
		$.ajax({
			type:		"get",
			dataType:	"json",
			data:		"time="+(new Date().getTime()),
			url:		"user_ajax.aspx?oper=ajaxUserInfo&id=<% = userid%>",
			error:		function(XmlHttpRequest,textStatus, errorThrown){if(XmlHttpRequest.responseText!=""){alert(XmlHttpRequest.responseText);}},
			success:	function(data){
				//数据绑定
				$(".u_username").text(data.table[0].username);
				$(".u_groupname").text(data.table[0].groupname);
				$(".u_points").text(data.table[0].points);
				$(".u_integral").text(data.table[0].integral);
				$(".u_email").text(data.table[0].email);
				if(data.table[0].sex=="2")
					$(".u_sex").text("女");
				if(data.table[0].sex=="0")
					$(".u_sex").text("保密");
				$(".u_birthday").text(data.table[0].birthday);
				$(".u_truename").text(data.table[0].truename);
				$(".u_provincecity").text(data.table[0].provincecity);
				if(data.table[0].idtype=="1")
					$(".u_idtype").text("身份证");
				else if(data.table[0].idtype=="2")
					$(".u_idtype").text("军官证");
				else if(data.table[0].idtype=="3")
					$(".u_idtype").text("学生证");
				else
					$(".u_idtype").text("其他证件");
				$(".u_idcard").text(data.table[0].idcard);
				$(".u_workunit").text(data.table[0].workunit);
				$(".u_address").text(data.table[0].address);
				$(".u_zipcode").text(data.table[0].zipcode);
				$(".u_mobiletel").text(data.table[0].mobiletel);
				$(".u_telephone").text(data.table[0].telephone);
				$(".u_qq").html(data.table[0].qq);
				$(".u_msn").html(data.table[0].msn);
				$(".u_homepage").html("<a href='"+data.table[0].homepage+"' target='_blank'>"+data.table[0].homepage+"</a>");
			}
		});
	});
})();
</script>
</head><body>
<br />
<div id="container-1">
	<ul>
		<li><a href="#fragment-1"><span>基本资料</span></a></li>
	</ul>
	<div id="fragment-1">
		<table class="formtable">
			<tr>
				<th> 用户名：</th>
				<td><span class="u_username"></span>(<span class="u_email"></span>)</td>
			</tr>
			<tr>
				<th> 昵称：</th>
				<td><span class="u_nickname"></span></td>
			</tr>
			<tr>
				<th> 当前账户：</th>
				<td>剩余博币：<span class="u_points" style="color:#ff5500"></span>，积分：<span class="u_integral" style="color:#ff5500"></span></td>
			</tr>
			<tr>
				<th> 分组：</th>
				<td><span class="u_groupname"></span></td>
			</tr>
			<tr>
				<th> 真实姓名：</th>
				<td><span class="u_truename"></span>(<span class="b"></span><span class="u_sex">男</span>)</td>
			</tr>
			<tr>
				<th> 出生日期：</th>
				<td><span class="u_birthday"></span></td>
			</tr>
			<tr>
				<th> 证件类型：</th>
				<td><span class="u_idtype"></span></td>
			</tr>
			<tr>
				<th> 证件号码：</th>
				<td><span class="u_idcard"></span></td>
			</tr>
			<tr>
				<th> 所在地区：</th>
				<td><span class="u_provincecity"></span></td>
			</tr>
			<tr>
				<th> 工作单位：</th>
				<td><span class="u_workunit"></span></td>
			</tr>
			<tr>
				<th> 联系地址：</th>
				<td><span class="u_address"></span></td>
			</tr>
			<tr>
				<th> 邮政编码：</th>
				<td><span class="u_zipcode"></span></td>
			</tr>
			<tr>
				<th> 手机号码：</th>
				<td><span class="u_mobiletel"></span></td>
			</tr>
			<tr>
				<th> 联系电话：</th>
				<td><span class="u_telephone"></span></td>
			</tr>
			<tr>
				<th> QQ：</th>
				<td><span class="u_qq"></span></td>
			</tr>
			<tr>
				<th> MSN：</th>
				<td><span class="u_msn"></span></td>
			</tr>
			<tr>
				<th> 个人主页：</th>
				<td><span class="u_homepage"></span></td>
			</tr>
		</table>
	</div>
</div>
<script type="text/javascript">_jcms_SetDialogTitle();</script>
</body>
</html>
