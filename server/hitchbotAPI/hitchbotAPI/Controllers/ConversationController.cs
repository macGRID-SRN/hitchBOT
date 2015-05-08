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
using hitchbotAPI.Models;

namespace hitchbotAPI.Controllers
{
    public class ConversationController : ApiController
    {
        /// <summary>
        /// Starts a new Conversation thread for a HitchBot and the Last Location. If a location is not available, it just assigns it to none.
        /// </summary>
        /// <param name="HitchBotID">The ID of the HitchBot to add a new Conversation to.</param>
        /// <param name="StartTime">The time the Conversation started</param>
        /// <returns>The ID of the Conversation being added.</returns>
        [HttpPost]
        public HttpResponseMessage StartNewConversation(int HitchBotID, string StartTime, bool adsaa = true)
        {
            try
            {
                DateTime StartTimeReal = DateTime.ParseExact(StartTime, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);

                using (var db = new Models.Database())
                {
                    var hitchbot = db.hitchBOTs.Include(l => l.Locations).Include(h => h.Conversations).FirstOrDefault(h => h.ID == HitchBotID);
                    if (hitchbot == null)
                    {
                        string message = string.Format("HitchBOT with id = {0} not found", HitchBotID);
                        HttpError error = new HttpError(message);
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, error);
                    }

                    if (DateTime.UtcNow - (hitchbot.Conversations.LastOrDefault() ?? (new Models.Conversation { StartTime = DateTime.UtcNow.Subtract(new TimeSpan(5)) })).StartTime > TimeSpan.FromHours(3))
                    {
                        var location = hitchbot.Locations.OrderBy(l => l.TakenTime).FirstOrDefault();
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

                    return Request.CreateResponse(HttpStatusCode.OK, true);
                }
            }
            catch (FormatException)
            {
                string message = string.Format("{0} is not a valid DateTime. Please use format yyyyMMddHHmmss", StartTime);
                HttpError error = new HttpError(message);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, error);
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
        /// Add's a SpeechEventLog - A testing version of speech interaction. Records something someone said to hitchbot and records what it said in return.
        /// </summary>
        /// <param name="HitchBotID">The ID of the HitchBOT in question.</param>
        /// <param name="SpeechHeard">The text that was said to HitchBOT.</param>
        /// <param name="SpeechSaid">What HitchBOT said in response.</param>
        /// <param name="TimeTaken">The time this occurred at.</param>
        /// <param name="Person">Who was testing?</param>
        /// <param name="Notes">What else should be know about the person/environment testing scenario. Was there a train going by?</param>
        /// <param name="RmsDecibelLevel">Option parameter for the decibel level given by the speech recognition engine.</param>
        /// <param name="EnvironmentType">Some kind of sortable enumerator which I do not have to worry about!!!!</param>
        /// <returns>Success.</returns>
        [HttpPost]
        public async Task<HttpResponseMessage> AddSpeechListen(int HitchBotID, string SpeechSaid, string SpeechHeard, string TimeTaken, string Person, string Notes, string MatchedLineLabel, int? MatchAccuracy = null, string RmsDecibelLevel = "", int? EnvironmentType = null, int? RecognitionScore = null, int? ResponseScore = null, int? RecognizerEnum = null)
        {
            using (var db = new Models.Database())
            {
                var OccuredTime = DateTime.ParseExact(TimeTaken, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);

                var speechEvent = new Models.SpeechLogEvent()
                {
                    HitchBOTID = HitchBotID,
                    Notes = Notes,
                    Person = Person,
                    SpeechHeard = SpeechHeard,
                    SpeechSaid = SpeechSaid,
                    TimeOccured = OccuredTime,
                    TimeAdded = DateTime.UtcNow,
                    EnvironmentType = EnvironmentType,
                    MatchAccuracy = MatchAccuracy,
                    MatchedLineLabel = MatchedLineLabel,
                    RecognitionScore = RecognitionScore,
                    ResponseScore = ResponseScore,
                    RecognizerType = (RecognizerType)(RecognizerEnum ?? 0)
                };

                if (!string.IsNullOrWhiteSpace(RmsDecibelLevel))
                {
                    double tempRMS;
                    if (double.TryParse(RmsDecibelLevel, out tempRMS))
                        speechEvent.RmsDecibalLevel = tempRMS;
                }

                db.SpeechLogEvents.Add(speechEvent);

                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, true);
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
    }
}