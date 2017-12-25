using System.Collections.Generic;
using System.Web.Mvc;
using AdmissionCommittee.Domain.Abstract;
using System.Linq;
using AdmissionCommittee.Domain.Entities;

namespace SportsStore.WebUI.Controllers
{
    public class NavigationController : Controller
    {
        private IEnrolleeRepository repository;

        public NavigationController(IEnrolleeRepository repo)
        {
            repository = repo;
        }
        /*
        public PartialViewResult Menu(EducationLevel? edLevel = null)
        {
            ViewBag.SelectedCategory = edLevel;
            
            IEnumerable<string> categories = repository.Enrollees
                .Select(x => x.EducationLevel.GetString())
                .Distinct()
                .OrderBy(x => x);
            return PartialView(categories);
        }*/
    }
}