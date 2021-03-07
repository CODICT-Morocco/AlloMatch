using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlloMatch.Entities
{
    public class OpeningHour : BaseEntity
    {
        public long OrganisationId { get; set; }
        public WeekDay WeekDay { get; set; }
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }

         
    }

    public enum WeekDay
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }
}
