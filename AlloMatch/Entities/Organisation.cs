using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlloMatch.Entities
{
    public class Organisation : BaseEntity
    {
        public string Name { get; set; }

        public List<SoccerField> SoccerFields { get; set; }

    }
}
