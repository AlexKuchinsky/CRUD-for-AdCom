using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Entities
{
    public class TreeNode
    {
        public int NodeId { get; set; }

        public int ParentId { get; set; }

        public int DataId { get; set; }

        public virtual TreeData Data { get; set; }
    }
}
