using AutoMapper;

namespace WarehouseManagement.Profiles
{
    public class BillDetailsProfile :Profile
    {
        public BillDetailsProfile()
        {
            CreateMap<Entities.BillDetails, Models.BillDetailsDto>();

            CreateMap<Models.BillDetailsDto, Entities.BillDetails>();
        }
    }
}
