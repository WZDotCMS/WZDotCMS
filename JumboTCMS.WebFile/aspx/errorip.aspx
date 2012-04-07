<%@ Page Language="C#" AutoEventWireup="True" Codebehind="errorip.aspx.cs" Inherits="JumboTCMS.WebFile._errorip" %>
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>IP错误页面</title>
    <style type="text/css">
<!--
body,td,th {
	font-family: "宋体",Verdana, Arial;
	font-size: 12px;
	text-align: left;
}
.btn
{
	border:1px solid #eeeeee;
	height:20px;
	width:50px; font-family: "宋体",Verdana, Arial ;
	font-size: 12px;
	text-align: center;
	FILTER: progid:DXImageTransform.Microsoft.Gradient(startColorStr=#FFFFFF,endColorStr=#C6C5D7)
}
-->
</style>
</head>
<body>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td align="center">
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="border: 1px solid #0D7CCB">
                    <tr>
                        <td height="23" align="left" valign="middle" bgcolor="#0D7CCB">
                            <span style="font-size: 14px; font-weight: bold; color: #FFFFFF; padding: 10px">提示信息</span></td>
                    </tr>
                    <tr>
                        <td align="center" valign="middle">
                            <label>
                            </label>
                            <table style="padding: 10px" width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="100%" align="left" valign="top">
                                        <span style="font-size: 13px; line-height: 17px;">您的IP已经被封，请联系站长。</span></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td height="30" valign="bottom">
                            <div align="center">
                                <a style="cursor: hand">
                                    <input style="cursor: hand" name="Submit" type="button" class="Btn" onclick="javascript:window.close();"
                                        value="确 定" /></a></div>
                        </td>
                    </tr>
                    <tr>
                        <td height="15" align="center">
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>
</html>