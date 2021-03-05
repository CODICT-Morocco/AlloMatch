using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace AlloMatch.Entities
{
    public class ApplicationUser : IdentityUser<long>, IBaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public List<Organisation> Organisations { get; set; }
    }
}
