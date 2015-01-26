<%@ Page Title="" Language="C#" MasterPageFile="~/ApproveImagesPages/Shared/PageWithHeader.master" AutoEventWireup="true" CodeBehind="AddMapMarkerSuccess.aspx.cs" Inherits="hitchbotAPI.ApproveImagesPages.AddMapMarkerSuccess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StyleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <div class="container">
        <div class="jumbotron">
            <h2>Your entry was successfully added!</h2>
            <div class="row">
                <div class="col-md-2">
                    <form action="AddMapMarker.aspx">
                        <button type="submit" class="btn btn-lg btn-primary">Add Another</button>
                    </form>
                </div>
                <div class="col-md-2">
                    <form action="ViewMap.aspx">
                        <button type="submit" class="btn btn-lg">View Map (May not be updated yet)</button>

                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="endScripts" runat="server">
</asp:Content>
