using AlloMatch.DTOs;
using AlloMatch.Entities;
using AutoMapper;
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
        private  DataContext _dataContext { get; set; }
        private IMapper _mapper { get; set; }
        private IMediaService _mediaService { get; set; }



        public OrganisationService(UserManager<ApplicationUser> userManager,IMapper mapper,DataContext dataContext,
                                  IMediaService mediaService  )
        {
            _userManager = userManager;
            _mapper = mapper;
            _dataContext = dataContext;
            _mediaService = mediaService;
        }


        public async Task<Response<List<OrganisationDto>>> GetOrganisations(string userId)
        {

            var user = await _userManager.FindByIdAsync(userId);
            

            if (user == null)
                return Response<List<OrganisationDto>>.Failure("User Not Found");

            var organisations = _dataContext.Organisation.Where(o => o.ApplicationUserId == user.Id);

            if (organisations == null)
                return Response<List<OrganisationDto>>.Failure("User doesn't have any Organisation");
            // return _mapper.Map<List<GetOrganisationsDto>>(organisations);
            var organisationsDto = _mapper.Map<List<OrganisationDto>>(organisations);

            return Response<List<OrganisationDto>>.Success(organisationsDto);

        }

        public async Task<Response> AddMedia(string userId, long organisationId, MediaEntryDto dto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new Response("User Not Found", new string[] {"User.Not.Found" });

            var organisation = await _dataContext.Organisation.FindAsync(organisationId);
            if(organisation == null || organisation?.ApplicationUser != user)
                return new Response("Organisation Not Found", new string[] { "Organisation.Not.Found" });

            if (!_mediaService.IsImage(dto))
            {
                return new Response("File Not AValid Image", new string[] { "File.Not.A.Valid.Image" });
            }
            string filename = await _mediaService.Upload(dto).ConfigureAwait(false);
            var entry = await _dataContext.OrganisationMedia.AddAsync(new OrganisationMedia
            {
                Organisation = organisation,
                Media = new Media
                {
                    FileName = filename,
                    Size = dto.Size,
                    
                }
            });
            await _dataContext.SaveChangesAsync();
            return new Response("Media added with success");
            






        }

    }
}
