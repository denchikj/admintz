using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.ModelBinding;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace BillingAdmin.Models.UsersView
{
    public class UsersCreateViewModel
    {
        [DisplayName("ID")]
        public string Id { get; set; }

        [DisplayName("Имя пользователя")]
        public string UserName { get; set; }

        [DisplayName("Email адрес")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

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

        [DisplayName("Роль пользователя")]
        public string RoleId { get; set; }

        [DisplayName("Учетная запись sip")]
        public string PhoneNumber { get; set; }

        // public virtual AspNetRoles Roles { get; set; }


    }
}