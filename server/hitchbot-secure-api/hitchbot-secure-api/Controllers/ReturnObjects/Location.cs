using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using hitchbot_secure_api.Models;

namespace hitchbot_secure_api.Controllers
{
    public partial class LocationController
    {
        public class ReturnLocation : ReturnObjects.GenericHitchBot
        {
            public decimal Latitude { get; set; }
            public decimal Longitude { get; set; }
            public decimal? Altitude { get; set; }
            public decimal? Accuracy { get; set; }
            public decimal? Velocity { get; set; }

            public int? LocationProviderEnum { get; set; }

            public LocationProvider _LocationProvider
            {
                get { return ((LocationProvider)(LocationProviderEnum ?? 0)); }
            }
        }
    }
}
