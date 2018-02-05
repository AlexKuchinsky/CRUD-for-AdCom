using AdmissionCommittee.Domain.Entities;
using System.Linq;

namespace AdmissionCommittee.Domain.Abstract
{
    public interface IEnrolleeRepository
    {
        IQueryable<Subject> Subjects { get; }
        IQueryable<Enrollee> Enrollees { get; }
        IQueryable<EducationPlace> EducationPlaces { get; }
        IQueryable<FinancingType> FinancingTypes { get; }
        IQueryable<Speciality> Specialities { get; }

        //IEnumerable<TreeNode> TreeNodes { get; }

        //IEnumerable<TreeData> TreeData { get; }

        void SaveEnrollee(Enrollee enrollee);

        Enrollee DeleteEnrollee(int enrolleeID);

        

        void DatabaseTest();
    }
}
