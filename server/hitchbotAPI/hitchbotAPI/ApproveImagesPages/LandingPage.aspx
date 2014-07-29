<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LandingPage.aspx.cs" Inherits="hitchbotAPI.ApproveImagesPages.LandingPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="font-size: large">
    
        Welcome to the HitchBotAPI Landing Page! Below you will find all the pages of interest.<br />
        <br />
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="ViewImages.aspx">Image Gallery</asp:HyperLink>
        <br />
        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="SavedImages.aspx">Saved Images</asp:HyperLink>
        <br />
        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="ConversationTweets.aspx">Send Conversation Tweets</asp:HyperLink>
    
        <br />
        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="HealthStatus.aspx">hitchBOT Health Status</asp:HyperLink>
        <br />
        <br />
        <hr />
        What&#39;s New?<asp:BulletedList ID="BulletedList1" runat="server">
            <asp:ListItem>Tweet things hitchbot has said</asp:ListItem>
            <asp:ListItem>Back link on each page.</asp:ListItem>
            <asp:ListItem>You can now save images from the gallery!</asp:ListItem>
            <asp:ListItem Value="See live battery info and other stats directly from hitchBOT!">See live battery info and other stats directly from hitchBOT</asp:ListItem>
            <asp:ListItem>This Bullet List!</asp:ListItem>
        </asp:BulletedList>
        (Still in Development)<br />
        <br />
        Location Portal<br />
        Removed Images<br />
        Target Location Tweeting</div>
    </form>
</body>
</html>
