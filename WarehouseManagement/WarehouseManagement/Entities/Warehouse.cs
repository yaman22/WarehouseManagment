using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseManagement.Entities
{
    public class Warehouse
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Address { get; set; }

        [ForeignKey("ManagerId")]
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public Manager Manager { get; set; }
        public Guid ManagerId { get; set; }

        [ForeignKey("MainWarehouseId")]
        public Warehouse? MainWarehouse { get; set; }
        public Guid? MainWarehouseId { get; set; }


        public ICollection<Product_Warehouse> Product_Warehouses { get; set; }
            = new List<Product_Warehouse>();

    }
}
