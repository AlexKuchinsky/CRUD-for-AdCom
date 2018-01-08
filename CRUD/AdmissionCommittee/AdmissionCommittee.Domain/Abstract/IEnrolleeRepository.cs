using System.Collections.Generic;
using AdmissionCommittee.Domain.Entities;

namespace AdmissionCommittee.Domain.Abstract
{
    public interface IEnrolleeRepository
    {
        IEnumerable<Subject> Subjects { get; }

        IEnumerable<Enrollee> Enrollees { get; }

        IEnumerable<TreeNode> TreeNodes { get; }

        void SaveEnrollee(Enrollee enrollee);

        Enrollee DeleteEnrollee(int enrolleeID);
    }
}
