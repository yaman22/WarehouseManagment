using AutoMapper;
using WarehouseManagement.Auth;

namespace WarehouseManagement.Profiles
{
    public class ApplicationUserProfile :Profile
    {
        public ApplicationUserProfile()
        {
            CreateMap<RegisterModel, ApplicationUser>();
        }
    }
}
