using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace BillingAdmin.Models.ProjectsView
{
    public class ProjectsDetailsViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название")]
        public string Name { get; set; }
        public long AccountId { get; set; }
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

        private static readonly Entities _db = new Entities();
        public static ProjectsDetailsViewModel GetModel(int id)
        {
            var model = _db.Projects
                 .Include(account => account.Accounts)
                .Include(t => t.ProjectTypes)
                .Select(projResult => new ProjectsDetailsViewModel
                {
                    Id = projResult.id,
                    Name = projResult.name,
                    AccountId = projResult.accountId,
                    AccountName = projResult.Accounts.ShortLegalName,
                    OktelCode = projResult.oktellCode,
                    TypeProject = projResult.ProjectTypes.Disciption,
                    CreateDate = projResult.date_create,
                    LastEditDate = projResult.date_last_edit
                }).Single(projResult => projResult.Id == id);
            return model;
        }
    }
}