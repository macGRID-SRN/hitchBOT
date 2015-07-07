using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using hitchbot_secure_api.Helpers.Location;
using hitchbot_secure_api.Models;

namespace hitchbot_unit_tests
{
    [TestClass]
    public class PolygonPointTest
    {
        /// <summary>
        /// Method was adapted from here:https://groups.google.com/forum/#!topic/nettopologysuite/gl2g2O807X0
        /// and here: http://gis.stackexchange.com/questions/13447/how-to-reproject-spatial-data-using-free-libraries
        /// MUST ENSURE there are border cases (large and small polygons with points very close to the edges)
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

        [TestMethod]
        public void TestPolygonIntersectionEuro()
        {
            //coordinates of the polygon
            var coords = new List<Location>
            {
                new Location
                {
                    Latitude = 37.98403156525845,
                    Longitude = 23.729236274957657
                },
                new Location
                {
                    Latitude = 37.98404213560616,
                    Longitude = 23.72919201850891
                },
                new Location
                {
                    Latitude = 37.98403685043247,
                    Longitude = 23.72914843261242
                },
				new Location
				{
					Latitude = 37.98401201011115,
					Longitude = 23.729122281074524
				},
				new Location
				{
					Latitude = 37.983990869405545,
					Longitude = 23.729122951626778
				},
				new Location
				{
					Latitude = 37.98399034088781,
					Longitude = 23.729051873087883
				},
				new Location
				{
					Latitude = 37.98397237128313,
					Longitude = 23.729054555296898
				},
				new Location
				{
					Latitude = 37.98397289980096,
					Longitude = 23.729126304388046
				},
				new Location
				{
					Latitude = 37.98395228760207,
					Longitude = 23.72913032770157
				},
				new Location
				{
					Latitude = 37.98394964501205,
					Longitude = 23.72905857861042
				},
				new Location
				{
					Latitude = 37.98393167539738,
					Longitude = 23.729062601923943
				},
				new Location
				{
					Latitude = 37.983936960578625,
					Longitude = 23.729133009910583
				},
				new Location
				{
					Latitude = 37.98390366393045,
					Longitude = 23.729157149791718
				},
				new Location
				{
					Latitude = 37.983897321710046,
					Longitude = 23.729221452200704
				},
				new Location
				{
					Latitude = 37.98391000615032,
					Longitude = 23.72925840318203
				},
				new Location
				{
					Latitude = 37.98404689226212,
					Longitude = 23.72916854918003
				}
            };

            //points which SHOULD be in the polygon
            var point = new List<Location>
            {
                new Location
                {
                    Latitude = 37.9839668528531,
                    Longitude = 23.729174513542944
                },
                new Location
                {
                    Latitude = 37.98398165135253,
                    Longitude = 23.72906387242108
                },
                new Location
                {
                    Latitude = 37.9839287995552,
                    Longitude = 23.729138974273496
                },
                new Location
                {
                    Latitude = 37.9840434879071,
                    Longitude = 23.729176525199705
                }
            };

            //points which are outside the half Euro
            var outsidePoints = new List<Location>
            {
                new Location()
                {
                    Latitude = 37.98392985659151,
                    Longitude = 23.72913763316899
                },
                new Location()
                {
                    Latitude = 37.984027632385256,
                    Longitude = 23.729196641767317
                },
                new Location()
                {
                    Latitude = 37.98396209619195,
                    Longitude = 23.7291215399149
                },
                new Location()
                {
                    Latitude = 37.98394676917057,
                    Longitude = 23.729239557111555
                }
            };

            point.ForEach(l => Assert.IsTrue(LocationHelper.PointInPolygon(coords, l)));

            outsidePoints.ForEach(k => Assert.IsFalse(LocationHelper.PointInPolygon(coords, k)));
        }

        [TestMethod]
        public void TestPolygonIntersectionFlipper()
        {
            //coordinates of the polygon
            var coords = new List<Location>
            {
                new Location
                {
                    Latitude = 5.922044619883305,
                    Longitude = -38.8037109375
                },
                new Location
                {
                    Latitude = -8.2767271011640329,
                    Longitude = -38.408203125
                },
                new Location
                {
                    Latitude = -11.178401873711772,
                    Longitude = -17.7099609375
                },
				new Location
				{
					Latitude = 8.4071681636010744,
					Longitude = -15.732421875
				},
				new Location
				{
					Latitude = 16.509832826905846,
					Longitude = -24.2578125
				},
				new Location
				{
					Latitude = 14.647368383896632,
					Longitude = -35.4638671875
				}
            };

            //points which SHOULD be in the polygon
            var point = new List<Location>
            {
                new Location
                {
                    Latitude = 9.38017942355342,
                    Longitude = -36.509031574999995
                },
                new Location
                {
                    Latitude = -7.693086920295743,
                    Longitude = -37.739500324999995
                },
                new Location
                {
                    Latitude = -10.642851546692047,
                    Longitude = -18.052000324999995
                },
                new Location
                {
                    Latitude = 12.57219864890512,
                    Longitude = -21.919187824999995
                }
            };

            //points which are outside the polygon
            var outsidePoints = new List<Location>
            {
                new Location()
                {
                    Latitude = 13.343065533441198,
                    Longitude = -19.634031574999995
                },
                new Location()
                {
                    Latitude = 10.332694515564578,
                    Longitude = -38.530515949999995
                },
                new Location()
                {
                    Latitude = -12.021649647626512,
                    Longitude = -29.653562824999995
                },
                new Location()
                {
                    Latitude = 8.946335430209233,
                    Longitude = -15.766850075000093
                }
            };

            point.ForEach(l => Assert.IsTrue(LocationHelper.PointInPolygon(coords, l)));

            outsidePoints.ForEach(k => Assert.IsFalse(LocationHelper.PointInPolygon(coords, k)));
        }
    }
}
