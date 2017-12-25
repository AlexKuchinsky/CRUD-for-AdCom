/*using System.Linq;
using System.Web.Mvc;
using AdmissionCommittee.Domain.Abstract;
using AdmissionCommittee.Domain.Entities;
using AdmissionCommittee.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class EnrolleeController : Controller
    {
        private IEnrolleeRepository repository;
        public int PageSize = 4;

        public EnrolleeController(IEnrolleeRepository enrolleeRepository)
        {
            repository = enrolleeRepository;
        }

        public ViewResult List(EducationLevel? edLevel, int page = 1)
        {
            EnrolleesListViewModel model = new EnrolleesListViewModel
            {
                Enrollees = repository.Enrollees
                    //.Where(p => edLevel == null || p.EducationLevel == edLevel)
                    .OrderBy(p => p.EnrolleeID)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Enrollees.Count()
                    TotalItems = edLevel == null ?
                        repository.Enrollees.Count() :
                        repository.Enrollees.Where(e => e.EducationLevel == edLevel).Count()
                },
                //CurrentCategoryOfEducation = edLevel
            };
            return View(model);
        }
    }
}*/