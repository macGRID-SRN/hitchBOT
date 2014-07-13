<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Directory.aspx.cs" Inherits="hitchbotAPI.Directory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <br />
        Tools Available:<asp:BulletedList ID="BulletedList1" runat="server" Font-Size="Large" OnClick="BulletedList1_Click">
            <asp:ListItem>Image Moderation</asp:ListItem>
            <asp:ListItem>LED Display Designer</asp:ListItem>
        </asp:BulletedList>
        <br />
    
    </div>
    </form>
</body>
</html>
