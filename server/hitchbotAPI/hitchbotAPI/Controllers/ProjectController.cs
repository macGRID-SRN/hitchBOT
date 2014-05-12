using hitchbotAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace hitchbotAPI.Controllers
{
    /// <summary>
    /// This is for all things related to HitchBot projects. A HitchBot Project is when one or more HitchBot's embark on a journey.
    /// </summary>
    public class ProjectController : ApiController
    {
        /// <summary>
        /// Start a new HitchBot Project.
        /// </summary>
        /// <param name="Name">The name of the Project being created.</param>
        /// <param name="Description">The description of the Project being created.</param>
        /// <param name="StartTime">The time the Project was Started</param>
        /// <returns>Success.</returns>
        [HttpPost]
        public bool StartProject(string Name, string Description, DateTime StartTime)
        {
            using (var db = new Models.Database())
            {
                var newProject = new Models.Project();
                newProject.Name = Name;
                newProject.Description = Description;
                newProject.StartTime = StartTime;
                newProject.TimeAdded = DateTime.UtcNow;
                db.Projects.Add(newProject);
                db.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// Get all of the active HitchBot Projects.
        /// </summary>
        /// <returns>All of the active HitchBot Projects.</returns>
        [HttpGet]
        public List<Models.Project> GetCurrentProjects()
        {
            using (var db = new Models.Database())
            {
                return db.Projects.Where(p => p.EndTime == null).Include(p => p.EndLocation).Include(p => p.StartLocation).ToList();
            }
        }

        /// <summary>
        /// Get the details of a Project.
        /// </summary>
        /// <param name="ID">ID of the requested Project.</param>
        /// <returns></returns>
        [HttpGet]
        public Models.Project GetProjectByID(int ID)
        {
            using (var db = new Models.Database())
            {
                return db.Projects.Where(p => p.ID == ID).Include(p => p.EndLocation).Include(p => p.StartLocation).FirstOrDefault();
            }
        }

        /// <summary>
        /// End a Project.
        /// </summary>
        /// <param name="toEndID">The ID of the Project to end.</param>
        /// <returns></returns>
        [HttpPost]
        public bool EndProject(int toEndID)
        {
            using (var db = new Models.Database())
            {
                var projectToEnd = db.Projects.Single(p => p.ID == toEndID);
                projectToEnd.EndTime = DateTime.UtcNow;
                db.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <param name="LocationToAdd"></param>
        /// <returns></returns>
        [HttpPost]
        public bool SetStartLocation(int ProjectID, int LocationToAdd)
        {
            using (var db = new Models.Database())
            {
                var project = db.Projects.Single(p => p.ID == ProjectID);
                project.StartLocation = db.Locations.Single(l => l.ID == LocationToAdd);
                db.SaveChanges();
            }
            return true;
        }
    }
}