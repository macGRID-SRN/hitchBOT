<%@ Page Title="Landing Page" Language="C#" MasterPageFile="~/ApproveImagesPages/Shared/PageWithHeader.master" AutoEventWireup="true" CodeBehind="LandingPage.aspx.cs" Inherits="hitchbotAPI.ApproveImagesPages.LandingPage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="StyleContent" runat="server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    <div class="container">
        <div class="jumbotron">
            <h1>Hi there!</h1>
            <p>
                Welcome to the new API! Colin finally got around to making this thing look nice. (well, kind of..)
            </p>
            <p>
                Take a peek at what is new below!
            </p>
        </div>
    </div>
    <div class="container">
        <div class="panel panel-default">
            <!-- Default panel contents -->
            <div class="panel-heading"><h3>What's new?</h3></div>
            <ul class="list-group">
                <li class="list-group-item">
                    <h4 class="list-grou-item-heading">Geo-relevant Wikipedia Entries!
                    </h4>
                    <p class="list-group-item-text">
                        Almost ready for use. You can use this tool to enter wikipedia articles and info that will be read by hitchBOT itself!
                    </p>
                </li>
                <li class="list-group-item">
                    <h4 class="list-grou-item-heading">Item 2
                    </h4>
                    <p class="list-group-item-text">
                        V extra nice.
                    </p>
                </li>
            </ul>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="endScripts" runat="server">
</asp:Content>
