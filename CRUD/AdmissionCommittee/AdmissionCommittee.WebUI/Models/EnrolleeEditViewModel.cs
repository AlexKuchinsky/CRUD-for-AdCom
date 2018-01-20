using System.Collections.Generic;
using AdmissionCommittee.Domain.Entities;
using System.Web.Mvc;

namespace AdmissionCommittee.WebUI.Models
{
    public class EnrolleeEditViewModel
    {
        public Enrollee Enrollee { get; set; }
        public SpecialtyInfo SpecialtyInfo { get; set; }
        public IEnumerable<Subject> Subjects { get; set; }
    }
}