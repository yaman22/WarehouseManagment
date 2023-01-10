using AutoMapper;

namespace WarehouseManagement.Profiels
{
    public class ManagersProfile : Profile
    {
        public ManagersProfile() 
        {
            CreateMap<Entities.Manager,Models.ManagerDto>();

            CreateMap<Models.ManagerForManipulationDto, Entities.Manager>();

            CreateMap<Models.ManagerForManipulationDto, Models.ManagerDto>();

            CreateMap<Entities.Manager, Models.ManagerForManipulationDto>();
        }

    }
}
