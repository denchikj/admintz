using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace BillingAdmin.Models.Site
{
    public class ReCaptchaConfig
    {
        public static string PublicKey { get; private set; }
        public static string SecretKey { get; private set; }
        public static string URL { get; private set; }
        public static string ParametrsRequestSecret { get; private set; }
        public static string ParametrsRequestResponse { get; private set; }
        public static List<string> ParametrsRequest { get; private set; }
        private static XDocument reCAPTCHAconfig;
        static ReCaptchaConfig()
        {
            LoadCongig();
            PublicKey = SetKey("publicKey");
            SecretKey = SetKey("secretKey");
            URL = SetUrl();
            ParametrsRequestSecret = SetParametrsRequest("secret");
            ParametrsRequestResponse = SetParametrsRequest("response");
        }
        static private void LoadCongig()
        {
            reCAPTCHAconfig = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/ReCaptcha.xml"));
        }
        static private string SetKey(string TypeKey)
        {
            return reCAPTCHAconfig.Root.Elements("key")
                .Select(k => k.Element(TypeKey).Value).Single();
        }
        static private string SetUrl()
        {
            return reCAPTCHAconfig.Root.Elements("api")
                .Select(u => u.Element("urlAdress").Value).Single();
        }
        static private string SetParametrsRequest(string TypeParametr)
        {
            return reCAPTCHAconfig.Root.Elements("api")
                .Select(p => p.Element("parametrsRequest"))
                .Select(t => t.Element(TypeParametr).Value).Single();
        }
    }
}