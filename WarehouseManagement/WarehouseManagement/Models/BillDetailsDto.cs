using WarehouseManagement.Entities;

namespace WarehouseManagement.Models
{
    public class BillDetailsDto
    {
        public Guid Id { get; set; }
        public OperationType type { get; set; }
        public DateTime Date { get; set; }
        public Guid managerId { get; set; }
        public Guid warehouseId { get; set; }
       
    }
}
