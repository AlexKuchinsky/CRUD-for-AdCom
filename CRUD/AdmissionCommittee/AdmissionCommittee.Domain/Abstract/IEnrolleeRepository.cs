using System.Collections.Generic;
using AdmissionCommittee.Domain.Entities;
using System.Web.Mvc;

namespace AdmissionCommittee.Domain.Abstract
{
    public interface IEnrolleeRepository
    {
        IEnumerable<Enrollee> Enrollees { get; }

        //void SaveEnrollee(Enrollee product);

        //Enrollee DeleteProduct(int productID);
    }
}
