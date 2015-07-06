using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using hitchbot_secure_api.Helpers.Location;
using hitchbot_secure_api.Models;

namespace hitchbot_unit_tests
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Method was adapted from here:https://groups.google.com/forum/#!topic/nettopologysuite/gl2g2O807X0
        /// and here: http://gis.stackexchange.com/questions/13447/how-to-reproject-spatial-data-using-free-libraries
        /// TODO: move this into the main project and create test cases based on the google maps coordinate system. MUST ENSURE there are border cases (large and small polygons with points very close to the edges)
        /// </summary>
        [TestMethod]
        public void TestPolygonIntersectionTriangle()
        {
            //coordinates of the polygon
            var coords = new List<Location>
            {
                new Location
                {
                    Latitude = 43.328174695525846,
                    Longitude = -79.851722717285156
                },
                new Location
                {
                    Latitude = 43.18565317214177,
                    Longitude = -79.668388366699219
                },
                new Location
                {
                    Latitude = 43.190659573987446,
                    Longitude = -80.028190612792969
                }
            };

            //points which SHOULD be in the polygon
            var point = new List<Location>
            {
                new Location
                {
                    Latitude = 43.2423582,
                    Longitude = -79.8391097
                },
                new Location
                {
                    Latitude = 43.25333024838933,
                    Longitude = -79.94622639921874
                },
                new Location
                {
                    Latitude = 43.328007170292935,
                    Longitude = -79.85172681120605
                },
                new Location
                {
                    Latitude = 43.18760993929186,
                    Longitude = -79.80247227778312
                }
            };

            //points which are outside the triangle
            var outsidePoints = new List<Location>
            {
                new Location()
                {
                    Latitude = 49.667627822621917,
                    Longitude = -112.060546875
                },
                new Location()
                {
                    Latitude = 52.855864177853995,
                    Longitude = -75.234375
                },
                new Location()
                {
                    Latitude = 43.253861607960644,
                    Longitude = -79.94897298125
                },
                new Location()
                {
                    Latitude = 43.31606502446367,
                    Longitude = -79.8360841682312
                }
            };

            point.ForEach(l => Assert.IsTrue(LocationHelper.PointInPolygon(coords, l)));

            outsidePoints.ForEach(k => Assert.IsFalse(LocationHelper.PointInPolygon(coords, k)));
        }
    }
}
