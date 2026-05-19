using Web.Domain;

namespace Web.Models.Profiles;

public class Profile : AutoMapper.Profile
{
    public Profile()
    {
        CreateMap<Producto, ProductoModel>();
        CreateMap<Producto, EditProductModel>()
            .IncludeBase<Producto, ProductoModel>();

        CreateMap<(Categoria, string), CategoriaModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Item1.Id))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Item1.Nombre))
            .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Item1.Descripcion))
            .ForMember(dest => dest.Activa, opt => opt.MapFrom(src => src.Item1.Activa))
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Item2));

        CreateMap<UpdateProductoModel, Producto>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Categoria, opt => opt.Ignore())
            .ForMember(dest => dest.CarritoProductos, opt => opt.Ignore())
            .ForMember(dest => dest.FechaCreacion, opt => opt.Ignore())
            .ForMember(dest => dest.FechaActualizacion, opt => opt.Ignore())
            .ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}