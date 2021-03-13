using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlloMatch.Entities
{
    public class OrganisationMedia : BaseEntity
    {
        public long OrganisationId { get; set; }
        public long MediaId { get; set; }

        public Organisation Organisation { get; set; }
        public Media Media { get; set; }
    }
}
