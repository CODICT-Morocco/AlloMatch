using AlloMatch.DTOs;
using AlloMatch.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlloMatch.Controllers
{
    [ApiController]
    [Route("organisations")]
    public class OrganisationController : ControllerBase
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IOrganisationService _organisationService;
        public OrganisationController( ICurrentUserService currentUserService, IOrganisationService organisationService)
        {
            _currentUserService = currentUserService;
            _organisationService = organisationService;
        }

        [Authorize("Professsional")]
        public async Task<Response<GetOrganisationsDto>> GetOrganisation()
        {

            return await _organisationService.GetOrganisations(_currentUserService.UserId);

        }
    }
}
