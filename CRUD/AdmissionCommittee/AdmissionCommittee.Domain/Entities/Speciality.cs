using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Entities
{
    public class Speciality
    {
        public int SpecialityId { get; set; }

        public int EducationFormId { get; set; }
        public virtual EducationForm EducationForm { get; set; }

        public int EducationPeriodId { get; set; }
        public virtual EducationPeriod EducationPeriod { get; set; }

        public int EducationPlaceId { get; set; }
        public virtual EducationPlace EducationPlace { get; set; }

        public int FinancingTypeId { get; set; }
        public virtual FinancingType FinancingType { get; set; }

        public int NCSQSpecialityId { get; set; }
        public virtual NCSQSpecialty NCSQSpecialty { get; set; }

        public int SpecialityAvailableDateId { get; set; }
        public virtual SpecialityAvailableDate SpecialityAvailableDate { get; set; }

        public int SpecialityGroupId { get; set; }
        public virtual SpecialityGroup SpecialityGroup { get; set; }

        public int SpecialityPositionsNumberId { get; set; }
        public virtual SpecialityPositionsNumber SpecialityPositionsNumber { get; set; }

        public int SpecialityThresholdId { get; set; }
        public virtual SpecialityThreshold SpecialityThreshold { get; set; }
    }
}
