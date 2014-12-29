<%@ Page Title="Landing Page" Language="C#" MasterPageFile="~/ApproveImagesPages/Shared/PageWithHeader.master" AutoEventWireup="true" CodeBehind="NewLandingPage.aspx.cs" Inherits="hitchbotAPI.ApproveImagesPages.NewLandingPage" %>

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
        <ul class="list-group">
            <li class="list-group-item">
                <h4 class="list-grou-item-heading">Test new item
                </h4>
                <p class="list-group-item-text">
                    V nice.
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
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="endScripts" runat="server">
</asp:Content>
