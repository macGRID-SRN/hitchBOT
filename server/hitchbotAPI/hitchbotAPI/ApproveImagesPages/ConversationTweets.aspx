<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConversationTweets.aspx.cs" Inherits="hitchbotAPI.ApproveImagesPages.ConversationTweets" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <h3>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="LandingPage.aspx">Back</asp:HyperLink>
            <br />
            <br />
            Approving Tweets from Speech said by hitchBOT.<br />
        </h3>
    
        <asp:Table ID="MainTable" runat="server">
        </asp:Table>
        
    </div>
    </form>
</body>
</html>
