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

        public int FacultyId { get; set; }

        public int SpecialtyId { get; set; }

        public Faculty Faculty { get; set; }

        public Specialty Specialty { get; set; }
    }
}
