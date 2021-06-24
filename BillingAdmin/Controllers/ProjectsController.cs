using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BillingAdmin.Models.ProjectsView;
using System.Net;
using BillingAdmin.Models;

namespace BillingAdmin.Controllers
{
    [CustomAuthorizeAttribute]
    public class ProjectsController : Controller
    {
        #region Index
        public ActionResult Index()
        {
            return View(ProjectsIndexViewModel.GetModel());
        }
        #endregion

        #region Details
        public ActionResult Details(int id)
        {
            var model = ProjectsDetailsViewModel.GetModel(id);
            return View(model);
        }
        #endregion

        #region Create
        public ActionResult Create(int id)
        {
            return View(ProjectsCreateViewModel.GetModel(id));
        }
        [HttpPost]
       // //[ValidateAntiForgeryToken]
        public ActionResult Create(ProjectsCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                int id = ProjectsCreateViewModel.SaveModel(model);
                return Json(new
                {
                    url = Url.Action("Details", new { id })
                });
            }
            var errors = ModelState.Values
                    .Cast<ModelState>()
                    .SelectMany(v => v.Errors)
                    .Select(v => v.ErrorMessage)
                    .ToArray();

            return Json(new
            {
                text = string.Join("<br/>", errors)
            });
        }
        #endregion

        #region Edit
        public ActionResult Edit(int id)
        {
            return View(ProjectsEditViewModel.GetModel(id));
        }
        [HttpPost]
       // //[ValidateAntiForgeryToken]
        public ActionResult Edit(ProjectsEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                ProjectsEditViewModel.SaveChangesModel(model);
                return Json(new
                {
                    url = Url.Action("Details", new { model.Id })
                });
            }
            var errors = ModelState.Values
                    .Cast<ModelState>()
                    .SelectMany(v => v.Errors)
                    .Select(v => v.ErrorMessage)
                    .ToArray();

            return Json(new
            {
                text = string.Join("<br/>", errors)
            });
        }
        #endregion

        #region ViewServiceOfProject
        public ActionResult ServiceOfProject(int id)
        {
            return PartialView("_Services", ServicesOfProjectViewModel.GetModel(id));
        }
        #endregion

        #region Service
        public ActionResult Service(int id)
        {
            return PartialView("_AddServices", ServiceViewModel.GetModel(id));
        }
        #endregion

        #region StatisticOfProject

        public ActionResult StatisticOfProject(int id)
        {
            return PartialView("_Statistic", StatisticForProjectViewModel.GetModel(id));
        }

        [HttpPost]
        public ActionResult EditStatistic(StatisticEditModel model)
        {
            if (StatisticEditModel.SaveStatistic(model))
            {
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
        }


        #endregion


        #region Preference
        [HttpPost]
        public ActionResult Preference(int id)
        {
            var model = PreferenceOfServiceViewModel.GetModel(id);
            return PartialView("_Preference", model);
        }
        #endregion

        #region AddService
        [HttpPost]
        public ActionResult AddService(AddServiceModel model)
        {
            if (AddServiceModel.SaveSevice(model))
            {
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
        }
        #endregion
        
    }
}