using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
            using(var db = new Models.Database())
            {
                var query = (from f in db.Faces
                             where f.ID == face.ID
                             select f).FirstOrDefault();
                query.Panels = face.Panels;
                query.Approved = approved;
                db.SaveChanges();
            }
        }
    }


}