using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BillingAdmin.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using BillingAdmin.HelpClasses;
using Dadata.Model;
using Newtonsoft.Json;

namespace BillingAdmin.Controllers
{
    [CustomAuthorizeAttribute]
    public class AspNetUsersController : Controller
    {
        private Entities db = new Entities();

        #region ForAspNetIdentity
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public AspNetUsersController()
        {
        }
        public AspNetUsersController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        #endregion     

        public ActionResult Index()
        {
            return View(Models.UsersView.UsersIndexViewModel.GetModel());
        }

        public async Task<ActionResult> IndexRole(Models.UsersView.UsersIndexViewModel model, string id)
        {
            //var roles = db.AspNetRoles.Select(x => x.Name);
            //ViewBag.Role = await db.AspNetRoles.Where(x => x.Id != null).OrderBy(x => db.AspN).Select(x => x.Name).ToListAsync();
            //ViewBag.Roles = await db.AspNetUsers.Where(x => x.Id != null).Select(c => c.AspNetRoles).ToListAsync();
            return View(Models.UsersView.UsersIndexViewModel.GetModel());
        }

        [HttpPost]
        public string GetIndexRole(int? draw, int? start, int? length)
        {
            var search = Request["search[value]"];
            var totalRecords = 0;
            var recordsFiltered = 0;
            start = start.HasValue ? start / length : 0;
            var catalog = new IndexRolesRepository().GetPaginated(ControllerContext, search, start ?? 0, length ?? 25, out totalRecords, out recordsFiltered);
            string json = JsonConvert.SerializeObject(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = totalRecords, data = catalog });
            return json;
        }

        public ActionResult IndexRoleAdmins()
        {
            var IdAdminRoleInAccount = db.Accounts_Roles.FirstOrDefault(account => account.Role == 1).Account;
            var userIdAccount = db.Accounts_Users.Where(account => account.accountId == IdAdminRoleInAccount).Select(user => user.AspNetUsers).ToList();
            return View(userIdAccount);
        }

        public ActionResult IndexRoleOperator()
        {
            var IdAdminRoleInAccount = db.Accounts_Roles.FirstOrDefault(account => account.Role == 1).Account;
            var userIdAccount = db.Accounts_Users.Where(account => account.accountId == IdAdminRoleInAccount && account.AspNetUsers.AspNetRoles.FirstOrDefault(role => role.Name == "Operator") != null).Select(user => user.AspNetUsers).ToList();
            return View(userIdAccount);
        }
        [HttpGet]
        public ActionResult Logs()
        {
            IQueryable<Logs> query = db.Logs.OrderByDescending(c => c.id).Take(1000);
            return View(query);
        }

        #region Details       
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(Models.UsersView.UsersDetailsViewModel.GetModel(id));
        }
        #endregion

        #region DetailsRole  
        public ActionResult DetailsRole(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(Models.UsersView.UsersDetailsViewModel.GetModel(id));
        }
        #endregion

        #region DetailsRoleAdmin      
        public ActionResult DetailsRoleAdmin(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(Models.UsersView.UsersDetailsViewModel.GetModel(id));
        }
        #endregion

        #region DetailsRoleOperator      
        public ActionResult DetailsRoleOperator(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(Models.UsersView.UsersDetailsViewModel.GetModel(id));
        }
        #endregion

        #region DetailsRoleCompany  
        public ActionResult DetailsRoleCompany(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(Models.UsersView.UsersDetailsViewModel.GetModel(id));
        }
        #endregion

        #region Create
        public ActionResult Create()
        {
            ViewBag.GeneratePassword = GeneratePassword(8, 1);
            ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name");
            ViewBag.UserRoles = db.AspNetRoles.ToList();
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Models.UsersView.UsersCreateViewModel model, string userRoles, string emailSend, string firstName = null, string midName = null, string lastName = null)
        {
            string contactName2 = "";
            string status = "sucess";

            ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email, PhoneNumber = model.PhoneNumber };
            IdentityResult result = await UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                if (firstName != null)
                {
                    var res = await UserManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.Name, firstName));
                    contactName2 += firstName + " ";
                }

                if (midName != null)
                {
                    var res = await UserManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.GivenName, midName));
                    contactName2 += midName + " ";
                }

                if (lastName != null)
                {
                    var res = await UserManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.Surname, lastName));
                    contactName2 += lastName + " ";
                }

                var userRolesObj = string.IsNullOrEmpty(userRoles) == false ? System.Web.Helpers.Json.Decode(userRoles) : new List<string>() { "0" };

                foreach (string userRole in userRolesObj)
                {
                    Guid id = Guid.NewGuid();
                    if (userRole != "0")
                    {
                        AspNetRoles role = db.AspNetRoles.FirstOrDefault(x => x.Name == userRole);
                        db.AspNetUsers.Where(c => c.Id == user.Id).FirstOrDefault().AspNetRoles.Add(role);
                    }
                    await db.SaveChangesAsync();
                    if (userRole == "0") break;
                }

                status = "error";

                if (emailSend != null)
                {
                    IQueryable<Accounts> contactAccount = db.Accounts.Where((Accounts c) => user.Email.Equals(c.Email));

                    if (contactAccount.Count() > 0)
                    {
                        string contactName = contactAccount.FirstOrDefault().ContactName;
                        await SendEmail.SendPassword(user.Email, base.HttpContext, contactName, contactAccount.FirstOrDefault().Id, model.Password);
                    }
                    else
                    {
                        await SendEmail.SendPassword(user.Email, base.HttpContext, user.Email, 0, model.Password);
                    }
                }

                return RedirectToAction("Details", new { id = user.Id });
            }
            AddErrors(result);

            ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name");
            return View(model);
        }

        #endregion



        #region CreateRole
        public ActionResult CreateRole()
        {
            ViewBag.GeneratePassword = GeneratePassword(8, 1);
            ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name");
            ViewBag.UserRoles = db.AspNetRoles.ToList();

            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateRole(Models.UsersView.UsersCreateViewModel model, string userRoles, string emailSend, string firstName = null, string midName = null, string lastName = null)
        {
            string contactName2 = "";
            string status = "sucess";

            ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email, PhoneNumber = model.PhoneNumber };
            IdentityResult result = await UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                if (firstName != null)
                {
                    var res = await UserManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.Name, firstName));
                    contactName2 += firstName + " ";
                }

                if (midName != null)
                {
                    var res = await UserManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.GivenName, midName));
                    contactName2 += midName + " ";
                }

                if (lastName != null)
                {
                    var res = await UserManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.Surname, lastName));
                    contactName2 += lastName + " ";
                }

                var userRolesObj = string.IsNullOrEmpty(userRoles) == false ? System.Web.Helpers.Json.Decode(userRoles) : new List<string>() { "0" };

                foreach (string userRole in userRolesObj)
                {
                    Guid id = Guid.NewGuid();
                    if (userRole != "0")
                    {
                        AspNetRoles role = db.AspNetRoles.FirstOrDefault(x => x.Name == userRole);
                        db.AspNetUsers.Where(c => c.Id == user.Id).FirstOrDefault().AspNetRoles.Add(role);
                    }
                    await db.SaveChangesAsync();
                    if (userRole == "0") break;
                }

                status = "error";

                if (emailSend != null)
                {
                    IQueryable<Accounts> contactAccount = db.Accounts.Where((Accounts c) => user.Email.Equals(c.Email));

                    if (contactAccount.Count() > 0)
                    {
                        string contactName = contactAccount.FirstOrDefault().ContactName;
                        await SendEmail.SendPassword(user.Email, base.HttpContext, contactName, contactAccount.FirstOrDefault().Id, model.Password);
                    }
                    else
                    {
                        await SendEmail.SendPassword(user.Email, base.HttpContext, user.Email, 0, model.Password);
                    }
                }

                return RedirectToAction("DetailsRole", new { id = user.Id });
            }
            AddErrors(result);

            ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name");
            return View(model);
        }
        #endregion

        #region CreateRoleAdmin
        public ActionResult CreateRoleAdmin()
        {
            ViewBag.GeneratePassword = GeneratePassword(8, 1);
            ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name");
            ViewBag.UserRoles = db.AspNetRoles.ToList();

            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateRoleAdmin(Models.UsersView.UsersCreateViewModel model, string userRoles, string emailSend, string firstName = null, string midName = null, string lastName = null)
        {
            string contactName2 = "";
            string status = "sucess";

            ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email, PhoneNumber = model.PhoneNumber };
            IdentityResult result = await UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                if (firstName != null)
                {
                    var res = await UserManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.Name, firstName));
                    contactName2 += firstName + " ";
                }

                if (midName != null)
                {
                    var res = await UserManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.GivenName, midName));
                    contactName2 += midName + " ";
                }

                if (lastName != null)
                {
                    var res = await UserManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.Surname, lastName));
                    contactName2 += lastName + " ";
                }

                var userRolesObj = string.IsNullOrEmpty(userRoles) == false ? System.Web.Helpers.Json.Decode(userRoles) : new List<string>() { "0" };

                foreach (string userRole in userRolesObj)
                {
                    Guid id = Guid.NewGuid();
                    if (userRole != "0")
                    {
                        AspNetRoles role = db.AspNetRoles.FirstOrDefault(x => x.Name == userRole);
                        db.AspNetUsers.Where(c => c.Id == user.Id).FirstOrDefault().AspNetRoles.Add(role);
                    }
                    await db.SaveChangesAsync();
                    if (userRole == "0") break;
                }

                status = "error";

                if (emailSend != null)
                {
                    IQueryable<Accounts> contactAccount = db.Accounts.Where((Accounts c) => user.Email.Equals(c.Email));

                    if (contactAccount.Count() > 0)
                    {
                        string contactName = contactAccount.FirstOrDefault().ContactName;
                        await SendEmail.SendPassword(user.Email, base.HttpContext, contactName, contactAccount.FirstOrDefault().Id, model.Password);
                    }
                    else
                    {
                        await SendEmail.SendPassword(user.Email, base.HttpContext, user.Email, 0, model.Password);
                    }
                }

                return RedirectToAction("DetailsRoleAdmin", new { id = user.Id });
            }
            AddErrors(result);

            ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name");
            return View(model);
        }
        #endregion

        #region CreateRoleOperator
        public ActionResult CreateRoleOperator()
        {
            ViewBag.GeneratePassword = GeneratePassword(8, 1);
            ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name");
            ViewBag.UserRoles = db.AspNetRoles.ToList();

            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateRoleOperator(Models.UsersView.UsersCreateViewModel model, string userRoles, string emailSend, string firstName = null, string midName = null, string lastName = null)
        {
            string contactName2 = "";
            string status = "sucess";

            ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email, PhoneNumber = model.PhoneNumber };
            IdentityResult result = await UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                if (firstName != null)
                {
                    var res = await UserManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.Name, firstName));
                    contactName2 += firstName + " ";
                }

                if (midName != null)
                {
                    var res = await UserManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.GivenName, midName));
                    contactName2 += midName + " ";
                }

                if (lastName != null)
                {
                    var res = await UserManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.Surname, lastName));
                    contactName2 += lastName + " ";
                }

                var userRolesObj = string.IsNullOrEmpty(userRoles) == false ? System.Web.Helpers.Json.Decode(userRoles) : new List<string>() { "0" };

                foreach (string userRole in userRolesObj)
                {
                    Guid id = Guid.NewGuid();
                    if (userRole != "0")
                    {
                        AspNetRoles role = db.AspNetRoles.FirstOrDefault(x => x.Name == userRole);
                        db.AspNetUsers.Where(c => c.Id == user.Id).FirstOrDefault().AspNetRoles.Add(role);
                    }
                    await db.SaveChangesAsync();
                    if (userRole == "0") break;
                }

                status = "error";

                if (emailSend != null)
                {
                    IQueryable<Accounts> contactAccount = db.Accounts.Where((Accounts c) => user.Email.Equals(c.Email));

                    if (contactAccount.Count() > 0)
                    {
                        string contactName = contactAccount.FirstOrDefault().ContactName;
                        await SendEmail.SendPassword(user.Email, base.HttpContext, contactName, contactAccount.FirstOrDefault().Id, model.Password);
                    }
                    else
                    {
                        await SendEmail.SendPassword(user.Email, base.HttpContext, user.Email, 0, model.Password);
                    }
                }

                return RedirectToAction("DetailsRoleOperator", new { id = user.Id });
            }
            AddErrors(result);

            ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name");
            return View(model);
        }
        #endregion

        #region CreateForAccount        
        //[HttpPost]
        public ActionResult CreateForAccount(int id)
        {
            ViewBag.GeneratePassword = GeneratePassword(8, 3);
            ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name");
            ViewBag.UserRoles = db.AspNetRoles.ToList();
            //ViewBag.CurrentUserRoles = db.AspNetUsers.Find(id).AspNetRoles.Select(role => role.Id).ToList(); ;

            return View(Models.UsersView.UserCreateForAccountViewModel.GetModel(id));
        }
        [HttpPost]
        public async Task<ActionResult> CreateForAccount(Models.UsersView.UserCreateForAccountViewModel model, string emailSend, string firstName = null, string midName = null, string lastName = null)
        {
            if (ModelState.IsValid)
            {
                AspNetRoles roles = db.AspNetRoles.Where(c => c.Id == model.RoleId).FirstOrDefault();

                ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email, PhoneNumber = model.PhoneNumber };
                //user.Roles.Add(roles.Name);

                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (firstName != null)
                    {
                        var res = await UserManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.Name, firstName));
                    }

                    if (midName != null)
                    {
                        var res = await UserManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.GivenName, midName));
                    }

                    if (lastName != null)
                    {
                        var res = await UserManager.AddClaimAsync(user.Id, new Claim(ClaimTypes.Surname, lastName));
                    }

                    await Models.UsersView.UserCreateForAccountViewModel.SaveModel(user.Id, model.AccountId);
                    UserManager.AddToRole(user.Id, roles.Name);

                    if (emailSend != null)
                    {
                        IQueryable<Accounts> contactAccount = db.Accounts.Where((Accounts c) => user.Email.Equals(c.Email));
                        // AspNetUsers u = db.AspNetUsers.Find(user.Id);
                        //u.PasswordHash = Utils.HashPassword(model.Password);
                        // u.SecurityStamp = Guid.NewGuid().ToString("D");

                        if (contactAccount.Count() > 0)
                        {
                            string contactName = contactAccount.FirstOrDefault().ContactName;
                            await SendEmail.SendPassword(user.Email, base.HttpContext, contactName, contactAccount.FirstOrDefault().Id, model.Password);

                        }
                        else
                        {
                            await SendEmail.SendPassword(user.Email, base.HttpContext, user.Email, 0, model.Password);

                        }
                    }

                    return RedirectToAction("Details", "Accounts", new { id = model.AccountId });
                }
                AddErrors(result);
            }
            ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name");
            return View(model);
        }
        #endregion

        [HttpPost]
        public string GeneratePassword(int Length, int NonAlphaNumericChars)
        {
            return Utils.GeneratePassword(Length, NonAlphaNumericChars);
        }
        #region AttachToAccount
        public ActionResult AttachToAccount(int id)
        {
            return View(Models.UsersView.UserAttachToAccountViewModel.GetModel(id));
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult AttachToAccount(Models.UsersView.UserAttachToAccountViewModel model)
        {
            Models.UsersView.UserAttachToAccountViewModel.AttachUser(model);
            return RedirectToAction("Details", "Accounts", new { id = model.AccountId });
        }
        public JsonResult GetUserList(string term)
        {
            var CategoryList = db.AspNetUsers.Where(x => x.UserName.ToLower().Contains(term.ToLower())).Select(c => new { Id = c.Id, UserName = c.UserName }).ToList();
            return Json(CategoryList, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region DetachUser
        public ActionResult DetachUser(string id, long accountId)
        {
            var AccountsUsers = db.Accounts_Users.Where(c => c.userId == id);
            db.Accounts_Users.RemoveRange(AccountsUsers);
            db.SaveChanges();

            return RedirectToAction("Details", "Accounts", new { id = accountId });
        }
        #endregion

        #region AddRoleUser
        private bool AddRoleUser(string idUser, string userRoles)
        {
            var UserRoles = db.AspNetUsers.Find(idUser).AspNetRoles.ToList();
            if (UserRoles.Count() > 0)
                foreach (var role in UserRoles)
                {
                    db.AspNetUsers.Find(idUser).AspNetRoles.Remove(role);
                }

            db.SaveChanges();
            AspNetUsers user = db.AspNetUsers.Find(idUser);

            var userRolesObj = string.IsNullOrEmpty(userRoles) == false ? System.Web.Helpers.Json.Decode(userRoles) : new List<string>() { "0" };

            foreach (string userRole in userRolesObj)
            {
                Guid id = Guid.NewGuid();
                //string userRoleId = null;
                if (userRole != "0")
                {
                    //userRoleId = db.AspNetRoles.FirstOrDefault().Id;
                    AspNetRoles roles = db.AspNetRoles.FirstOrDefault(role => role.Name == userRole);
                    //if (rules.Select(roles => roles.id).ToList().Contains(id.ToString()))
                    //id = new Guid("d");
                    db.AspNetUsers.Where(c => c.Id == user.Id).FirstOrDefault().AspNetRoles.Add(roles);
                }
                db.SaveChanges();
                if (userRole == "0") break;
            }
            return true;
        }
        #endregion


        #region Edit       
        public async Task<ActionResult> Edit(string id, string emailSend)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (db.AspNetUsers.Find(id).AspNetRoles.Count() > 0)
            {
                var RoleId = db.AspNetUsers.Find(id).AspNetRoles.FirstOrDefault().Id;
                ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name", RoleId);

                ViewBag.CurrentUserRoles = db.AspNetUsers.Find(id).AspNetRoles.Select(role => role.Id).ToList();
                ViewBag.UserRoles = db.AspNetRoles.ToList();
            }
            else
            {
                ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name");
            }
            ViewBag.UserRoles = db.AspNetRoles.ToList();

            return View(Models.UsersView.UsersEditViewModel.GetModel(id));
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(Models.UsersView.UsersEditViewModel model, string userRoles)
        {
            if (ModelState.IsValid)
            {
                db.SaveChanges();

                if (userRoles != "null")
                {
                    if (AddRoleUser(model.Id, userRoles) == true)
                    {
                        //return RedirectToAction("Index");
                    }
                }
                else
                {
                    var UserRoles = db.AspNetUsers.Find(model.Id).AspNetRoles;
                    if (UserRoles.Count() > 0)
                        foreach (var role in UserRoles)
                        {
                            UserRoles.Remove(role);
                        }

                    db.SaveChanges();
                }

                return RedirectToAction("Details", new { id = model.Id });
            }

            ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name", model.RoleId);
            return View(model);
        }
        #endregion

        #region EditRole       
        public async Task<ActionResult> EditRole(string id, string emailSend)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (db.AspNetUsers.Find(id).AspNetRoles.Count() > 0)
            {
                var RoleId = db.AspNetUsers.Find(id).AspNetRoles.FirstOrDefault().Id;
                ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name", RoleId);

                ViewBag.CurrentUserRoles = db.AspNetUsers.Find(id).AspNetRoles.Select(role => role.Id).ToList();
                ViewBag.UserRoles = db.AspNetRoles.ToList();
            }
            else
            {
                ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name");
            }
            ViewBag.UserRoles = db.AspNetRoles.ToList();

            return View(Models.UsersView.UsersEditViewModel.GetModel(id));
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult EditRole(Models.UsersView.UsersEditViewModel model, string userRoles)
        {
            if (ModelState.IsValid)
            {
                db.SaveChanges();

                if (userRoles != "null")
                {
                    if (AddRoleUser(model.Id, userRoles) == true)
                    {
                        //return RedirectToAction("Index");
                    }
                }
                else
                {
                    var UserRoles = db.AspNetUsers.Find(model.Id).AspNetRoles;
                    if (UserRoles.Count() > 0)
                        foreach (var role in UserRoles)
                        {
                            UserRoles.Remove(role);
                        }

                    db.SaveChanges();
                }

                return RedirectToAction("DetailsRole", new { id = model.Id });
            }

            ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name", model.RoleId);
            return View(model);
        }
        #endregion

        #region EditRoleAdmin     
        public async Task<ActionResult> EditRoleAdmins(string id, string emailSend)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (db.AspNetUsers.Find(id).AspNetRoles.Count() > 0)
            {
                var RoleId = db.AspNetUsers.Find(id).AspNetRoles.FirstOrDefault().Id;
                ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name", RoleId);

                ViewBag.CurrentUserRoles = db.AspNetUsers.Find(id).AspNetRoles.Select(role => role.Id).ToList();
                ViewBag.UserRoles = db.AspNetRoles.ToList();
            }
            else
            {
                ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name");
            }
            ViewBag.UserRoles = db.AspNetRoles.ToList();

            return View(Models.UsersView.UsersEditViewModel.GetModel(id));
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult EditRoleAdmins(Models.UsersView.UsersEditViewModel model, string userRoles)
        {
            if (ModelState.IsValid)
            {
                db.SaveChanges();

                if (userRoles != "null")
                {
                    if (AddRoleUser(model.Id, userRoles) == true)
                    {
                        //return RedirectToAction("Index");
                    }
                }
                else
                {
                    var UserRoles = db.AspNetUsers.Find(model.Id).AspNetRoles;
                    if (UserRoles.Count() > 0)
                        foreach (var role in UserRoles)
                        {
                            UserRoles.Remove(role);
                        }

                    db.SaveChanges();
                }

                return RedirectToAction("DetailsRoleAdmin", new { id = model.Id });
            }

            ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name", model.RoleId);
            return View(model);
        }
        #endregion

        #region EditRoleOperator    
        public async Task<ActionResult> EditRoleOperator(string id, string emailSend)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (db.AspNetUsers.Find(id).AspNetRoles.Count() > 0)
            {
                var RoleId = db.AspNetUsers.Find(id).AspNetRoles.FirstOrDefault().Id;
                ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name", RoleId);

                ViewBag.CurrentUserRoles = db.AspNetUsers.Find(id).AspNetRoles.Select(role => role.Id).ToList();
                ViewBag.UserRoles = db.AspNetRoles.ToList();
            }
            else
            {
                ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name");
            }
            ViewBag.UserRoles = db.AspNetRoles.ToList();

            return View(Models.UsersView.UsersEditViewModel.GetModel(id));
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult EditRoleOperator(Models.UsersView.UsersEditViewModel model, string userRoles)
        {
            if (ModelState.IsValid)
            {
                db.SaveChanges();

                if (userRoles != "null")
                {
                    if (AddRoleUser(model.Id, userRoles) == true)
                    {
                        //return RedirectToAction("Index");
                    }
                }
                else
                {
                    var UserRoles = db.AspNetUsers.Find(model.Id).AspNetRoles;
                    if (UserRoles.Count() > 0)
                        foreach (var role in UserRoles)
                        {
                            UserRoles.Remove(role);
                        }

                    db.SaveChanges();
                }

                return RedirectToAction("DetailsRoleOperator", new { id = model.Id });
            }

            ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name", model.RoleId);
            return View(model);
        }
        #endregion

        #region ResetPassword        
        public ActionResult ResetPassword(string id)
        {
            ViewBag.GeneratePassword = GeneratePassword(8, 1);


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(Models.UsersView.UserResetPasswordViewModel.GetModel(id));
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(Models.UsersView.UserResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await UserManager.FindByIdAsync(model.Id);
                IdentityResult result = await UserManager.RemovePasswordAsync(user.Id);
                if (result.Succeeded)
                {
                    result = await UserManager.AddPasswordAsync(user.Id, model.Password);
                    if (result.Succeeded)
                    {
                        if (user.Email != null)
                        {

                            IQueryable<Accounts> contactAccount = db.Accounts.Where((Accounts c) => user.Email.Equals(c.Email));
                            // AspNetUsers u = db.AspNetUsers.Find(user.Id);
                            //u.PasswordHash = Utils.HashPassword(model.Password);
                            // u.SecurityStamp = Guid.NewGuid().ToString("D");

                            if (contactAccount.Count() > 0)
                            {
                                string contactName = contactAccount.FirstOrDefault().ContactName;
                                await SendEmail.SendPassword(user.Email, base.HttpContext, contactName, contactAccount.FirstOrDefault().Id, model.Password);

                            }
                            else
                            {
                                await SendEmail.SendPassword(user.Email, base.HttpContext, user.Email, 0, model.Password);

                            }
                        }
                        return RedirectToAction("Details", new { id = user.Id });
                    }
                    AddErrors(result);
                }
            }
            return View(model);
        }
        #endregion

        #region Delete       
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(Models.UsersView.UsersDetailsViewModel.GetModel(id));
        }
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Models.UsersView.UsersDetailsViewModel.DeleteModel(id);
            return RedirectToAction("Index");
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        #region  Отправка пароля
        [HttpPost]
        public async Task<string> SendPasswordsAsync(long[] idsAccounts)
        {
            if (idsAccounts != null)
            {
                IQueryable<AspNetUsers> users = from c in db.Accounts_Users
                                                where idsAccounts.Contains(c.accountId)
                                                select c.AspNetUsers;
                int i = 0;
                using (IEnumerator<AspNetUsers> enumerator = users.GetEnumerator())
                {
                    AspNetUsers item;
                    while (enumerator.MoveNext())
                    {
                        item = enumerator.Current;
                        i++;
                        using (Entities dbe = new Entities())
                        {
                            long accId = item.Accounts_Users.Select((Accounts_Users c) => c.Accounts).FirstOrDefault().Id;
                            IQueryable<Accounts> contactAccount = db.Accounts.Where((Accounts c) => item.Email.Equals(c.Email));
                            string password = Utils.GeneratePassword(8, 3);
                            AspNetUsers u = dbe.AspNetUsers.Find(item.Id);
                            u.PasswordHash = Utils.HashPassword(password);
                            u.SecurityStamp = Guid.NewGuid().ToString("D");
                            if (contactAccount.Count() > 0)
                            {
                                string contactName = contactAccount.FirstOrDefault().ContactName;
                                await SendEmail.SendPassword(item.Email, base.HttpContext, contactName, accId, password, item.EmailConfirmed);

                            }
                            else
                            {
                                await SendEmail.SendPassword(item.Email, base.HttpContext, item.Email, accId, password, item.EmailConfirmed);

                            }
                        }
                    }
                }
                return i.ToString() + " Паролей разослано!";
            }
            return "Пароли не разосланы";
        }
        #endregion
    }
}
