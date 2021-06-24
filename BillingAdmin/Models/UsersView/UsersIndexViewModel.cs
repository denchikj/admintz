using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BillingAdmin.Models.UsersView
{
    public class UsersIndexViewModel
    {
        [DisplayName("ID")]
        public string Id { get; set; }
        [DisplayName("Имя пользователя")]
        public string UserName { get; set; }
        [DisplayName("Email адрес")]
        public string Email { get; set; }
        [DisplayName("Роль пользователя")]
        public string RoleId { get; set; }

        static private Entities db = new Entities();
        static public IEnumerable<UsersIndexViewModel> GetModel()
        {
            return db.AspNetUsers.Select(u => new UsersIndexViewModel
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email
            });
        }         
    }
}