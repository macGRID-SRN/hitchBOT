using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace hitchbot_secure_api.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? Altitude { get; set; }
        public double? Accuracy { get; set; }
        public double? Velocity { get; set; }

        public int? SpotID { get; set; }
        public SpotGpsMessageType? SpotGpsMessageType { get; set; }
        public LocationProvider LocationProvider { get; set; }

        public string NearestCity { get; set; }
        public bool ForceProduction { get; set; } /*force this point into being used in production*/
        public bool HideFromProduction { get; set; }

        public int? HitchBotId { get; set; }
        public virtual HitchBot HitchBot { get; set; }

        public int? CleverscriptContentId { get; set; }
        public virtual CleverscriptContext CleverscriptContext { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public DateTime? TakenTime { get; set; }
        public DateTime TimeAdded { get; set; }

        public Location()
        {
            TimeAdded = DateTime.UtcNow;
        }

        public Location(Controllers.ReturnObjects.GenericHitchBot context)
            : this()
        {
            HitchBotId = context.HitchBotId;
            TakenTime = context.DateTime;
        }
    }

    /// <summary>
    /// Where did a GPS point come from? It is always good to know!
    /// </summary>
    public enum LocationProvider
    {
        Unknown,
        ManualInsert, /*Sometimes a point is added as the map can get confusing at times if weird things happen.*/
        TabletAGPS,
        SpotGPS
    }

    public enum SpotGpsMessageType
    {
        Unknown,
        Ok,
        Track,
        ExtremeTrack,
        UnlimitedTrack,
        NewMovement,
        Help,
        HelpCancel,
        Custom,
        Poi,
        Stop,
        PowerOff
    }

    public class SpotApiMapping
    {
        public static readonly Dictionary<string, SpotGpsMessageType> SpotMap = new Dictionary<string, SpotGpsMessageType>
        {
            {"OK", SpotGpsMessageType.Ok},
            {"TRACK", SpotGpsMessageType.Track},
            {"EXTREME-TRACK", SpotGpsMessageType.ExtremeTrack},
            {"UNLIMITED-TRACK", SpotGpsMessageType.UnlimitedTrack},
            {"NEWMOVEMENT", SpotGpsMessageType.NewMovement},
            {"NEW", SpotGpsMessageType.Help},
            {"HELP-CANCEL", SpotGpsMessageType.HelpCancel},
            {"CUSTOM", SpotGpsMessageType.Custom},
            {"POI", SpotGpsMessageType.Poi},
            {"STOP",SpotGpsMessageType.Stop}
        };
    }

    //used http://json2csharp.com/ to get this. haha feels like such a dirty cheat.
    public class SpotApiCall
    {
        public class Feed
        {
            public string id { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public string status { get; set; }
            public int usage { get; set; }
            public int daysRange { get; set; }
            public bool detailedMessageShown { get; set; }
        }

        public class Message
        {
            public int id { get; set; }
            public string messengerId { get; set; }
            public string messengerName { get; set; }
            public int unixTime { get; set; }
            public string messageType { get; set; }
            public double latitude { get; set; }
            public double longitude { get; set; }
            public string modelId { get; set; }
            public string showCustomMsg { get; set; }
            public string dateTime { get; set; }
            public string batteryState { get; set; }
            public int hidden { get; set; }
            public string messageContent { get; set; }
            [JsonIgnore]
            public SpotGpsMessageType _messageType
            {
                get
                {
                    if (SpotApiMapping.SpotMap.ContainsKey(messageType))
                        return SpotApiMapping.SpotMap[messageType];
                    return SpotGpsMessageType.Unknown;
                }
            }
        }

        public class Messages
        {
            public List<Message> message { get; set; }
        }

        public class FeedMessageResponse
        {
            public int count { get; set; }
            public Feed feed { get; set; }
            public int totalCount { get; set; }
            public int activityCount { get; set; }
            public Messages messages { get; set; }
        }

        public class Response
        {
            public FeedMessageResponse feedMessageResponse { get; set; }
        }


        public Response response { get; set; }

    }
}
