using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlloMatch.Entities
{
    public class Organisation : BaseEntity
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Address { get; set; }
        public bool IsDefault { get; set; }
        public long ApplicationUserId { get; set; }
        public long CityId { get; set; }

        public Media ThumbNail { get; set; }
        public List<OpeningHour> OpeningHours { get; set; }
        public List<OrganisationMedia> Medias { get; set; }
        public City City { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public List<SoccerField> SoccerFields { get; set; }
    }
}
