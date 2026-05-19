using Web.Domain;

namespace Web.Models.Profiles;

public class Profile : AutoMapper.Profile
{
    public Profile()
    {
        CreateMap<Producto, ProductoModel>();
        CreateMap<Categoria, CategoriaModel>()
            .ForMember(dest => dest.Url, opt => opt.Ignore());
        CreateMap<(Categoria, string), CategoriaModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Item1.Id))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Item1.Nombre))
            .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Item1.Descripcion))
            .ForMember(dest => dest.Activa, opt => opt.MapFrom(src => src.Item1.Activa))
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Item2));
    }
}