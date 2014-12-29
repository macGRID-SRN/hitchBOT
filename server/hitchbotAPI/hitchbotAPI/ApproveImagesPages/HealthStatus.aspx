<%@ Page Title="" Language="C#" MasterPageFile="~/ApproveImagesPages/Shared/PageWithHeader.master" AutoEventWireup="true" CodeBehind="HealthStatus.aspx.cs" Inherits="hitchbotAPI.ApproveImagesPages.HealthStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StyleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <div class="container">
        <div class="jumbotron">
            <h2>hitchBOT Health Status</h2>
            <p>&nbsp;</p>
            <dl class="dl-horizontal">
                <dt>
                    <asp:Label ID="lblLastCheckin" runat="server" Text="Last Check-In Time: "></asp:Label>
                </dt>
                <dd>
                    <asp:Label ID="lblActualLastCheckin" runat="server"></asp:Label>
                </dd>
                <dt>
                    <asp:Label ID="lblBatteryStat" runat="server" Text="Battery Percentage: "></asp:Label>
                </dt>
                <dd>
                    <asp:Label ID="lblBatteryStatActual" runat="server"></asp:Label>
                </dd>
                <dt>
                    <asp:Label ID="lblBatteryTemp" runat="server" Text="Battery Temperature: "></asp:Label>
                </dt>
                <dd>
                    <asp:Label ID="lblBatteryTempActual" runat="server"></asp:Label>
                </dd>
                <dt>
                    <asp:Label ID="lblIsPluggedIn" runat="server" Text="Charging: "></asp:Label>
                </dt>
                <dd>
                    <asp:Label ID="lblIsPluggedInActual" runat="server"></asp:Label>
                </dd>
            </dl>

        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="endScripts" runat="server">
</asp:Content>
