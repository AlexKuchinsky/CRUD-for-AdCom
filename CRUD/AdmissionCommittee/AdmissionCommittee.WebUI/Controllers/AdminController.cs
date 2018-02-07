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
        private IEnrolleeRepository _repository;
        public int PageSize = 100;

        public AdminController(IEnrolleeRepository repo)
        {
            _repository = repo;
        }

        public ViewResult Index(int page = 1)
        {
            //_repository.DatabaseTest();
            EnrolleesListViewModel model = new EnrolleesListViewModel
            {
                Enrollees = _repository.Enrollees
                    .OrderBy(p => p.EnrolleeId)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _repository.Enrollees.Count()
                },
            };
            return View(model);
        }

        public ViewResult Edit(int enrolleeId)
        {
            Enrollee enrollee = _repository.Enrollees.FirstOrDefault(p => p.EnrolleeId == enrolleeId);
            var model = new EnrolleeEditViewModel
            {
                Enrollee = enrollee,
                Subjects = _repository.Subjects
            };
            return View(model);
        }

        public ViewResult Show(int enrolleeId)
        {
            Enrollee enrollee = _repository.Enrollees
                    .FirstOrDefault(p => p.EnrolleeId == enrolleeId);
            return View(enrollee);
        }

        public ViewResult Create()
        {
            return View("Edit", new EnrolleeEditViewModel()
            {
                Enrollee = new Enrollee()
                {
                    Applications = new List<Application>()
                    {
                        new Application()
                    }
                },
                Subjects = _repository.Subjects
            });
        }

        public ViewResult Application(int enrolleeId, int applicationId)
        {
            Application application = _repository.Applications
                .FirstOrDefault(app => app.ApplicationId == applicationId) ?? new Application()
                {
                    EnrolleeId = enrolleeId,
                    Specialities = new List<ApplicationToSpeciality>()
                };

            return View(application);
        }

        [HttpPost]
        public ActionResult Edit(EnrolleeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveEnrollee(model.Enrollee);
                return RedirectToAction("Index");
            }
            else
            {
                model.Subjects = _repository.Subjects;
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Delete(int enrolleeId)
        {
            Enrollee deletedEnrollee = _repository.DeleteEnrollee(enrolleeId);
            if (deletedEnrollee != null)
            {
                TempData["message"] = string.Format("{0} was deleted",
                deletedEnrollee.LastName + " " + deletedEnrollee.FirstName);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditApplication(Application application)
        {

        }

        [HttpPost]
        public JsonResult LoadEducationPlaceOptions(AjaxSpecialityDataModel parameters)
        {
            var options = SelectSpecialities(parameters)
                .GroupBy(sp => sp.EducationPlace)
                .Select(group => new
                {
                    Text = group.Key.Name,
                    Value = group.Key.EducationPlaceId,
                    Label = group.Key.Name
                });
            return Json(options);
        }

        [HttpPost]
        public JsonResult LoadFinancingTypeOptions(AjaxSpecialityDataModel parameters)
        {
            var finTypeSet = new HashSet<FinancingType>(SelectSpecialities(parameters).Select(sp => sp.FinancingType));
            var options = finTypeSet.Select(ft => new
            {
                Text = ft.Type,
                Value = ft.FinancingTypeId,
                Label = ft.Type
            });
            return Json(options);
        }

        [HttpPost]
        public JsonResult LoadEducationFormOptions(AjaxSpecialityDataModel parameters)
        {
            var educationFormSet = new HashSet<EducationForm>(SelectSpecialities(parameters).Select(sp => sp.EducationForm));
            var options = educationFormSet.Select(sp => new
            {
                Text = sp.Name,
                Value = sp.EducationFormId,
                Label = sp.IsInternal ? "Full-time" : "Correspondence"
            });
            return Json(options);
        }

        [HttpPost]
        public JsonResult LoadEducationDurationOptions(AjaxSpecialityDataModel parameters)
        {
            var educationDurationSet = new HashSet<EducationDuration>(SelectSpecialities(parameters).Select(sp => sp.EducationDuration));
            var options = educationDurationSet.Select(sp => new
            {
                Text = sp.Duration,
                Value = sp.EducationDurationId,
                Label = sp.Duration
            });
            return Json(options);
        }

        [HttpPost]
        public JsonResult LoadNCSQSpecialityOptions(AjaxSpecialityDataModel parameters)
        {
            var specialitySet = new HashSet<NCSQSpeciality>(SelectSpecialities(parameters).Select(sp => sp.NCSQSpeciality));
            var options = specialitySet.Select(sp => new
            {
                Text = sp.Name,
                Value = sp.NCSQSpecialityId,
                Label = sp.Cipher + " " + sp.Name
            });
            return Json(options);
        }

        [HttpPost]
        public JsonResult UpdateSpecialityDataId(AjaxSpecialityDataModel parameters)
        {
            var speciality = SelectSpecialities(parameters).FirstOrDefault();
            var specialityId = SelectSpecialities(parameters).FirstOrDefault()?.SpecialityId ?? 0;
            return Json(new { Id = specialityId });
        }

        [HttpPost]
        public JsonResult UpdateGroupData(int specialityId)
        {
            SpecialityGroup group = _repository.Specialities.FirstOrDefault(sp => sp.SpecialityId == specialityId).SpecialityGroup;
            return Json(new { Id = group.SpecialityGroupId, Count = group.MaxNumOfSpec });
        }

        [HttpPost]
        public PartialViewResult LoadEmptySpeciality()
        {
            return PartialView("PartialSelectedSpeciality", new ApplicationToSpeciality() {
                Speciality = new Speciality(),
                Priority = 0
            });
        }

        public IQueryable<Speciality> SelectSpecialities(AjaxSpecialityDataModel parameters)
        {
            var specialities = _repository.Specialities;
            if (parameters.GroupId != null)
            {
                specialities = specialities.Where(sp => sp.SpecialityGroupId == parameters.GroupId);
            }
            if (parameters.EducationPlaceId != null)
            {
                specialities = specialities.Where(sp => sp.EducationPlaceId == parameters.EducationPlaceId);
            }
            if (parameters.FinancingTypeId != null)
            {
                specialities = specialities.Where(sp => sp.FinancingTypeId == parameters.FinancingTypeId);
            }
            if (parameters.EducationFormId != null)
            {
                specialities = specialities.Where(sp => sp.EducationFormId == parameters.EducationFormId);
            }
            if (parameters.EducationDurationId != null)
            {
                specialities = specialities.Where(sp => sp.EducationDurationId == parameters.EducationDurationId);
            }
            if (parameters.SpecialityId != null)
            {
                specialities = specialities.Where(sp => sp.NCSQSpecialityId == parameters.SpecialityId);
            }
            if (parameters.SelectedSpecialities != null)
            {
                specialities = specialities.Where(sp => !parameters.SelectedSpecialities.Contains(sp.SpecialityId));
            }
            return specialities;
        }

        //[HttpPost]
        //public JsonResult LoadNode(int? id)
        //{
        //    var children = _repository.TreeNodes.
        //            Where(node => node.ParentId == id)
        //            .Select(child => new
        //            {
        //                Id = child.NodeId,
        //                Name = child.Data.Name,
        //                FullName = child.Data.FullName,
        //                NumChildren = _repository.TreeNodes.Where(node => node.ParentId == child.NodeId).Count()
        //            });
        //    return Json(children, JsonRequestBehavior.AllowGet);         
        //}

        //[HttpPost]
        //public JsonResult LoadDropdown(int? parentId)
        //{
        //    var children = _repository.TreeNodes.
        //        Where(node => node.ParentId == parentId).
        //        Select(node => new
        //        {
        //            Name = node.Data.Name,
        //            NodeId = node.NodeId,
        //            FullName = node.Data.FullName
        //        });
        //    return Json(children, JsonRequestBehavior.AllowGet);
        //}
    }
}