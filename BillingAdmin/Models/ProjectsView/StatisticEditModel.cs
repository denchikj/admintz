using System;
using System.Collections.Generic;
using System.Linq;

namespace BillingAdmin.Models.ProjectsView
{
    public class StatisticEditModel
    {
        public int ProjectId { get; set; }
        public int PreferencesId { get; set; }
        public string StatusStatistic { get; set; }

        private static Entities _db = new Entities();

        public static bool SaveStatistic(StatisticEditModel model)
        {
            try
            {
                var data = _db.PrjSettings_Data.FirstOrDefault(d =>
                    (d.projects_id == model.ProjectId && d.preferences_id == model.PreferencesId));
                if (data==null)
                {
                    _db.PrjSettings_Data.Add(new PrjSettings_Data
                    {
                        projects_id = model.ProjectId,
                        preferences_id = model.PreferencesId,
                        value = model.StatusStatistic
                    });
                    _db.SaveChanges();
                    return true;
                }

                data.value = model.StatusStatistic;
                _db.SaveChanges();

                return true;
            }
            catch 
            {
                return false;
            }
        }
    }
}