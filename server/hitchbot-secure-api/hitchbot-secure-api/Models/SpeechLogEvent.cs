using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hitchbot_secure_api.Controllers;
using hitchbot_secure_api.Controllers.ReturnObjects;

namespace hitchbot_secure_api.Models
{
    public class SpeechLogEvent
    {
        public int Id { get; set; }

        public int HitchBotId { get; set; }
        public virtual HitchBot HitchBot { get; set; }

        public SpeechData Speech { get; set; }

        public int? MatchAccuracy { get; set; }
        public int? EnvironmentType { get; set; }
        public double? RecognitionScore { get; set; }
        public double? GoogleRecognitionScore { get; set; }
        public int? ResponseScore { get; set; }

        public RecognizerType? RecognizerType { get; set; }

        public double? RmsDecibalLevel { get; set; }

        public DateTime TimeOccured { get; set; }
        public DateTime TimeAdded { get; set; }

        public SpeechLogEvent()
        {
            TimeAdded = DateTime.UtcNow;
        }

        public SpeechLogEvent(GenericHitchBot context)
            : this()
        {
            TimeOccured = context.DateTime;
            HitchBotId = context.HitchBotId;
        }
    }

    [ComplexType]
    public class SpeechData
    {
        public string Said { get; set; }
        public string Heard { get; set; }
        public string Person { get; set; }
        public string Notes { get; set; }
        public string MatchedLineLabel { get; set; }
    }

    public enum RecognizerType
    {
        Unknown = 0,
        GoogleOnline = 1,
        GoogleOffline = 2,
        PocketSphinx = 3
    }
}
