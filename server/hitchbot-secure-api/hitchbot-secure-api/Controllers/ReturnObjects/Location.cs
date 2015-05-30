using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using hitchbot_secure_api.Controllers.ReturnObjects;
using hitchbot_secure_api.Models;

namespace hitchbot_secure_api.Controllers
{
    public partial class LocationController
    {
        public class ReturnLocation : GenericHitchBot
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            public double? Altitude { get; set; }
            public double? Accuracy { get; set; }
            public double? Velocity { get; set; }

            public int? LocationProviderEnum { get; set; }

            public LocationProvider _LocationProvider
            {
                get { return ((LocationProvider)(LocationProviderEnum ?? 0)); }
            }
        }

        public class SpotDto
        {
            public int spotId { get; set; }
            public double latitude { get; set; }
            public double longitude { get; set; }

            public int unixTime { get; set; }

            public DateTime Time
            {
                get
                {
                    return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTime);

                }
            }
        }
    }
}
