var _jcms_UserData = new Object();
function ajaxBindUserData(returnFunc){
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"time="+(new Date().getTime()),
		url:		site.Dir + "ajax/user.aspx?oper=ajaxUserInfo",
		error:		function(XmlHttpRequest,textStatus, errorThrown){if(XmlHttpRequest.responseText!=""){alert(XmlHttpRequest.responseText);}},
		success:	function(data){
			_jcms_UserData = data;
			$(".loginName").html(data.username);
			if(data.adminid!="0") $("#li_admin").show();
			$("#u_myface_l").attr("src",site.Dir + "_data/avatar/"+data.userid+"_l.jpg?" + (new Date().getTime()));
			$("#u_myface_m").attr("src",site.Dir + "_data/avatar/"+data.userid+"_m.jpg?" + (new Date().getTime()));
			$("#u_myface_s").attr("src",site.Dir + "_data/avatar/"+data.userid+"_s.jpg?" + (new Date().getTime()));
			if(data.newmessage != "0")
				$(".newmessage").html("短消息("+data.newmessage+"条未读)");
			else
				$(".newmessage").html("短消息");
			if(data.newnotice != "0"){
				$(".notify").show();	
				$(".newnotice").html(data.newnotice+"条新通知");	
			}
            $(".u_points").text(data.points);
            $(".u_integral").text(data.integral);
            $(".u_email").text(data.email);
			if(returnFunc!=null){
				eval(returnFunc);
			}
		}
	});
}
var _jcms_UserLogout = function(){
	top.location.href=site.Dir + "passport/logout.aspx?refer=../passport/login.aspx&userkey=" + user.userkey;
}
function ajaxDelMessage(id)
{
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"id="+id,
		url:		site.Dir + "user/ajax.aspx?oper=ajaxDelMessage&time="+(new Date().getTime()),
		success:	function(d){
			switch (d.result)
			{
			case '0':
				JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				location.href='message_list.aspx';
				break;
			}
		}
	});
}