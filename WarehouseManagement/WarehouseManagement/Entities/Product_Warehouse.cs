using System.ComponentModel.DataAnnotations;

namespace WarehouseManagement.Entities
{
    public class Product_Warehouse
    {
        
        public Guid Id { get; set; }
        public double Amount { get; set; }

        public Warehouse Warehouse { get; set; }
        public Guid WarehouseId { get; set; }

        public Product Product { get; set; }
        public Guid ProductId { get; set; }


        public ICollection<Product_Bill> Product_Bills { get; set; }
            = new List<Product_Bill>();
    }
}
