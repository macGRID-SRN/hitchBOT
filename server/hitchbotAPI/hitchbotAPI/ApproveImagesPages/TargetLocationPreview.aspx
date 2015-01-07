<%@ Page Title="" Language="C#" MasterPageFile="~/ApproveImagesPages/Shared/PageWithHeader.master" AutoEventWireup="true" CodeBehind="TargetLocationPreview.aspx.cs" Inherits="hitchbotAPI.ApproveImagesPages.TargetLocationPreview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StyleContent" runat="server">
    <style type="text/css">
        html, body, #map-canvas {
            height: 100%;
            margin: 0;
            padding: 0;
        }

        .map-wrapper {
            height: 500px;
        }
    </style>
    <script type="text/javascript"
        src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCV-d9jbUEWesRS6LRsWCWZpKZdOmXCUWA">
    </script>
    <asp:Literal ID="coordsOutput" runat="server">

    </asp:Literal>
    <script type="text/javascript">

        var map;

        function initialize() {
            var myLatlng = new google.maps.LatLng(50.983027, 10.445880);

            var mapOptions = {
                center: myLatlng,
                zoom: 5
            };

            map = new google.maps.Map(document.getElementById('map-canvas'),
                mapOptions);

            for (i = 0; i < coords.length; i++) {
                var tempMarker = new google.maps.Marker({
                    position: coords[i].coord,
                    map: map,
                    draggable: false,
                    title: "Drag me!"
                });

                var circleOptions = {
                    strokeColor: '#0000FF',
                    strokeOpacity: 0.8,
                    strokeWeight: 2,
                    fillColor: '#0000FF',
                    fillOpacity: 0.35,
                    map: map,
                    center: coords[i].coord,
                    radius: coords[i].radius * 1000
                };

                var TempCircle = new google.maps.Circle(circleOptions);
            }
        }
        google.maps.event.addDomListener(window, 'load', initialize);
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <div class="container">
        <div class="jumbotron">
            <h2>Wikipedia Entries</h2>
            <div class="map-wrapper">
                <div id="map-canvas">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="endScripts" runat="server">
</asp:Content>
