using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WarehouseManagement.Entities;

namespace WarehouseManagement.Models
{
    public class WarehouseForManipulationDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(50, ErrorMessage = "Max length of name is 50.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [MaxLength(100, ErrorMessage = "Max length of name is 100.")]
        public string? Address { get; set; }

        public Guid ManagerId { get; set; }

        public Guid? MainWarehouseId { get; set; }

    }
}
