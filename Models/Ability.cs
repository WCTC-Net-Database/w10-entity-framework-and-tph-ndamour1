using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W9_assignment_template.Models
{
    public abstract class Ability
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Navigation property to Characters
        public virtual ICollection<Character> Characters { get; set; }
    }
}
