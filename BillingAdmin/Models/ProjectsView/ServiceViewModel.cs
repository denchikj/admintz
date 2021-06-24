using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingAdmin.Models.ProjectsView
{
    public class ServiceViewModel
    {
        public int ProjectId { get; set; }         
        public Dictionary<int,string> Services { get; set; }   

        static private Entities db = new Entities();         
        static public ServiceViewModel GetModel(int id)
        {
            return new ServiceViewModel
            {
                ProjectId = id,
                Services = db.Services.ToDictionary(key => key.id, value => value.name)
            };
        }
    }
}