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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>().ToTable("Address");
            modelBuilder.Entity<Enrollee>().ToTable("Enrollees");
            modelBuilder.Entity<EnrolleeToSubject>().ToTable("EnrolleeToSubjects");
            modelBuilder.Entity<Subject>().ToTable("Subjects");

            //primary keys
            modelBuilder.Entity<Enrollee>().
                HasKey(en => en.EnrolleeID);
            modelBuilder.Entity<Subject>().
                HasKey(s => s.SubjectID);
            modelBuilder.Entity<EnrolleeToSubject>().
                HasKey(ens => ens.EnrolleeToSubjectID);
            modelBuilder.Entity<Address>().
                HasKey(ad => ad.EnrolleeID);

            //relationships

            //many-to-many
            modelBuilder.Entity<Enrollee>().
                HasMany(en => en.Marks).
                WithRequired(s => s.Enrollee).
                HasForeignKey(ens => ens.EnrolleeID);

            modelBuilder.Entity<Subject>().
                HasMany(s => s.EnrolleeToSubjects).
                WithRequired(en => en.Subject).
                HasForeignKey(ens => ens.SubjectID);

            modelBuilder.Entity<Address>().
                HasRequired(ad => ad.Enrollee).
                WithRequiredPrincipal(en => en.Address);
            //one-to-many
            /*modelBuilder.Entity<Enrollee>().
                HasRequired(en => en.CTFirstSubject).
                WithRequiredPrincipal(m => m.Enrollee);

            modelBuilder.Entity<Enrollee>().
                HasRequired(en => en.CTSecondSubject).
                WithRequiredPrincipal(m => m.Enrollee);

            modelBuilder.Entity<Enrollee>().
                HasRequired(en => en.CTLanguage).
                WithRequiredPrincipal(m => m.Enrollee);*/
        }
    }
}
