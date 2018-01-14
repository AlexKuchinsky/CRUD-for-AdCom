﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Entities
{
    public class EnrolleeToSubject
    {
        public int EnrolleeToSubjectId { get; set; }

        public int EnrolleeId { get; set; }

        public virtual Enrollee Enrollee { get; set; }

        public int SubjectId { get; set; }

        public virtual Subject Subject { get; set; }

        public int Mark { get; set; }
    }
}
