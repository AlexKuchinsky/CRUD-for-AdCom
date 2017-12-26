using System.Linq;
using System.Web.Mvc;
using AdmissionCommittee.Domain.Abstract;
using AdmissionCommittee.Domain.Entities;
using AdmissionCommittee.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IEnrolleeRepository repository;
        public int PageSize = 5;

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
            Enrollee enrollee = repository.Enrollees
                .FirstOrDefault(p => p.EnrolleeID == enrolleeId);
            return View(enrollee);
        }

        public ViewResult Show(int enrolleeId)
        {
            Enrollee enrollee = repository.Enrollees
                    .FirstOrDefault(p => p.EnrolleeID == enrolleeId);
            return View(enrollee);
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

        public ViewResult Create()
        {
            return View("Edit", new Enrollee());
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
    }
}