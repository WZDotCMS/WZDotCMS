document.write('<div id="qqonlineDiv" style="display:none;z-index:99999">');
document.write('<table cellSpacing="0" cellPadding="0" width="110" border="0" id="qqtab">');
document.write('    <tr>');
document.write('      <td width="110" onclick="if($i(\'qqtab\').style.display==\'none\'){$i(\'qqtab\').style.display=\'\'} else {$i(\'qqtab\').style.display=\'none\'}"><img src="'+site.Dir+'extends/qqonline/images/qqskin/'+QQOnlineInfo.siteskin+'/top.gif" border="0"></td>');
document.write('    </tr>');
document.write('    <tr id="qqstab">');
document.write('      <td valign="middle" align="center" background="'+site.Dir+'extends/qqonline/images/qqskin/'+QQOnlineInfo.siteskin+'/middle.gif">');
document.write('<table border="0" width="80" cellSpacing="0" cellPadding="0">');
document.write('  <tr>');
document.write('    <td width="80" height="5" border="0" colspan="2"></td>');
document.write('  </tr>');
for (var i = 0; i < QQOnlineInfo.table.length; i++) 
{
	var state = online[i]==0 ? 'f' : 'm';
	document.write('  <tr>');
	document.write('    <td width="25" height="22" valign="middle" align="center">');
	document.write('<img src="'+site.Dir+'extends/qqonline/images/qqface/'+QQOnlineInfo.table[i].face+'_'+state+'.gif" border="0">');
	document.write('    </td>');
	document.write('    <td width="55" height="22" valign="middle" align="left">');
	document.write('<a href="http://wpa.qq.com/msgrd?V=1&amp;Uin='+QQOnlineInfo.table[i].qqid+'&amp;Site='+site.Name+'&amp;Menu=yes" target="blank"><font style="font-size:12px;TEXT-DECORATION:none;color:'+QQOnlineInfo.table[i].tcolor+';">'+QQOnlineInfo.table[i].title+'</font></a><br>');
	document.write('    </td>');
	document.write('  </tr>');
}
document.write('</table>');
document.write('</td>');
document.write('    </tr>');
document.write('    <tr>');
document.write('      <td width="110" onclick="if($i(\'qqstab\').style.display==\'none\'){$i(\'qqstab\').style.display=\'\'} else {$i(\'qqstab\').style.display=\'none\'}"><img src="'+site.Dir+'extends/qqonline/images/qqskin/'+QQOnlineInfo.siteskin+'/bottom.gif" border="0"></td>');
document.write('    </tr>');
document.write('</table>');
document.write('</div>');
if(QQOnlineInfo.sitearea =='0'){
	$("#qqonlineDiv").jFloat({position:"left",top:parseInt(QQOnlineInfo.siteshowy,10),width:110,height:200,left:parseInt(QQOnlineInfo.siteshowx,10)});
}
else{
	$("#qqonlineDiv").jFloat({position:"right",top:parseInt(QQOnlineInfo.siteshowy,10),width:110,height:200,right:parseInt(QQOnlineInfo.siteshowx,10)});
}
