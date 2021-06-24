using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BillingAdmin.Models.UsersView
{
    public class UserAttachToAccountViewModel
    {
       
        public string Id { get; set; } 
        public long AccountId { get; set; }
        [DisplayName("Сокращенное название клиента")]
        public string AccountName { get; set; }

        [DisplayName("Список пользователей")]
        public IEnumerable<UsersDetailsViewModel> ListUsers { get; set; }

        static private Entities db = new Entities();
        static public UserAttachToAccountViewModel GetModel(int accountId)
        {
            return db.Accounts.Select(model => new UserAttachToAccountViewModel
            {
                AccountId = model.Id,
                AccountName = model.ShortLegalName,
                ListUsers = db.AspNetUsers.Select(user => new UsersDetailsViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName
                }).AsEnumerable()

            }).Where(model => model.AccountId == accountId).Single();
        }
        static public void AttachUser(UserAttachToAccountViewModel model)
        {
            db.Accounts_Users.Add(new Accounts_Users
            {   id=0,
                userId= model.Id,
                accountId=model.AccountId
            });
            db.SaveChanges();
        }
    }
}