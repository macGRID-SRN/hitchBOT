using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace hitchbotAPI.Controllers
{
    public class LedPanelController
    {
        public static bool addFace(Models.Face face)
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

        public static bool addPanel(Models.LedPanel panel)
        {
            using (var db = new Models.Database())
            {
                db.LedPanels.Add(panel);
                db.SaveChanges();
            }
            return true;
        }

        public static Models.Face getLastFace(Models.Password user)
        {
            List<Models.Face> face = new List<Models.Face>();
            using (var db = new Models.Database())
            {
                var query = from f in db.Faces.Include("UserAccount").Include("Panels.Rows")
                            where f.Approved == false && f.UserAccount.ID == user.ID
                            orderby f.TimeAdded
                            select f;
                foreach(var item in query)
                {
                    face.Add(item);
                }
            }
            return face[0];
        }

        public static void updateFace(Models.Face face, bool approved)
        {
            updatePanel(face.Panels.ToList()[0]);
            using(var db = new Models.Database())
            {
                var original = db.Faces.Find(face.ID);

                if (original != null)
                {
                    original.Name = face.Name;
                    original.Description = face.Description;
                    original.Panels = face.Panels;
                    original.TimeAdded = face.TimeAdded;
                    original.Approved = approved;
                    original.UserAccount = face.UserAccount;
                    db.SaveChanges();
                }
            }
        }

        public static void updatePanel(Models.LedPanel panel)
        {
            using (var db = new Models.Database())
            {
                var original = db.LedPanels.Find(panel.ID);

                if (original != null)
                {
                    original.Rows = panel.Rows;
                    original.TimeAdded = panel.TimeAdded;
                    db.SaveChanges();
                }
            }
        }
    }


}