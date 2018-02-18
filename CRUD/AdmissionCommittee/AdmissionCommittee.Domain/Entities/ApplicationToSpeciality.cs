
namespace AdmissionCommittee.Domain.Entities
{
    public class ApplicationToSpeciality
    {
        public int ApplicationToSpecialityId { get; set; }

        public int ApplicationId { get; set; }
        public virtual Application Application { get; set; }

        public int SpecialityId { get; set; }
        public virtual Speciality Speciality { get; set; }

        public int Priority { get; set; }
    }
}
