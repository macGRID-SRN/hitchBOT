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

        [TestMethod]
        public void TestPolygonIntersectionRussia2()
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

        [TestMethod]
        public void TestPolygonIntersectionJapan()
        {
            //coordinates of the polygon
            var coords = new List<Location>
            {
                new Location
                {
                    Latitude = 34.409742177014174,
                    Longitude = 132.4285125732422
                },
                new Location
                {
                    Latitude = 34.509385763804225,
                    Longitude = 132.37014770507812
                },
                new Location
                {
                    Latitude = 34.54389356378646,
                    Longitude = 132.4566650390625
                },
				new Location
				{
					Latitude = 34.52579289427138,
					Longitude = 132.5109100341797
				},
				new Location
				{
					Latitude = 34.41937202924269,
					Longitude = 132.5012969970703
				},
				new Location
				{
					Latitude = 34.40351050532855,
					Longitude = 132.52464294433594
				},
				new Location
				{
					Latitude = 34.38197934098774,
					Longitude = 132.65167236328125
				},
				new Location
				{
					Latitude = 34.32529192442733,
					Longitude = 132.6544189453125
				},
				new Location
				{
					Latitude = 34.31962107462055,
					Longitude = 132.54867553710938
				},
				new Location
				{
					Latitude = 34.37064492478658,
					Longitude = 132.46627807617188
				},
				new Location
				{
					Latitude = 34.32756015705253,
					Longitude = 132.46627807617188
				},
				new Location
				{
					Latitude = 34.326426048404265,
					Longitude = 132.33718872070312
				},
				new Location
				{
					Latitude = 34.38084596839499,
					Longitude = 132.33169555664062
				},
				new Location
				{
					Latitude = 34.40237742424137,
					Longitude = 132.38937377929688
				}
            };

            //points which SHOULD be in the polygon
            var point = new List<Location>
            {
                new Location
                {
                    Latitude =  34.39575598392493,
                    Longitude = 132.46206827851563
                },
                new Location
                {
                    Latitude = 34.34701513021934,
                    Longitude = 132.60489054414063
                },
                new Location
                {
                    Latitude = 34.342479655726464,
                    Longitude = 132.3782975265625
                },
                new Location
                {
                    Latitude = 34.37075150127045,
                    Longitude = 132.46627398225098
                }
            };

            //points which are outside the polygon
            var outsidePoints = new List<Location>
            {
                new Location()
                {
                    Latitude = 34.370326431365186,
                    Longitude = 132.46627398225098
                },
                new Location()
                {
                    Latitude = 34.3633833176024,
                    Longitude = 132.46678896638184
                },
                new Location()
                {
                    Latitude = 34.41664635292772,
                    Longitude = 132.3926312515381
                },
                new Location()
                {
                    Latitude = 32.755628486998,
                    Longitude = 129.8767621109131
                }
            };

            point.ForEach(l => Assert.IsTrue(LocationHelper.PointInPolygon(coords, l)));

            outsidePoints.ForEach(k => Assert.IsFalse(LocationHelper.PointInPolygon(coords, k)));
        }
        [TestMethod]
        public void TestPolygonIntersectionAustralia()
        {
            //coordinates of the polygon
            var coords = new List<Location>
            {
                new Location
                {
                    Latitude = -11.264612212504426,
                    Longitude = 142.646484375
                },
				new Location
				{
					Latitude = -14.77488250651626,
					Longitude = -14.77488250651626
				},
                new Location
                {
                    Latitude = -17.5602465032949,
                    Longitude = 140.185546875
                },
                new Location
                {
                    Latitude = -12.297068292853803,
                    Longitude = 136.142578125
                },
				new Location
				{
					Latitude = -12.683214911818654,
					Longitude = 130.517578125
				},
				new Location
				{
					Latitude = -5.178482088522876,
					Longitude = 135.439453125
				}
            };

            //points which SHOULD be in the polygon
            var point = new List<Location>
            {
                new Location
                {
                    Latitude =  -14.884861080225496,
                    Longitude = 138.92066392500004
                },
                new Location
                {
                    Latitude = -12.236471071067005,
                    Longitude = 136.1301385812501
                },
                new Location
                {
                    Latitude =  -12.129081996686415,
                    Longitude = 130.9665643625001
                },
                new Location
                {
                    Latitude =  -9.4529137448475,
                    Longitude = 138.5910760812501
                }
            };

            //points which are outside the polygon
            var outsidePoints = new List<Location>
            {
                new Location()
                {
                    Latitude = -12.644148556315264,
                    Longitude = 135.7566034250001
                },
                new Location()
                {
                    Latitude = -14.012485124795791,
                    Longitude = 132.5705682687501
                },
                new Location()
                {
                    Latitude = 12.957930389842817,
                    Longitude = -135.6935838874998
                },
                new Location()
                {
                    Latitude = 62.783101880664816,
                    Longitude = 75.59547861250013
                }
            };

            point.ForEach(l => Assert.IsTrue(LocationHelper.PointInPolygon(coords, l)));

            outsidePoints.ForEach(k => Assert.IsFalse(LocationHelper.PointInPolygon(coords, k)));
        }

        [TestMethod]
        public void TestPolygonIntersectionAfrica()
        {
            //coordinates of the polygon
            var coords = new List<Location>
            {
                new Location
                {
                    Latitude = 6.227933930268672,
                    Longitude = 2.373046875
                },
                new Location
                {
                    Latitude = 7.100892668623654,
                    Longitude = 3.4716796875
                },
                new Location
                {
                    Latitude = 6.031310707125822,
                    Longitude = 4.06494140625
                }
            };

            //points which SHOULD be in the polygon
            var point = new List<Location>
            {
                new Location
                {
                    Latitude =  6.63890417840871,
                    Longitude = 3.2175309250000055 
                },
                new Location
                {
                    Latitude = 6.114825056961008,
                    Longitude = 3.9646012375000055
                },
                new Location
                {
                    Latitude = 6.27865704233154,
                    Longitude = 2.4594742843750055
                },
                new Location
                {
                    Latitude = 6.125748765817029,
                    Longitude = 3.3164078781250055
                }
            };

            //points which are outside the polygon
            var outsidePoints = new List<Location>
            {
                new Location()
                {
                    Latitude = 5.568363319076693,
                    Longitude = 3.2724625656250055
                },
                new Location()
                {
                    Latitude = 7.184226622869553,
                    Longitude = 0.5478531906250055
                },
                new Location()
                {
                    Latitude = 40.17588975589248,
                    Longitude = 21.905275065625005
                },
                new Location()
                {
                    Latitude = -6.668486081145337,
                    Longitude = -134.012693684375
                }
            };

            point.ForEach(l => Assert.IsTrue(LocationHelper.PointInPolygon(coords, l)));

            outsidePoints.ForEach(k => Assert.IsFalse(LocationHelper.PointInPolygon(coords, k)));
        }

        [TestMethod]
        public void TestPolygonIntersectionAlaska()
        {
            //coordinates of the polygon
            var coords = new List<Location>
            {
                new Location
                {
                    Latitude = 68.65655498475735,
                    Longitude = -165.41015625
                },
                new Location
                {
                    Latitude = 55.42901345240742,
                    Longitude = -161.54296875
                },
                new Location
                {
                    Latitude = 60.19615576604439,
                    Longitude = -140.888671875
                },
				new Location
				{
					Latitude = 69.59589006237648,
					Longitude = -141.15234375
				},
				new Location
				{
					Latitude = 71.13098770917023,
					Longitude = -156.97265625 
				}
            };

            //points which SHOULD be in the polygon
            var point = new List<Location>
            {
                new Location
                {
                    Latitude =  64.83793688127888,
                    Longitude = -153.052000325
                },
                new Location
                {
                    Latitude = 59.521194940814844,
                    Longitude = -159.907469075
                },
                new Location
                {
                    Latitude = 70.48693170401272,
                    Longitude = -156.919187825
                },
                new Location
                {
                    Latitude = 59.96405517837254,
                    Longitude = -144.262937825
                }
            };

            //points which are outside the polygon
            var outsidePoints = new List<Location>
            {
                new Location()
                {
                    Latitude = 43.252369100916745,
                    Longitude = -79.84154514578552
                },
                new Location()
                {
                    Latitude = 61.89186140575847,
                    Longitude = -168.43529514578552
                },
                new Location()
                {
                    Latitude = 70.69594044240277,
                    Longitude = -176.52123264578552
                },
                new Location()
                {
                    Latitude = 60.0368439431227,
                    Longitude =  -141.54076389578552
                }
            };

            point.ForEach(l => Assert.IsTrue(LocationHelper.PointInPolygon(coords, l)));

            outsidePoints.ForEach(k => Assert.IsFalse(LocationHelper.PointInPolygon(coords, k)));
        }

        [TestMethod]
        public void TestPolygonIntersectionNorthKorea()
        {
            //coordinates of the polygon
            var coords = new List<Location>
            {
                new Location
                {
                    Latitude = 42.779275360241904,
                    Longitude = 129.70458984375
                },
                new Location
                {
                    Latitude = 38.8225909761771,
                    Longitude = 125.48583984375
                },
                new Location
                {
                    Latitude = 38.27268853598097,
                    Longitude = 128.232421875
                },
				new Location
				{
					Latitude = 40.413496049701955,
					Longitude = 125.1123046875
				},
				new Location
				{
					Latitude = 40.84706035607122,
					Longitude = 129.79248046875
				},
				new Location
				{
					Latitude = 129.79248046875,
					Longitude = 126.18896484375
				}
            };

            //points which SHOULD be in the polygon
            var point = new List<Location>
            {
                new Location
                {
                    Latitude = 39.02468527021409,
                    Longitude = 126.33032389375
                },
                new Location
                {
                    Latitude = 42.38812451948162,
                    Longitude = 129.31860514375
                },
                new Location
                {
                    Latitude = 40.39379034950477,
                    Longitude = 125.209718425
                },
                new Location
                {
                    Latitude = 38.37305408682593,
                    Longitude = 128.022218425
                }
            };

            //points which are outside the polygon
            var outsidePoints = new List<Location>
            {
                new Location()
                {
                    Latitude = 39.7210851158478,
                    Longitude = 127.05542155
                },
                new Location()
                {
                    Latitude = 41.653579593196994,
                    Longitude = 126.50610514375
                },
                new Location()
                {
                    Latitude = 38.200586697825955,
                    Longitude = 127.03344889375
                },
                new Location()
                {
                    Latitude = 42.776409176734376,
                    Longitude = 129.71411295625
                }
            };

            point.ForEach(l => Assert.IsTrue(LocationHelper.PointInPolygon(coords, l)));

            outsidePoints.ForEach(k => Assert.IsFalse(LocationHelper.PointInPolygon(coords, k)));
        }
    }
}
