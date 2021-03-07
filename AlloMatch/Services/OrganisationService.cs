using AlloMatch.DTOs;
using AlloMatch.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlloMatch.Services
{
    public class OrganisationService : IOrganisationService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public OrganisationService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        public async Task<Response<GetOrganisationsDto>> GetOrganisations(string userId)
        {

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return Response<GetOrganisationsDto>.Failure("User Not Found");

            var organisations = user.Organisations.Where(o => o.ApplicationUserId == user.Id);

            if (organisations == null)
                return Response<GetOrganisationsDto>.Failure("User doesn't have any Organisation");

            return Response<GetOrganisationsDto>.Failure("User doesn't have any Organisation");








        }
    }
}
