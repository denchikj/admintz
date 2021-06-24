using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BillingAdmin.Models.AccountsView
{
    public class AccountsIndexViewModel
    {
        public long Id { get; set; }
        [DisplayName("Сокращенное название")]
        public string ShortLegalName { get; set; }
        [DisplayName("Полное название")]
        public string FullLegalName { get; set; }
        [DisplayName("ИНН")]
        public string INN { get; set; }
        [DisplayName("КПП")]
        public string KPP { get; set; }
        [DisplayName("Директор (ФИО)")]
        public string DirectorFullName { get; set; }
        [DisplayName("Контактное лицо")]
        public string ContactName { get; set; }
        [DisplayName("Email адрес")]
        public string Email { get; set; }
        [DisplayName("Телефон")]
        public string Phone { get; set; }

        static private Entities db = new Entities();
        
        static public IEnumerable<AccountsIndexViewModel> SetModel()
        {
           return db.Accounts.Select(a => new AccountsIndexViewModel
            {
                Id = a.Id,
                ShortLegalName = a.ShortLegalName,
                FullLegalName = a.FullLegalName,
                INN = a.INN,
                KPP = a.KPP.ToString(),
                DirectorFullName = a.DirectorFullName,
                ContactName = a.ContactName,
                Email = a.Email,
                Phone = a.Phone,
            });
        }
    }
}