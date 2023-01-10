using System.ComponentModel.DataAnnotations;

namespace WarehouseManagement.Entities
{
    public class BillDetails
    {
        [Key]
        public Guid Id { get; set; }
        public OperationType type { get; set; }
        public DateTime Date { get; set; }
        public double TotalCost { get; set; }
        public Guid managerId { get; set; }
        public Guid warehouseId { get; set; }
        public ICollection<Product_Bill> Product_Bills { get; set; }
            = new List<Product_Bill>();
    }
    public enum OperationType
    {
        Export,
        Import
    }
}
