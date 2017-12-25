using System.Collections.Generic;
using AdmissionCommittee.Domain.Entities;

namespace AdmissionCommittee.WebUI.Models
{
    public class EnrolleesListViewModel
    {
        public IEnumerable<Enrollee> Enrollees { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public EducationLevel? CurrentCategoryOfEducation { get; set; }
    }
}