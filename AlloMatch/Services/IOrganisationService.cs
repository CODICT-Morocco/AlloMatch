using AlloMatch.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlloMatch.Services
{
    public interface IOrganisationService
    {
        Task<Response<List<OrganisationDto>>> GetOrganisations(string userId);
        Task<Response>AddMedia(string userId, long organisationId, MediaEntryDto dto);

    }
}
