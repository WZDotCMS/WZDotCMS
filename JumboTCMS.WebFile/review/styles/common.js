var ajaxReviewPageSize = 5;//每页显示的评论数
function ajaxReviewAdd(ccid,id)
{
	var uName=$("#reviewName").val();
	var uCode=$("#reviewCode").val();
	var parentid=$("#reviewParentId").val();
	var content=$("#reviewContent").val();
	if(!uName) {
		alert("用户名不能为空!");
		return;
	}
	if(uName.length>16){
		alert("用户名太长!");
		return;
	}
	if(uCode == "" || uCode.toLowerCase() != JumboTCMS.Cookie.get("ValidateCode").toLowerCase()){
		alert('验证码错误');
		return;
	}
	if(!content || content.length<5) {
		alert("评论字符太少!");
		return;
	}
	if(content.length>200){
		alert("评论字符太多!");
		return;
	}
	$("#btnReviewAdd").attr("disabled","disabled");
	
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"ccid="+ccid+"&id="+id+"&parentid="+parentid+"&name="+encodeURIComponent(uName)+"&code="+encodeURIComponent(uCode)+"&content="+encodeURIComponent(content),
		url:		site.Dir + "review/ajax.aspx?oper=ajaxReviewAdd&time="+(new Date().getTime()),
		error:		function(XmlHttpRequest,textStatus, errorThrown){if(XmlHttpRequest.responseText!=""){alert(XmlHttpRequest.responseText);}},
		success:	function(d){
			if(d.result=="1")
			{
				$("#reviewContent").val("");
				$("#reviewCode").val("");
				alert(d.returnval);
				ajaxReviewList(ccid,id,1);
				$("#btnReviewAdd").attr("disabled","");
				_jcms_GetRefreshCode('imgCode');
			}
			else
			{
				alert(d.returnval);
			}
		}
	});
}
function ajaxReviewDelete(ccid,id,reviewid)
{
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"reviewid="+reviewid,
		url:		site.Dir + "review/ajax.aspx?oper=ajaxReviewDel&time="+(new Date().getTime()),
		error:		function(XmlHttpRequest,textStatus, errorThrown){if(XmlHttpRequest.responseText!=""){alert(XmlHttpRequest.responseText);}},
		success:	function(d){
			if(d.result=="1")
			{
				ajaxReviewList(ccid,id,page);
                alert(d.returnval);
			}
			else
			{
				alert(d.returnval);
			}
		}
	});
}
function ajaxReviewReply2(parentid)
{
	$("#reviewParentId").val(parentid);
	$("#reviewContent").val("");
	$("#reviewContent")[0].focus();
}
function ajaxReviewUserInfo()
{
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"oper=ajaxReviewUserInfo&time="+(new Date().getTime()),
		url:		site.Dir + "review/ajax.aspx",
		error:		function(XmlHttpRequest,textStatus, errorThrown){if(XmlHttpRequest.responseText!=""){alert(XmlHttpRequest.responseText);}},
		success:	function(data){
			if(data.username=="")
			{
				$("#reviewName").val("匿名游客");
				$(".reviewName").html("匿名游客");
				$("#guest_btn").show();
			}
			else{
				$("#reviewName").val(data.username);
				$(".reviewName").html(data.username);
			}
		}
	});
}