using System.ComponentModel.DataAnnotations;
using WarehouseManagement.Entities;

namespace WarehouseManagement.Models
{
    public class ManagerForManipulationDto
    {
        [Required(ErrorMessage ="Name is required.")]
        [MaxLength(50,ErrorMessage ="Max length for name is 50.")]
        public string? Name { get; set; }
       

    }
}
