using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Entities
{
    public class Faculty
    {
        public int FacultyID { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public virtual ICollection<Specialty> Specialities { get; set; }

        public bool HasPaid { get; set; }

        public bool HasGrand { get; set; }
    }
}
