using AdmissionCommittee.Domain.Abstract;
using AdmissionCommittee.Domain.Entities;
using System.Linq;
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

        public IEnumerable<Subject> Subjects
        {
            get { return context.Subjects; }
        }

        public IEnumerable<EducationPlace> EducationPlaces
        {
            get { return context.EducationPlaces; }
        }

        public IEnumerable<FinancingType> FinancingTypes
        {
            get { return context.FinancingTypes; }
        }

        public IEnumerable<Speciality> Specialities
        {
            get { return context.Specialities; }
        }

        //public IEnumerable<TreeNode> TreeNodes
        //{
        //    get { return context.TreeNodes; }
        //}

        //public IEnumerable<TreeData> TreeData
        //{
        //    get { return context.TreeData; }
        //}


        //public int GetSpecialtyInfoId(SpecialtyInfo info)
        //{
        //    SpecialtyInfo findedInfo = ((IEnumerable<SpecialtyInfo>)context.SpecialtyInfo)
        //        .Where(si => si.Equals(info)).FirstOrDefault();
        //    if(findedInfo != null)
        //    {
        //        return findedInfo.SpecialtyInfoId;
        //    }
        //    else
        //    {
        //        context.SpecialtyInfo.Add(info);
        //        context.SaveChanges();
        //        return ((IEnumerable<SpecialtyInfo>)context.SpecialtyInfo).Where(si => si.Equals(info)).FirstOrDefault().SpecialtyInfoId;
        //    }

        //}
        public void DatabaseTest()
        {
            var er1 = context.Address.ToList();
            var er2 = context.Applications.ToList();
            var er3 = context.Colors.ToList();
            var er4 = context.EducationDurations.ToList();
            var er5 = context.EducationForms.ToList();
            var er6 = context.EducationPlaces.ToList();
            var er7 = context.Enrollees.ToList();
            var er8 = context.FinancingTypes.ToList();
            //var er9 = context.GroupFriendships.ToList();
            var er10 = context.NCSQSpecialities.ToList();
            var er11 = context.Specialities.ToList();
            var er12 = context.SpecialityAvailableDates.ToList();
            var er13 = context.SpecialityGroups.ToList();
            var er14 = context.SpecialityPositionsNumbers.ToList();
            var er15 = context.SpecialitySubjects.ToList();
            var er16 = context.SubjectMarks.ToList();
            var er17 = context.Subjects.ToList();
            var er18 = context.SubjectThresholds.ToList();
            //var er19 = context.ApplicationToSpecialities.ToList();
        }

        public void SaveEnrollee(Enrollee enrollee)
        {
            if (enrollee.EnrolleeId == 0)
            {
                context.Enrollees.Add(enrollee);
            }
            else
            {
                Enrollee dbEntry = context.Enrollees.Find(enrollee.EnrolleeId);
                if (dbEntry != null)
                {
                    dbEntry.FirstName = enrollee.FirstName;
                    dbEntry.LastName = enrollee.LastName;
                    dbEntry.Patronymic = enrollee.Patronymic;
                    dbEntry.PassportNumber = enrollee.PassportNumber;
                    dbEntry.DateOfBirth = enrollee.DateOfBirth;
                    dbEntry.Address.Country = enrollee.Address.Country;
                    dbEntry.Address.City = enrollee.Address.City;
                    dbEntry.Address.Region = enrollee.Address.Region;
                    dbEntry.Address.Street = enrollee.Address.Street;
                    dbEntry.Address.BuildingNumber = enrollee.Address.BuildingNumber;
                    dbEntry.Address.ApartmentNumber = enrollee.Address.ApartmentNumber;
                    dbEntry.Address.PostalCode = enrollee.Address.PostalCode;
                    dbEntry.Phone = enrollee.Phone;
                    //for(int i = 0; i < dbEntry.Marks.Count; i++)
                    //{
                    //    dbEntry.Marks[i].SubjectId = enrollee.Marks[i].SubjectId;
                    //    dbEntry.Marks[i].Mark = enrollee.Marks[i].Mark;
                    //}
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