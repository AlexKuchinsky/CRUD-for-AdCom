using AdmissionCommittee.Domain.Entities;
using System.Data.Entity;

namespace AdmissionCommittee.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Enrollee> Enrollees { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<EnrolleeToSubject> EnrolleeToObjects { get; set; }
        public DbSet<TreeNode> TreeNodes { get; set; }
        public DbSet<TreeData> TreeData { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<SpecialtyInfo> SpecialtyInfo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>().ToTable("Address");
            modelBuilder.Entity<Enrollee>().ToTable("Enrollees");
            modelBuilder.Entity<EnrolleeToSubject>().ToTable("EnrolleeToSubjects");
            modelBuilder.Entity<Subject>().ToTable("Subjects");
            modelBuilder.Entity<TreeNode>().ToTable("Tree");
            modelBuilder.Entity<TreeData>().ToTable("TreeData");
            modelBuilder.Entity<Faculty>().ToTable("Faculties");
            modelBuilder.Entity<Specialty>().ToTable("Specialties");
            modelBuilder.Entity<SpecialtyInfo>().ToTable("SpecialtyInfo");

            //primary keys
            modelBuilder.Entity<Enrollee>().
                HasKey(en => en.EnrolleeId);
            modelBuilder.Entity<Subject>().
                HasKey(s => s.SubjectId);
            modelBuilder.Entity<EnrolleeToSubject>().
                HasKey(ens => ens.EnrolleeToSubjectId);
            modelBuilder.Entity<Address>().
                HasKey(ad => ad.EnrolleeId);
            modelBuilder.Entity<TreeNode>().
                HasKey(node => node.NodeId);
            modelBuilder.Entity<TreeData>().
                HasKey(data => data.DataId);
            modelBuilder.Entity<Faculty>().
                HasKey(f => f.FacultyId);
            modelBuilder.Entity<Specialty>().
                HasKey(sp => sp.SpecialtyId);
            modelBuilder.Entity<SpecialtyInfo>().
                HasKey(si => si.SpecialtyInfoId);

            //relationships
            //one-to-one
            modelBuilder.Entity<Address>().
                HasRequired(ad => ad.Enrollee).
                WithRequiredPrincipal(en => en.Address);

            //one-to-many
            modelBuilder.Entity<Enrollee>().
                HasMany(en => en.Marks).
                WithRequired(s => s.Enrollee).
                HasForeignKey(ens => ens.EnrolleeId);

            modelBuilder.Entity<Subject>().
                HasMany(s => s.EnrolleeToSubjects).
                WithRequired(en => en.Subject).
                HasForeignKey(ens => ens.SubjectId);

            modelBuilder.Entity<Faculty>().
                HasMany(f => f.Specialities).
                WithRequired(sp => sp.Faculty).
                HasForeignKey(sp => sp.FacultyId);

            modelBuilder.Entity<TreeData>().
                HasMany(data => data.Nodes).
                WithRequired(node => node.Data).
                HasForeignKey(node => node.DataId);

            modelBuilder.Entity<SpecialtyInfo>().
                HasMany(si => si.Enrollees).
                WithRequired(en => en.SpecialtyInfo).
                HasForeignKey(en => en.SpecialtyInfoId);

            //specialtyinfo
            modelBuilder.Entity<SpecialtyInfo>().
                HasRequired(si => si.University).
                WithMany().
                HasForeignKey(en => en.UniversityId);
            modelBuilder.Entity<SpecialtyInfo>().
                HasRequired(si => si.Faculty).
                WithMany().
                HasForeignKey(en => en.FacultyId);
            modelBuilder.Entity<SpecialtyInfo>().
                HasRequired(si => si.Specialty).
                WithMany().
                HasForeignKey(en => en.SpecialtyId);
            modelBuilder.Entity<SpecialtyInfo>().
                HasRequired(si => si.Specialization).
                WithMany().
                HasForeignKey(en => en.SpecializationId);
            modelBuilder.Entity<SpecialtyInfo>().
                HasRequired(si => si.FormOfStudy).
                WithMany().
                HasForeignKey(en => en.FormOfStudyId);
            modelBuilder.Entity<SpecialtyInfo>().
                HasRequired(si => si.Payment).
                WithMany().
                HasForeignKey(en => en.PaymentId);

        }
    }
}
