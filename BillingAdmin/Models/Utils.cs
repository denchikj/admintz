using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace BillingAdmin.Models
{
    public class IndexRole : UsersView.UsersIndexViewModel
    {
        public string UserRoles { get; set; }
    }
    public class Client
    {
        public long Id { get; set; }
        public string ShortLegalName { get; set; }
        public string INN { get; set; }
        public string DirectorFullName { get; set; }
        public string ContactName { get; set; }
        public string Email { get; set; }
    }
    public class Statistic_pbxModel : Statistic_pbx
    {
        public long Id { get; set; }
        public string CallStart { get; set; } //Начало звонка
        public string OutcNumber { get; set; } //Номер звонящего
        public string IncNumber { get; set; } //Номер принявшего звонок
        public string Recording { get; set; } // Запись звонка
        public string Status { get; set; }
        public string Duration { get; set; }
        public string Razg { get; set; }
    }

    public static class Utils
    {
        public static string GeneratePassword(int Length, int NonAlphaNumericChars)
        {
            //генератор не правильный, цифра должна быть обязательной
            //1 цифра одна заглавная буква и 1 спецсимвол - например Frtq5g@s
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
            string allowedNum = "0123456789";
            string allowedNonAlpha = "!@#$%^&*_-:|";
            Random rd = new Random();

            if (NonAlphaNumericChars > Length || Length <= 0 || NonAlphaNumericChars < 0 || NonAlphaNumericChars*2 >= Length)
                throw new ArgumentOutOfRangeException();

            char[] pass = new char[Length];
            int[] pos = new int[Length];
            int i = 0, j = 0, temp = 0;
            bool flag = false;

            //Random the position values of the pos array for the string Pass
            while (i < Length - 1)
            {
                j = 0;
                flag = false;
                temp = rd.Next(0, Length);
                for (j = 0; j < Length; j++)
                    if (temp == pos[j])
                    {
                        flag = true;
                        j = Length;
                    }

                if (!flag)
                {
                    pos[i] = temp;
                    i++;
                }
            }

            //Random the AlphaNumericChars
            for (i = 0; i < Length - NonAlphaNumericChars - NonAlphaNumericChars; i++)

                pass[i] = allowedChars[rd.Next(0, allowedChars.Length)];

            //Random the NonAlphaNumericChars
            for (i = Length - NonAlphaNumericChars - NonAlphaNumericChars; i < Length - NonAlphaNumericChars; i++)

                pass[i] = allowedNum[rd.Next(0, allowedNum.Length)];

            //Генерируем Спецсимволы
            for (i = Length - NonAlphaNumericChars; i < Length; i++)
                pass[i] = allowedNonAlpha[rd.Next(0, allowedNonAlpha.Length)];

            //Set the sorted array values by the pos array for the rigth posistion
            char[] sorted = new char[Length];
            for (i = 0; i < Length; i++)
                sorted[i] = pass[pos[i]];

            string Pass = new String(sorted);

            return Pass;
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

    }
    
}