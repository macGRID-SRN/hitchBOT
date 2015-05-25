using System.Collections.Generic;
using hitchbot_secure_api.Models;

namespace hitchbot_secure_api.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<hitchbot_secure_api.Dal.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
        }

        //methodology used was adapted from http://www.asp.net/mvc/overview/getting-started/getting-started-with-ef-using-mvc/creating-a-more-complex-data-model-for-an-asp-net-mvc-application
        protected override void Seed(hitchbot_secure_api.Dal.DatabaseContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var locations = new List<Location>()
            {
                new Location()
                {
                    NearestCity  = "SeedLocation1",
                    Latitude = 43.2423582m,
                    Longitude = -79.8391097m //also the true birthplace of hitchBOT
                },
                new Location()
                {
                    NearestCity = "SeedLocation2",
                    Latitude = 43.7m,
                    Longitude = -79.4m,
                },
                new Location()
                {
                    NearestCity = "SeedHitchBOTLocation1",
                    Latitude = 45,
                    Longitude = -34,
                    TakenTime = DateTime.UtcNow.AddMinutes(-1)
                },
                new Location()
                {
                    NearestCity = "SeedHitchBOTLocation2",
                    Latitude = 46,
                    Longitude = -35,
                    TakenTime = DateTime.UtcNow.AddMinutes(-1)
                },
                new Location()
                {
                    NearestCity = "SeedHitchBOT2Location1",
                    Latitude = 49,
                    Longitude = -12,
                    TakenTime = DateTime.UtcNow.AddMinutes(-1)
                },
                new Location()
                {
                    NearestCity = "SeedHitchBOT2Location2",
                    Latitude = 12,
                    Longitude = -7,
                    TakenTime = DateTime.UtcNow.AddMinutes(-1)
                }
            };

            locations.ForEach(l => context.Locations.AddOrUpdate(s => s.NearestCity, l));
            context.SaveChanges();

            context.Journeys.AddOrUpdate(
               new Journey
                {
                    Name = "SeedJourney",
                    StartLocation = context.Locations.Single(l => l.NearestCity == "SeedLocation1"),
                    EndLocation = context.Locations.Single(l => l.NearestCity == "SeedLocation2"),
                    StartTime = DateTime.UtcNow,
                }
            );
            context.SaveChanges();

            var hitchBots = new List<HitchBot>()
            {
                new HitchBot
                {
                    Name = "SeedHitchBot1",
                    JourneyId = context.Journeys.Single(l=>l.Name == "SeedJourney").Id,
                    Locations = new List<Location>()
                    {
                        context.Locations.Single(l=>l.NearestCity == "SeedHitchBOTLocation1"),
                        context.Locations.Single(l=>l.NearestCity == "SeedHitchBOTLocation2")
                    }
                },
                new HitchBot
                {
                    Name = "SeedHitchBot2",
                    JourneyId = context.Journeys.Single(l=>l.Name == "SeedJourney").Id,
                    Locations = new List<Location>()
                    {
                        context.Locations.Single(l=>l.NearestCity == "SeedHitchBOT2Location1"),
                        context.Locations.Single(l=>l.NearestCity == "SeedHitchBOT2Location2")
                    }
                }
            };

            hitchBots.ForEach(l => context.HitchBots.AddOrUpdate(s => s.Name, l));
        }
    }
}
