using System.Collections.Generic;
using AdmissionCommittee.Domain.Entities;

namespace AdmissionCommittee.Domain.Abstract
{
    public interface IEnrolleeRepository
    {
        IEnumerable<Subject> Subjects { get; }

        IEnumerable<Enrollee> Enrollees { get; }

        IEnumerable<TreeNode> TreeNodes { get; }

        IEnumerable<TreeData> TreeData { get; }

        IEnumerable<Faculty> Faculties { get; }

        IEnumerable<Specialty> Specialties { get; }

        void SaveEnrollee(Enrollee enrollee);

        Enrollee DeleteEnrollee(int enrolleeID);
    }
}
