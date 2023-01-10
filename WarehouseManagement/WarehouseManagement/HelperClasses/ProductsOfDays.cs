namespace WarehouseManagement.HelperClasses
{
    public class ProductsOfDays
    {
        public DateTime day { get; set; }

        public List<ProductQuantityForImportAndExport> productsReport { get; set; } = new List<ProductQuantityForImportAndExport>();

    }
}
