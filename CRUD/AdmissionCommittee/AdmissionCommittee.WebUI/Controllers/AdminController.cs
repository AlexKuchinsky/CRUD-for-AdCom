using System.Linq;
using System.Web.Mvc;
using AdmissionCommittee.Domain.Abstract;
using AdmissionCommittee.Domain.Entities;
using AdmissionCommittee.WebUI.Models;
using System.Collections;
using System;
using System.Collections.Generic;

namespace SportsStore.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IEnrolleeRepository repository;
        public int PageSize = 100;

        public AdminController(IEnrolleeRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index(EducationLevel? edLevel = null, int page = 1)
        {
            EnrolleesListViewModel model = new EnrolleesListViewModel
            {
                Enrollees = repository.Enrollees
                    .Where(p => edLevel == null || p.EducationLevel == edLevel)
                    .OrderBy(p => p.EnrolleeId)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = edLevel == null ?
                        repository.Enrollees.Count() :
                        repository.Enrollees.Where(e => e.EducationLevel == edLevel).Count()
                },
                CurrentCategoryOfEducation = edLevel
            };
            return View(model);
        }

        public ViewResult Edit(int enrolleeId)
        {
            Enrollee enrollee = repository.Enrollees.FirstOrDefault(p => p.EnrolleeId == enrolleeId);
            var model = new EnrolleeEditViewModel
            {
                Enrollee = enrollee,
                Subjects = repository.Subjects
            };
            return View(model);
        }

        public ViewResult Show(int enrolleeId)
        {
            Enrollee enrollee = repository.Enrollees
                    .FirstOrDefault(p => p.EnrolleeId == enrolleeId);
            return View(enrollee);
        }

        public ViewResult Create()
        {
            return View("Edit", new EnrolleeEditViewModel()
            {
                Enrollee = new Enrollee(),
                Subjects = repository.Subjects
            });
        }

        public ViewResult Specialities(int enrolleeId)
        {
            IEnumerable<Speciality> specialities = repository.Enrollees
                    .FirstOrDefault(p => p.EnrolleeId == enrolleeId).Specialities;
            return View(specialities);
        }

        //[HttpPost]
        //public PartialViewResult LoadEnrolleeTable(IEnumerable<AjaxTreeDataModel> model)
        //{
        //    if (model == null)
        //    {
        //        return PartialView("PartialEnrolleeTable", repository.Enrollees);
        //    }
        //    else
        //    {
        //        var level1 = model.Where(m => m.Level == 1).Select(m => m.Id);
        //        var spOn1Level = repository.SpecialtyInfo.Where(si => level1.Contains(si.UniversityId));
        //        var level2 = model.Where(m => m.Level == 2).Select(m => m.Id);
        //        var spOn2Level = repository.SpecialtyInfo.Where(si => level2.Contains(si.FacultyId));
        //        var level3 = model.Where(m => m.Level == 3).Select(m => m.Id);
        //        var spOn3Level = repository.SpecialtyInfo.Where(si => level3.Contains(si.SpecialtyId));
        //        var level4 = model.Where(m => m.Level == 4).Select(m => m.Id);
        //        var spOn4Level = repository.SpecialtyInfo.Where(si => level4.Contains(si.SpecializationId));
        //        var level5 = model.Where(m => m.Level == 5).Select(m => m.Id);
        //        var spOn5Level = repository.SpecialtyInfo.Where(si => level5.Contains(si.FormOfStudyId));
        //        var level6 = model.Where(m => m.Level == 6).Select(m => m.Id);
        //        var spOn6Level = repository.SpecialtyInfo.Where(si => level6.Contains(si.PaymentId));

        //        var selectedSpecialties = spOn1Level.Concat(spOn2Level).Concat(spOn3Level).Concat(spOn4Level)
        //            .Concat(spOn5Level).Concat(spOn6Level).Select(a => a.SpecialtyInfoId);
        //        var result = repository.Enrollees.Where(en => selectedSpecialties.Contains(en.SpecialtyInfoId));
        //        return PartialView("PartialEnrolleeTable", result);
        //    }
        //}

        //[HttpPost]
        //public ActionResult Edit(EnrolleeEditViewModel model)
        //{
        //    if(model.Enrollee.SpecialtyInfo.UniversityId == 0 ||
        //       model.Enrollee.SpecialtyInfo.FacultyId == 0 ||
        //       model.Enrollee.SpecialtyInfo.SpecialtyId == 0 ||
        //       model.Enrollee.SpecialtyInfo.SpecializationId == 0 ||
        //       model.Enrollee.SpecialtyInfo.FormOfStudyId == 0 ||
        //       model.Enrollee.SpecialtyInfo.PaymentId == 0)
        //    {
        //        ModelState.AddModelError("Enrollee.SpecialtyInfo", "Select specialty");
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        model.Enrollee.SpecialtyInfoId = repository.GetSpecialtyInfoId(model.Enrollee.SpecialtyInfo);
        //        repository.SaveEnrollee(model.Enrollee);
        //        TempData["message"] = string.Format("{0} has been saved", model.Enrollee.LastName + " " + model.Enrollee.FirstName);
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        model.Subjects = repository.Subjects;
        //        return View(model);
        //    }
        //}    

        [HttpPost]
        public ActionResult Delete(int enrolleeId)
        {
            Enrollee deletedEnrollee = repository.DeleteEnrollee(enrolleeId);
            if (deletedEnrollee != null)
            {
                TempData["message"] = string.Format("{0} was deleted",
                deletedEnrollee.LastName + " " + deletedEnrollee.FirstName);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult LoadNode(int? id)
        {
            var children = repository.TreeNodes.
                    Where(node => node.ParentId == id)
                    .Select(child => new
                    {
                        Id = child.NodeId,
                        Name = child.Data.Name,
                        FullName = child.Data.FullName,
                        NumChildren = repository.TreeNodes.Where(node => node.ParentId == child.NodeId).Count()
                    });
            return Json(children, JsonRequestBehavior.AllowGet);         
        }

        [HttpPost]
        public JsonResult LoadDropdown(int? parentId)
        {
            var children = repository.TreeNodes.
                Where(node => node.ParentId == parentId).
                Select(node => new
                {
                    Name = node.Data.Name,
                    NodeId = node.NodeId,
                    FullName = node.Data.FullName
                });
            return Json(children, JsonRequestBehavior.AllowGet);
        }
    }
}