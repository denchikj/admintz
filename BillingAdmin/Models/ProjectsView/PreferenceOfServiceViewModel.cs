using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingAdmin.Models.ProjectsView
{
    public class PreferenceOfServiceViewModel
    {
        public int PrefereceId { get; set; }
        public string PreferenceName { get; set; }
        public string PreferenceValue { get; set; }

        private static Entities db = new Entities();
        public static IEnumerable<PreferenceOfServiceViewModel> GetModel(int id)
        {
            return db.Services_Preferences
                .Where(service => service.serviceid == id)
                .Join(
                db.Services_Preferences_HB,
                a => a.preferenceId,
                b => b.id,
                (a, b) => new
                {
                    id = b.id,
                    name = b.name
                }).Select(preference => new PreferenceOfServiceViewModel
                {
                    PrefereceId = preference.id,
                    PreferenceName = preference.name
                }).AsEnumerable();
        }
    }
}