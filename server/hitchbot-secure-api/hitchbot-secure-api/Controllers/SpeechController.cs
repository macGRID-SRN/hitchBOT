using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using hitchbot_secure_api.Models;

namespace hitchbot_secure_api.Controllers
{
    public class SpeechController : ApiController
    {
        [HttpPost]
        public async Task<IHttpActionResult> AddSpeechLog([FromBody] Controller.ReturnSpeech context)
        {
            using (var db = new Dal.DatabaseContext())
            {
                var speechEvent = new SpeechLogEvent(context)
                {
                    Speech = context.SpeechData,
                    EnvironmentType = context.EnvironmentType,
                    MatchAccuracy = context.MatchAccuracy,
                    RecognitionScore = context.RecognitionScore,
                    GoogleRecognitionScore = context.GoogleRecognitionScore,
                    ResponseScore = context.ResponseScore,
                    RmsDecibalLevel = context.RmsDecibalLevel,
                    RecognizerType = context.RecognizerType
                };


                db.SpeechLogEvents.Add(speechEvent);

                db.SaveChanges();

                return Ok();
            }
        }
    }
}
