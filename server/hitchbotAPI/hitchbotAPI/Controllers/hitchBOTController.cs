using hitchbotAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace hitchbotAPI.Controllers
{
    /// <summary>
    /// For all things related to an instance of HitchBot.
    /// </summary>
    public class hitchBOTController : ApiController
    {
        /// <summary>
        /// Given the ID of a hitchBOT instance, this will return it's information.
        /// </summary>
        /// <param name="ID">ID of the requested hitchBOT.</param>
        /// <returns>The hitchBOT instance requested.</returns>
        [HttpGet]
        public hitchBOT GetHitchbot(int ID)
        {
            using (var db = new Database())
            {
                return db.hitchBOTs.Single(h => h.ID == ID);
            }
        }

        /// <summary>
        /// Call this method when adding a new HitchBot to an existing project.
        /// </summary>
        /// <param name="ProjectID">ID of the Project the HitchBot will be added to.</param>
        /// <param name="HitchbotName">The name of the HitchBot that will be created.</param>
        /// <returns>Success.</returns>
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

        /// <summary>
        /// Given the ID of a HitchBot instance, this will return it's most recent Location.
        /// </summary>
        /// <param name="HitchBotID">ID of the requested hitchBOT.</param>
        /// <returns>The HitchBots most recent Location.</returns>
        [HttpGet]
        public Location GetMostRecentLocation(int HitchBotID)
        {
            using (var db = new Database())
            {
                return db.hitchBOTs.Single(h => h.ID == HitchBotID).Locations.Last();
            }
        }
    }
}