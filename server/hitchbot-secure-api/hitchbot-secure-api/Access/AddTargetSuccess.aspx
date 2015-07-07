<%@ Page Title="" Language="C#" MasterPageFile="~/Access/Shared/PageWithHeader.master" AutoEventWireup="true" CodeBehind="AddTargetSuccess.aspx.cs" Inherits="hitchbot_secure_api.Access.AddTargetSuccess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StyleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <div class="container">
        <div class="jumbotron">
            <h2>Your entry was successfully added!</h2>
            <div class="row">
                <div class="col-md-3">
                    <form action="AddTargetLocation.aspx">
                        <button type="submit" class="btn btn-lg btn-primary">Add Another (Circle)</button>
                    </form>
                </div>

                <div class="col-md-3">
                    <form action="AddTargetLocationPolygon.aspx">
                        <button type="submit" class="btn btn-lg btn-primary">Add Another (Polygon)</button>
                    </form>
                </div>
                <div class="col-md-2">
                    <form action="PreviewMapCoverage.aspx">
                        <button type="submit" class="btn btn-lg">View Entries</button>
                    </form>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="endScripts" runat="server">
</asp:Content>
