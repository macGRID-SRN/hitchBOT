using hitchbotAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace hitchbotAPI.Controllers
{
    public class ProjectController : ApiController
    {
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

        [HttpGet]
        public List<Project> GetCurrentProjects()
        {
            using (var db = new Database())
            {
                return db.Projects.Where(p => p.EndTime == null).ToList();
            }
        }

        [HttpGet]
        public Project GetProjectByID(int ID)
        {
            using (var db = new Database())
            {
                return db.Projects.Single(p => p.ID == ID);
            }
        }

        [HttpPost]
        public bool EndProject(int ID)
        {
            using (var db = new Database()){
                var projectToEnd = db.Projects.Single(p => p.ID == ID);
                projectToEnd.EndTime = DateTime.UtcNow;
                db.SaveChanges();
                return true;
            }
        }
    }
}