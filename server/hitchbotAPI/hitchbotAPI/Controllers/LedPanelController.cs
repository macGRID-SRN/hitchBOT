using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hitchbotAPI.Controllers
{
    public class LedPanelController
    {
        public bool addFace(Models.Face face)
        {
            using (var db = new Models.Database())
            {
                foreach(var panel in face.Panels)
                {
                    addPanel(panel);
                    db.LedPanels.Attach(panel);
                }
                db.Faces.Add(face);
                db.SaveChanges();
            }
            return true;
        }

        public bool addPanel(Models.LedPanel panel)
        {
            using (var db = new Models.Database())
            {
                db.LedPanels.Add(panel);
                db.SaveChanges();
            }
            return true;
        }
    }
}