using AutoMapper;
using Projekt.Entities.Models;
using Projekt.Entities.Models.DTOs.Requests;
using Projekt.Entities.Models.DTOs.Responses;

namespace Projekt.MappersProfiles;

public class BooksMapperProfile : Profile
{
    public BooksMapperProfile()
    {
        CreateMap<BookDto, Book>()
            .ForMember(dest => dest.Images, opt => opt.Ignore());
        CreateMap<AddBookInstanceDto, BookInstance>();
        CreateMap<Book, BookResponseDto>();
        CreateMap<AddReviewDto, Review>();
        CreateMap<Review, ReviewResponseDto>()
            .ForMember(dest => dest.UserName,
                opt => opt.MapFrom(src => src.AppUser.UserName))
            .ForMember(dest => dest.Data,
                opt => opt.MapFrom(src => src.DateAdded));
    }
}