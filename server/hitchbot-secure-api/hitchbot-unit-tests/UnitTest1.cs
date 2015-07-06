using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DotSpatial.Projections;
using DotSpatial.Data;
using DotSpatial.Topology;

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
        public void TestPolygonIntersection()
        {
            // this feature can be see visually here http://www.allhx.ca/on/toronto/westmount-park-road/25/
            string feature = "43.328174695525846,-79.851722717285156 43.18565317214177,-79.668388366699219 43.190659573987446,-80.028190612792969";

            string[] coordinates = feature.Split(' ');

            // dotspatial takes the x,y in a single array, and z in a separate array.  I'm sure there's a 
            // reason for this, but I don't know what it is.
            double[] xy = new double[coordinates.Length * 2];
            double[] z = new double[coordinates.Length];
            for (int i = 0; i < coordinates.Length; i++)
            {
                double lon = double.Parse(coordinates[i].Split(',')[0]);
                double lat = double.Parse(coordinates[i].Split(',')[1]);
                xy[i * 2] = lon;
                xy[i * 2 + 1] = lat;
                z[i] = 0;
            }

            ProjectionInfo pStart = KnownCoordinateSystems.Geographic.World.WGS1984;

            //which UTM zone to use can be found here http://en.wikipedia.org/wiki/Universal_Transverse_Mercator_coordinate_system
            //the feature is in Toronto, Canada which is Zone17.  Technically, should use 17T I think but this isn't defined
            //so am just using 17N - not sure what impact this has.
            //ProjectionInfo pEnd = KnownCoordinateSystems.Projected.UtmNad1927.NAD1927UTMZone17N;
            ProjectionInfo pEnd = KnownCoordinateSystems.Projected.NorthAmerica.USAContiguousLambertConformalConic;

            // do the actual reprojection
            Reproject.ReprojectPoints(xy, z, pStart, pEnd, 0, coordinates.Length);


            double[] pointXy = { 43.2423582, -79.8391097 };

            Reproject.ReprojectPoints(pointXy, z, pStart, pEnd, 0, 1);

            // build up a list of Coordinate, to create the polygon
            List<Coordinate> co = new List<Coordinate>();
            for (int i = 0; i < coordinates.Length; i++)
            {
                co.Add(new Coordinate(xy[i * 2], xy[i * 2 + 1]));
            }

            var middle = GeometryFactory.Default.CreatePoint(new Coordinate(pointXy[0], pointXy[1]));

            Polygon polygon = new Polygon(co);

            Assert.IsTrue(middle.Within(polygon));

            double area = polygon.Area;
        }
    }
}
