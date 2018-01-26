using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Entities
{
    public class SpecialityThreshold
    {       
        public int SpecialityThresholdId { get; set; }

        //first
        public int FirstSubjectId { get; set; }
        public virtual Subject FirstSubject { get; set; }

        public int FirstMinScore { get; set; }

        public int FirstMinAdditionalScore { get; set; }

        //second
        public int SecondSubjectId { get; set; }
        public virtual Subject SecondSubject { get; set; }

        public int SecondMinScore { get; set; }

        public int SecondMinAdditionalScore { get; set; }

        //language
        public int LanguageSubjectId { get; set; }
        public virtual Subject LanguageSubject { get; set; }

        public int LanguageMinScore { get; set; }

        public int LanguageMinAdditionalScore { get; set; }
    }
}
