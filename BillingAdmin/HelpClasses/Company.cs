using Dadata;
using Dadata.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillingAdmin.HelpClasses
{
    public class Company
    {
        const string token = "b73496e656ed05029886fe43c0a27fae2c1f0023";
        public static Party FindCompany(string inn)
        {
            SuggestClient api;
            try
            {
                api = new SuggestClient(token);
            }
            catch (Exception)
            {
                throw new Exception("There's no internet connection or API's not responding");
            }

            SuggestResponse<Party> response = api.FindParty(inn.ToString());
            return response.suggestions[0].data;

        }
    }
}