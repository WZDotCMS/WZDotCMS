var mtype = q('ctype');//模型
var ctype = joinValue('ctype');//频道类型
var ccid = joinValue('ccid');//频道ID
var cid = joinValue('cid');//栏目ID
var k=joinValue('k');//关键字
var f=joinValue('f');//检索字段
var s=joinValue('s');//检索状态
var isimg=joinValue('isimg');
var istop=joinValue('istop');
var isfocus=joinValue('isfocus');
var d=joinValue('d');//检索时间
var pagesize=50;
var page=thispage();
$(document).ready(function(){
	ajaxList(page);
	FormatFontWeight();
});
function ajaxList(currentpage)
{
	if(currentpage!=null) page=currentpage;
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"page="+currentpage+"&pagesize="+pagesize+"&time="+(new Date().getTime()),
		url:		site.Dir + "modules/" + mtype + "_admin_ajax.aspx?oper=ajaxGetList"+ccid+cid+k+f+s+d+isimg+istop+isfocus,
		error:		function(XmlHttpRequest,textStatus, errorThrown) { alert(XmlHttpRequest.responseText);},
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				top.JumboTCMS.Alert(d.returnval, "0", "top.window.location='"+site.Dir+"admin/login.aspx';");
				break;
			case '0':
				top.JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				$("#ajaxList").setTemplateElement("tplList", null, {filter_data: false});
				$("#ajaxList").processTemplate(d);
				$("#ajaxPageBar").html(d.pagerbar);
				ActiveCoolTable();
				break;
			}
		}
	});
}
function RefreshChannelNum(ccid,totalcount){
	var ChannelName = top.$("#channel_"+ccid).html().split('(')[0];
	if(totalcount!="0")
		top.$("#channel_"+ccid).html(ChannelName+'('+totalcount+')');
	else
		top.$("#channel_"+ccid).html(ChannelName);
}
function FormatFontWeight(){
	$("#menu-s0").attr('class', s =='&s=0' ? 'menu1':'menu0');
	$("#menu-s1").attr('class', s =='&s=1' ? 'menu1':'menu0');
	$("#menu-s-1").attr('class', s =='&s=-1' ? 'menu1':'menu0');
	$("#menu-s").attr('class', (s =='' || s =='&s=') ? 'menu1':'menu0');
	$("#menu-isimg1").attr('class', isimg =='&isimg=1' ? 'menu1':'menu0');
	$("#menu-isimg2").attr('class', isimg =='&isimg=2' ? 'menu1':'menu0');
	$("#menu-isimg-1").attr('class', isimg =='&isimg=-1' ? 'menu1':'menu0');
	$("#menu-isimg").attr('class', (isimg =='' || isimg =='&isimg=') ? 'menu1':'menu0');
	$("#menu-istop1").attr('class', istop =='&istop=1' ? 'menu1':'menu0');
	$("#menu-istop-1").attr('class', istop =='&istop=-1' ? 'menu1':'menu0');
	$("#menu-istop").attr('class', (istop =='' || istop =='&istop=') ? 'menu1':'menu0');
}
function ConfirmCopy(id){
	ajaxCopy(id);
}
function ajaxCopy(id){
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"id="+id,
		url:		site.Dir + "modules/" + mtype + "_admin_ajax.aspx?oper=ajaxCopy&time="+(new Date().getTime()) + ccid,
		error:		function(XmlHttpRequest,textStatus, errorThrown){alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				top.JumboTCMS.Alert(d.returnval, "0", "top.window.location='"+site.Dir+"admin/login.aspx';");
				break;
			case '0':
				top.JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				top.JumboTCMS.Message(d.returnval, "1");
				ajaxList(page);
				break;
			}
		}
	});
}
function ajaxSearch(){
	top.JumboTCMS.Popup.show(site.Dir+'admin/content_searchform.aspx?ctype='+mtype+ccid+cid+k+f+s+d+isimg+istop+isfocus,500,280,false);
}
function ajaxBatchOper(act,ids,classid){
	top.JumboTCMS.Loading.show("正在处理...");
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"ids="+ids+"&tocid="+classid,
		url:		site.Dir + "modules/" + mtype + "_admin_ajax.aspx?oper=ajaxBatchOper&act="+act+"&time="+(new Date().getTime()) + ccid,
		error:		function(XmlHttpRequest,textStatus, errorThrown){top.JumboTCMS.Loading.hide();alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				top.JumboTCMS.Alert(d.returnval, "0", "top.window.location='"+site.Dir+"admin/login.aspx';");
				break;
			case '0':
				top.JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				top.JumboTCMS.Message(d.returnval, "1");
				ajaxList(page);
				break;
			}
		}
	});
}
function ConfirmDel(id){
	top.JumboTCMS.Confirm("确定要删除吗?", "IframeOper.ajaxDel("+id+")");
}
function ajaxDel(id){
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"id="+id,
		url:		site.Dir + "modules/" + mtype + "_admin_ajax.aspx?oper=ajaxDel&time="+(new Date().getTime()) + ccid,
		error:		function(XmlHttpRequest,textStatus, errorThrown){alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				top.JumboTCMS.Alert(d.returnval, "0", "top.window.location='"+site.Dir+"admin/login.aspx';");
				break;
			case '0':
				top.JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				ajaxList(page);
				break;
			}
		}
	});
}
var PublicID1="";
function operater(act,classid){
	PublicID1 = JoinSelect("selectID");
	if(PublicID1=="")
	{
		top.JumboTCMS.Alert("请先勾选要操作的内容", "0"); 
		return;
	}
	ajaxBatchOper(act,PublicID1,classid);
}

function move2class(){
	PublicID1 = JoinSelect("selectID");
	if(PublicID1=="")
	{
		top.JumboTCMS.Alert("请先勾选要操作的内容", "0"); 
		return;
	}
	top.JumboTCMS.Popup.show(site.Dir+'admin/content_move2class.aspx?oper=null'+ccid,500,340,false);
}
function ajaxMove2Class(classid){
	operater("move2class",classid);
}
var PublicID2="";
function move2special(){
	PublicID2 = JoinSelect("selectID");
	if(PublicID2=="")
	{
		top.JumboTCMS.Alert("请先勾选要操作的内容", "0"); 
		return;
	}
	top.JumboTCMS.Popup.show(site.Dir + 'admin/special_list2.aspx',520,-1,false);
}
function ajaxMove2Special(specialid)
{
	top.JumboTCMS.Loading.show("正在处理...");
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"ids="+PublicID2+"&tosid="+specialid,
		url:		site.Dir + "admin/content_ajax.aspx?oper=ajaxMove2Special&time="+(new Date().getTime()) + ccid,
		error:		function(XmlHttpRequest,textStatus, errorThrown){top.JumboTCMS.Loading.hide();alert(XmlHttpRequest.responseText); },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				top.JumboTCMS.Alert(d.returnval, "0", "top.window.location='../admin/login.aspx';");
				break;
			case '0':
				top.JumboTCMS.Alert(d.returnval, "0");
				break;
			case '1':
				top.JumboTCMS.Message(d.returnval, "1");
				ajaxList(page);
				break;
			}
		}
	});
}
//批量内容操作
function formatContentOper(_value, _id, _type)
{
	var _str;
	switch(_type){
		case 'img':
			_str = '<img title="'+formatIsImg(_value)+'" src="'+site.Dir+'admin/images/ico_isimg'+_value+'.gif" border="0" />';
			break;
		case 'top':
			if(_value==1)
				_str = '<a href="javascript:void(0)" title="取消推荐" onclick="ajaxBatchOper(\'notop\','+_id+')"><img src="'+site.Dir+'admin/images/ico_istop'+_value+'.gif" border="0" /></a>';
			else
				_str = '<a href="javascript:void(0)" title="设为推荐" onclick="ajaxBatchOper(\'top\','+_id+')"><img src="'+site.Dir+'admin/images/ico_istop'+_value+'.gif" border="0" /></a>';
			break;
		case 'focus':
			if(_value==1)
				_str = '<a href="javascript:void(0)" title="取消焦点" onclick="ajaxBatchOper(\'nofocus\','+_id+')"><img src="'+site.Dir+'admin/images/ico_isfocus'+_value+'.gif" border="0" /></a>';
			else
				_str = '<a href="javascript:void(0)" title="设为焦点" onclick="ajaxBatchOper(\'focus\','+_id+')"><img src="'+site.Dir+'admin/images/ico_isfocus'+_value+'.gif" border="0" /></a>';

			break;
		default:
			if(_value==1)
				_str = '<img alt="已发布" src="'+site.Dir+'admin/images/ico_ispass'+_value+'.gif" border="0" />';
			if(_value==-1)
				_str = '<img alt="待审" src="'+site.Dir+'admin/images/ico_ispass'+_value+'.gif" border="0" />';
			if(_value==0)
				_str = '<img alt="新的" src="'+site.Dir+'admin/images/ico_ispass'+_value+'.gif" border="0" />';
			break;
	}
	return _str;
}