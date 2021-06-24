using BillingAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Microsoft.Owin.Security;

namespace BillingAdmin.Controllers
{
    public class HomeController : Controller
    {
        private Entities db = new Entities();
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult _MainMenu()
        {

            menuMain M = new menuMain();
            Dictionary<int, string> projects = new Dictionary<int, string>();
            Dictionary<long, string> accounts = new Dictionary<long, string>();

            var uid = User.Identity.GetUserId();

            var acc = db.Accounts.Where(a => a.Accounts_Users.Select(u => u.AspNetUsers.Id).Contains(uid));

            List<ItemMenuProfile> itemMenuProfile = db.Projects
                .Where(p => p.Accounts.Accounts_Users
                .Select(u => u.AspNetUsers.Id)
                .Contains(uid))
                .Select(i => new ItemMenuProfile { id = i.id, name = i.name, guid = i.guid }).ToList();

            foreach (var item in db.Projects.Where(p => acc.Select(a => a.Id).Contains(p.accountId)))
            {
                projects.Add(item.id, item.name);
            }

            foreach (var item in acc)
            {
                accounts.Add(item.Id, item.ShortLegalName);
            }

            M.menuProject = projects;
            M.menuAccaunts = accounts;
            M.menuProfile = itemMenuProfile;
            return PartialView("_MainMenu", M);
        }

        public ActionResult _ProfileMenu()
        {
            menuMainProfile M = new menuMainProfile();

            var uid = User.Identity.GetUserId();
            var acc = db.Accounts.Where(a => a.Accounts_Users.Select(u => u.AspNetUsers.Id).Contains(uid));
            if (acc.Count() == 0)
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return PartialView("_ProfileMenu", M);
            }
            M.accountId = acc.FirstOrDefault().Id;
            M.accountName = acc.FirstOrDefault().ShortLegalName;

            return PartialView("_ProfileMenu", M);
        }
        public ActionResult _AccountMenu()
        {
            menuMain M = new menuMain();

            Dictionary<long, string> accounts = new Dictionary<long, string>();

            var uid = User.Identity.GetUserId();
            var acc = db.Accounts.Where(a => a.Accounts_Users.Select(u => u.AspNetUsers.Id).Contains(uid));

            foreach (var item in acc)
            {
                accounts.Add(item.Id, item.ShortLegalName);
            }

            M.menuAccaunts = accounts;
            return PartialView("_AccountMenu", M);
        }
        public ActionResult _FormFeedbackRequests()
        {
            ViewBag.PublicKey = Models.Site.ReCaptchaConfig.PublicKey;
            return PartialView();
        }
    }
}