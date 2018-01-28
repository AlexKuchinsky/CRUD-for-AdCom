using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Entities
{
    public class SubjectThresholds
    {
        public int SubjectThresholdsId { get; set; }

        public int MinScore { get; set; }

        public int MinAdditionalScore { get; set; }
    }
}
