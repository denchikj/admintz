using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.ModelBinding;

namespace BillingAdmin.Models.AccountsView
{
    public class AccountsCreateViewModel
    {
        public int Id { get; set; }

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


        #region Договор

        [DisplayName("Номер договора")]
        public string NumberContract { get; set; }

        [DisplayName("Дата регистрации договора")]
        public DateTime? DateContract { get; set; }

        #endregion





        private static Entities db = new Entities();

        public static long SaveModel(AccountsCreateViewModel model)
        {
            Accounts account = new Accounts
            {
                ShortLegalName = model.ShortLegalName,
                FullLegalName = model.FullLegalName,
                INN = model.Inn,
                KPP = model.Kpp,
                Ogrn=model.Ogrn,
                Site = model.SiteAddress,
                Actual_Street = model.ActualStreet,
                Legal_Street = model.LegalStreet,

                DirectorFullName = model.DirectorFullName,
                PostDirector=model.PostDirector,
                Document=model.Document,
                ContactName = model.ContactName,
                Interaction=model.Interaction,
                Email = model.Email,
                Phone = new string(model.Phone?.Where(char.IsDigit).ToArray()),
                Industry = null,
                
                Bank_Bic = model.BankBic,
                Bank_Name = model.BankName,
                Bank_Account = model.BankAccount,
                Bank_Korrespondent = model.BankKorrespondent,

                NumberContract = model.NumberContract,
                DateContract=model.DateContract
            };
            
            
            db.Accounts.Add(account);
            db.SaveChanges();
            return account.Id;
        }

    }
}