using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingAdmin.Models.ProjectsView
{
    public class ServicesOfProjectViewModel
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public IEnumerable<PreferenceOfServiceViewModel> Preferences { get; set; }

        static private Entities db = new Entities();

        public static IEnumerable<ServicesOfProjectViewModel> GetModel(int id)
        {
            return db.Projects_Services.Where(pr => pr.projectId == id).Join(
                db.Services,
                proj => proj.serviceId,
                serv => serv.id,
                (proj, serv) => new
                {
                    serviceId = serv.id,
                    srviceName = serv.name,
                    serviceData = proj.Services_Data

                }).Select(service => new ServicesOfProjectViewModel
                {
                    Id = service.serviceId,
                    ServiceName = service.srviceName,
                    Preferences = service.serviceData.Select(preference => new PreferenceOfServiceViewModel
                    {
                        PrefereceId = (int)preference.preferenceId,
                        PreferenceName = preference.Services_Preferences_HB.name,
                        PreferenceValue = preference.preferenceValue
                    }).AsEnumerable()
                }).AsEnumerable();
        }
    }

}