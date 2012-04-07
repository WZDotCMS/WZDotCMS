<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="JumboTCMS.WebFile.Test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <script type="text/javascript" src="/_data/languages/cn.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
    
    </div>
    </form>
    <script>alert(JumbotLng.Home);</script>
</body>
</html>
