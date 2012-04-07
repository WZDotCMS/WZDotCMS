function ajaxQuestionAdd()
{
	var uName=$("#questionName").val();
	var uCode=$("#questionCode").val();
	var parentid=$("#questionParentId").val();
	var classid=$("#questionClassId").val();
	var title=ajaxQuestionSiftSymbol($("#questionTitle").val());
	if(parentid!="0") title="tttttt";//表示回复
	var content=ajaxQuestionSiftSymbol($("#questionContent").val());
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
	if(!title || title.length<5) {
		alert("留言标题不能少于5个!");
		return;
	}
	if(title.length>16){
		alert("留言标题太长!");
		return;
	}
	if(!content || content.length<5) {
		alert("留言字符不能少于5个!");
		return;
	}
	if(content.length>200){
		alert("留言字符太多!");
		return;
	}
	$("#question-form").hide();
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"parentid="+parentid+"&classid="+classid+"&name="+encodeURIComponent(uName)+"&code="+escape(uCode)+"&title="+encodeURIComponent(title)+"&content="+encodeURIComponent(content),
		url:		site.Dir + "question/ajax.aspx?oper=ajaxQuestionAdd&time="+(new Date().getTime()),
		error:		function(XmlHttpRequest,textStatus, errorThrown){if(XmlHttpRequest.responseText!=""){alert(XmlHttpRequest.responseText);}},
		success:	function(d){
			if(d.result=="1")
			{
				$("#questionTitle").val("");
				$("#questionContent").val("");
				$("#questionCode").val("");
				alert(d.returnval);
				$("#question-form").show();
				ajaxQuestionList(1);
			}
			else
			{
				alert(d.returnval);
				$("#question-form").show();
			}
			_jcms_GetRefreshCode('imgCode');
		}
	});
}
function ajaxQuestionDelete(questionid)
{
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"questionid="+questionid+"&classid="+classid,
		url:		site.Dir + "question/ajax.aspx?oper=ajaxQuestionDel&time="+(new Date().getTime()),
		error:		function(XmlHttpRequest,textStatus, errorThrown){if(XmlHttpRequest.responseText!=""){alert(XmlHttpRequest.responseText);}},
		success:	function(d){
			if(d.result=="1")
			{
				ajaxQuestionList(page);
			}
			else
			{
				alert(d.returnval);
			}
		}
	});
}
function ajaxQuestionReply2(parentid)
{
	$("#questionParentId").val(parentid);
	$("#questionTitle").val("管理员回复 #"+parentid);
	$("#questionContent").val("");
	$("#questionContent")[0].focus();
}
function ajaxQuestionSiftSymbol(str){ 
	str = str.replace(/[\-]{10}/g,"").replace(/[,]{10}/g,"").replace(/[\.]{10}/g,""); 
	return(str);
} 