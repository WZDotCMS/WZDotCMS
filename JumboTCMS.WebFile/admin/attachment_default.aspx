<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="attachment_default.aspx.cs" Inherits="JumboTCMS.WebFile.Admin.Attachment._index" %>

<%@ Register Assembly="JumboTCMS.WebControls" Namespace="JumboTCMS.WebControls" TagPrefix="Jumbot" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>附件上传</title>
<style type="text/css">
body{margin: 0px;}
</style>
<script type="text/javascript">
function OnCompleted(callBack){
	var filename = callBack.split('|')[0];
	var filesize = callBack.split('|')[1];
	var s = filename.lastIndexOf(".");
	var e = filename.substring(s+1).toLowerCase();
	parent.AttachmentOperater(filename,e,filesize);
	//alert(filename+'上传成功');
	window.location.reload();
}
</script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="SWFUpload">
        <Jumbot:FlashUpload ID="flashUpload" runat="server" FileTypeDescription="*.*" UploadPage="/FlashUpload.asx"
            Args="" UploadFileSizeLimit="1800000">
        </Jumbot:FlashUpload>
    </div>
    </form>
</body>
</html>
