<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DynamicMap.aspx.cs" Inherits="hitchbotAPI.ApproveImagesPages.DynamicMap" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" runat="server" id="HtmlMasterTag">
<head runat="server">
    <title></title>
    <style type="text/css">
      html, body, #map-canvas { height: 100%; margin: 0; padding: 0;}
    </style>
    <asp:Literal runat="server" ID="gmapsString" EnableViewState ="false" />
    <asp:Literal runat="server" id="jsDataLocation" EnableViewState="false" />
</head>
<body>
    <div id="map-canvas"></div>
    <form id="form1" runat="server"></form>
</body>
</html>
