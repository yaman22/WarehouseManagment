using System.ComponentModel.DataAnnotations;

namespace WarehouseManagement.Entities
{
    public class Manager
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }

        public ICollection<Warehouse> Warehouses { get; set; }
            = new List<Warehouse>();
    }
}
