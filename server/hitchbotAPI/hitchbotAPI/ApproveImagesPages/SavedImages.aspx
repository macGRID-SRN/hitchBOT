<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SavedImages.aspx.cs" Inherits="hitchbotAPI.ApproveImagesPages.SavedImages" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="LandingPage.aspx">Back</asp:HyperLink>
        <br />
        Time Taken is now correct! - Images are now sorted by most recently saved first.<br />
    
        <asp:Label ID="Label1" runat="server" Text="Notice: It has come to my attention that the &quot;Time Taken&quot; is incorrect. This is actually the time the server received the image." Font-Strikeout="True"></asp:Label>
    
        <asp:Table ID="tableViewImage" runat="server">
        </asp:Table>
    
    </div>
    </form>
</body>
</html>
