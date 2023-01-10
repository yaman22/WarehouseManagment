using System.ComponentModel.DataAnnotations;

namespace WarehouseManagement.Entities
{
    public class Product_Bill
    {
        
        public Guid Id { get; set; }
        public double Amount { get; set; }
        public double Cost { get; set; }
        public DateTime Date { get; set; }
        public OperationType Type { get; set; }

        public BillDetails BillDetail { get; set; }
        public Guid BillDetailsId { get; set; }

        public Product_Warehouse Product_Warehouse { get; set; }
        public Guid Product_Warehouse_Id { get; set; }


    }
   
}
