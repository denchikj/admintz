using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BillingAdmin.HelpClasses;
using BillingAdmin.Models;
using BillingAdmin.Models.AccountsView;
using Dadata.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;

namespace BillingAdmin.Controllers
{

    [CustomAuthorizeAttribute]
    public class AccountsController : Controller
    {
        Entities db = new Entities();

        #region ForAspNetIdentity
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public AccountsController()
        {
        }
        public AccountsController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

        #region Index

        public ActionResult Index()
        {
            IQueryable<Accounts> query = from a in db.Accounts
                                         select a;

            return View(query);
        }

        #endregion

        #region IndexRoleCompany

        public ActionResult IndexRoleCompany()
        {
            var role = db.Accounts_Roles.FirstOrDefault(a => a.Role == a.Accounts_RolesHB.Id).Account;
            ViewBag.Role = db.Accounts_Users.Where(a => a.accountId == role);
            return View(AccountsIndexViewModel.SetModel());
        }

        #endregion

        public ActionResult IndexRole()
        {
            var role = db.Accounts_Roles.FirstOrDefault(a => a.Role == a.Accounts_RolesHB.Id).Account;
            var roles = db.Accounts_Users.Where(a => a.accountId == role).Select(x => x.Accounts).ToList();
            //var roles = db.Accounts_RolesHB.ToList();
            //var result = ObjectContext.GetObjectType(roles.GetType());
            ViewBag.Role = roles;
            return View(AccountsIndexViewModel.SetModel());
        }


        #region LoadClient

        [HttpPost]
        public string LoadClient(int? draw, int? start, int? length)
        {
            var search = Request["search[value]"];
            var totalRecords = 0;
            var recordsFiltered = 0;
            start = start.HasValue ? start / length : 0;
            var catalog = new ClientRepository().GetPaginated(ControllerContext, search, start ?? 0, length ?? 25, out totalRecords, out recordsFiltered);
            string json = JsonConvert.SerializeObject(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = totalRecords, data = catalog });
            return json;

        }

        #endregion

        #region Details

        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View(AccountsDetailsViewModel.GetModel(id));
        }

        #endregion

        #region DetailsRoleCompany

        public ActionResult DetailsRoleCompany(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View(AccountsDetailsViewModel.GetModel(id));
        }

        #endregion

        #region Create        

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        // //[ValidateAntiForgeryToken]
        public ActionResult Create(Accounts model)
        {  
            if (model.Phone != null)
            {
                model.Phone = model.Phone.Replace("(", "").Replace(")", "").Replace("-", "").Trim().Replace(" ", "");
            }

            bool valid = ModelState.IsValid;
            if (ModelState.IsValid)
            {
                try
                {
                    // var id = AccountsCreateViewModel.SaveModel(model);
                    db.Accounts.Add(model);
                    db.SaveChanges();
                    var id = db.Accounts.OrderByDescending(c => c.Id).FirstOrDefault().Id;
                    return RedirectToAction("Details", new { id });

                }
                catch (Exception ex)
                {
                    //(new System.Collections.Generic.Mscorlib_CollectionDebugView<System.Data.Entity.Validation.DbValidationError>((new System.Collections.Generic.Mscorlib_CollectionDebugView<System.Data.Entity.Validation.DbEntityValidationResult>(((System.Data.Entity.Validation.DbEntityValidationException)(ex)).EntityValidationErrors as System.Collections.Generic.List<System.Data.Entity.Validation.DbEntityValidationResult>)).Items[0].ValidationErrors as System.Collections.Generic.List<System.Data.Entity.Validation.DbValidationError>)).Items[0].ErrorMessage
                    try
                    {
                        var innerException = ex as DbEntityValidationException;

                        StringBuilder sb = new StringBuilder();

                        sb.AppendLine();
                        sb.AppendLine();
                        foreach (var eve in innerException.EntityValidationErrors)
                        {
                            sb.AppendLine(string.Format("- Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().FullName, eve.Entry.State));
                            foreach (var ve in eve.ValidationErrors)
                            {
                                sb.AppendLine(string.Format("-- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                                    ve.PropertyName,
                                    eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                                    ve.ErrorMessage));
                            }
                        }
                        sb.AppendLine();
                        ModelState.Clear();
                        ModelState.AddModelError("", sb.ToString());
                    }
                    catch (Exception)
                    {
                        
                    }
                }
            }
            else
            {
                string messages = string.Join("; ", ModelState.Values
                                      .SelectMany(x => x.Errors)
                                      .Select(x => x.ErrorMessage));
                ModelState.Clear();
                ModelState.AddModelError("", messages);
            }
            return View(model);
        }

        #endregion

        #region CreateForAccount        
        //[HttpPost]
        public ActionResult CreateForAccount(int id)
        {
            ViewBag.GeneratePassword = GeneratePassword(8, 3);
            ViewBag.RoleId = new SelectList(db.AspNetRoles, "Id", "Name");
            return View(Models.UsersView.UserCreateForAccountViewModel.GetModel(id));
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateForAccount(Models.UsersView.UserCreateForAccountViewModel model, string userRoles, string emailSend, string firstName = null, string midName = null, string lastName = null)
        {
            string status = "sucess";

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

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        #region Edit        

        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            // return View(AccountsEditViewModel.GetModel((int) id));
            if (db.Accounts.Find(id).Accounts_Roles.Count() > 0)
            {
                var RoleId = db.Accounts.Find(id).Accounts_Roles.FirstOrDefault().Id;
                ViewBag.RoleId = new SelectList(db.Accounts_RolesHB, "Id", "Name", RoleId);

                ViewBag.CurrentUserRoles = db.Accounts.Find(id).Accounts_Roles.Select(role => role.Id).ToList();
                ViewBag.UserRoles = db.Accounts_RolesHB.ToList();
            }
            else
            {
                ViewBag.RoleId = new SelectList(db.Accounts_RolesHB, "Id", "Name");
            }
            ViewBag.UserRoles = db.Accounts_RolesHB.ToList();

            return View(db.Accounts.Find(id));
        }

        [HttpPost]
        // //[ValidateAntiForgeryToken]
        public ActionResult Edit(Accounts_Roles model, Accounts accounts, string userRoles)
        {
            if (ModelState.IsValid)
            {
                var RoleId = db.Accounts.Find(model.Id).Accounts_Roles.FirstOrDefault();
                db.Accounts.Find(model.Id).Accounts_Roles.Remove(RoleId);
                db.Accounts.Find(model.Id).Accounts_Roles.Add(db.Accounts_Roles.Where(c => c.Id == model.Role).FirstOrDefault());
                db.SaveChanges();
                if (userRoles == "null")
                {
                    var UserRoles = db.Accounts.Find(model.Id).Accounts_Roles;
                    if (UserRoles.Count() > 0)
                        foreach (var role in UserRoles)
                        {
                            UserRoles.Remove(role);
                        }

                    db.SaveChanges();
                }
                db.Entry(accounts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = model.Id });
            }

            if (accounts.Phone != null)
            {
                accounts.Phone = accounts.Phone.Replace("(", "").Replace(")", "").Replace("-", "").Trim().Replace(" ", "");
            }
            //if (ModelState.IsValid)
            if (!string.IsNullOrEmpty(accounts.DirectorFullName) && !string.IsNullOrEmpty(accounts.FullLegalName) && !string.IsNullOrEmpty(accounts.ShortLegalName) && !string.IsNullOrEmpty(accounts.INN) && !string.IsNullOrEmpty(accounts.Email))
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        db.Entry(model).State = EntityState.Modified;
                        db.SaveChanges();
                        // AccountsEditViewModel.SaveChangesModel(model);
                        return RedirectToAction("Details", new { id = model.Id });
                    }
                    catch (System.Exception ex)
                    {
                        var innerException = ex as DbEntityValidationException;

                        StringBuilder sb = new StringBuilder();

                        sb.AppendLine();
                        sb.AppendLine();
                        foreach (var eve in innerException.EntityValidationErrors)
                        {
                            sb.AppendLine(string.Format("- Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().FullName, eve.Entry.State));
                            foreach (var ve in eve.ValidationErrors)
                            {
                                sb.AppendLine(string.Format("-- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                                    ve.PropertyName,
                                    eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                                    ve.ErrorMessage));
                            }
                        }
                        sb.AppendLine();
                        ModelState.Clear();
                        ModelState.AddModelError("", sb.ToString());

                        return View(model);
                    }
                }
                else
                {
                    string messages = string.Join("; ", ModelState.Values
                                          .SelectMany(x => x.Errors)
                                          .Select(x => x.ErrorMessage));
                    ModelState.Clear();
                    ModelState.AddModelError("", messages);
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", "Не зполнено Короткое или длинное Название,Имя Директора, ИНН  или Email");
            }

            ViewBag.RoleId = new SelectList(db.Accounts_RolesHB, "Id", "Name", model.Role);
            return View(model);
        }

        #endregion

        #region EditRoleCompany        

        public ActionResult EditRoleCompany(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            // return View(AccountsEditViewModel.GetModel((int) id));
            return View(db.Accounts.Find(id));
        }

        [HttpPost]
        // //[ValidateAntiForgeryToken]
        public ActionResult EditRoleCompany(Accounts model, Accounts accounts, string action)
        {
            if (base.ModelState.IsValid)
            {
                db.Entry(accounts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", accounts);
            }

            if (model.Phone != null)
            {
                model.Phone = model.Phone.Replace("(", "").Replace(")", "").Replace("-", "").Trim().Replace(" ", "");
            }
            //if (ModelState.IsValid)
            if (!string.IsNullOrEmpty(model.DirectorFullName) && !string.IsNullOrEmpty(model.FullLegalName) && !string.IsNullOrEmpty(model.ShortLegalName) && !string.IsNullOrEmpty(model.INN) && !string.IsNullOrEmpty(model.Email))
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        db.Entry(model).State = EntityState.Modified;
                        db.SaveChanges();
                        // AccountsEditViewModel.SaveChangesModel(model);
                        return RedirectToAction("Details", new { id = model.Id });
                    }
                    catch (System.Exception ex)
                    {
                        var innerException = ex as DbEntityValidationException;

                        StringBuilder sb = new StringBuilder();

                        sb.AppendLine();
                        sb.AppendLine();
                        foreach (var eve in innerException.EntityValidationErrors)
                        {
                            sb.AppendLine(string.Format("- Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().FullName, eve.Entry.State));
                            foreach (var ve in eve.ValidationErrors)
                            {
                                sb.AppendLine(string.Format("-- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                                    ve.PropertyName,
                                    eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                                    ve.ErrorMessage));
                            }
                        }
                        sb.AppendLine();
                        ModelState.Clear();
                        ModelState.AddModelError("", sb.ToString());

                        return View(model);
                    }
                }
                else
                {
                    string messages = string.Join("; ", ModelState.Values
                                          .SelectMany(x => x.Errors)
                                          .Select(x => x.ErrorMessage));
                    ModelState.Clear();
                    ModelState.AddModelError("", messages);
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", "Не зполнено Короткое или длинное Название,Имя Директора, ИНН  или Email");
            }

            return View(model);
        }

        #endregion

        #region Delete

        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View(AccountsDetailsViewModel.GetModel((int)id));
        }


        [HttpPost]
        public string UpdateRole(int idUser, int userRoles)
        {
            var UserRoles = db.Accounts_Users.Find(idUser).Accounts.Accounts_Roles.Where(c => c.Account == idUser && c.Role == userRoles);

            if (UserRoles.Count() > 0)
            {
                foreach (var role in UserRoles)
                {
                    db.Accounts_Roles.RemoveRange(UserRoles);
                }
                //db.Accounts_Roles.RemoveRange(ar);
                db.SaveChanges();
                Accounts_Users user = db.Accounts_Users.Find(idUser);

                var userRolesObj = string.IsNullOrEmpty(userRoles.ToString()) == false ? System.Web.Helpers.Json.Decode(userRoles.ToString()) : new List<string>() { "0" };

                foreach (string userRole in userRolesObj)
                {
                    Guid id = Guid.NewGuid();
                    //string userRoleId = null;
                    if (userRole != "0")
                    {
                        //userRoleId = db.AspNetRoles.FirstOrDefault().Id;
                        Accounts_Roles roles = db.Accounts_Roles.FirstOrDefault(role => role.Accounts_RolesHB.Name == userRole);
                        //if (rules.Select(roles => roles.id).ToList().Contains(id.ToString()))
                        //id = new Guid("d");
                        db.Accounts_Users.Where(c => c.id == user.id).FirstOrDefault().Accounts.Accounts_Roles.Add(roles);
                    }
                    db.SaveChanges();
                    if (userRole == "0") break;
                }         
            }
            else
            {
                db.Accounts_Roles.Add(new Accounts_Roles()
                {
                    Account = idUser,
                    Role = userRoles
                });
            }
            return "ok";
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ActionName("Delete")]
        // //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccountsDetailsViewModel.DeleteModel(id);
            return RedirectToAction("Index");
        }

        #endregion

        #region загрузка по ИНН

        public JsonResult GetUserList(string term)
        {
            var CategoryList = db.AspNetUsers.Where(x => x.UserName.ToLower().Contains(term.ToLower())).Select(c => new { Id = c.Id, UserName = c.UserName }).ToList();
            return Json(CategoryList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UploadINN(string innCompany/*int accountId*/)
        {
            Accounts accounts = new Accounts();
            accounts.INN = innCompany;
            try
            {
                // JsonConvert.DeserializeObject(accountsJSON) as Accounts;//db.Accounts.Find(accountId);
                Party infoByINN = Company.FindCompany(accounts.INN);
                accounts.ShortLegalName = infoByINN.name.short_with_opf;
                accounts.FullLegalName = infoByINN.name.full_with_opf;
                accounts.KPP = infoByINN.kpp;
                accounts.Ogrn = infoByINN.ogrn;
                //accounts.Industry 
                if (infoByINN.opf.@short == "ИП")
                    accounts.DirectorFullName = infoByINN.name.full;
                else
                    accounts.DirectorFullName = infoByINN.management.name;

                accounts.Legal_Street = infoByINN.address.unrestricted_value;
            }
            catch (Exception Errors)
            {
                string Error = Errors.Message;
                ModelState.AddModelError("","Сервис проверки по ИНН возвратил ошибку");
            }
            return PartialView("~/Views/Accounts/CustomData.cshtml", accounts);
        }

        #endregion
        //    #region  Отправка пароля
        //    [HttpPost]
        //    public async Task<string> SendPasswordsAsync(long[] idsAccounts)
        //    {
        //        if (idsAccounts != null)
        //        {
        //            IQueryable<AspNetUsers> users = from c in db.Accounts_Users
        //                                            where idsAccounts.Contains(c.accountId)
        //                                            select c.AspNetUsers;
        //            int i = 0;
        //            using (IEnumerator<AspNetUsers> enumerator = users.GetEnumerator())
        //            {
        //                AspNetUsers item;
        //                while (enumerator.MoveNext())
        //                {
        //                    item = enumerator.Current;
        //                    i++;
        //                    using (Entities dbe = new Entities())
        //                    {
        //                        long accId = item.Accounts_Users.Select((Accounts_Users c) => c.Accounts).FirstOrDefault().Id;
        //                        IQueryable<Accounts> contactAccount = db.Accounts.Where((Accounts c) => item.Email.Equals(c.Email));
        //                        string password = Utils.GeneratePassword(8, 3);
        //                        AspNetUsers u = dbe.AspNetUsers.Find(item.Id);
        //                        u.PasswordHash = Utils.HashPassword(password);
        //                        u.SecurityStamp = Guid.NewGuid().ToString("D");
        //                        if (contactAccount.Count() > 0)
        //                        {
        //                            string contactName = contactAccount.FirstOrDefault().ContactName;
        //                            await SendEmail.SendPassword(item.Email, base.HttpContext, contactName, accId, password, item.EmailConfirmed);

        //                        }
        //                        else
        //                        {
        //                            await SendEmail.SendPassword(item.Email, base.HttpContext, item.Email, accId, password, item.EmailConfirmed);

        //                        }
        //                    }
        //                }
        //            }
        //            return i.ToString() + " Паролей разослано!";
        //        }
        //        return "Пароли не разосланы";
        //    }
        //    #endregion
    }
}