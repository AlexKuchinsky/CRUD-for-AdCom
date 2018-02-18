using System;
using System.Collections.Generic;

namespace AdmissionCommittee.Domain.Entities
{
    public class Enrollee
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? EnrolleeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Patronymic { get; set; }

        public string PassportNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Phone { get; set; }

        public virtual IList<SubjectMark> CTSubjects { get; set; }

        public virtual IList<Application> Applications { get; set; }
     
        public virtual Address Address { get; set; }

        public Enrollee()
        {
            CTSubjects = new List<SubjectMark>();
        }

        public int GetCTSum()
        {
            int CTSum = 0;
            foreach (var subject in CTSubjects)
            {
                CTSum += subject.Mark;
            }
            return CTSum;
        }
    }
}
