using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Projekt.Models;
using Projekt.Models.DTOs.Responses;

namespace Projekt.MappersProfiles;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<AppUser, UserInfoResponseDTO>()
            .ForMember(dest => dest.Roles,
                opt => opt.Ignore());
    }
}