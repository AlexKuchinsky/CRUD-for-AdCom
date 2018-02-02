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

        public int EnrolleeId { get; set; }
        public virtual Enrollee Enrollee { get; set; }

        public virtual IList<ApplicationToSpeciality> Specialities { get; set; }
    }
}
