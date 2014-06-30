<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApproveImageLogin.aspx.cs" Inherits="hitchbotAPI.ApproveImage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            font-size: x-large;
        }
        .auto-style2 {
            width: 100%;
        }
        .auto-style4 {
            width: 436px;
        }
        .auto-style5 {
            width: 47px;
        }
        .auto-style6 {
            width: 96px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="auto-style1">
    
        Approve Images &amp; Tweets</div>
        <table class="auto-style2">
            <tr>
                <td class="auto-style5">Username</td>
                <td class="auto-style6">
                    <asp:TextBox ID="userName" runat="server" Width="187px"></asp:TextBox>
                </td>
                <td class="auto-style4">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style5">Password</td>
                <td class="auto-style6">
                    <asp:TextBox ID="passWord" runat="server" Width="187px" TextMode="Password"></asp:TextBox>
                </td>
                <td class="auto-style4">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style5">
                    <asp:Label ID="lblError" runat="server" ForeColor="#FF3300"></asp:Label>
                </td>
                <td class="auto-style6">
                    <asp:Button ID="SubmitPassword" runat="server" OnClick="Button1_Click" Text="Submit" Width="193px" />
                </td>
                <td class="auto-style4">
                    &nbsp;</td>
            </tr>
        </table>
    </form>
</body>
</html>
