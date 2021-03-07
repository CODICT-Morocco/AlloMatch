using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlloMatch.Entities
{
    public class Media
    {

        public string FileName { get; set; }
        public double Size { get; set; }
        public  long OrganisationId { get; set; }
        public long? SoccerFieldId { get; set; }

        public Organisation Organisation { get; set; }
        public SoccerField SoccerField { get; set; }

    }
}
