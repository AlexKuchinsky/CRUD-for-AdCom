using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace AdmissionCommittee.Domain.Entities
{
    public class SpecialtyInfo : IEquatable<SpecialtyInfo>
    {
        public int SpecialtyInfoId { get; set; }

        [Required]
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
        [Required]
        public int FacultyId { get; set; }
        public virtual TreeNode Faculty { get; set; }

        [Required]
        public int SpecialtyId { get; set; }
        public virtual TreeNode Specialty { get; set; }

        [Required]
        public int SpecializationId { get; set; }
        public virtual TreeNode Specialization { get; set; }

        [Required]
        public int FormOfStudyId { get; set; }
        public virtual TreeNode FormOfStudy { get; set; }

        [Required]
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
