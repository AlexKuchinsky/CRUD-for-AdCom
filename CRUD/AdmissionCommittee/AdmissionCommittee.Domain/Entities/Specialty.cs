using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Entities
{
    public class Specialty
    {
        public int SpecialtyID { get; set; }

        public int FacultyID { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public virtual Faculty Faculty { get; set; }

        public bool HasPaid { get; set; }

        public bool HasGrand { get; set; }
    }
}
