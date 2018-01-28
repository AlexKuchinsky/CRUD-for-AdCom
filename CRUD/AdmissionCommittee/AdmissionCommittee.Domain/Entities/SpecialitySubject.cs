using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Entities
{
    public class SpecialitySubject
    {       
        public int SpecialitySubjectId { get; set; }

        public int SpecialityId { get; set; }

        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }

        public int SubjectThresholdsId { get; set; }
        public virtual SubjectThresholds SubjectThresholds { get; set; }

        public int Priority { get; set; }
    }
}
