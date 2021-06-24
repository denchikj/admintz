using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data.Entity;
using BillingAdmin.Models.ProjectsView;
using BillingAdmin.Models.UsersView;


namespace BillingAdmin.Models.AccountsView
{
    public class AccountsDetailsViewModel
    {
        public long Id { get; set; }


        #region Общие данные

        [DisplayName("Сокращенное название")]
        public string ShortLegalName { get; set; }

        [DisplayName("Полное название")]
        public string FullLegalName { get; set; }

        [DisplayName("ИНН")]
        public string Inn { get; set; }

        [DisplayName("КПП")]
        public string Kpp { get; set; }

        [DisplayName("ОГРН")]
        public string Ogrn { get; set; }

        [DisplayName("Адрес сайта")]
        public string SiteAddress { get; set; }

        [DisplayName("Фактический адрес")]
        public string ActualStreet { get; set; }

        [DisplayName("Юридический адрес")]
        public string LegalStreet { get; set; }
        
        #endregion


        #region Контактные данные

        [DisplayName("Директор (ФИО)")]
        public string DirectorFullName { get; set; }

        [DisplayName("Должность директора")]
        public string PostDirector { get; set; }

        [DisplayName("Документ подтверждающий полномочия")]
        public string Document { get; set; }

        [DisplayName("Контактное лицо")]
        public string ContactName { get; set; }

        [DisplayName("Вопросы взаимодействия")]
        public string Interaction { get; set; }

        [DisplayName("Email адрес")]
        public string Email { get; set; }

        [DisplayName("Телефон")]
        public string Phone { get; set; }
        
        #endregion


        #region Банковские реквизиты

        [DisplayName("БИК")]
        public string BankBic { get; set; }

        [DisplayName("Наименование банка")]
        public string BankName { get; set; }

        [DisplayName("Расчетный счет")]
        public string BankAccount { get; set; }

        [DisplayName("Корреспонденский счет")]
        public string BankKorrespondent { get; set; }
        
        #endregion


        #region Договор

        [DisplayName("Номер договора")]
        public string NumberContract { get; set; }
        [DisplayName("Дата регистрации договора")]
        public DateTime? DateContract { get; set; }
        
        #endregion


        #region Список пользователей

        public IEnumerable<UsersIndexViewModel> Users { get; set; }
        
        #endregion


        #region Список проектов

        public IEnumerable<ProjectsIndexViewModel> Projects { get; set; }
        
        #endregion


        private static Entities db = new Entities();

        public static AccountsDetailsViewModel GetModel(long? id)
        {

            return db.Accounts
                .Include(accountsUsers => accountsUsers.Accounts_Users).Include(projects => projects.Projects)
                .Select(accounts => new AccountsDetailsViewModel
                {
                    Id = accounts.Id,
                    ShortLegalName = accounts.ShortLegalName,
                    FullLegalName = accounts.FullLegalName,
                    Inn = accounts.INN,
                    Kpp = accounts.KPP,
                    Ogrn = accounts.Ogrn,
                    SiteAddress = accounts.Site,
                    ActualStreet = accounts.Actual_Street,
                    LegalStreet = accounts.Legal_Street,

                    DirectorFullName = accounts.DirectorFullName,
                    PostDirector = accounts.PostDirector,
                    Document= accounts.Document,
                    ContactName = accounts.ContactName,
                    Interaction=accounts.Interaction,
                    Email = accounts.Email,
                    Phone = accounts.Phone,

                    BankBic = accounts.Bank_Bic,
                    BankName = accounts.Bank_Name,
                    BankAccount = accounts.Bank_Account,
                    BankKorrespondent = accounts.Bank_Korrespondent,
                    NumberContract = accounts.NumberContract,

                    DateContract = accounts.DateContract,
                    Users = accounts.Accounts_Users.Select(users => new UsersIndexViewModel
                    {
                        Id = users.AspNetUsers.Id,
                        UserName = users.AspNetUsers.UserName,
                        Email = users.AspNetUsers.Email
                    }).AsEnumerable(),
                    Projects = accounts.Projects.Select(projects => new ProjectsIndexViewModel
                    {
                        Id = projects.id,
                        Name = projects.name,
                        AccountName = projects.Accounts.ShortLegalName,
                        OktelCode = projects.oktellCode,
                        TypeProject = projects.ProjectTypes.Disciption,
                        CreateDate = projects.date_create,
                        LastEditDate = projects.date_last_edit
                    }).AsEnumerable()

                }).Single(viewModel => viewModel.Id == id);
        }

        public static void DeleteModel(int id)
        {
            var projects = db.Accounts.Where(v => v.Id == id).SelectMany(v => v.Projects);
            var statistics = projects.SelectMany(v => v.PrjSettings_Data);

            db.PrjSettings_Data.RemoveRange(statistics);
            db.Projects.RemoveRange(projects);

            var accounts = db.Accounts.Find(id);
            // ReSharper disable once AssignNullToNotNullAttribute
            db.Accounts.Remove(accounts);
            db.SaveChanges();
        }
    }
}