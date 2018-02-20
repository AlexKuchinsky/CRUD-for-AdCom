using AdmissionCommittee.Domain.Abstract;
using AdmissionCommittee.Domain.Entities;
using System.Linq;

namespace AdmissionCommittee.Domain.Concrete
{
    public class EFEnrolleeRepository : IEnrolleeRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Enrollee> Enrollees
        {
            get { return context.Enrollees; }
        }

        public IQueryable<Subject> Subjects
        {
            get { return context.Subjects; }
        }

        public IQueryable<EducationPlace> EducationPlaces
        {
            get { return context.EducationPlaces; }
        }

        public IQueryable<FinancingType> FinancingTypes
        {
            get { return context.FinancingTypes; }
        }

        public IQueryable<Speciality> Specialities
        {
            get { return context.Specialities; }
        }

        public IQueryable<Application> Applications
        {
            get { return context.Applications; }
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
            var er10 = context.NCSQSpecialities.ToList();
            var er11 = context.Specialities.ToList();
            var er12 = context.SpecialityAvailableDates.ToList();
            var er13 = context.SpecialityGroups.ToList();
            var er14 = context.SpecialityPositionsNumbers.ToList();
            var er15 = context.SpecialitySubjects.ToList();
            var er16 = context.SubjectMarks.ToList();
            var er17 = context.Subjects.ToList();
            var er18 = context.SubjectThresholds.ToList();
            var er19 = context.ApplicationToSpecialities.ToList();
        }

        public void SaveEnrollee(Enrollee enrollee)
        {
            if (enrollee.EnrolleeId == null)
            {
                var address = enrollee.Address;
                enrollee.Address = null;
                context.Enrollees.Add(enrollee);
                context.SaveChanges();
                if(enrollee.EnrolleeId != null)
                {
                    address.EnrolleeId = (int)enrollee.EnrolleeId;
                    context.Address.Add(address);
                    context.SaveChanges();
                }
            }
            else
            {
                Enrollee dbEnrollee = context.Enrollees.Find(enrollee.EnrolleeId);
                if (dbEnrollee != null)
                {
                    dbEnrollee.FirstName = enrollee.FirstName;
                    dbEnrollee.LastName = enrollee.LastName;
                    dbEnrollee.Patronymic = enrollee.Patronymic;
                    dbEnrollee.PassportNumber = enrollee.PassportNumber;
                    dbEnrollee.DateOfBirth = enrollee.DateOfBirth;
                    dbEnrollee.Phone = enrollee.Phone;
                    dbEnrollee.Address.Country = enrollee.Address.Country;
                    dbEnrollee.Address.Region = enrollee.Address.Region;
                    dbEnrollee.Address.City = enrollee.Address.City;                 
                    dbEnrollee.Address.Street = enrollee.Address.Street;
                    dbEnrollee.Address.BuildingNumber = enrollee.Address.BuildingNumber;
                    dbEnrollee.Address.ApartmentNumber = enrollee.Address.ApartmentNumber;
                    dbEnrollee.Address.PostalCode = enrollee.Address.PostalCode;
                    
                    //for(int i = 0; i < dbEntry.Marks.Count; i++)
                    //{
                    //    dbEntry.Marks[i].SubjectId = enrollee.Marks[i].SubjectId;
                    //    dbEntry.Marks[i].Mark = enrollee.Marks[i].Mark;
                    //}
                }
            }
            context.SaveChanges();
        }

        public Enrollee DeleteEnrollee(int enrolleeId)
        {
            Enrollee dbEntry = context.Enrollees.Find(enrolleeId);
            if (dbEntry != null)
            {
                context.Enrollees.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public bool SaveApplication(Application application)
        {
            if(application.ApplicationId == 0)
            {
                context.Applications.Add(application);
                //int applicationId = context.Applications.FirstOrDefault(app => app.EnrolleeId == application.EnrolleeId).ApplicationId;
                //application.ApplicationId = applicationId;
            }


            //check that all selected specialities exist in database
            foreach(var speciality in application.Specialities)
            {
                Speciality dbSpec = context.Specialities.Find(speciality.SpecialityId);
                if(dbSpec == null)
                {
                    return false;
                }
            }

            var deletedSpec = context.ApplicationToSpecialities
                .Where(appsp => appsp.ApplicationId == application.ApplicationId);

            context.ApplicationToSpecialities.RemoveRange(deletedSpec);

            foreach(var speciality in application.Specialities)
            {
                if(speciality.ApplicationToSpecialityId == 0)
                {
                    context.ApplicationToSpecialities.Add(speciality);
                }
            }

            context.SaveChanges();
            return true;
        }

        public bool DeleteApplication(int applicationId)
        {
            Application dbApplication = context.Applications.Find(applicationId);
            if (dbApplication != null)
            {
                context.Applications.Remove(dbApplication);
                context.SaveChanges();
            }
            return true;
        }
    }
}