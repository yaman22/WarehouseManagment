using System.ComponentModel.DataAnnotations;

namespace WarehouseManagement.Auth
{
    public class TokenRequestModel
    {

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
