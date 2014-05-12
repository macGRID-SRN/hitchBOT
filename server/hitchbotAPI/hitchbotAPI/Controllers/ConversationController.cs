using hitchbotAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace hitchbotAPI.Controllers
{
    public class ConversationController : ApiController
    {
        /// <summary>
        /// Starts a new Conversation thread for a HitchBot and already defined Location.
        /// </summary>
        /// <param name="HitchBotID">The ID of the HitchBot to add a new Conversation to.</param>
        /// <param name="StartTime">The time the Conversation started</param>
        /// <param name="LocationID">The ID of the Location where the Conversation started.</param>
        /// <returns></returns>
        [HttpPost]
        public int StartNewConversation(int HitchBotID, DateTime StartTime, int LocationID)
        {
            using (var db = new Database())
            {
                var newConversation = new Conversation();
                newConversation.StartTime = StartTime;
                var location = db.Locations.Single(l => l.ID == LocationID);
                newConversation.StartLocation = location;
                newConversation.TimeAdded = DateTime.UtcNow;
                var hitchbot = db.hitchBOTs.Single(h => h.ID == HitchBotID);
                hitchbot.Conversations.Add(newConversation);
                db.SaveChanges();
                //untested, I am not sure if this will make bad stuff happen or not.
                return newConversation.ID;
            }
        }
    }
}