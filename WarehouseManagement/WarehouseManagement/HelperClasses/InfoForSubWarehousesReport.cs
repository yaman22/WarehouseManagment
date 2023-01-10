namespace WarehouseManagement.HelperClasses
{
    public class InfoForSubWarehousesReport
    {
        public Guid warehouseId { get; set; }
        public string Name { get; set; }

        public List<ProductsOfDays> productsReport { get; set; } = new List<ProductsOfDays>();
    }
}
