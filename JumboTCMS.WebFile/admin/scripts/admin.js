function chkLogout(){
	$.ajax({
		type:		"get",
		dataType:	"json",
		url:		"ajax.aspx?oper=logout&time="+(new Date().getTime()),
        	error:		function(XmlHttpRequest,textStatus, errorThrown) { alert(XmlHttpRequest.responseText);},
		success:	function(d){
			if(d.result=="1")
				top.location.href = 'login.aspx';
		}
	});
}
function ajaxClearSystemCache()
{
	top.JumboTCMS.Loading.show("正在更新，请等待...");
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"oper=ajaxClearSystemCache",
		url:		"ajax.aspx?time="+(new Date().getTime()),
		error:		function(XmlHttpRequest,textStatus, errorThrown){top.JumboTCMS.Loading.hide();alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				top.JumboTCMS.Alert(d.returnval, "0", "top.window.location='" + site.Dir + "admin/login.aspx';");
				break;
			case '0':
				top.JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				top.JumboTCMS.Message(d.returnval, "1");
				break;
			}
		}
	});
}
function ajaxCreateSystemCount()
{
	top.JumboTCMS.Loading.show("正在更新，时间可能会比较长...");
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"oper=ajaxCreateSystemCount",
		url:		"ajax.aspx?time="+(new Date().getTime()),
		error:		function(XmlHttpRequest,textStatus, errorThrown){top.JumboTCMS.Loading.hide();alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				top.JumboTCMS.Alert(d.returnval, "0", "top.window.location='" + site.Dir + "admin/login.aspx';");
				break;
			case '0':
				top.JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				top.JumboTCMS.Message(d.returnval, "1");
				break;
			}
		}
	});
}

function ajaxCreateIndexPage()
{
	top.JumboTCMS.Loading.show("正在更新，请等待...");
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"oper=ajaxCreateIndexPage",
		url:		"ajax.aspx?time="+(new Date().getTime()),
		error:		function(XmlHttpRequest,textStatus, errorThrown){top.JumboTCMS.Loading.hide();alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				top.JumboTCMS.Alert(d.returnval, "0", "top.window.location='" + site.Dir + "admin/login.aspx';");
				break;
			case '0':
				top.JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				top.JumboTCMS.Message(d.returnval, "1");
				break;
			}
		}
	});
}
function ajaxEmailServerExport()
{
	top.JumboTCMS.Loading.show("正在导出，请等待...",260,80);
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"oper=ajaxEmailServerExport",
		url:		"emailserver_ajax.aspx?time="+(new Date().getTime()),
		error:		function(XmlHttpRequest,textStatus, errorThrown){top.JumboTCMS.Loading.hide();alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				top.JumboTCMS.Alert(d.returnval, "0", "top.window.location='" + site.Dir + "admin/login.aspx';");
				break;
			case '0':
				top.JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				top.JumboTCMS.Message(d.returnval, "1");
				ajaxList(1);
				break;
			}
		}
	});
}
function ajaxEmailServerImport()
{
	top.JumboTCMS.Loading.show("正在导出，请等待...",260,80);
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"oper=ajaxEmailServerImport",
		url:		"emailserver_ajax.aspx?time="+(new Date().getTime()),
		error:		function(XmlHttpRequest,textStatus, errorThrown){top.JumboTCMS.Loading.hide();alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				top.JumboTCMS.Alert(d.returnval, "0", "top.window.location='" + site.Dir + "admin/login.aspx';");
				break;
			case '0':
				top.JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				top.JumboTCMS.Message(d.returnval, "1");
				ajaxList(1);
				break;
			}
		}
	});
}
function ajaxCreateJavascript()
{
	top.JumboTCMS.Loading.show("正在更新，请等待...");
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"oper=ajaxCreateJavascript",
		url:		"javascript_ajax.aspx?time="+(new Date().getTime()),
		error:		function(XmlHttpRequest,textStatus, errorThrown){top.JumboTCMS.Loading.hide();alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				top.JumboTCMS.Alert(d.returnval, "0", "top.window.location='" + site.Dir + "admin/login.aspx';");
				break;
			case '0':
				top.JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				top.JumboTCMS.Message(d.returnval, "1");
				break;
			}
		}
	});
}
/*更新include包含文件*/
function ajaxTemplateIncludeUpdateFore(pid,source)
{
	if(source==null)source="";
	top.JumboTCMS.Loading.show("正在更新，请等待...");
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"time="+(new Date().getTime()),
		url:		site.Dir + "admin/templateinclude_ajax.aspx?oper=updatefore&pid="+pid+"&source="+source,
		error:		function(XmlHttpRequest,textStatus, errorThrown){top.JumboTCMS.Loading.hide();alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				top.JumboTCMS.Alert(d.returnval, "0", "top.window.location='" + site.Dir + "admin/login.aspx';");
				break;
			case '0':
				top.JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				top.JumboTCMS.Message(d.returnval, "1");
				break;
			}
		}
	});
}
function ajaxModuleUpdateFore()
{
	top.JumboTCMS.Loading.show("正在更新，请等待...");
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"time="+(new Date().getTime()),
		url:		"modules_ajax.aspx?oper=updatefore",
		error:		function(XmlHttpRequest,textStatus, errorThrown){top.JumboTCMS.Loading.hide();alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				top.JumboTCMS.Alert(d.returnval, "0", "top.window.location='" + site.Dir + "admin/login.aspx';");
				break;
			case '0':
				top.JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				top.JumboTCMS.Message(d.returnval, "1");
				break;
			}
		}
	});
}
/*更新站内搜索索引*/
function ajaxCreateSearchIndex(create)
{
	top.JumboTCMS.Loading.show("更新过程可能比较缓慢，请耐心等待...");
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"oper=ajaxCreateSearchIndex&create="+create,
		url:		"ajax.aspx?time="+(new Date().getTime()),
		error:		function(XmlHttpRequest,textStatus, errorThrown){top.JumboTCMS.Loading.hide();alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				top.JumboTCMS.Alert(d.returnval, "0", "top.window.location='" + site.Dir + "admin/login.aspx';");
				break;
			case '0':
				top.JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				top.JumboTCMS.Message(d.returnval, "1");
				break;
			}
		}
	});
}