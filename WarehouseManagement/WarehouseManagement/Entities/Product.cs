using System.ComponentModel.DataAnnotations;

namespace WarehouseManagement.Entities
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }

        [Required]
        public double Price { get; set; }


        public ICollection<Product_Warehouse> Product_Warehouses { get; set; }
            = new List<Product_Warehouse>();
    }
}
