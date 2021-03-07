using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlloMatch.DTOs

{
    public record OrganisationDto(string Name, string PhoneNumber);
    public record GetOrganisationsDto(
        List<OrganisationDto> Organisations
        );
}
