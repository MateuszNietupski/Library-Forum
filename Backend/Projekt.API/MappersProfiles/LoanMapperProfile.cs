using AutoMapper;
using Projekt.Entities.Models;
using Projekt.Entities.Models.DTOs.Requests;

namespace Projekt.MappersProfiles;

public class LoanMapperProfile : Profile
{
    public LoanMapperProfile()
    {
        CreateMap<LoanDTO, Loan>();
        
    }
}