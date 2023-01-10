using System.ComponentModel.DataAnnotations;

namespace WarehouseManagement.Auth
{
    public class RoleModel
    {
        [Required]
        public string? userId { get; set; }

        [Required]
        public string? Role { get; set; }
    }
}
