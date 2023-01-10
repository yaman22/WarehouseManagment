using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WarehouseManagement.DbContexts;
using WarehouseManagement.Entities;
using WarehouseManagement.HelperClasses;

namespace WarehouseManagement.Services
{
    public class WarehouseManagmentRepository : IWarehouseManagmentRepository
    {
        private readonly WarehouseManagmentContext context;
        public WarehouseManagmentRepository(WarehouseManagmentContext _context)
        {
            context= _context ?? throw new ArgumentNullException(nameof(_context));
        }
        

        public Manager GetManager(Guid managerId)
        {
            if (managerId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(managerId));
            }
            

            return context.Managers.FirstOrDefault(m => m.Id == managerId);
        }
        public IEnumerable<Manager> GetManagers()
        {
            return context.Managers.ToList();
        }
        public bool ManagerExists(Guid managerId)
        {
            if (managerId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(managerId));
            }

            return context.Managers.Any(m => m.Id == managerId);
        }
        public void AddManager(Manager manager)
        {
            if (manager == null) throw new ArgumentNullException(nameof(manager));

            manager.Id = Guid.NewGuid();

            foreach (var warehouse in manager.Warehouses)
            {
                warehouse.Id = Guid.NewGuid();
            }

            context.Managers.Add(manager);
        }
        public void UpdateManager(Manager manager)
        {
            //
        }
        public void RemoveManager(Manager manager)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }

            context.Managers.Remove(manager);
        }


        public IEnumerable<Product> GetProducts()
        {
            return context.Products.ToList();
        }
        public Product GetProduct(Guid productId)
        {
            if (productId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(productId));
            }

            return context.Products.FirstOrDefault(p => p.Id == productId);
        }
        public IEnumerable<Product> GetProducts(Guid warehouseId)
        {
            throw new NotImplementedException();
        }
        public bool ProductExists(Guid productId)
        {
            if (productId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(productId));
            }

            return context.Products.Any(p => p.Id == productId);
        }
        public void AddProduct(Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));

            product.Id = Guid.NewGuid();

            foreach(var productWithWarehouse in product.Product_Warehouses)
            {
                productWithWarehouse.Id = Guid.NewGuid();
            }

            context.Products.Add(product);
        }
        public void updateProduct(Product product)
        {
            //
        }
        public void RemoveProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            context.Products.Remove(product);
        }
        
        
        public IEnumerable<Warehouse> GetWarehouses()
        {
            return context.Warehouses.ToList();
        }
        public IEnumerable<Warehouse> GetWarehouses(Guid managerId)
        {
            if (managerId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(managerId));
            }

            return context.Warehouses.Where(c=>c.ManagerId == managerId).ToList();
        }
        public IEnumerable<Warehouse> GetSubWarehouses(Guid mainWarehouseId, Guid managerId)
        {
            if (managerId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(managerId));
            }

            if (mainWarehouseId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(mainWarehouseId));
            }

            return context.Warehouses.Where(w=>w.ManagerId==managerId && w.MainWarehouseId==mainWarehouseId).ToList();
        }
        public IEnumerable<Warehouse> GetSubWarehouses()
        {
            return context.Warehouses.Where(w=>w.MainWarehouseId !=Guid.Empty).ToList();
        }
        public Warehouse GetWarehouse(Guid warehouseId, Guid managerId)
        {
            if (managerId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(managerId));
            }

            if (warehouseId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(warehouseId));
            }

            return context.Warehouses.Where(w => w.ManagerId == managerId && w.Id == warehouseId).FirstOrDefault();

        }
        public Warehouse GetWarehouse(Guid warehouseId)
        {
            if (warehouseId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(warehouseId));
            }

            return context.Warehouses.Where(w=> w.Id == warehouseId).FirstOrDefault();
        }
        public void AddMainWarehouse(Warehouse warehouse, Guid managerId)
        {
            if (managerId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(managerId));
            }

            if (warehouse == null)
            {
                throw new ArgumentNullException(nameof(warehouse));
            }

            warehouse.ManagerId = managerId;

            warehouse.Id = Guid.NewGuid();

            foreach (var productWithWarehouse in warehouse.Product_Warehouses)
            {
                productWithWarehouse.Id = Guid.NewGuid();
            }

            context.Warehouses.Add(warehouse);
        }
        public void AddSubWarehouse(Warehouse mainWarehouse, Warehouse subWarehouse)
        {
            if(mainWarehouse==null)
            {
                throw new ArgumentNullException(nameof(mainWarehouse));
            }

            if (subWarehouse == null)
            {
                throw new ArgumentNullException(nameof(subWarehouse));
            }

            if (subWarehouse.MainWarehouseId == mainWarehouse.Id)
            {
                throw new Exception("Already a subWarehouse of the mainWarehouse you want to add it to.");
            }

            subWarehouse.MainWarehouseId = mainWarehouse.Id;

            subWarehouse.ManagerId = mainWarehouse.ManagerId;

            subWarehouse.Id = Guid.NewGuid();

            foreach (var productWithWarehouse in subWarehouse.Product_Warehouses)
            {
                productWithWarehouse.Id = Guid.NewGuid();
            }

            context.Warehouses.Add(subWarehouse);
        }
        public void UpdateWarehouse(Warehouse warehouse)
        {
            //
        }
        public void RemoveWarehouse(Warehouse warehouse)
        {
            if (warehouse == null)
            {
                throw new ArgumentNullException(nameof(warehouse));
            }

            context.Warehouses.Remove(warehouse);
        }
        public bool WarehouseExists(Guid warehouseId)
        {
            if (warehouseId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(warehouseId));
            }

            return context.Warehouses.Any(w=>w.Id == warehouseId);
        }


        public void AddProductToWarehouse(Product_Warehouse ProductWithWarehouse)
        {
            if(ProductWithWarehouse== null)
            {
                throw new ArgumentNullException(nameof(ProductWithWarehouse));
            }
            
            foreach(var productBill in ProductWithWarehouse.Product_Bills)
            {
                productBill.Id = Guid.NewGuid();
            }

            context.Products_Warehouses.Add(ProductWithWarehouse);
        }
        public Product_Warehouse GetProductWithWarehouse(Guid productId, Guid warehouseId)
        {
            if (productId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(productId));
            }

            if (warehouseId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(warehouseId));
            }

            return context.Products_Warehouses.FirstOrDefault(pw=>pw.ProductId==productId && pw.WarehouseId==warehouseId);
        }
        public IEnumerable<Product_Warehouse> GetProductWithWarehouses()
        {
            return context.Products_Warehouses.ToList();
        }
        public void UpdateAmountOfProductInWarehouse(Guid productId, Guid warehouseId, double newAmount)
        {
            //
        }
        public bool ProductWithWarehouseExists(Guid productId, Guid warehouseId)
        {
            if (productId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(productId));
            }

            if (warehouseId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(warehouseId));
            }

            return context.Products_Warehouses.Any(pw => pw.ProductId == productId && pw.WarehouseId == warehouseId);
        }


        public void AddBillDetails(BillDetails bill)
        {
            if (bill == null)
            {
                throw new ArgumentNullException(nameof(bill));
            }

            foreach(var productBill in bill.Product_Bills)
            {
                productBill.Id = Guid.NewGuid();
            }

            context.BillDetails.Add(bill);
        }
        public BillDetails GetBillDetails(Guid billDetailsId)
        {
            if (billDetailsId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(billDetailsId));
            }

            return context.BillDetails.FirstOrDefault(bd=>bd.Id==billDetailsId);
        }
        public IEnumerable<BillDetails> GetAllImportBillDetails()
        {
            return context.BillDetails.Where(bd=>bd.type==OperationType.Import).ToList();
        }
        public IEnumerable<BillDetails> GetAllExportBillDetails()
        {
            return context.BillDetails.Where(bd => bd.type == OperationType.Export).ToList();
        }


        public void AddBillOfProducts(Product_Bill billOfProduct)
        {
            if (billOfProduct == null)
            {
                throw new ArgumentNullException(nameof(billOfProduct));
            }

            billOfProduct.Id= Guid.NewGuid();

            context.Products_Bills.Add(billOfProduct);
        }
        public IEnumerable<Product_Bill> GetProduct_Bills()
        {
            return context.Products_Bills.ToList();
        }

        

        public bool Save()
        {
            return (context.SaveChanges() >= 0);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose resources when needed
            }
        }

    }
}
