using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.Model
{
    public class TestAttribute
    {
        [Key, Column(Order = 0)]
        public int TestId { get; set; }
        [Key, Column(Order = 1)]
        public int AttributeId { get; set; }
        public string AttributeValue { get; set; }

        public virtual CustomAttribute Attribute { get; set; }
        public virtual Test Test { get; set; }
    }
}
