<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LocationPortalOld.aspx.cs" Inherits="hitchbotAPI.ApproveImagesPages.LocationPortalOld" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            font-size: large;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h3>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="LandingPage.aspx">Back</asp:HyperLink>
            <br />
            Most recent (exact) location. Force Map updates.</h3>
    <div>
    
        <asp:Label ID="lblLocation" runat="server" CssClass="auto-style1" Text="Current Location: "></asp:Label>
        <br class="auto-style1" />
        <asp:Label ID="lblGmapsOutput" runat="server" CssClass="auto-style1" Text="Region: "></asp:Label>
        <br class="auto-style1" />
        <asp:Label ID="lblVelocity" runat="server" CssClass="auto-style1" Text="Velocity: "></asp:Label>
        <br class="auto-style1" />
        <asp:Label ID="lblTime" runat="server" CssClass="auto-style1" Text="Time Taken: "></asp:Label>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Force Map Generation" />
        <br />
        <asp:Label ID="Label1" runat="server" Text="(override auto generation)"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblGen" runat="server" style="font-size: large" Text="Time Generated: "></asp:Label>
        <br />
        <br />
        <asp:Image ID="Image1" runat="server" ImageUrl="http://hitchbotapi.azurewebsites.net/api/Location?HitchBotID=3" />
    
    </div>
    </form>
</body>
</html>
