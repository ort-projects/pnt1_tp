using Web.Domain;

namespace Web.Models.Profiles;

public class Profile : AutoMapper.Profile
{
    public Profile()
    {
        CreateMap<Producto, ProductoModel>();
    }
}