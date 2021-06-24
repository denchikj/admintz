using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BillingAdmin.Models.UsersView
{
    public class UsersEditViewModel
    {
        [DisplayName("ID")]
        public string Id { get; set; }
        [DisplayName("Имя пользователя")]
        public string UserName { get; set; }
        [DisplayName("Email адрес (Имя пользователя)")]
        public string Email { get; set; }
        [DisplayName("Роль пользователя")]
        public string RoleId { get; set; }
        [DisplayName("Учетная запись sip")]
    
        public string PhoneNumber { get; set; }

        static private Entities db = new Entities();
        static public UsersEditViewModel GetModel(string id)
        {
            return db.AspNetUsers
                .Select(u => new UsersEditViewModel
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email
                }).Where(u => u.Id == id).Single();
        }
    }
}