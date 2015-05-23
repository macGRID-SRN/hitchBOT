using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hitchbot_secure_api.Models
{
    public class SpeechLogEvent
    {
        public int ID { get; set; }

        public int HitchBotId { get; set; }
        public virtual HitchBot HitchBot { get; set; }

        public SpeechData Speech { get; set; }

        public int? MatchAccuracy { get; set; }
        public int? EnvironmentType { get; set; }
        public int? RecognitionScore { get; set; }
        public int? GoogleRecognitionScore { get; set; }
        public int? ResponseScore { get; set; }

        public RecognizerType? RecognizerType { get; set; }

        public double? RmsDecibalLevel { get; set; }

        public DateTime TimeOccured { get; set; }
        public DateTime TimeAdded { get; set; }

        public SpeechLogEvent()
        {
            TimeAdded = DateTime.UtcNow;
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
