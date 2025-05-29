using AutoMapper;
using Projekt.Entities.Models;
using Projekt.Entities.Models.DTOs.Requests;

namespace Projekt.MappersProfiles;

public class BooksMapperProfile : Profile
{
    public BooksMapperProfile()
    {
        CreateMap<BookDto, Book>()
            .ForMember(dest => dest.Images, opt => opt.Ignore());
        CreateMap<AddBookInstanceDto, BookInstance>();
    }
}