using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Entities
{
    public class GroupFriendship
    {
        public int GroupFriendshipId { get; set; }

        public int RequestingGroupId { get; set; }
        public virtual SpecialityGroup RequestingGroup { get; set; }

        public int ReceivingGroupId { get; set; }
        public virtual SpecialityGroup ReceivingGroup { get; set; }
    }
}
