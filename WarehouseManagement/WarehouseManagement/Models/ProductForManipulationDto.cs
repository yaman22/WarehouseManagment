using System.ComponentModel.DataAnnotations;
using WarehouseManagement.Entities;

namespace WarehouseManagement.Models
{
    public class ProductForManipulationDto
    {
        [Required(ErrorMessage ="Name is required.")]
        [MaxLength(50,ErrorMessage ="Max length of name is 50.")]
        public string? Name { get; set; }

        [Required(ErrorMessage ="Price is required.")]
        public double Price { get; set; }

    }
}
