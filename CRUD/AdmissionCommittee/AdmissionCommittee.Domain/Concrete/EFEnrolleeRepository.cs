using AdmissionCommittee.Domain.Abstract;
using AdmissionCommittee.Domain.Entities;
using System.Collections.Generic;

namespace AdmissionCommittee.Domain.Concrete
{
    public class EFEnrolleeRepository : IEnrolleeRepository
    {
        private EFDbContext context = new EFDbContext();
        public IEnumerable<Enrollee> Enrollees
        {
            get { return context.Enrollees; }
        }
    }
}