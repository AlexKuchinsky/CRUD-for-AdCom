using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Entities
{
    public class UniversitySpecialty
    {
        public bool IsPaid { get; set; }

        public int FacultyID { get; set; }

        public int SpecialtyID { get; set; }

        public Faculty Faculty { get; set; }

        public Specialty Specialty { get; set; }
    }
}
