using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Entities
{
    public class SpecialityPositionsNumber
    {
        public int SpecialityPositionsNumberId { get; set; }

        public int TotalNumber { get; set; }

        public int HonorGuardNumber { get; set; }

        public int OrphanNumber { get; set; }

        public int AdditionalNumber { get; set; }
    }
}
