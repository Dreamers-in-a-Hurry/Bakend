using AutoMapper;
using Fitshirt.Api.Dtos.Categories;
using Fitshirt.Api.Dtos.Colors;
using Fitshirt.Api.Dtos.Designs;
using Fitshirt.Api.Dtos.Posts;
using Fitshirt.Api.Dtos.PostsSizes;
using Fitshirt.Api.Dtos.Shields;
using Fitshirt.Api.Dtos.Sizes;
using Fitshirt.Api.Dtos.Users;
using Fitshirt.Infrastructure.Models.Common.Entities;
using Fitshirt.Infrastructure.Models.Designs;
using Fitshirt.Infrastructure.Models.Designs.Entities;
using Fitshirt.Infrastructure.Models.Posts;
using Fitshirt.Infrastructure.Models.Posts.Entities;
using Fitshirt.Infrastructure.Models.Users;

namespace Fitshirt.Api.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Post, PostResponse>();
        CreateMap<Post, ShirtVm>();
        CreateMap<PostRequest, Post>();
        CreateMap<Design, DesignResponse>();
        CreateMap<Design, ShirtVm>();
        CreateMap<DesignRequest, Design>();

        CreateMap<PostSize, PostSizeResponse>();
        
        CreateMap<Size, SizeResponse>();

        CreateMap<Category, CategoryResponse>();

        CreateMap<Color, ColorResponse>();

        CreateMap<Shield, ShieldResponse>();
        
        CreateMap<User, UserResponse>();
    }
    
}