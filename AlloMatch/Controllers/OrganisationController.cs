using AlloMatch.Constants;
using AlloMatch.DTOs;
using AlloMatch.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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

        [Authorize(Roles.Professional)]
        [HttpGet("")]
        public async Task<Response<List<OrganisationDto>>> GetOrganisation()
        {
            return await _organisationService.GetOrganisations(_currentUserService.UserId);

        }

       // [Authorize(Roles.Professional)]
        [HttpPost("organisations/{id}/medias")]
        public async Task<Response> AddMedia(long id, [FromForm] MediaModel model)
        {
            await using var stream = model.File.OpenReadStream();
            var mediaEntry = new MediaEntryDto(model.File.FileName, model.File.ContentType, model.File.Length, stream, 
                                              Path.GetExtension(model.File.FileName).Substring(1));

            return await _organisationService.AddMedia(_currentUserService.UserId,id, mediaEntry);

        }

    }
}
