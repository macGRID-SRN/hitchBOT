<%@ Page Title="" Language="C#" MasterPageFile="~/Access/Shared/PageWithHeader.master" AutoEventWireup="true" CodeBehind="ViewStats.aspx.cs" Inherits="hitchbot_secure_api.Access.ViewStats" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StyleContent" runat="server">
    <style>
        .small-info {
            font-weight: lighter !important;
            font-size: small !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <div class="container">
        <div class="jumbotron">
            <h1>Stats</h1>
            <p>
                Last Update Received from Tablet: <%=mostRecentUpdate %>
            </p>
            <p>
                Last Satellite GPS Movement Detected: <%=hitchBOTSpotUpdate %>
            </p>
            <p>
                Tablet Battery: <%=hitchBOTBatteryLevel %>
            </p>
            <p>
                Tablet Plugged In: <%=hitchBOTCharging %>
            </p>
            <p>
                Tablet Battery Temperature: <%=hitchBOTBatteryTemp %>
            </p>
            <p class="small-info">
            If the Tablet is not plugged in and the Tablet battery percentage is less than 90% it suggests the main battery is dead. (face not on, speakers powered down)
            </p>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="endScripts" runat="server">
</asp:Content>
