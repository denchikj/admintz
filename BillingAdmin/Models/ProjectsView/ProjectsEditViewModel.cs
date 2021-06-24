using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel;
using System.Data.Entity.Migrations;

namespace BillingAdmin.Models.ProjectsView
{
    public class ProjectsEditViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название")]
        public string Name { get; set; }
        public long AccountId { get; set; }
        [DisplayName("Сокращенное название клиента")]
        public string AccountName { get; set; }
        [DisplayName("Код OKTEL")]
        public string OktelCode { get; set; }
        [DisplayName("Тип Проекта")]
        public Guid TypeProjectId { get; set; }
        [DisplayName("Дата создания")]
        public DateTime? CreateDate { get; set; }
        [DisplayName("Дата последнего редактирования")]
        public DateTime? LastEditDate { get; set; }

        
        public IEnumerable<TypeProjectViewModel> TypesProject { get; set; }

        private static Entities db = new Entities();
        public static ProjectsEditViewModel GetModel(int id)
        {
            return db.Projects.Include(account => account.Accounts).Include(t => t.ProjectTypes).Select(projResult => new ProjectsEditViewModel
            {
                Id = projResult.id,
                Name = projResult.name,
                AccountId = projResult.accountId,
                AccountName = projResult.Accounts.ShortLegalName,
                OktelCode = projResult.oktellCode,
                TypeProjectId = (Guid)projResult.TypeId,
                CreateDate = projResult.date_create,
                LastEditDate = projResult.date_last_edit,
                TypesProject = db.ProjectTypes.Select(types => new TypeProjectViewModel
                {
                    Id = types.Id,
                    Name = types.Name,
                    Disciption = types.Disciption
                }).AsEnumerable()
            }).Single(projResult => projResult.Id == id);
        }
        static public void SaveChangesModel(ProjectsEditViewModel model)
        {
            var project = db.Projects.Find(model.Id);
            project.name = model.Name;
            project.oktellCode = model.OktelCode;
            project.TypeId = model.TypeProjectId;
            project.date_last_edit = DateTime.Now;
            db.SaveChanges();
        }
    }
}