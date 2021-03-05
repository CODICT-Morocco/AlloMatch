using System.Collections.Generic;

namespace AlloMatch.Entities
{
    public class Organisation : BaseEntity
    {
        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public List<SoccerField> SoccerFields { get; set; }
    }
}
