using hitchbotAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

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
        /// <returns>Success.</returns>
        [HttpPost]
        public bool StartProject(string Name, string Description)
        {
            using (var db = new Database())
            {
                var newProject = new Project();
                newProject.Name = Name;
                newProject.Description = Description;
                newProject.StartTime = DateTime.UtcNow;
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
        public List<Project> GetCurrentProjects()
        {
            using (var db = new Database())
            {
                return db.Projects.Where(p => p.EndTime == null).ToList();
            }
        }

        /// <summary>
        /// Get the details of a Project.
        /// </summary>
        /// <param name="ID">ID of the requested Project.</param>
        /// <returns></returns>
        [HttpGet]
        public Project GetProjectByID(int ID)
        {
            using (var db = new Database())
            {
                return db.Projects.Single(p => p.ID == ID);
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
            using (var db = new Database())
            {
                var projectToEnd = db.Projects.Single(p => p.ID == toEndID);
                projectToEnd.EndTime = DateTime.UtcNow;
                db.SaveChanges();
                return true;
            }
        }
    }
}