<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewImage.aspx.cs" Inherits="hitchbotAPI.ViewImage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

        .auto-style1 {
            font-size: x-large;
        }
        .auto-style2 {
            width: 50%;
        }
        .auto-style3 {
            width: 448px;
        }
        .auto-style4 {
            width: 358px;
        }
    </style>
</head>
<body>
    <form id="form2" runat="server">
    <div class="auto-style1">
    
        Tweet Image</div>
        <table class="auto-style2">
            <tr>
                <td class="auto-style3">Tweet Text:<br />
                    <asp:TextBox ID="TextBox1" runat="server" Height="138px" Width="325px"></asp:TextBox>
                    <br />
                    <asp:Button ID="Approve" runat="server" OnClick="Approve_Click" Text="Approve and Tweet" Width="151px" style="height: 26px" Height="35px" />
                    <asp:Button ID="Deny" runat="server" Text="Deny" Width="130px" OnClick="Deny_Click" Height="26px" />
                    <br />
                    Note: Images are not deleted. If you click deny they will not show up again in the interface nor be tweeted</td>
                <td class="auto-style4">
                    <asp:Image ID="imagePreview" runat="server" Height="336px" Width="360px" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
