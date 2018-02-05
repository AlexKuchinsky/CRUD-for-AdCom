using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Entities
{
    public class SpecialityGroup
    {
        public int SpecialityGroupId { get; set; }

        public string Name { get; set; }

        public int MaxNumOfSpec { get; set; }

        public virtual IList<Speciality> Specialities { get; set; }

        public virtual IList<SpecialityGroup> FriendlyGroups { get; set; }

        public SpecialityGroup()
        {
            Specialities = new List<Speciality>();
            FriendlyGroups = new List<SpecialityGroup>();
        }
    }
}
