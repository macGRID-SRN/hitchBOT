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

        .wiki-form {
            font-size: 16px;
        }

        .help-block2 {
            font-size: 14px;
        }

        #inputRaduisValue, #inputRadius {
            max-width: 50px;
        }

        .radius-select {
            max-width: 100px;
        }
    </style>
    <script type="text/javascript"
        src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCV-d9jbUEWesRS6LRsWCWZpKZdOmXCUWA">
    </script>
    <script type="text/javascript">
        var markerCircle;
        var markerCircleRadius;
        var marker;
        var map;
        //updates the displayed coords to work with 
        function UpdateCoordsOnPage(latlng) {

            markerCircle.setCenter(latlng);
            $(".latValue").text(latlng.lat());
            $(".lngValue").text(latlng.lng());
        }

        function setRadiusListener() {


        }

        function initialize() {
            var myLatlng = new google.maps.LatLng(50.983027, 10.445880);

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

            google.maps.event.addListener(marker, 'drag', function () {
                UpdateCoordsOnPage(marker.getPosition());
            });

            var circleOptions = {
                strokeColor: '#0000FF',
                strokeOpacity: 0.8,
                strokeWeight: 2,
                fillColor: '#0000FF',
                fillOpacity: 0.35,
                map: map,
                center: myLatlng,
                radius: 15000
            };

            markerCircle = new google.maps.Circle(circleOptions);

            UpdateCoordsOnPage(marker.getPosition());
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

            <form class="wiki-form" runat="server">
                <%-- This code was borrowed from http://www.bootply.com/katie/9CvIygzob8 --%>
                <div class="form-group">
                    <label for="inputRadius">Select A Radius</label>

                    <div class="input-group radius-select">
                        <input id="inputRadiusValue" type="text" class="form-control inputRadiusValue" aria-label="..." size="6" maxlength="3" runat="server">
                        <div class="input-group-btn" id="inputRadius">
                            <a class="btn btn-default dropdown-toggle btn-select2" data-toggle="dropdown" href="#">Select<span class="caret"></span></a>
                            <ul class="dropdown-menu">
                                <li><a href="#">5 km</a></li>
                                <li><a href="#">10 km</a></li>
                                <li><a href="#">15 km</a></li>
                                <li><a href="#">25 km</a></li>
                                <li><a href="#">50 km</a></li>
                                <li><a href="#">75 km</a></li>
                                <li><a href="#">100 km</a></li>
                                <li><a href="#">125 km</a></li>
                                <li><a href="#">150 km</a></li>
                                <li><a href="#">175 km</a></li>
                                <li><a href="#">200 km</a></li>
                            </ul>
                        </div>
                    </div>
                    <p class="help-block help-block2">
                        <strong>Note: </strong>Due to the roundness of the earth and map projections,
                        <br />
                        ensure the selected radius is larger than the intended area.
                    </p>
                </div>

                <div class="form-group">
                    <label for="inputName">Location Name</label>
                    <input type="text" class="form-control" id="inputName" placeholder="Enter Name of Location" runat="server">
                </div>
                <div class="form-group">
                    <label for="exampleInputPassword1">Wikipedia Entries (One Per Line)</label>
                    <textarea class="form-control" id="inputWiki1" placeholder="Wikipedia Entries" rows="5" runat="server"></textarea>
                </div>
                <div class="form-group">
                    <asp:Button ID="buttonSubmit" runat="server" Text="Submit" class="btn btn-success" OnClick="buttonSubmit_Click" />
                </div>

                <input type="hidden" id="circleRadiusValue" class="circleRadiusValue" runat="server" />

            </form>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="endScripts" runat="server">
    <script>
        $(".dropdown-menu li a").click(function () {
            var selText = $(this).text();
            $(this).parents('.input-group-btn').find('.dropdown-toggle').html(selText + ' <span class="caret"></span>');
            markerCircleRadius = parseInt(selText.split(" ")[0]);

            markerCircle.setRadius(markerCircleRadius * 1000);

            var hiddenRadiusVal = $('.inputRadiusValue');
            hiddenRadiusVal.val(markerCircleRadius);
        });

        $(".inputRadiusValue").bind("change paste keyup", function () {

            var value = parseInt($(this).val());
            markerCircle.setRadius(value * 1000);

            $('.input-group-btn').find('.dropdown-toggle').html(value.toString() + ' km<span class="caret"></span>');
        });

        $("#btnSearch").click(function () {
            alert($('.btn-select').text() + ", " + $('.btn-select2').text());
        });

    </script>
</asp:Content>
