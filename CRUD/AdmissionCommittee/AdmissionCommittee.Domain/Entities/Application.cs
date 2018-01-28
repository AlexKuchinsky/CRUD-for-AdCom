using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Entities
{
    public class Application
    {
        public int ApplicationId { get; set; }

        public int FinancingTypeId { get; set; }
        public virtual FinancingType FinancingType { get; set; }

        public int EnrolleeId { get; set; }
        public virtual Enrollee Enrollee { get; set; }

        public virtual IList<Speciality> Specialities { get; set; }
    }
}
