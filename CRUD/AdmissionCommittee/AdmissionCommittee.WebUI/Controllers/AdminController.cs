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

        public ViewResult Index(int page = 1)
        {
            //repository.DatabaseTest();
            EnrolleesListViewModel model = new EnrolleesListViewModel
            {
                Enrollees = repository.Enrollees
                    .OrderBy(p => p.EnrolleeId)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Enrollees.Count()
                },
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

        public ViewResult Application(int enrolleeId, int applicationId)
        {
            var model = new ApplicationEditViewModel()
            {
                Application = repository.Enrollees
                    .FirstOrDefault(en => en.EnrolleeId == enrolleeId).Applications
                    .FirstOrDefault(app => app.ApplicationId == applicationId),
                EducationPlaces = repository.EducationPlaces,
                FinancingTypes = repository.FinancingTypes
            };
            return View(model);
        }

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

        //[HttpPost]
        //public JsonResult LoadSpecialities(AjaxSpecialityDataModel parameters)
        //{      
        //    if(parameters.MainSpecialityId > 0) {
        //        var mainGroup = repository.Specialities
        //            .FirstOrDefault(sp => sp.SpecialityId == parameters.MainSpecialityId)
        //            .SpecialityGroup;
        //        var availableGroupIds = mainGroup.FriendlyGroups
        //            .Select(gr => gr.SpecialityGroupId)
        //            .Concat(new[] { mainGroup.SpecialityGroupId});
        //        var speciality = repository.Specialities
        //            .Where(sp => 
        //                sp.EducationPlaceId == parameters.EducationPlaceId &&
        //                sp.FinancingTypeId == parameters.FinancingTypeId && 
        //                availableGroupIds.Contains(sp.SpecialityGroupId));
        //    }
        //    else
        //    {
        //        var options = repository.Specialities.Where(sp => sp.EducationPlaceId == parameters.EducationPlaceId
        //            && sp.FinancingTypeId == parameters.FinancingTypeId);
        //    }
        //}

        //public IEnumerable<Speciality> SelectByGroup(IEnumerable<Speciality> specialities, int? mainSpecialityId)
        //{
        //    if (mainSpecialityId != null)
        //    {
        //        var mainGroup = repository.Specialities
        //            .FirstOrDefault(sp => sp.SpecialityId == mainSpecialityId)
        //            .SpecialityGroup;
        //        var availableGroupIds = mainGroup.FriendlyGroups
        //            .Select(gr => gr.SpecialityGroupId)
        //            .Concat(new[] { mainGroup.SpecialityGroupId });
        //        return specialities.Where(sp => availableGroupIds.Contains(sp.SpecialityGroupId));
        //    }
        //    return specialities;
        //}

        //public IEnumerable<Speciality> SelectSpecialities(AjaxSpecialityDataModel parameters)
        //{
        //    IEnumerable<Speciality> specialities = new List<Speciality>();

        //    if(parameters.EducationPlaceId == null || parameters.FinancingTypeId == null)
        //    {
        //        return SelectByGroup(specialities, parameters.MainSpecialityId);
        //    }
        //    specialities = repository.Specialities.Where(sp => 
        //        sp.EducationPlaceId == parameters.EducationPlaceId &&
        //        sp.FinancingTypeId == parameters.FinancingTypeId);

        //    if(parameters.SpecialityId == null)
        //    {
        //        return SelectByGroup(specialities, parameters.MainSpecialityId);
        //    }
        //    specialities = specialities.Where(sp =>sp.SpecialityId == parameters.SpecialityId);

        //    if(parameters.EducationFormId == null)
        //    {
        //        return SelectByGroup(specialities, parameters.MainSpecialityId);
        //    }
        //    specialities = specialities.Where(sp => sp.EducationFormId == parameters.EducationFormId);

        //    if(parameters.EducationDurationId != null)
        //    {
        //        return SelectByGroup(specialities, parameters.MainSpecialityId);
        //    }
        //    specialities = specialities.Where(sp => sp.EducationDurationId == parameters.EducationDurationId);
        //    return SelectByGroup(specialities, parameters.MainSpecialityId);
        //}

        public IEnumerable<Speciality> SelectSpecialities(AjaxSpecialityDataModel parameters)
        {
            var specialities = repository.Specialities;
            if(parameters.GroupId != null)
            {
                specialities = specialities.Where(sp => sp.SpecialityGroupId == parameters.GroupId);
            }
            if (parameters.EducationPlaceId != null)
            {
                specialities = specialities.Where(sp => sp.EducationPlaceId == parameters.EducationPlaceId);
            }           
            if(parameters.FinancingTypeId != null)
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
            return specialities;
        }

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
            //var specialitySet = new HashSet<NCSQSpeciality>(SelectSpecialities(parameters).Select(sp => sp.NCSQSpeciality));
            //var options = specialitySet.Select(sp => new
            //{
            //    Text = sp.Name,
            //    Value = sp.NCSQSpecialityId,
            //    Label = sp.Cipher + " " + sp.Name
            //});
            return Json(options, JsonRequestBehavior.AllowGet);
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
            return Json(options, JsonRequestBehavior.AllowGet);
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
            return Json(options, JsonRequestBehavior.AllowGet);
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
            return Json(options, JsonRequestBehavior.AllowGet);
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
            return Json(options, JsonRequestBehavior.AllowGet);
        }

        

       

        //[HttpPost]
        //public PartialViewResult LoadSpeciality(int specialityId)
        //{
        //    var speciality = repository.Specialities.FirstOrDefault(sp => sp.SpecialityId == specialityId);
        //    return PartialView("PArtialSelectedSpeciality", speciality);
        //}

        [HttpPost]
        public PartialViewResult LoadEmptySpeciality()
        {
            return PartialView("PartialSelectedSpeciality", new ApplicationToSpeciality() {
                Speciality = new Speciality(),
                Priority = 0
            });
        }
        //[HttpPost]
        //public JsonResult LoadNode(int? id)
        //{
        //    var children = repository.TreeNodes.
        //            Where(node => node.ParentId == id)
        //            .Select(child => new
        //            {
        //                Id = child.NodeId,
        //                Name = child.Data.Name,
        //                FullName = child.Data.FullName,
        //                NumChildren = repository.TreeNodes.Where(node => node.ParentId == child.NodeId).Count()
        //            });
        //    return Json(children, JsonRequestBehavior.AllowGet);         
        //}

        //[HttpPost]
        //public JsonResult LoadDropdown(int? parentId)
        //{
        //    var children = repository.TreeNodes.
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