using AdmissionCommittee.Domain.Abstract;
using AdmissionCommittee.Domain.Entities;
using System.Collections.Generic;

namespace AdmissionCommittee.Domain.Concrete
{
    public class EFEnrolleeRepository : IEnrolleeRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Enrollee> Enrollees
        {
            get { return context.Enrollees; }
        }

        public void SaveEnrollee(Enrollee enrollee)
        {
            if (enrollee.EnrolleeID == 0)
            {
                context.Enrollees.Add(enrollee);
            }
            else
            {
                Enrollee dbEntry = context.Enrollees.Find(enrollee.EnrolleeID);
                if (dbEntry != null)
                {
                    dbEntry.FirstName = enrollee.FirstName;
                    dbEntry.LastName = enrollee.LastName;
                    dbEntry.Patronymic = enrollee.Patronymic;
                    dbEntry.PassportNumber = enrollee.PassportNumber;
                    dbEntry.CTLanguage = enrollee.CTLanguage;
                    dbEntry.CTFirstSubject = enrollee.CTFirstSubject;
                    dbEntry.CTSecondSubject = enrollee.CTSecondSubject;
                    dbEntry.DateOfBirth = enrollee.DateOfBirth;
                    dbEntry.City = enrollee.City;
                    dbEntry.Address = enrollee.Address;
                    dbEntry.GPA = enrollee.GPA;
                    dbEntry.Phone = enrollee.Phone;
                    dbEntry.EducationLevel = enrollee.EducationLevel;
                }
            }
            context.SaveChanges();
        }

        public Enrollee DeleteEnrollee(int enrolleeID)
        {
            Enrollee dbEntry = context.Enrollees.Find(enrolleeID);
            if (dbEntry != null)
            {
                context.Enrollees.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}