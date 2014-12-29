<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Unauthorized.aspx.cs" Inherits="hitchbotAPI.Unauthorized" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="font-size: x-large">
    <form id="form1" runat="server">
    <div>You are not authorized to view this page.</div>
        <asp:HyperLink ID="HyperLink1" runat="server" href="ApproveImageLogin.aspx">Sign In</asp:HyperLink>
    </form>
</body>
</html>
