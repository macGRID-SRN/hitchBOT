using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using hitchbot_secure_api.Models;

namespace hitchbot_secure_api.Controllers
{
    public partial class SpeechController : ApiController
    {
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> LogSpeech([FromBody] ReturnSpeech context)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model sent was not valid.");

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
