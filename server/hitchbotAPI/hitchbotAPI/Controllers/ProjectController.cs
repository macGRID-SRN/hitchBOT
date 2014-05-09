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
    }
}