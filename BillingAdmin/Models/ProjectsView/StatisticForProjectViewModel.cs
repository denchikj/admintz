using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingAdmin.Models.ProjectsView
{
    public class StatisticForProjectViewModel
    {
        public int Id { get; set; }
        public int? IdSettings { get; set; }
        public string StatisticName { get; set; }
        public bool StatisticStatus { get; set; }

        private static readonly Entities _db = new Entities();

        public static IEnumerable<StatisticForProjectViewModel> GetModel(int projectId)
        {
            var statistics = _db.PrjPreferences.Join(
                _db.StatisticLeftMenu,
                prefer => prefer.id,
                statLeftMenu => statLeftMenu.project_setting_id,
                (prefer, statLeftMenu) => new { prefer.id, prefer.name }
            ).Select(model => new StatisticForProjectViewModel
            {
                IdSettings = model.id,
                StatisticName = model.name
            }).ToList();

            foreach (var item in statistics)
            {
                var data = _db.PrjSettings_Data
                    .FirstOrDefault(d => (d.projects_id == projectId && d.preferences_id == item.IdSettings));
                if (data == null)
                {
                    continue;
                }
                item.StatisticStatus = data.value.ToUpper().Equals("TRUE");
                item.Id = data.id;
            }
            return statistics;
        }

    }
}