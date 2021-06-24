using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Threading.Tasks;

namespace BillingAdmin.Models.UsersView
{
    public class UserCreateForAccountViewModel
    {
        [DisplayName("ID")]
        public string Id { get; set; }
        [DisplayName("Имя пользователя")]
        public string UserName { get; set; }

        public long AccountId { get; set; }
        [DisplayName("Сокращенное название клиента")]
        public string AccountName { get; set; }

        [DisplayName("Email адрес")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DisplayName("Роль пользователя")]
        public string RoleId { get; set; }

        [DisplayName("Телефон")]
        public string PhoneNumber { get; set; }

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
        static public UserCreateForAccountViewModel GetModel(int id)
        {
            return db.Accounts.Include(acus => acus.Accounts_Users)
                .Select(accoun => new UserCreateForAccountViewModel
                {
                    AccountId = accoun.Id,
                    AccountName = accoun.ShortLegalName
                }).Where(account => account.AccountId == id).Single();
        }
        static public async Task SaveModel(string userId, long accountId)
        {
            Accounts_Users accounts_Users = new Accounts_Users
            {
                accountId = accountId,
                userId = userId
            };
            db.Accounts_Users.Add(accounts_Users);
            await db.SaveChangesAsync();
        }
    }
}