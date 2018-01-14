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

            //modelBuilder.Entity<TreeNode>().
            //    HasRequired(node => node.Data).
            //    WithMany();

            modelBuilder.Entity<TreeData>().
                HasMany(data => data.Nodes).
                WithRequired(node => node.Data);
        }
    }
}
