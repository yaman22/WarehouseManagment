using AutoMapper;

namespace WarehouseManagement.Profiles
{
    public class ProductsProfile : Profile
    {
        public ProductsProfile()
        {
            CreateMap<Entities.Product, Models.ProductDto>();

            CreateMap<Models.ProductForManipulationDto, Entities.Product>();

            CreateMap<Models.ProductForManipulationDto, Models.ProductDto>();

            CreateMap<Entities.Product, Models.ProductForManipulationDto>();
        }
    }
}
