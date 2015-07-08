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

        /// <summary>
        /// Method was adapted from here:https://groups.google.com/forum/#!topic/nettopologysuite/gl2g2O807X0
        /// and here: http://gis.stackexchange.com/questions/13447/how-to-reproject-spatial-data-using-free-libraries
        /// TODO: move this into the main project and create test cases based on the google maps coordinate system. MUST ENSURE there are border cases (large and small polygons with points very close to the edges)
        /// </summary>
        [TestMethod]
        public void TestPolygonIntersectionSouthAmerica()
        {
            //coordinates of the polygon
            var coords = new List<Location>
            {
                new Location
                {
                    Latitude = -38.32442042700653,
                    Longitude = -64.951171875
                },
                new Location
                {
                    Latitude = -40.7472569628042,
                    Longitude = -65.2587890625
                },
                new Location
                {
                    Latitude = -41.11246878918085,
                    Longitude = -62.490234375
                },
				new Location
				{
					Latitude = -40.530501775743204,
					Longitude = -61.67724609375
				},
				new Location
				{
					Latitude = -39.50404070558415,
					Longitude = -62.9736328125
				},
				new Location
				{
					Latitude = -37.99616267972812,
					Longitude = -63.69873046875
				}
            };

            //points which SHOULD be in the polygon
            var point = new List<Location>
            {
                new Location
                {
                    Latitude =  -40.064245174172065,
                    Longitude = -63.744138996874995
                },
                new Location
                {
                    Latitude = -40.53050149101196,
                    Longitude = -61.6772602459547
                },
                new Location
                {
                    Latitude = -40.529463305491795,
                    Longitude = -61.67856849340171
                },
                new Location
                {
                    Latitude = -39.50404507299803,
                    Longitude = -62.973628189241595
                }
            };

            //points which are outside the polygon
            var outsidePoints = new List<Location>
            {
                new Location()
                {
                    Latitude = -40.530499962031854,
                    Longitude = -61.67724817601413
                },
                new Location()
                {
                    Latitude = -39.50404714253596,
                    Longitude = -62.973612095987505
                },
                new Location()
                {
                    Latitude = -57.287977345866345,
                    Longitude = -62.55353524891109
                },
                new Location()
                {
                    Latitude = 13.822028428951164,
                    Longitude = -61.85041024891109
                }
            };

            point.ForEach(l => Assert.IsTrue(LocationHelper.PointInPolygon(coords, l)));

            outsidePoints.ForEach(k => Assert.IsFalse(LocationHelper.PointInPolygon(coords, k)));
        }


        /// <summary>
        /// Method was adapted from here:https://groups.google.com/forum/#!topic/nettopologysuite/gl2g2O807X0
        /// and here: http://gis.stackexchange.com/questions/13447/how-to-reproject-spatial-data-using-free-libraries
        /// TODO: move this into the main project and create test cases based on the google maps coordinate system. MUST ENSURE there are border cases (large and small polygons with points very close to the edges)
        /// </summary>
        [TestMethod]
        public void TestPolygonIntersectionRussia()
        {
            //coordinates of the polygon
            var coords = new List<Location>
            {
                new Location
                {
                    Latitude = 52.93539665862318,
                    Longitude = 23.5986328125
                },
                new Location
                {
                    Latitude = 45.67548217560647,
                    Longitude = 25.3125
                },
                new Location
                {
                    Latitude = 46.86019101567027,
                    Longitude = 46.86019101567027
                },
				new Location
				{
					Latitude = 49.32512199104001,
					Longitude = 65.0390625
				},
				new Location
				{
					Latitude = 59.712097173322924,
					Longitude = 47.4609375
				},
				new Location
				{
					Latitude = 56.8970039212726,
					Longitude = 21.6650390625
				}
            };

            //points which SHOULD be in the polygon
            var point = new List<Location>
            {
                new Location
                {
                    Latitude =  50.469005451461314,
                    Longitude = 30.529542643750005
                },
                new Location
                {
                    Latitude = 46.02477054707349,
                    Longitude = 25.739503581250005
                },
                new Location
                {
                    Latitude = 48.412026716676024,
                    Longitude = 55.007081706250005
                },
                new Location
                {
                    Latitude = 59.61023683281112,
                    Longitude = 47.360597331250005
                }
            };

            //points which are outside the polygon
            var outsidePoints = new List<Location>
            {
                new Location()
                {
                    Latitude = 59.89799984145697,
                    Longitude = 46.789308268750005
                },
                new Location()
                {
                    Latitude = 56.69029725267831,
                    Longitude = 21.608644206250005
                },
                new Location()
                {
                    Latitude = 50.13216125899563,
                    Longitude = 24.113527018750005
                },
                new Location()
                {
                    Latitude = 52.35979854180613,
                    Longitude = 18.971925456250005
                }
            };

            point.ForEach(l => Assert.IsTrue(LocationHelper.PointInPolygon(coords, l)));

            outsidePoints.ForEach(k => Assert.IsFalse(LocationHelper.PointInPolygon(coords, k)));
        }
    }
}
