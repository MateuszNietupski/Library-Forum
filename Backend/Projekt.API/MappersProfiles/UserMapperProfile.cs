using AutoMapper;
using Projekt.Entities.Models;
using Projekt.Entities.Models.DTOs.Responses;

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