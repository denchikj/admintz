using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace BillingAdmin.Models.Site
{
    public class ReCaptchaCheck
    {
        private static string GRecaptchaResponse;
        private static WebClient WebClient = new WebClient();
        static public bool IsSuccessful(string gRecaptchaResponse)
        {
            GRecaptchaResponse = gRecaptchaResponse;
            return Validation();
        }
        static private bool Validation()
        {
            return JObject.Parse(RequestToAPI())["success"].Value<bool>();
        }
        static private string RequestToAPI()
        {
            return WebClient.DownloadString(UrlAddress());
        }
        static private string UrlAddress()
        {
            return String.Concat(ReCaptchaConfig.URL, "?", GetParametrsRequest());
        }
        static private string GetParametrsRequest()
        {
            return String.Concat(ParametrRequestSecret(), "&", ParametrRequestResponse());
        }
        static private string ParametrRequestSecret()
        {
            return String.Concat(ReCaptchaConfig.ParametrsRequestSecret, ReCaptchaConfig.SecretKey);
        }
        static private string ParametrRequestResponse()
        {
            return String.Concat(ReCaptchaConfig.ParametrsRequestResponse, GRecaptchaResponse);
        }
    }
}