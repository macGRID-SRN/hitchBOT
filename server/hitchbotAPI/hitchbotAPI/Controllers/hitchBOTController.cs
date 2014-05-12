using hitchbotAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

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
        public Models.hitchBOT GetHitchbot(int ID)
        {
            using (var db = new Models.Database())
            {
                return db.hitchBOTs.Single(h => h.ID == ID);
            }
        }

        /// <summary>
        /// Call this method when adding a new HitchBot to an existing project.
        /// </summary>
        /// <param name="ProjectID">ID of the Project the HitchBot will be added to.</param>
        /// <param name="HitchbotName">The name of the HitchBot that will be created.</param>
        /// <param name="Creation">The time this HitchBot instance was created.</param>
        /// <returns>Success.</returns>
        [HttpPost]
        public bool AddHitchBotToProject(int ProjectID, string HitchbotName, DateTime Creation)
        {
            using (var db = new Models.Database())
            {
                var project = db.Projects.Single(p => p.ID == ProjectID);
                var newHitchBot = new Models.hitchBOT
                {
                    CreationTime = Creation,
                    TimeAdded = DateTime.UtcNow,
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
        public Models.Location GetMostRecentLocation(int HitchBotID)
        {
            //makes the assumption that the TakenTime is correct from the HitchBot. This will most likely be correct as the Tablets should get the time from cell networks.
            using (var db = new Models.Database())
            {
                return db.hitchBOTs.Where(h => h.ID == HitchBotID).Include(l => l.Locations).FirstOrDefault().Locations.OrderByDescending(l => l.TakenTime).First();
            }
        }

        /// <summary>
        /// Get's all the Locations a HitchBot has ever checked in to, ordered by when they were taken.
        /// </summary>
        /// <param name="HitchBotLocationsID">The ID of the HitchBot to get all the Locations for.</param>
        /// <returns>A list of all the Locations, sorted.</returns>
        [HttpGet]
        public List<Models.Location> GetAllLocationsInOrder(int HitchBotLocationsID)
        {
            using (var db = new Models.Database())
            {
                return db.hitchBOTs.Single(h => h.ID == HitchBotLocationsID).Locations.OrderBy(l => l.TakenTime).ToList();
            }
        }
    }
}