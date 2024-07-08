using AutoMapper;
using Projekt.Models;
using Projekt.Models.DTOs.Requests;
using Projekt.Models.DTOs.Responses;

namespace Projekt.MappersProfiles;

public class ForumMapperProfile : Profile
{
    public ForumMapperProfile()
    {
        CreateMap<Post, PostResponseDTO>()
            .ForMember(dest => dest.Comments,
                opt => opt.MapFrom(src => src.Comments))
            .ForMember(dest => dest.Data,
            opt => opt.MapFrom(src => src.DateAdded));
        CreateMap<Comment, CommentResponseDTO>()
            .ForMember(dest => dest.UserName,
                opt => opt.MapFrom(src => src.AppUser.UserName))
            .ForMember(dest => dest.Data,
                opt => opt.MapFrom(src => src.DateAdded));
        CreateMap<Category, CategoriesResponseDTO>()
            .ForMember(dest => dest.Subcategories,
                opt => opt.MapFrom(src => src.Subcategories));
        CreateMap<Subcategory, SubCategoryResponseDTO>();
        CreateMap<AddPostDTO, Post>();
        CreateMap<AddSubCategoryDTO, Subcategory>();
        CreateMap<AddCategoryDTO, Category>();
        CreateMap<AddCommentDTO, Comment>();
    }
}