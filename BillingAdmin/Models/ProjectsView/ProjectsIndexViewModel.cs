using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data.Entity;
using System.Web;

namespace BillingAdmin.Models.ProjectsView
{
    public class ProjectsIndexViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название")]
        public string Name { get; set; }
        [DisplayName("Сокращенное название клиента")]
        public string AccountName { get; set; }
        [DisplayName("Код OKTEL")]
        public string OktelCode { get; set; }
        [DisplayName("Тип проекта")]
        public string TypeProject { get; set; }
        [DisplayName("Дата создания")]
        public DateTime? CreateDate { get; set; }
        [DisplayName("Дата последнего редактирования")]
        public DateTime? LastEditDate { get; set; }

        static private Entities db = new Entities();
        static public IEnumerable<ProjectsIndexViewModel> GetModel()
        {
            return db.Projects.Include(a => a.Accounts).Include(t=>t.ProjectTypes).Select(p => new ProjectsIndexViewModel
            {
                Id = p.id,
                Name = p.name,
                AccountName = p.Accounts.ShortLegalName,
                OktelCode = p.oktellCode,
                CreateDate = p.date_create,
                LastEditDate = p.date_last_edit,
                TypeProject = p.ProjectTypes.Disciption
                
            });
        }
    }
}