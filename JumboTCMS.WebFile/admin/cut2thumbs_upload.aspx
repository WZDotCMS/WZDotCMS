<%@ Page Language="C#" AutoEventWireup="true" Codebehind="cut2thumbs_upload.aspx.cs" Inherits="JumboTCMS.WebFile.Admin.Cut2Thumbs._upload" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>上传</title>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/jcmsV5.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/jcmsV5.css" />
<link type="text/css" rel="stylesheet" href="css/common.css" />
<style type="text/css">
*{ margin:0; padding:0; }
body {margin: 0px;padding: 0; font-size: 12px; background-color: #FFFFFF;color: #333333;}
td,input {font-size: 12px;}
</style>
<script type="text/javascript">
    function GO2Page(){
        window.location="cut2thumbs_default.aspx?ccid=<%=q("ccid")%>&tphoto=" + document.getElementById("Image1").src + "&tow=" + $("#ThumbsSize").val().split('|')[0] + "&toh=" + $("#ThumbsSize").val().split('|')[1] + "&type=" + $("#CutType").val() + "&i=1";
    }
	function ajaxFileUpload()
	{
		$("#buttonUpload")
		.ajaxStart(function(){
			$(this).hide();
			JumboTCMS.Loading.show("正在上传...");
		})
		.ajaxComplete(function(){
			$(this).show();
		});

		$.ajaxFileUpload
		(
			{
				url:"cut2thumbs_upfile.aspx?ccid=<%=q("ccid")%>&ThumbsSize="+$("#ThumbsSize").val()+"&CutType="+$("#CutType").val(),
				secureuri:false,
				fileElementId:'fileToUpload',
				dataType: 'json',
				success: function (data, status)
				{
					if(data.result != '1')
					{
						JumboTCMS.Alert(data.returnval, "0");
					}else
					{
						document.getElementById("Image1").src = data.returnval+"?"+(new Date().getTime());
						JumboTCMS.Loading.hide();
					}
				},
				error: function (data, status, e)
				{
					JumboTCMS.Loading.hide();
					alert(e);
				}
			}
		)
		
		return false;

	}
	</script>
</head>
<body>
    <form id="frmUpload" runat="server" method="post" enctype="multipart/form-data">
    <table id="UploadDiv" runat="server" style="width: 100%;">
        <tr>
            <td>
                <asp:FileUpload ID="fileToUpload" runat="server" Width="295px" />
                <input type="button" id="btnUpload" onclick="return ajaxFileUpload();" value="上传新图片" /><br />
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/_data/tempfiles/blank.jpg" />
                <br />
                缩略图比例：<asp:DropDownList ID="ThumbsSize" runat="server">
                </asp:DropDownList>
                <asp:DropDownList ID="CutType" runat="server">
                    <asp:ListItem Value="1" Selected="True">手工裁剪</asp:ListItem>
                    <asp:ListItem Value="CUT">自动裁剪</asp:ListItem>
                    <asp:ListItem Value="FILL">自动填充</asp:ListItem>
                </asp:DropDownList>
                &nbsp;<input type="button" id="btnSubmit" onclick="GO2Page();" value="确认" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
