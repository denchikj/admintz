using System;
using System.ComponentModel;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace BillingAdmin.Models.AccountsView
{
    public class AccountsEditViewModel
    {
        public long Id { get; set; }

        #region Общие данные

        [DisplayName("Сокращенное название")]
        [Required(ErrorMessage = "{0}, обязательно для заполнения!")]
        public string ShortLegalName { get; set; }

        [DisplayName("Полное название")]
        [Required(ErrorMessage = "{0}, обязательно для заполнения!")]
        public string FullLegalName { get; set; }

        [DisplayName("ОГРН")]
        [Required(ErrorMessage = "{0}, обязательно для заполнения!")]
        public string Ogrn { get; set; }

        [DisplayName("ИНН")]
        [Required(ErrorMessage = "{0}, обязательно для заполнения!")]
        public string Inn { get; set; }

        [DisplayName("КПП")]
        [StringLength(9, ErrorMessage = "{0} должен быть {1} знаков", MinimumLength = 9)]
        public string Kpp { get; set; }

        [DisplayName("Фактический адрес")]
        public string ActualStreet { get; set; }
        [DisplayName("Юридический адрес")]
        public string LegalStreet { get; set; }

        [DisplayName("Адрес сайта")]
        [Url(ErrorMessage = "{0} не содержит допустимый полный URL-адрес http, https или ftp.")]
        public string SiteAddress { get; set; }

        #endregion


        #region Контактные данные

        [DisplayName("Директор (ФИО)")]
        [Required(ErrorMessage = "{0}, обязательно для заполнения!")]
        public string DirectorFullName { get; set; }

        [DisplayName("Должность директора")]
        public string PostDirector { get; set; }

        [DisplayName("Документ подтверждающий полномочия")]
        public string Document { get; set; }

        [DisplayName("Контактное лицо")]
        public string ContactName { get; set; }
        
        [DisplayName("Телефон")]
        [Phone]
        public string Phone { get; set; }

        [DisplayName("Email адрес")]
        [Required(ErrorMessage = "{0}, обязательно для заполнения!")]
        [EmailAddress(ErrorMessage = "{0} не содержит допустимый адрес электронной почты")]
        public string Email { get; set; }

        [DisplayName("Вопросы взаимодействия")]
        public string Interaction { get; set; }

        #endregion


        #region Банковские реквизиты

        [DisplayName("БИК")]
        [StringLength(9, ErrorMessage = "{0} должен быть {1} знаков", MinimumLength = 9)]
        public string BankBic { get; set; }

        [DisplayName("Наименование банка")]
        public string BankName { get; set; }

        [DisplayName("Расчетный счет")]
        [StringLength(20, ErrorMessage = "{0} должен быть {1} знаков", MinimumLength = 20)]
        public string BankAccount { get; set; }

        [DisplayName("Корреспонденский счет")]
        [StringLength(20, ErrorMessage = "{0} должен быть {1} знаков", MinimumLength = 20)]
        public string BankKorrespondent { get; set; }

        #endregion


        [DisplayName("Номер договора")]
        public string NumberContract { get; set; }
        [DisplayName("Дата регистрации договора")]
        public DateTime? DateContract { get; set; }


        private static Entities db = new Entities();

        public static AccountsEditViewModel GetModel(long id)
        {
            return db.Accounts.Select(a => new AccountsEditViewModel
                {
                    Id = a.Id,
                    ShortLegalName = a.ShortLegalName,
                    FullLegalName = a.FullLegalName,
                    Inn = a.INN,
                    Kpp = a.KPP,
                    DirectorFullName = a.DirectorFullName,
                    ContactName = a.ContactName,
                    Email = a.Email,
                    Phone = a.Phone,
                    SiteAddress = a.Site,
                    ActualStreet = a.Actual_Street,
                    LegalStreet = a.Legal_Street,
                    BankBic = a.Bank_Bic,
                    BankName = a.Bank_Name,
                    BankAccount = a.Bank_Account,
                    BankKorrespondent = a.Bank_Korrespondent,
                    NumberContract = a.NumberContract,
                    DateContract =a.DateContract,
                     Ogrn=a.Ogrn,
                      Document=a.Document,
                       PostDirector=a.PostDirector,
                        Interaction=a.Interaction
                    
                }).Single(a => a.Id == id);
        }
        public static void SaveChangesModel(AccountsEditViewModel model)
        {
            var account = db.Accounts.Find(model.Id);
            // ReSharper disable once PossibleNullReferenceException
            account.ShortLegalName = model.ShortLegalName;
            account.FullLegalName = model.FullLegalName;
            account.INN = model.Inn;
            account.KPP = model.Kpp;
            account.DirectorFullName = model.DirectorFullName;
            account.ContactName = model.ContactName;
            account.Email = model.Email;
            account.Phone = new string(model.Phone?.Where(char.IsDigit).ToArray());
            account.Industry = null;
            account.Site = model.SiteAddress;
            account.Actual_Street = model.ActualStreet;
            account.Legal_Street = model.LegalStreet;
            account.Bank_Bic = model.BankBic;
            account.Bank_Name = model.BankName;
            account.Bank_Account = model.BankAccount;
            account.Bank_Korrespondent = model.BankKorrespondent;
            account.NumberContract = model.NumberContract;
            account.DateContract = model.DateContract;
            db.SaveChanges();
        }
    }
}