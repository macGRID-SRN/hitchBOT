using hitchbotAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Globalization;
using System.Data.Entity;
using System.Threading.Tasks;

namespace hitchbotAPI.Controllers
{
    public class ConversationController : ApiController
    {
        /// <summary>
        /// Starts a new Conversation thread for a HitchBot and already defined Location.
        /// </summary>
        /// <param name="HitchBotID">The ID of the HitchBot to add a new Conversation to.</param>
        /// <param name="StartTime">The time the Conversation started</param>
        /// <returns>The ID of the Conversation being added.</returns>
        [HttpPost]
        public bool StartNewConversation(int HitchBotID, string StartTime)
        {
            DateTime StartTimeReal = DateTime.ParseExact(StartTime, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            using (var db = new Models.Database())
            {
                var hitchbot = db.hitchBOTs.Include(l => l.Locations).Include(h => h.Conversations).First(h => h.ID == HitchBotID);
                if (DateTime.UtcNow - hitchbot.Conversations.Last().TimeAdded > TimeSpan.FromHours(3))
                {
                    var location = hitchbot.Locations.OrderBy(l => l.TakenTime).First();
                    var newConversation = new Models.Conversation()
                    {
                        StartTime = StartTimeReal,
                        TimeAdded = DateTime.UtcNow,
                        StartLocation = location,
                        HitchBOT = hitchbot
                    };

                    db.Conversations.Add(newConversation);
                    db.SaveChanges();
                }
                return true;
            }
        }

        /// <summary>
        /// Add's a SpeechEvent - Something a HitchBot says.
        /// </summary>
        /// <param name="HitchBotID">The ID for the HitchBot to add conversation.</param>
        /// <param name="SpeechSaid">The text which HitchBot said.</param>
        /// <param name="TimeTaken">When HitchBot said this.</param>
        /// <returns>The ID of the newly created SpeechEvent.</returns>
        [HttpPost]
        public bool AddSpeech(int HitchBotID, string SpeechSaid, string TimeTaken)
        {
            using (var db = new Models.Database())
            {
                var speechEvent = new Models.SpeechEvent();
                speechEvent.TimeAdded = DateTime.UtcNow;
                speechEvent.SpeechSaid = SpeechSaid;

                speechEvent.OccuredTime = DateTime.ParseExact(TimeTaken, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                var hitchbot = db.hitchBOTs.Include(h => h.Conversations).First(h => h.ID == HitchBotID);
                speechEvent.Conversation = hitchbot.CurrentConversation;
                db.SpeechEvents.Add(speechEvent);
                db.SaveChanges();
                hitchbot.Conversations.OrderBy(l => l.StartTime).Last().SpeechEvents.Add(speechEvent);
                db.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// Add's a ListenEvent - Something a HitchBot hears.
        /// </summary>
        /// <param name="HitchBotID">The ID of the HitchBOT to add conversation to.</param>
        /// <param name="SpeechHeard">The text which HitchBot heard.</param>
        /// <param name="TimeTaken">When HitchBot heard this.</param>
        /// <returns>The ID of the newly created ListenEvent.</returns>
        [HttpPost]
        public bool AddListen(int HitchBotID, string SpeechHeard, string TimeTaken)
        {
            using (var db = new Models.Database())
            {
                var listenEvent = new Models.ListenEvent();
                listenEvent.TimeAdded = DateTime.UtcNow;
                listenEvent.HeardTime = DateTime.ParseExact(TimeTaken, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                listenEvent.SpeechHeard = SpeechHeard;
                var hitchbot = db.hitchBOTs.Include(h => h.Conversations).First(h => h.ID == HitchBotID);

                listenEvent.Conversation = hitchbot.CurrentConversation;
                db.ListenEvents.Add(listenEvent);
                db.SaveChanges();
                hitchbot.Conversations.OrderBy(l => l.StartTime).Last().ListenEvents.Add(listenEvent);
                db.SaveChanges();
                return true;
            }
        }


        //[HttpGet]
        //public async Task<bool> ToggleConversationTweet(int HitchBotID)
        //{
        //    using (var db = new Models.Database())
        //    {
        //        var hitchy = db.hitchBOTs.Include(l => l.Conversations).First(h => h.ID == HitchBotID);

        //        var conversations = db.SpeechEvents.Include(l => l.Conversation).Where(l => l.Conversation.ID == hitchy.CurrentConversation.ID).ToList();

        //        if (conversations != null)
        //        {
        //            Random randy = new Random();
        //            var selectedSpeechEvent = conversations[randy.Next(conversations.Count)];
        //            await Helpers.TwitterHelper.PostTweetWithLocation(HitchBotID, 1, selectedSpeechEvent.SpeechSaid); //location ID for now because reasons
        //        }
        //    }
        //    return true;
        //}
    }
}