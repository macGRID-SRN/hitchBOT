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
    
        Approve and Tweet Image</div>
        <table class="auto-style2">
            <tr>
                <td class="auto-style3">Tweet Text:<br />
                    <asp:TextBox ID="TextBox1" runat="server" Height="138px" Width="325px"></asp:TextBox>
                    <br />
                    <br />
                    Time to Tweet:<br />
                    <asp:Calendar ID="datePicker" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" OnSelectionChanged="datePicker_SelectionChanged" Width="200px">
                        <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                        <NextPrevStyle VerticalAlign="Bottom" />
                        <OtherMonthDayStyle ForeColor="#808080" />
                        <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                        <SelectorStyle BackColor="#CCCCCC" />
                        <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                        <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <WeekendDayStyle BackColor="#FFFFCC" />
                    </asp:Calendar>
                    <br />
                    <asp:TextBox ID="time24hrBOX" runat="server" OnTextChanged="time24hrBOX_TextChanged">HH:mm:ss</asp:TextBox>
                    <br />
                    <asp:Label ID="selectedTimePreview" runat="server"></asp:Label>
                    <br />
                    (24 Hour Time - UTC) <br />
                    <br />
                    <asp:Button ID="Approve" runat="server" OnClick="Approve_Click" Text="Approve and Tweet" Width="151px" style="height: 26px" Height="35px" />
                    <asp:Button ID="Deny" runat="server" Text="Deny" Width="130px" OnClick="Deny_Click" Height="26px" />
                    <br />
                    <br />
                    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                    <br />
                    <br />
                    Note: Images are never deleted. If you click deny they will not show up again in the interface nor be tweeted.</td>
                <td class="auto-style4">
                    <asp:Image ID="imagePreview" runat="server" Height="443px" Width="601px" ImageAlign="TextTop" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
