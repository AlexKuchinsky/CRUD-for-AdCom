using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdmissionCommittee.Domain.Abstract;

namespace AdmissionCommittee.WebUI.Controllers
{
    public class EnrolleeController : Controller
    {
        private IEnrolleeRepository repository;
        
        public EnrolleeController(IEnrolleeRepository enrolleeRepository)
        {
            repository = enrolleeRepository;
        }

        public ViewResult List()
        {
            return View(repository.Enrollees);
        }
    }
}