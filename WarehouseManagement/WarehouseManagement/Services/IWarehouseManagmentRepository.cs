using WarehouseManagement.Entities;
using WarehouseManagement.HelperClasses;

namespace WarehouseManagement.Services
{
    public interface IWarehouseManagmentRepository
    {
        IEnumerable<Manager> GetManagers();
        Manager GetManager(Guid managerId);
        void AddManager(Manager manager);
        void RemoveManager(Manager manager);
        void UpdateManager(Manager manager);
        bool ManagerExists(Guid managerId);

        IEnumerable<Warehouse> GetWarehouses();
        IEnumerable<Warehouse> GetWarehouses(Guid managerId);
        IEnumerable<Warehouse> GetSubWarehouses(Guid mainWarehouseId, Guid managerId);
        IEnumerable<Warehouse> GetSubWarehouses();
        Warehouse GetWarehouse(Guid warehouseId,Guid manageId);
        Warehouse GetWarehouse(Guid warehouseId);
        void AddMainWarehouse(Warehouse warehouse,Guid managerId);
        void AddSubWarehouse(Warehouse mainWarehouse, Warehouse subWarehouse);
        void RemoveWarehouse(Warehouse warehouse);
        void UpdateWarehouse(Warehouse warehouse);
        bool WarehouseExists(Guid warehouseId);

        IEnumerable<Product> GetProducts();
        IEnumerable<Product> GetProducts(Guid warehouseId);
        Product GetProduct(Guid productId);
        // Guid GetProductByProductWarehouseId(Guid ProductWarehouseIde);
        void AddProduct(Product product);
        void updateProduct(Product product);
        void RemoveProduct(Product product);
        bool ProductExists(Guid productId);


        void AddProductToWarehouse(Product_Warehouse ProductWithWarehouse);
        Product_Warehouse GetProductWithWarehouse(Guid productId, Guid warehouseId);
        IEnumerable<Product_Warehouse> GetProductWithWarehouses();
        void UpdateAmountOfProductInWarehouse(Guid productId, Guid warehouseId, double newAmount);
        bool ProductWithWarehouseExists(Guid productId, Guid warehouseId);


        void AddBillDetails(BillDetails bill);
        BillDetails GetBillDetails(Guid billDetailsId);
        IEnumerable<BillDetails> GetAllImportBillDetails();
        IEnumerable<BillDetails> GetAllExportBillDetails();


        void AddBillOfProducts(Product_Bill billOfProduct);
        IEnumerable<Product_Bill> GetProduct_Bills();


        // IEnumerable<InfoForSubWarehousesReport> GetSubWarehousesReport();





















        bool Save();

    }
}
