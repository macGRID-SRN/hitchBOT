<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DynamicMap.aspx.cs" Inherits="hitchbotAPI.ApproveImagesPages.DynamicMap" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
      html, body, #map-canvas { height: 100%; margin: 0; padding: 0;}
    </style>
    <script type="text/javascript"
      src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCV-d9jbUEWesRS6LRsWCWZpKZdOmXCUWA">
    </script>
    <asp:Literal runat="server" id="jsDataLocation" EnableViewState="false" />
</head>
<body>
    <div id="map-canvas"></div>
    <form id="form1" runat="server"></form>
</body>
</html>
