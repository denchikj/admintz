using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BillingAdmin.Models.UsersView
{
    public class UserResetPasswordViewModel
    {
        [DisplayName("ID")]
        public string Id { get; set; }
        [DisplayName("Имя пользователя")]
        public string UserName { get; set; }

        [DisplayName("Пароль")]
        [Required]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DisplayName("Подтверждение пароля")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }

        static private Entities db = new Entities();
        static public UserResetPasswordViewModel GetModel(string id)
        {
            return db.AspNetUsers
                .Select(u => new UserResetPasswordViewModel
                {
                    Id = u.Id,
                    UserName = u.UserName
                }).Where(u => u.Id == id).Single();
        }
    }
}