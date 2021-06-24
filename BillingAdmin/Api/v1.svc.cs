using BillingAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web;
using BillingAdmin.Controllers;
using System.ServiceModel.Activation;
using System.Web.Script.Serialization;
using System.Security.Cryptography;

namespace BillingAdmin.Api
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "v1" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы v1.svc или v1.svc.cs в обозревателе решений и начните отладку.
   // [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class v1 : Iv1
    {
        private Entities db = new Entities();

       
        public string DoWork(String composite)
        {
            //use the JavaScriptSerializer to convert the string to a CompositeType instance
            JavaScriptSerializer jscript = new JavaScriptSerializer();
            CompositeType newComp = jscript.Deserialize<CompositeType>(composite);
            newComp.StringValue += " NEW!";
            return newComp.StringValue;
            //return composite.StringValue;
        }

        public async System.Threading.Tasks.Task<string> CreateUserAsync(String userData)
        {
            string status = "sucess";

            JavaScriptSerializer jscript = new JavaScriptSerializer();
            UserLoginDataType newUserData = jscript.Deserialize<UserLoginDataType>(userData);

            Models.UsersView.UsersCreateViewModel model = new Models.UsersView.UsersCreateViewModel();
            model.UserName = newUserData.UserName;
            model.Password = newUserData.Password;
            model.Email = newUserData.UserName;
            model.RoleId = newUserData.RoleId;

           
            AspNetRoles roles = db.AspNetRoles.Where(c => c.Id == model.RoleId).FirstOrDefault();
            AspNetUsers users = new AspNetUsers();
            users.Email = newUserData.UserName;
            users.PasswordHash = HashPassword(newUserData.Password);
            users.UserName = newUserData.UserName;
            users.AspNetRoles.Add(roles);
            users.EmailConfirmed = true;
            users.PhoneNumberConfirmed = true;
            users.TwoFactorEnabled = false;
            users.LockoutEnabled = false;
            users.AccessFailedCount = 0;
            users.Id =  Guid.NewGuid().ToString();


            try
            {
                db.AspNetUsers.Add(users);
                var result = await db.SaveChangesAsync();

                if (result == -1)
                {
                    status = "error";
                }

            }
            catch (Exception ex)
            {

                status = "error";
            }  

            return status;
        }

        private static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (a == null && b == null)
            {
                return true;
            }
            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }
            var areSame = true;
            for (var i = 0; i < a.Length; i++)
            {
                areSame &= (a[i] == b[i]);
            }
            return areSame;
        }



        public static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return ByteArraysEqual(buffer3, buffer4);
        }



    }
}
