﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Entities
{
    public class Subject
    {
        public int SubjectID { get; set; }

        public string Name { get; set; }

        public virtual ICollection<EnrolleeToSubject> EnrolleeToSubjects { get; set; }
    }
}