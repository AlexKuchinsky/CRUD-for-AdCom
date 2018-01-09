using System.Linq;
using System.Web.Mvc;
using AdmissionCommittee.Domain.Abstract;
using AdmissionCommittee.Domain.Entities;
using AdmissionCommittee.WebUI.Models;
using System.Collections;
using System;

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
                    .OrderBy(p => p.EnrolleeID)
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
            var model = new EnrolleeEditViewModel
            {
                Enrollee = repository.Enrollees.FirstOrDefault(p => p.EnrolleeID == enrolleeId),
                Subjects = repository.Subjects
            };
            return View(model);
        }

        public ViewResult Show(int enrolleeId)
        {
            Enrollee enrollee = repository.Enrollees
                    .FirstOrDefault(p => p.EnrolleeID == enrolleeId);
            return View(enrollee);
        }

        public ViewResult Create()
        {
            return View("Edit", new EnrolleeEditViewModel()
            {
                Subjects = repository.Subjects
            });
        }

        [HttpPost]
        public ActionResult Edit(Enrollee enrollee)
        {
            if (ModelState.IsValid)
            {
                repository.SaveEnrollee(enrollee);
                TempData["message"] = string.Format("{0} has been saved", enrollee.LastName + " " + enrollee.FirstName);
                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values
                return View(enrollee);
            }
        }    

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
        public JsonResult LoadNode(int id)
        {
            var parentsId = repository.TreeNodes.Select(parent => parent.ParentID);
            var children = repository.TreeNodes
                .Where(child => child.ParentID == id)
                .Select(child => new
                {
                    Id = child.ID,
                    Title = child.Title,
                    isFolder = parentsId.Contains(child.ID)
                });

            return Json(children, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult LoadFaculties(bool isPaid)
        {
            var faculties = repository.Faculties
                .Where(fac => fac.HasPaid == isPaid)
                .Select(fac => new
                {
                    Text = fac.Name,
                    Value = fac.FacultyID,
                });
            return Json(faculties, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult LoadSpecialties(bool isPaid, int idFac)
        {
            var faculties = repository.Specialties
                .Where(sp => sp.HasPaid == isPaid && sp.FacultyID == idFac)
                .Select(sp => new
                {
                    Text = sp.Name,
                    Value = sp.SpecialtyID,
                });
            return Json(faculties, JsonRequestBehavior.AllowGet);
        }
    }
}