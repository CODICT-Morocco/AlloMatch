using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlloMatch.Entities
{
    public class SoccerFieldMedia : BaseEntity
    {
        public long SoccierFieldId { get; set; }
        public long MediaId { get; set; }

        public SoccerField SoccierField { get; set; }
        public Media Media { get; set; }

    }
}
