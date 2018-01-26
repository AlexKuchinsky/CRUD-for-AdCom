using AdmissionCommittee.Domain.Entities;
using System.Data.Entity;

namespace AdmissionCommittee.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Enrollee> Enrollees { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Speciality> Specilities { get; set; }
        public DbSet<EducationForm> EducationForms { get; set; }
        public DbSet<EducationPeriod> EducationPeriods { get; set; }
        public DbSet<EducationPlace> EducationPlaces { get; set; }
        public DbSet<FinancingType> FinancingTypes { get; set; }
        public DbSet<NCSQSpecialty> NCSQSpecialities { get; set; }
        public DbSet<SpecialityAvailableDate> SpecialityAvailableDates { get; set; }
        public DbSet<SpecialityGroup> SpecialityGroups { get; set; }
        public DbSet<SpecialityPositionsNumber> SpecialityPositionsNumbers { get; set; }
        public DbSet<SpecialityThreshold> SpecialityThresholds { get; set; }
        public DbSet<Subject> Subjects { get; set; }       
        public DbSet<SubjectMark> SubjectMarks { get; set; }
        //public DbSet<TreeNode> TreeNodes { get; set; }
        //public DbSet<TreeData> TreeData { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enrollee>().ToTable("Enrollees");
            modelBuilder.Entity<Address>().ToTable("Address");
            modelBuilder.Entity<Speciality>().ToTable("Speciality");
            modelBuilder.Entity<EducationForm>().ToTable("EducationForms");
            modelBuilder.Entity<EducationPeriod>().ToTable("EducationPeriods");
            modelBuilder.Entity<EducationPlace>().ToTable("EducationPlaces");
            modelBuilder.Entity<FinancingType>().ToTable("FinancingTypes");
            modelBuilder.Entity<NCSQSpecialty>().ToTable("NCSQSpecialities");
            modelBuilder.Entity<SpecialityAvailableDate>().ToTable("SpecialityAvailableDates");
            modelBuilder.Entity<SpecialityGroup>().ToTable("SpecialityGroups");
            modelBuilder.Entity<SpecialityPositionsNumber>().ToTable("SpecialityPositionsNumbers");
            modelBuilder.Entity<SpecialityThreshold>().ToTable("SpecialityThresholds");
            modelBuilder.Entity<Subject>().ToTable("Subjects");
            modelBuilder.Entity<SubjectMark>().ToTable("subjectMarks");
            //modelBuilder.Entity<TreeNode>().ToTable("Tree");
            //modelBuilder.Entity<TreeData>().ToTable("TreeData");

            //primary keys
            modelBuilder.Entity<Enrollee>().
                HasKey(en => en.EnrolleeId);
            modelBuilder.Entity<Address>().
                HasKey(ad => ad.EnrolleeId);
            modelBuilder.Entity<Speciality>().
                HasKey(sp => sp.SpecialityId);
            modelBuilder.Entity<EducationForm>().
                HasKey(ef => ef.EducationFormId);
            modelBuilder.Entity<EducationPeriod>().
                HasKey(ep => ep.EducationPeriodId);
            modelBuilder.Entity<EducationPlace>().
                HasKey(epl => epl.EducationPlaceId);
            modelBuilder.Entity<FinancingType>().
                HasKey(ft => ft.FinancingTypeId);
            modelBuilder.Entity<NCSQSpecialty>().
                HasKey(ns => ns.NCSQSpecialtyId);
            modelBuilder.Entity<SpecialityAvailableDate>().
                HasKey(sad => sad.SpecialityAvailableDateId);
            modelBuilder.Entity<SpecialityGroup>().
                HasKey(sg => sg.SpecialityGroupId);
            modelBuilder.Entity<SpecialityPositionsNumber>().
                HasKey(spn => spn.SpecialityPositionsNumberId);
            modelBuilder.Entity<SpecialityThreshold>().
                HasKey(st => st.SpecialityThresholdId);
            modelBuilder.Entity<Subject>().
                HasKey(s => s.SubjectId);
            modelBuilder.Entity<SubjectMark>().
               HasKey(ens => ens.SubjectMarkId);
            //modelBuilder.Entity<TreeNode>().
            //    HasKey(node => node.NodeId);
            //modelBuilder.Entity<TreeData>().
            //    HasKey(data => data.DataId);

            //relationships
            //one-to-one
            modelBuilder.Entity<Address>().
                HasRequired(ad => ad.Enrollee).
                WithRequiredPrincipal(en => en.Address);

            //one-to-many       
            modelBuilder.Entity<Speciality>().
                HasRequired(sp => sp.EducationForm).
                WithMany().
                HasForeignKey(sp => sp.EducationFormId);
            modelBuilder.Entity<Speciality>().
                HasRequired(sp => sp.EducationPeriod).
                WithMany().
                HasForeignKey(sp => sp.EducationPeriodId);
            modelBuilder.Entity<Speciality>().
                HasRequired(sp => sp.EducationPlace).
                WithMany().
                HasForeignKey(sp => sp.EducationPlaceId);
            modelBuilder.Entity<Speciality>().
                HasRequired(sp => sp.FinancingType).
                WithMany().
                HasForeignKey(sp => sp.FinancingTypeId);
            modelBuilder.Entity<Speciality>().
                HasRequired(sp => sp.NCSQSpecialty).
                WithMany().
                HasForeignKey(sp => sp.NCSQSpecialityId);
            modelBuilder.Entity<Speciality>().
                HasRequired(sp => sp.SpecialityAvailableDate).
                WithMany().
                HasForeignKey(sp => sp.SpecialityAvailableDateId);
            modelBuilder.Entity<Speciality>().
                HasRequired(sp => sp.SpecialityGroup).
                WithMany().
                HasForeignKey(sp => sp.SpecialityGroupId);
            modelBuilder.Entity<Speciality>().
                HasRequired(sp => sp.SpecialityPositionsNumber).
                WithMany().
                HasForeignKey(sp => sp.SpecialityPositionsNumberId);
            modelBuilder.Entity<Speciality>().
                HasRequired(sp => sp.SpecialityThreshold).
                WithMany().
                HasForeignKey(sp => sp.SpecialityThresholdId);
            modelBuilder.Entity<SpecialityThreshold>().
                HasRequired(st => st.FirstSubject).
                WithMany().
                HasForeignKey(st => st.FirstSubjectId);
            modelBuilder.Entity<SpecialityThreshold>().
                HasRequired(st => st.SecondSubject).
                WithMany().
                HasForeignKey(st => st.SecondSubjectId);
            modelBuilder.Entity<SpecialityThreshold>().
                HasRequired(st => st.LanguageSubject).
                WithMany().
                HasForeignKey(st => st.LanguageSubjectId);
            modelBuilder.Entity<Enrollee>().
                HasMany(en => en.CTSubjects).
                WithRequired().
                HasForeignKey(ens => ens.EnrolleeId);
            modelBuilder.Entity<SubjectMark>().
                HasRequired(sm => sm.Subject).
                WithMany().
                HasForeignKey(sm => sm.SubjectId);
            //modelBuilder.Entity<TreeData>().
            //    HasMany(data => data.Nodes).
            //    WithRequired(node => node.Data).
            //    HasForeignKey(node => node.DataId);
        }
    }
}
