﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Entities
{
    public class EducationForm
    {
        public int EducationFormId { get; set; }

        public string Name { get; set; }

        public bool IsInternal { get; set; }
    }
}
