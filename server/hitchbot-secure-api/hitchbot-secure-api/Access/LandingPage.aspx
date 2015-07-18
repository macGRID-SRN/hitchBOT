<%@ Page Title="" Language="C#" MasterPageFile="~/Access/Shared/PageWithHeader.master" AutoEventWireup="true" CodeBehind="LandingPage.aspx.cs" Inherits="hitchbot_secure_api.Access.LandingPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="StyleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
       <div class="container">
        <div class="jumbotron">
            <h1>Hi there!</h1>
            <p>
                Welcome to the new API! You just might like it here.
            </p>
        </div>
    </div>
    <div class="container">
        <div class="panel panel-default">
            <!-- Default panel contents -->
            <div class="panel-heading"><h3>What's new?</h3></div>
            <ul class="list-group">
                <li class="list-group-item">
                    <h4 class="list-group-item-heading">Image Gallery
                    </h4>
                    <p class="list-group-item-text">
                        View the latest pictures from hitchBOT.
                    </p>
                </li>
                <li class="list-group-item">
                    <h4 class="list-group-item-heading">Review Entered Cleverscript Content!
                    </h4>
                    <p class="list-group-item-text">
                        Ready for use. You can use this tool to view Cleverscript sentences previously entered.
                    </p>
                </li>
                <li class="list-group-item">
                    <h4 class="list-group-item-heading">Geo-relevant Cleverscript Entries!
                    </h4>
                    <p class="list-group-item-text">
                        Ready for use. You can use this tool to enter Cleverscript sentences and info that will be read by hitchBOT itself!
                    </p>
                </li>
                <li class="list-group-item">
                    <h4 class="list-group-item-heading">Website Styling!
                    </h4>
                    <p class="list-group-item-text">
                        V extra pretty.
                    </p>
                </li>
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="endScripts" runat="server">
</asp:Content>
