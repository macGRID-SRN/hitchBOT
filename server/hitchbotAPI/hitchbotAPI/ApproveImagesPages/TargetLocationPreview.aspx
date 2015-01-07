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
        var markerCircle;
        var markerCircleRadius;
        var marker;
        var map;
        //updates the displayed coords to work with 
        function UpdateCoordsOnPage(latlng) {

            markerCircle.setCenter(latlng);
            $(".latValue").val(latlng.lat());
            $(".lngValue").val(latlng.lng());
        }

        function UpdateCoordsOnMap() {

            var lat = parseFloat($(".latValue").val());
            var lng = parseFloat($(".lngValue").val());
            var latlng = new google.maps.LatLng(lat, lng);
            markerCircle.setCenter(latlng);
            marker.setPosition(latlng);
        }

        function initialize() {
            var myLatlng = new google.maps.LatLng(50.983027, 10.445880);

            var coords = [];

            var mapOptions = {
                center: myLatlng,
                zoom: 5
            };

            map = new google.maps.Map(document.getElementById('map-canvas'),
                mapOptions);

            marker = new google.maps.Marker({
                position: myLatlng,
                map: map,
                draggable: true,
                title: "Drag me!"
            });
        }
        google.maps.event.addDomListener(window, 'load', initialize);
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="endScripts" runat="server">
</asp:Content>
