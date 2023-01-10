using AutoMapper;

namespace WarehouseManagement.Profiles
{
    public class WarehousesProfiles : Profile
    {
        public WarehousesProfiles()
        {
            CreateMap<Entities.Warehouse, Models.WarehouseDto>();

            CreateMap<Models.WarehouseForManipulationDto, Entities.Warehouse>();

            CreateMap<Models.WarehouseForManipulationDto, Models.WarehouseDto>();

            CreateMap<Entities.Warehouse, Models.WarehouseForManipulationDto>();
        }

            
    }
}
