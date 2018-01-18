using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Entities
{
    public class SpecialtyInfo
    {
        public int SpecialtyInfoId { get; set; }

        public int UniversityId { get; set; }
        private TreeData university;
        public virtual TreeData University
        {
            get
            {
                return university;
            }
            set
            {
                if(value.Type == TreeDataType.University)
                {
                    university = value;
                }
                else
                {
                    throw new Exception("Incorrect university");
                }
            }
        }

        public int FacultyId { get; set; }
        public virtual TreeData Faculty { get; set; }

        public int SpecialtyId { get; set; }
        public virtual TreeData Specialty { get; set; }

        public int SpecializationId { get; set; }
        public virtual TreeData Specialization { get; set; }

        public int FormOfStudyId { get; set; }
        public virtual TreeData FormOfStudy { get; set; }

        public int PaymentId { get; set; }
        public virtual TreeData Payment { get; set; }

        public virtual IList<Enrollee> Enrollees { get; set; }
    }
}
