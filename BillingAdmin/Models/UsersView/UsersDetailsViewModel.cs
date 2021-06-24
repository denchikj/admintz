using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data.Entity;
using System.Web;

namespace BillingAdmin.Models.UsersView
{
    public class UsersDetailsViewModel
    {
        [DisplayName("ID")]
        public string Id { get; set; }
        [DisplayName("Имя пользователя")]
        public string UserName { get; set; }
        [DisplayName("Email адрес")]
        public string Email { get; set; }

        public IEnumerable<Models.AccountsView.AccountsIndexViewModel> Accounts { get; set; }

        static private Entities db = new Entities();
        static public UsersDetailsViewModel GetModel(string id)
        {
           return db.AspNetUsers.Include(a => a.Accounts_Users).Select(u => new UsersDetailsViewModel
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                Accounts = u.Accounts_Users.Select(a => new Models.AccountsView.AccountsIndexViewModel
                {
                    Id = a.Accounts.Id,
                    ShortLegalName = a.Accounts.ShortLegalName,
                    FullLegalName = a.Accounts.FullLegalName,
                    INN = a.Accounts.INN,
                    KPP = a.Accounts.KPP,
                    DirectorFullName = a.Accounts.DirectorFullName,
                    ContactName = a.Accounts.ContactName,
                    Email = a.Accounts.Email,
                    Phone = a.Accounts.Phone
                }).AsEnumerable()
            }).Where(u => u.Id == id).Single();
        }
        static public void DeleteModel(string id)
        {
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            db.AspNetUsers.Remove(aspNetUsers);
            db.SaveChanges();
        }
    }
}