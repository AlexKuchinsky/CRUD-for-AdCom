using AdmissionCommittee.Domain.Entities;
using System.Data.Entity;

namespace AdmissionCommittee.Domain.Concrete
{
    public class EFDbContext : DbContext
    {    
        public DbSet<Address> Address { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<ApplicationToSpeciality> ApplicationToSpecialities { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<EducationDuration> EducationDurations { get; set; }
        public DbSet<EducationForm> EducationForms { get; set; }
        public DbSet<EducationPlace> EducationPlaces { get; set; }
        public DbSet<Enrollee> Enrollees { get; set; }
        public DbSet<FinancingType> FinancingTypes { get; set; }
        public DbSet<NCSQSpeciality> NCSQSpecialities { get; set; }
        public DbSet<Speciality> Specialities { get; set; }
        public DbSet<SpecialityAvailableDate> SpecialityAvailableDates { get; set; }
        public DbSet<SpecialityGroup> SpecialityGroups { get; set; }
        public DbSet<SpecialityPositionsNumber> SpecialityPositionsNumbers { get; set; }
        public DbSet<SpecialitySubject> SpecialitySubjects { get; set; }
        public DbSet<Subject> Subjects { get; set; }       
        public DbSet<SubjectMark> SubjectMarks { get; set; }
        public DbSet<SubjectThresholds> SubjectThresholds { get; set; }
        //public DbSet<TreeNode> TreeNodes { get; set; }
        //public DbSet<TreeData> TreeData { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>().ToTable("Addresses");
            modelBuilder.Entity<Application>().ToTable("Applications");
            modelBuilder.Entity<ApplicationToSpeciality>().ToTable("ApplicationToSpecialities");
            modelBuilder.Entity<Color>().ToTable("Colors");
            modelBuilder.Entity<EducationDuration>().ToTable("EducationDurations");
            modelBuilder.Entity<EducationForm>().ToTable("EducationForms");
            modelBuilder.Entity<EducationPlace>().ToTable("EducationPlaces");
            modelBuilder.Entity<Enrollee>().ToTable("Enrollees");
            modelBuilder.Entity<FinancingType>().ToTable("FinancingTypes");
            modelBuilder.Entity<NCSQSpeciality>().ToTable("NCSQSpecialities");
            modelBuilder.Entity<Speciality>().ToTable("Specialities");
            modelBuilder.Entity<SpecialityAvailableDate>().ToTable("SpecialityAvailableDates");
            modelBuilder.Entity<SpecialityGroup>().ToTable("SpecialityGroups");
            modelBuilder.Entity<SpecialityPositionsNumber>().ToTable("SpecialityPositionsNumbers");
            modelBuilder.Entity<SpecialitySubject>().ToTable("SpecialitySubjects");
            modelBuilder.Entity<Subject>().ToTable("Subjects");
            modelBuilder.Entity<SubjectMark>().ToTable("SubjectMarks");
            modelBuilder.Entity<SubjectThresholds>().ToTable("SubjectThresholds");
            //modelBuilder.Entity<TreeNode>().ToTable("Tree");
            //modelBuilder.Entity<TreeData>().ToTable("TreeData");

            //primary keys   
            modelBuilder.Entity<Address>().
                HasKey(ad => ad.EnrolleeId);
            modelBuilder.Entity<Application>().
                HasKey(app => app.ApplicationId);
            modelBuilder.Entity<ApplicationToSpeciality>().
                HasKey(apps => apps.ApplicationToSpecialityId);
            modelBuilder.Entity<Color>().
                HasKey(c => c.ColorId);
            modelBuilder.Entity<EducationDuration>().
                HasKey(ep => ep.EducationDurationId);
            modelBuilder.Entity<EducationForm>().
                HasKey(ef => ef.EducationFormId);
            modelBuilder.Entity<EducationPlace>().
                HasKey(epl => epl.EducationPlaceId);
            modelBuilder.Entity<Enrollee>().
                HasKey(en => en.EnrolleeId);
            modelBuilder.Entity<FinancingType>().
                HasKey(ft => ft.FinancingTypeId);
            modelBuilder.Entity<NCSQSpeciality>().
                HasKey(ns => ns.NCSQSpecialityId);
            modelBuilder.Entity<Speciality>().
                HasKey(sp => sp.SpecialityId);
            modelBuilder.Entity<SpecialityAvailableDate>().
                HasKey(sad => sad.SpecialityAvailableDateId);
            modelBuilder.Entity<SpecialityGroup>().
                HasKey(sg => sg.SpecialityGroupId);
            modelBuilder.Entity<SpecialityPositionsNumber>().
                HasKey(spn => spn.SpecialityPositionsNumberId);
            modelBuilder.Entity<SpecialitySubject>().
                HasKey(ss => ss.SpecialitySubjectId);
            modelBuilder.Entity<Subject>().
                HasKey(s => s.SubjectId);
            modelBuilder.Entity<SubjectMark>().
               HasKey(ens => ens.SubjectMarkId);
            modelBuilder.Entity<SubjectThresholds>().
                HasKey(st => st.SubjectThresholdsId);
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
            modelBuilder.Entity<Enrollee>().
                HasMany(en => en.CTSubjects).
                WithRequired().
                HasForeignKey(ens => ens.EnrolleeId);
            modelBuilder.Entity<Enrollee>().
                HasMany(en => en.Applications).
                WithRequired(app => app.Enrollee).
                HasForeignKey(app => app.EnrolleeId);
            modelBuilder.Entity<Application>().
                HasMany(app => app.Specialities).
                WithRequired(apps => apps.Application).
                HasForeignKey(apps => apps.ApplicationId);
            modelBuilder.Entity<ApplicationToSpeciality>().
                HasRequired(apps => apps.Speciality).
                WithMany(sp => sp.Applications).
                HasForeignKey(apps => apps.SpecialityId);
            modelBuilder.Entity<SubjectMark>().
                HasRequired(sm => sm.Subject).
                WithMany().
                HasForeignKey(sm => sm.SubjectId);
            modelBuilder.Entity<Speciality>().
                HasRequired(sp => sp.EducationForm).
                WithMany().
                HasForeignKey(sp => sp.EducationFormId);
            modelBuilder.Entity<Speciality>().
                HasRequired(sp => sp.EducationDuration).
                WithMany().
                HasForeignKey(sp => sp.EducationDurationId);
            modelBuilder.Entity<Speciality>().
                HasRequired(sp => sp.EducationPlace).
                WithMany().
                HasForeignKey(sp => sp.EducationPlaceId);
            modelBuilder.Entity<Speciality>().
                HasRequired(sp => sp.FinancingType).
                WithMany().
                HasForeignKey(sp => sp.FinancingTypeId);
            modelBuilder.Entity<Speciality>().
                HasRequired(sp => sp.NCSQSpeciality).
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
                HasMany(sp => sp.SpecialitySubjects).
                WithRequired().
                HasForeignKey(ss => ss.SpecialityId);
            modelBuilder.Entity<Speciality>().
                HasRequired(sp => sp.SpecialityGroup).
                WithMany(gr => gr.Specialities).
                HasForeignKey(sp => sp.SpecialityGroupId);
            modelBuilder.Entity<SpecialitySubject>().
                HasRequired(ss => ss.SubjectThresholds).
                WithMany().
                HasForeignKey(ss => ss.SubjectThresholdsId);
            modelBuilder.Entity<SpecialitySubject>().
                HasRequired(ss => ss.Subject).
                WithMany().
                HasForeignKey(ss => ss.SubjectId);
            //modelBuilder.Entity<SpecialityGroup>().
            //    HasMany(sg => sg.Friendships).
            //    WithRequired(gf => gf.RequestingGroup).
            //    HasForeignKey(gf => gf.RequestingGroupId);
            modelBuilder.Entity<GroupFriendship>().
                HasRequired(gf => gf.ReceivingGroup).
                WithMany().
                HasForeignKey(gf => gf.ReceivingGroupId);

            //many-to-many
            

            modelBuilder.Entity<SpecialityGroup>().
                HasMany(sg => sg.FriendlyGroups).
                WithMany().
                Map(m =>
                {
                    m.MapLeftKey("RequestingGroupId");
                    m.MapRightKey("ReceivingGroupId");
                    m.ToTable("GroupFriendships");
                });
        }
    }
}
