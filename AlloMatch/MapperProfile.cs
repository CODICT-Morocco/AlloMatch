using AlloMatch.DTOs;
using AlloMatch.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlloMatch
{
    public class MapperProfile : Profile
    {
            public MapperProfile()
            {
            CreateMap<Organisation, OrganisationDto>();

            }
        
    }
}
