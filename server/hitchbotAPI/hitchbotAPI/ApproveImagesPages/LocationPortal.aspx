<%@ Page Title="" Language="C#" MasterPageFile="~/ApproveImagesPages/Shared/PageWithHeader.master" AutoEventWireup="true" CodeBehind="LocationPortal.aspx.cs" Inherits="hitchbotAPI.ApproveImagesPages.LocationPortal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="StyleContent" runat="server">
    <style type="text/css">

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <div class="container">
        <div class="jumbotron">
            <h2>Most Recent Location</h2>
            
            <br />
            <iframe seamless="seamless" height=600 width=850 src="DynamicMap.aspx?hbID=3" frameBorder="0"></iframe>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="endScripts" runat="server">
</asp:Content>
