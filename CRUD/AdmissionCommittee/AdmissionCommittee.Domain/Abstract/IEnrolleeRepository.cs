using System.Collections.Generic;
using AdmissionCommittee.Domain.Entities;

namespace AdmissionCommittee.Domain.Abstract
{
    public interface IEnrolleeRepository
    {
        IEnumerable<Subject> Subjects { get; }

        IEnumerable<Enrollee> Enrollees { get; }

        //IEnumerable<TreeNode> TreeNodes { get; }

        //IEnumerable<TreeData> TreeData { get; }

        void SaveEnrollee(Enrollee enrollee);

        Enrollee DeleteEnrollee(int enrolleeID);

        IEnumerable<EducationPlace> EducationPlaces { get; }

        IEnumerable<FinancingType> FinancingTypes { get; }

        IEnumerable<Speciality> Specialities { get; }

        void DatabaseTest();
    }
}
