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
            var model = new EnrolleeEditViewModel
            {
                Enrollee = repository.Enrollees.FirstOrDefault(p => p.EnrolleeId == enrolleeId),
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
        public JsonResult LoadPayment()
        {
            var payment = new[] 
            {
                new
                {
                    Text = "For a fee",
                    Value = "true",
                    Tooltip = ""
                },
                new
                {
                    Text = "From grand",
                    Value = "False",
                    Tooltip = ""
                }
            };
            return Json(payment, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult LoadTreeNode(int? parentId)
        {
            var children = repository.TreeNodes.
                Where(node => node.ParentId == parentId).
                Select(node => new
                {
                    Text = node.Data.Name,
                    Value = node.NodeId,
                    Title = node.Data.FullName
                });
            return Json(children, JsonRequestBehavior.AllowGet);
        }
    }
}