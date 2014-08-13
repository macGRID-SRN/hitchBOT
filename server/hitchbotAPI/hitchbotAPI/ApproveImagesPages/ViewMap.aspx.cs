using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hitchbotAPI.ApproveImagesPages
{
    public partial class ViewMap : System.Web.UI.Page
    {
        protected void Button1_Click(object sender, EventArgs e)
        {
            string mapScript = "function init(){var myLatlng = new google.maps.LatLng(-25.363882,131.044922);var mapOptions = {zoom: 4,center: myLatlng};var map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);} init();";
            innerScript.Text = "<script type='text/javascript'>" + mapScript + "</script>";
        }
    }
}