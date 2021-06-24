using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;

namespace BillingAdmin.Models.ProjectsView
{
    public class ProjectsCreateViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название")]
        [Required]
        public string Name { get; set; }
        [Required]
        public long AccountId { get; set; }
        [DisplayName("Сокращенное название клиента")]
        public string AccountName { get; set; }
        [DisplayName("Код OKTEL")]
        public string OktelCode { get; set; }
        [DisplayName("Тип Проекта")]
        public string TypeProjectId { get; set; }

        public IEnumerable<StatisticForProjectViewModel> Statistic { get; set; }
        public IEnumerable<TypeProjectViewModel> TypesProject { get; set; }
        

        static private Entities db = new Entities();

        static public ProjectsCreateViewModel GetModel(int accountId)
        {
            return db.Accounts.Include(proj => proj.Projects).Select(account => new ProjectsCreateViewModel
            {
                AccountId = account.Id,
                AccountName = account.ShortLegalName,
                Statistic = db.StatisticLeftMenu.Select(statistic => new StatisticForProjectViewModel
                {
                    StatisticName = statistic.name,
                    IdSettings = (int)statistic.project_setting_id
                }).AsEnumerable(),
                TypesProject = db.ProjectTypes.Select(type => new TypeProjectViewModel
                {
                    Id = type.Id,
                    Name = type.Name,
                    Disciption = type.Disciption
                }).AsEnumerable()
            }).Single(account => account.AccountId == accountId);
        }
        static public int SaveModel(ProjectsCreateViewModel model)
        {
            Projects newProjects = new Projects
            {
                id = model.Id,
                name = model.Name,
                accountId = model.AccountId,
                oktellCode = model.OktelCode,
                guid = Guid.NewGuid().ToString(),
                TypeId = Guid.Parse(model.TypeProjectId),
                date_create = DateTime.Now,
                date_last_edit = DateTime.Now
            };
            db.Projects.Add(newProjects);
            db.SaveChanges();
            int newProjId = newProjects.id;

            IEnumerable<PrjSettings_Data> newPrjSettingsData = model.Statistic
                .Select(data => new PrjSettings_Data
                {
                    preferences_id = data.IdSettings,
                    projects_id = newProjId,
                    value = data.StatisticStatus.ToString()
                }).AsEnumerable();
            db.PrjSettings_Data.AddRange(newPrjSettingsData);
            db.SaveChanges();  
            return newProjId;
        }
    }
}