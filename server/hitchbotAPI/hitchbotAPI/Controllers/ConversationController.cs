using hitchbotAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Globalization;
using System.Data.Entity;

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
        /// <returns>The ID of the Conversation being added.</returns>
        [HttpPost]
        public int StartNewConversation(int HitchBotID, string StartTime)
        {
            DateTime StartTimeReal = DateTime.ParseExact(StartTime, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            using (var db = new Models.Database())
            {
                var newConversation = new Models.Conversation();
                newConversation.StartTime = StartTimeReal;
                newConversation.TimeAdded = DateTime.UtcNow;
                var hitchbot = db.hitchBOTs.Include(l => l.Locations).First(h => h.ID == HitchBotID);
                var location = hitchbot.Locations.OrderBy(l => l.TakenTime).First();
                newConversation.StartLocation = location;
                hitchbot.Conversations.Add(newConversation);
                db.SaveChanges();
                //untested, I am not sure if this will make bad stuff happen or not.
                return newConversation.ID;
            }
        }

        /// <summary>
        /// Add's a SpeechEvent - Something a HitchBot says.
        /// </summary>
        /// <param name="convID">The ID of the Conversation being continued.</param>
        /// <param name="SpeechSaid">The text which HitchBot said.</param>
        /// <param name="TimeTaken">When HitchBot said this.</param>
        /// <returns>The ID of the newly created SpeechEvent.</returns>
        [HttpPost]
        public int AddSpeech(int HitchBotID, string SpeechSaid, string TimeTaken)
        {
            using (var db = new Models.Database())
            {
                var speechEvent = new Models.SpeechEvent();
                speechEvent.TimeAdded = DateTime.UtcNow;
                speechEvent.SpeechSaid = SpeechSaid;
                speechEvent.OccuredTime = DateTime.ParseExact(TimeTaken, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                var hitchbot = db.hitchBOTs.Include(h => h.Conversations).First(h => h.ID == HitchBotID);
                db.SpeechEvents.Add(speechEvent);
                db.SaveChanges();
                hitchbot.Conversations.OrderBy(l => l.StartTime).Last().SpeechEvents.Add(speechEvent);
                db.SaveChanges();
                return speechEvent.ID;
            }
        }

        /// <summary>
        /// Add's a ListenEvent - Something a HitchBot hears.
        /// </summary>
        /// <param name="convID">The ID of the Conversation being continued.</param>
        /// <param name="SpeechHeard">The text which HitchBot heard.</param>
        /// <param name="TimeTaken">When HitchBot heard this.</param>
        /// <returns>The ID of the newly created ListenEvent.</returns>
        [HttpPost]
        public int AddListen(int HitchBotID, string SpeechHeard, string TimeTaken)
        {
            using (var db = new Models.Database())
            {
                var listenEvent = new Models.ListenEvent();
                listenEvent.TimeAdded = DateTime.UtcNow;
                listenEvent.HeardTime = DateTime.ParseExact(TimeTaken, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                listenEvent.SpeechHeard = SpeechHeard;
                var hitchbot = db.hitchBOTs.Include(h => h.Conversations).First(h => h.ID == HitchBotID);
                db.ListenEvents.Add(listenEvent);
                db.SaveChanges();
                hitchbot.Conversations.OrderBy(l => l.StartTime).Last().ListenEvents.Add(listenEvent);
                db.SaveChanges();
                return listenEvent.ID;
            }
        }
    }
}