<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="JumboTCMS.WebFile.API._99bill._default" %>

<!doctype html public "-//w3c//dtd html 4.0 transitional//en" >
<html>
<head>
    <title>使用快钱支付</title>
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
</head>
<body>
<div style="display:none;">
    <div align="center">
        <table width="259" border="0" cellpadding="1" cellspacing="1" bgcolor="#CCCCCC">
            <tr bgcolor="#FFFFFF">
                <td width="80">
                    支付方式:
                </td>
                <td>
                    快钱[99bill]
                </td>
            </tr>
            <tr bgcolor="#FFFFFF">
                <td>
                    订单编号:
                </td>
                <td>
                    <asp:Label ID="Lab_orderId" runat="Server" />
                </td>
            </tr>
            <tr bgcolor="#FFFFFF">
                <td>
                    订单金额:
                </td>
                <td>
                    <asp:Label ID="Lab_orderAmount" runat="Server" />
                </td>
            </tr>
            <tr bgcolor="#FFFFFF">
                <td>
                    支付人:
                </td>
                <td>
                    <asp:Label ID="Lab_payerName" runat="Server" />
                </td>
            </tr>
            <tr bgcolor="#FFFFFF">
                <td>
                    商品名称:
                </td>
                <td>
                    <asp:Label ID="Lab_productName" runat="Server" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    <div align="center" style="font-size:12px; font-weight: bold; color: red;">
        <form id="kqPay" name="kqPay" method="post" action="https://www.99bill.com/gateway/recvMerchantInfoAction.htm" />
        <input type="hidden" id="inputCharset" runat="server" />
        <input type="hidden" id="bgUrl" runat="server" />
        <input type="hidden" id="version" runat="server" />
        <input type="hidden" id="language" runat="server" />
        <input type="hidden" id="signType" runat="server" />
        <input type="hidden" id="signMsg" runat="server" />
        <input type="hidden" id="merchantAcctId" runat="server" />
        <input type="hidden" id="payerName" runat="server" />
        <input type="hidden" id="payerContactType" runat="server" />
        <input type="hidden" id="payerContact" runat="server" />
        <input type="hidden" id="orderId" runat="server" />
        <input type="hidden" id="orderAmount" runat="server" />
        <input type="hidden" id="orderTime" runat="server" />
        <input type="hidden" id="productName" runat="server" />
        <input type="hidden" id="productNum" runat="server" />
        <input type="hidden" id="productId" runat="server" />
        <input type="hidden" id="productDesc" runat="server" />
        <input type="hidden" id="ext1" runat="server" />
        <input type="hidden" id="ext2" runat="server" />
        <input type="hidden" id="payType" runat="server" />
        <input type="hidden" id="redoFlag" runat="server" />
        <input type="hidden" id="pid" runat="server" />
        <input type="submit" id="submit2" value="确认支付" />
        </form>
    </div>
</div>
<script type="text/javascript">
document.forms[0].submit();
</script>
</body>
</html>
