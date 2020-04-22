using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CrudPoc.Web.Models;
using CrudPoc.Helper;
using CrudPoc.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CrudPoc.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApiClient _client;

        public HomeController()
        {
            _client = new ApiClient("http://localhost:64200/api/");
        }

        public IActionResult Index()
        {
            CrudViewModel viewModel = new CrudViewModel
            {
                Saved = Convert.ToBoolean(TempData["Saved"]),
                LstAnnouncements = _client.Get<List<AnnouncementWebMotors>>("Crud").Result.ToList()
            };

            return View(viewModel);
        }

        public IActionResult Create()
        {
            CrudViewModel viewModel = new CrudViewModel();
            FillWMDropDowns(viewModel);

            return View("CreateEdit", viewModel);
        }

        public IActionResult Edit(int iD)
        {
            CrudViewModel viewModel = new CrudViewModel
            {
                Announcement = _client.Get<AnnouncementWebMotors>(string.Format("Crud/{0}", iD)).Result
            };

            FillWMDropDowns(viewModel);

            return View("CreateEdit", viewModel);
        }

        [HttpPost]
        public ActionResult CreateEdit(CrudViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                bool saved;

                if (viewModel.Announcement.ID == 0)
                    saved = _client.Post("Crud", viewModel.Announcement).Result;
                else
                    saved = _client.Put(string.Format("Crud/{0}", viewModel.Announcement.ID), viewModel.Announcement).Result;

                TempData["Saved"] = saved;
                TempData.Keep("Saved");

                return RedirectToAction("Index");
            }

            FillWMDropDowns(viewModel);

            return View(viewModel);
        }

        public IActionResult Delete(int iD)
        {
            TempData["Saved"] = _client.Delete(string.Format("Crud/{0}", iD)).Result;
            TempData.Keep("Saved");

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public JsonResult GetModels(int makeID)
        {
            return Json(_client.Get<List<dynamic>>($"WebMotors/Model?makeID={makeID}").Result);
        }

        public JsonResult GetVersions(int modelID)
        {
            return Json(_client.Get<List<dynamic>>($"WebMotors/Version?modelID={modelID}").Result);
        }

        private void FillWMDropDowns(CrudViewModel viewModel)
        {
            _client.Get<List<dynamic>>("WebMotors/Make").Result.ForEach(x =>
            {
                viewModel.Makes.Add(new SelectListItem { Text = x.name, Value = x.id });
            });

            if (!string.IsNullOrEmpty(viewModel.Announcement.Make))
            {
                _client.Get<List<dynamic>>($"WebMotors/Model?makeID={viewModel.Announcement.Make}").Result.ForEach(x =>
                {
                    viewModel.Models.Add(new SelectListItem { Text = x.name, Value = x.id });
                });

                if (!string.IsNullOrEmpty(viewModel.Announcement.Model))
                    _client.Get<List<dynamic>>($"WebMotors/Version?modelID={viewModel.Announcement.Model}").Result.ForEach(x =>
                    {
                        viewModel.Versions.Add(new SelectListItem { Text = x.name, Value = x.id });
                    });
            }

        }
    }
}