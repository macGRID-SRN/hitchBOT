using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hitchbot_secure_api.Controllers.ReturnObjects;
using hitchbot_secure_api.Models;

namespace hitchbot_secure_api.Controllers
{
    public partial class SpeechController
    {
        public class ReturnSpeech : GenericHitchBot
        {
            public string Said { get; set; }
            public string Heard { get; set; }
            public string Person { get; set; }
            public string Notes { get; set; }
            public string MatchedLineLabel { get; set; }

            public int? MatchAccuracy { get; set; }
            public int? EnvironmentType { get; set; }
            public double? RecognitionScore { get; set; }
            public double? GoogleRecognitionScore { get; set; }
            public int? ResponseScore { get; set; }

            public int? RecognizerTypeEnum { get; set; }

            public RecognizerType? RecognizerType
            {
                get { return (RecognizerType)(RecognizerTypeEnum ?? 0); }
            }

            public double? RmsDecibalLevel { get; set; }

            public SpeechData SpeechData
            {
                get
                {
                    return new SpeechData()
                    {
                        Heard = Heard,
                        MatchedLineLabel = MatchedLineLabel,
                        Notes = Notes,
                        Person = Person,
                        Said = Said
                    };
                }
            }

        }
    }
}
