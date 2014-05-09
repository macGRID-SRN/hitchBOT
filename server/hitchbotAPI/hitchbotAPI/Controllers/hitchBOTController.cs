using hitchbotAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace hitchbotAPI.Controllers
{
    public class hitchBOTController : ApiController
    {
        /// <summary>
        /// Given the ID of a hitchBOT instance, this will return it's information.
        /// </summary>
        /// <param name="ID">ID of the requested hitchBOT information.</param>
        /// <returns>The hitchBOT instance requested.</returns>
        [HttpGet]
        public hitchBOT GetHitchbot(int ID)
        {
            using (var db = new Database())
            {
                return db.hitchBOTs.Single(h => h.ID == ID);
            }
        }

        [HttpPost]
        public bool AddHitchBotToProject(int ProjectID, string HitchbotName)
        {
            using (var db = new Database())
            {
                var project = db.Projects.Single(p => p.ID == ProjectID);
                var newHitchBot = new hitchBOT
                {
                    CreationTime = DateTime.UtcNow,
                    Name = HitchbotName
                };
                project.hitchBOTs.Add(newHitchBot);
                db.SaveChanges();
            }
            return true;
        }
    }
}