using System.Collections.Generic;

namespace AlloMatch.Entities
{
    public class SoccerField : BaseEntity
    {
        public string Name { get; set; }
        public long OrganisationId { get; set; }
        public List<Booking> Bookings { get; set; }
        public Organisation Organisation { get; set; }
        public Size Size { get; set; }

    }

    public enum Size
    {
        FourVsFour,
        FiveVsFive,
        SixVsSix
    }
}
