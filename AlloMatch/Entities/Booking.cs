using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlloMatch.Entities
{
    public class Booking : BaseEntity
    {
        public DateTime BookingDate { get; set; }
        public float Duration { get; set; }
        public string BookingName { get; set; }
        public string Notes { get; set; }
        public SoccerField SoccerField { get; set; }

    }
}
