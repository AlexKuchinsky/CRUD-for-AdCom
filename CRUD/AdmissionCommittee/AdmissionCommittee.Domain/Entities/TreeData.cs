using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Entities
{
    public enum TreeDataType
    {
        University,
        Faculty,
        Specialty,
        Specialization,
        FormOfStudy,
        Payment
    }

    public class TreeData
    {
        public int DataId { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public TreeDataType Type { get; set; }

        public ICollection<TreeNode> Nodes { get; set; }
    }
}
