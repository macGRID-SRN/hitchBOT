﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using hitchbotAPI.Helpers;

namespace hitchbotAPI.ApproveImagesPages
{
    public partial class LocationPortal : System.Web.UI.Page
    {
        Models.Project Project;
        Models.hitchBOT HitchBOT;
        Models.Password user;
        hitchbotAPI.Helpers.Location.GoogleMapsBuilder GoogleMapsBuilder;

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void reload_JS(object sender, EventArgs e)
        {
            using (var db = new Models.Database())
            {
                // Get current user, HitchBOT and Project
                this.user = (Models.Password)Session["New"];
                this.HitchBOT = db.hitchBOTs.First(l => l.ID == this.user.hitchBOT.ID);
                this.Project = db.Projects.Where(p => p.EndTime == null).ToArray().Last();

                // Create new builder to build JS files
                GoogleMapsBuilder = new Helpers.Location.GoogleMapsBuilder(this.HitchBOT.ID, this.Project.ID, this.user.ID);

                // Build JS using GoogleMapsBuilder
                GoogleMapsBuilder.BuildJsAndUpload();
            }
        }
    }

}