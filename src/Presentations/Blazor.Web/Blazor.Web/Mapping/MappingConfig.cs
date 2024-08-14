using AutoMapper;

namespace Blazor.Web;

public class MappingConfig : Profile
{
    public MappingConfig()
    {

        CreateMap<UserSignUpDTO, User>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.createdDate));

        CreateMap<Role, RoleDTO>();
        CreateMap<RoleDTO, Role>()
            .ForMember(ent => ent.Id, opt => opt.Ignore());
    }
}
