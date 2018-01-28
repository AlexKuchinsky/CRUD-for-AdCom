using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Entities
{
    public class ApplicationToSpeciality
    {
        public int ApplicationToSpecialityId { get; set; }

        public int ApplicationId { get; set; }
        public Application Application { get; set; }

        public int SpecialityId { get; set; }
        public Speciality Speciality { get; set; }
    }
}
