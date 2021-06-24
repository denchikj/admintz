using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingAdmin.Models.ProjectsView
{
    public class AddServiceModel
    {
        public int ProjectId { get; set; }
        public int ServiceId { get; set; }
        public Dictionary<int, string> Preferences { get; set; }

        private static Entities db = new Entities();
        public static bool SaveSevice(AddServiceModel model)
        {
            try
            {
                Projects_Services newProjectsServices = new Projects_Services
                {
                    projectId = model.ProjectId,
                    serviceId = model.ServiceId
                };
                db.Projects_Services.Add(newProjectsServices);
                db.SaveChanges();
                int newId = newProjectsServices.id;
                if (model.Preferences != null)
                {
                    IEnumerable<Services_Data> newServicesData = model.Preferences
                        .Select(preference => new Services_Data
                        {
                            project_serviceId = newId,
                            preferenceId = preference.Key,
                            preferenceValue = preference.Value
                        }).AsEnumerable();
                    db.Services_Data.AddRange(newServicesData);
                    db.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}