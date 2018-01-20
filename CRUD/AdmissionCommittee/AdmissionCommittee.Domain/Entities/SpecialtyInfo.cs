using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Entities
{
    public class SpecialtyInfo : IEquatable<SpecialtyInfo>
    {
        public int SpecialtyInfoId { get; set; }

        public int UniversityId { get; set; }
        public virtual TreeNode University { get; set; }
        //private TreeNode university;
        //public virtual TreeNode University
        //{
        //    get
        //    {
        //        return university;
        //    }
        //    set
        //    {
        //        if(value.Data.Type == TreeDataType.University)
        //        {
        //            university = value;
        //        }
        //        else
        //        {
        //            throw new Exception("Incorrect university");
        //        }
        //    }
        //}

        public int FacultyId { get; set; }
        public virtual TreeNode Faculty { get; set; }

        public int SpecialtyId { get; set; }
        public virtual TreeNode Specialty { get; set; }

        public int SpecializationId { get; set; }
        public virtual TreeNode Specialization { get; set; }

        public int FormOfStudyId { get; set; }
        public virtual TreeNode FormOfStudy { get; set; }

        public int PaymentId { get; set; }
        public virtual TreeNode Payment { get; set; }

        public virtual IList<Enrollee> Enrollees { get; set; }

        public bool Equals(SpecialtyInfo other)
        {
            return
                UniversityId == other.UniversityId &&
                FacultyId == other.FacultyId &&
                SpecialtyId == other.SpecialtyId &&
                SpecializationId == other.SpecializationId &&
                FormOfStudyId == other.FormOfStudyId &&
                PaymentId == other.PaymentId;
        }
    }
}
