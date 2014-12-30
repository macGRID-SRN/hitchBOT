<%@ Page Title="" Language="C#" MasterPageFile="~/ApproveImagesPages/Shared/PageWithHeader.master" AutoEventWireup="true" CodeBehind="AddTargetLocations.aspx.cs" Inherits="hitchbotAPI.ApproveImagesPages.AddTargetLocations" %>

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
    <script type="text/javascript">
        function UpdateCoordsOnPage(latlng) {

            $(".latValue").text(latlng.lat());
            $(".lngValue").text(latlng.lng());
        }

        function initialize() {
            var myLatlng = new google.maps.LatLng(50.983027, 10.445880);
            
            var mapOptions = {
                center: myLatlng,
                zoom: 5
            };

            var map = new google.maps.Map(document.getElementById('map-canvas'),
                mapOptions);

            var marker = new google.maps.Marker({
                position: myLatlng,
                map: map,
                draggable: true,
                title: "Drag me!"
            });

            google.maps.event.addListener(marker, 'drag', function () {
                UpdateCoordsOnPage(marker.getPosition());
            });
        }
        google.maps.event.addDomListener(window, 'load', initialize);
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <div class="container">
        <div class="jumbotron">
            <h2>Add Wikipedia Entries</h2>
            <div class="map-wrapper">
                <div id="map-canvas">
                </div>
            </div>
            <p>&nbsp;</p>
            <dl class="dl-horizontal">
                <dt>
                    <asp:Label ID="lblLat" runat="server" Text="Latitude: "></asp:Label>
                </dt>
                <dd>
                    <asp:Label ID="lblLatValue" runat="server" class="latValue"></asp:Label>
                </dd>
                <dt>
                    <asp:Label ID="lblLong" runat="server" Text="Longitude: "></asp:Label>
                </dt>
                <dd>
                    <asp:Label ID="lblLongValue" runat="server" class="lngValue"></asp:Label>
                </dd>
            </dl>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="endScripts" runat="server">
</asp:Content>
