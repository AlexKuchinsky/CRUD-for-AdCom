using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Entities
{
    public class EnrolleeToSubject
    {
        public int EnrolleeToSubjectID { get; set; }

        public int EnrolleeID { get; set; }

        public virtual Enrollee Enrollee { get; set; }

        public int SubjectID { get; set; }

        public virtual Subject Subject { get; set; }

        public int Mark { get; set; }
    }
}
