<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HealthStatus.aspx.cs" Inherits="hitchbotAPI.ApproveImagesPages.HealthStatus" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <p>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="LandingPage.aspx">Back</asp:HyperLink>
        </p>
        <h2>
            <asp:Label ID="Label1" runat="server" Text="hitchBOT Health Status"></asp:Label>
        </h2>
        <p>
            &nbsp;</p>
        <h3>
            <asp:Label ID="lblLastCheckin" runat="server" Text="Last Check-In Time: "></asp:Label>
        </h3>
        <h3>
            <asp:Label ID="lblBatteryStat" runat="server" Text="Battery Percentage: "></asp:Label>
        </h3>
        <h3>
            <asp:Label ID="lblBatteryTemp" runat="server" Text="Battery Temperature: "></asp:Label>
        </h3>
        <h3>
            <asp:Label ID="lblIsPluggedIn" runat="server" Text="Plugged In: "></asp:Label>
        </h3>
    
    </div>
    </form>
</body>
</html>
