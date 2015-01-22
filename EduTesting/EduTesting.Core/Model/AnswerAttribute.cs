using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.Model
{
    public class AnswerAttribute
    {
        [Key, Column(Order = 0)]
        public int AnswerId { get; set; }
        [Key, Column(Order = 1)]
        public int AttributeId { get; set; }
        public string AttributeValue { get; set; }

        public virtual CustomAttribute Attribute { get; set; }
        public virtual Answer Answer { get; set; }
    }
}
