﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LedPanelPreview.aspx.cs" Inherits="hitchbotAPI.ApproveImagesPages.LedPanelPreview" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="LedPanelDesigner.aspx">Back</asp:HyperLink>
    <div style="text-align:center">
    
        <asp:Table ID="Table1" runat="server">
        </asp:Table>
    
    </div>
        <div style="height:50px"></div>
        <div style="text-align:center">

            <asp:Button ID="Button1" runat="server" Text="Submit" />

        </div>
    </form>
</body>
</html>
