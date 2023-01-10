using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Entities;
using WarehouseManagement.HelperClasses;
using WarehouseManagement.Models;
using WarehouseManagement.Services;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using WarehouseManagement.Auth;

namespace WarehouseManagement.Controllers
{
    [Authorize(Roles = UserRoles.Manager)]
    [Route("api/operation")]
    [ApiController]
    public class OperationController:ControllerBase
    {
        private readonly IWarehouseManagmentRepository warehouseManagmentRepository;
        private readonly IMapper mapper;

        public OperationController(IWarehouseManagmentRepository _warehouseManagmentRepository, IMapper _mapper)
        {
            warehouseManagmentRepository = _warehouseManagmentRepository
            ?? throw new ArgumentNullException(nameof(_warehouseManagmentRepository));
            mapper = _mapper
           ?? throw new ArgumentNullException(nameof(_mapper));
        }


        [HttpGet("GetBillDetailsById/{billDetailsId}",Name = "GetBillDetails")]
        public ActionResult<BillDetailsDto> GetBillDetails(Guid billDetailsId)
        {
            var billDetailsFromRepo = warehouseManagmentRepository.GetBillDetails(billDetailsId);

            if (billDetailsFromRepo == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<Models.BillDetailsDto>(billDetailsFromRepo));
        }


        [Route("Import/{managerId}/{warehouseId}")]
        [HttpPost]
        public ActionResult<BillDetailsDto> Import(Guid managerId,Guid warehouseId,[FromBody] IEnumerable<Product_Amount> productsWithAmount)
        {
            if(!warehouseManagmentRepository.ManagerExists(managerId))
            {
                return NotFound();
            }

            var warehouseFromRepo = warehouseManagmentRepository.GetWarehouse(warehouseId, managerId);

            if (warehouseFromRepo == null)
            {
                return NotFound();
            }

            var billDetails = new BillDetails
            {
                Id =Guid.NewGuid(),
                type = OperationType.Import,
                Date = DateTime.Now,
                managerId= managerId,
                warehouseId= warehouseId
            };

            double TotalCost=0;

            foreach(var productAmount in productsWithAmount)
            {
                double cost = 0;

                var ProductWithWarehouse = new Product_Warehouse();

                if (warehouseManagmentRepository.ProductWithWarehouseExists(productAmount.productId,warehouseId))
                {
                    var existedProductWithWarehouse = warehouseManagmentRepository.GetProductWithWarehouse(productAmount.productId, warehouseId);

                    existedProductWithWarehouse.Amount += productAmount.amount;

                    warehouseManagmentRepository.UpdateAmountOfProductInWarehouse(productAmount.productId,warehouseId,productAmount.amount);

                    ProductWithWarehouse = existedProductWithWarehouse;

                }
                else
                {
                    var newProductWithWarehouse = new Product_Warehouse()
                    {
                        Id = Guid.NewGuid(),
                        ProductId = productAmount.productId,
                        WarehouseId = warehouseId,
                        Amount = productAmount.amount
                    };

                    warehouseManagmentRepository.AddProductToWarehouse(newProductWithWarehouse);

                    ProductWithWarehouse = newProductWithWarehouse;

                }

                var productFromRepo= warehouseManagmentRepository.GetProduct(productAmount.productId);
                cost += productFromRepo.Price*productAmount.amount;
                TotalCost+=cost;

                var productBill = new Product_Bill()
                {
                    BillDetailsId = billDetails.Id,
                    Product_Warehouse_Id = ProductWithWarehouse.Id,
                    Amount = productAmount.amount,
                    Cost = cost,
                    Date = billDetails.Date,
                    Type = OperationType.Import
                };

                productBill.Product_Warehouse = ProductWithWarehouse;

                productBill.BillDetail = billDetails;

                billDetails.Product_Bills.Add(productBill);

                ProductWithWarehouse.Product_Bills.Add(productBill);

                warehouseManagmentRepository.AddBillOfProducts(productBill);
            }

            billDetails.TotalCost = TotalCost;

            warehouseManagmentRepository.AddBillDetails(billDetails);

            warehouseManagmentRepository.Save();

            var billDetailsToReturn = mapper.Map<Models.BillDetailsDto>(billDetails);

            return CreatedAtRoute("GetBillDetails", new { billDetailsId = billDetailsToReturn.Id }, billDetailsToReturn);

        }


        [Route("Export/{managerId}/{warehouseId}")]
        [HttpPost]
        public ActionResult<BillDetailsDto> Export(Guid managerId, Guid warehouseId,[FromBody] IEnumerable<Product_Amount> productsWithAmount)
        {
            if(!warehouseManagmentRepository.ManagerExists(managerId))
            {
                return NotFound();
            }

            var warehouseFromRepo = warehouseManagmentRepository.GetWarehouse(warehouseId, managerId);

            if (warehouseFromRepo == null)
            {
                return NotFound();
            }

            var billDetails = new BillDetails
            {
                Id = Guid.NewGuid(),
                type = OperationType.Export,
                Date = DateTime.Now,
                managerId= managerId,
                warehouseId= warehouseId
            };

            double TotalCost = 0;

            foreach(var productAmount in productsWithAmount)
            {
                double cost = 0;

                var ProductWithWarehouse = new Product_Warehouse();

                if (warehouseManagmentRepository.ProductWithWarehouseExists(productAmount.productId, warehouseId) 
                    && warehouseManagmentRepository.ProductExists(productAmount.productId))
                {
                    var existedProductWithWarehouse = warehouseManagmentRepository.GetProductWithWarehouse(productAmount.productId, warehouseId);

                    existedProductWithWarehouse.Amount -= productAmount.amount;

                    warehouseManagmentRepository.UpdateAmountOfProductInWarehouse(productAmount.productId, warehouseId, productAmount.amount);

                    ProductWithWarehouse = existedProductWithWarehouse;
                }
                else
                {
                    return NotFound();
                }

                var productFromRepo = warehouseManagmentRepository.GetProduct(productAmount.productId);
                cost += productFromRepo.Price * productAmount.amount;
                TotalCost += cost;

                var productBill = new Product_Bill()
                {
                    BillDetailsId = billDetails.Id,
                    Product_Warehouse_Id = ProductWithWarehouse.Id,
                    Amount = productAmount.amount,
                    Cost = cost,
                    Date = billDetails.Date
                };

                warehouseManagmentRepository.AddBillOfProducts(productBill);

                billDetails.Product_Bills.Add(productBill);
                ProductWithWarehouse.Product_Bills.Add(productBill);

                productBill.Product_Warehouse = ProductWithWarehouse;
                productBill.BillDetail = billDetails;
            }

            billDetails.TotalCost = TotalCost;

            warehouseManagmentRepository.AddBillDetails(billDetails);

            warehouseManagmentRepository.Save();

            var billDetailsToReturn = mapper.Map<Models.BillDetailsDto>(billDetails);

            return CreatedAtRoute("GetBillDetails", new { billDetailsId = billDetailsToReturn.Id }, billDetailsToReturn);
        }
    }
}
