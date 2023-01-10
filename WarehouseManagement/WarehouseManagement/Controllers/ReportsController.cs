using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections;
using System.Data;
using System.Linq;
using WarehouseManagement.Auth;
using WarehouseManagement.Entities;
using WarehouseManagement.HelperClasses;
using WarehouseManagement.Services;

namespace WarehouseManagement.Controllers
{
    [Authorize(Roles = UserRoles.Manager)]
    [ApiController]
    [Route("api/reports")]
    public class ReportsController : ControllerBase
    {
        private readonly IWarehouseManagmentRepository warehouseManagmentRepository;
        private readonly IMapper mapper;

        public ReportsController(IWarehouseManagmentRepository _warehouseManagmentRepository, IMapper _mapper)
        {
            warehouseManagmentRepository = _warehouseManagmentRepository
            ?? throw new ArgumentNullException(nameof(_warehouseManagmentRepository));
            mapper = _mapper
           ?? throw new ArgumentNullException(nameof(_mapper));
        }

        //[HttpGet("GetSubWarehousesReport")]
        //public ActionResult<IEnumerable<InfoForSubWarehousesReport>> GetSubWarehousesReport()
        //{
        //    var subWarehousesId = warehouseManagmentRepository.GetSubWarehouses().Where(mw => mw.MainWarehouseId is not null)
        //        .Select(s => s.Id) as List<Guid>;

        //    var productsWarehouses = warehouseManagmentRepository.GetProductWithWarehouses().Where(ph => subWarehousesId.Contains(ph.WarehouseId));

        //    var productsWarehousesId = productsWarehouses.Select(i => i.Id) as List<Guid>;

        //    var productBill = warehouseManagmentRepository.GetProduct_Bills().Where(pb => (productsWarehousesId.Contains(pb.Product_Warehouse_Id))
        //         && pb.Type == OperationType.Import && pb.Date.Month.Equals(DateTime.Now.Month));



        //    List<InfoForSubWarehousesReport> infos = new List<InfoForSubWarehousesReport>();

        //    double sumQuantityOfDay = 0.0d;

        //    foreach(var day in productBill.Select(pb => pb.Date))
        //    {
        //        var productsBillOfDay = productBill.Where(p=>p.Date.Day == day.Day);

        //        var productsBillOfDayGroupedByProductWarehouseId = productsBillOfDay.GroupBy(d => d.Product_Warehouse_Id);

        //        //.Select(s => new { productWarehouseId = s.Select(e => e.Product_Warehouse_Id), Amount = s.Sum(g => g.Amount) })

        //        var list = new List<ProductWarehouseAmount>();

        //        foreach (var item in productsBillOfDayGroupedByProductWarehouseId)
        //        {
        //            list.Add(new ProductWarehouseAmount { ProductWarehouseId = item.Select(e => e.Product_Warehouse_Id), Amount = item.Sum(g => g.Amount) });
        //        }



        //            foreach (var productBillOfDay in productsBillOfDay)
        //        {

        //            sumQuantityOfDay = productBillOfDay.Amount;
        //            var Warehouse = warehouseManagmentRepository.GetSubWarehouses().FirstOrDefault(sw => sw.Id ==
        //            warehouseManagmentRepository.GetProductWithWarehouses().FirstOrDefault(pw => pw.Id == productBillOfDay.Id).WarehouseId);

        //            List<ProductsOfDays> productsOfDays= new List<ProductsOfDays>();

        //            List<ProductQuantityForImportAndExport> productQuantities= new List<ProductQuantityForImportAndExport>();

        //            //productQuantities.Add(new ProductQuantityForImportAndExport()
        //            //{
        //            //    productName = warehouseManagmentRepository.GetProducts().FirstOrDefault(p => p.Id ==
        //            //    warehouseManagmentRepository.GetProductWithWarehouses().FirstOrDefault(pw => pw.Id == productBillOfDay.Id).ProductId).Name,

        //            //    //TotalQuantityOfImport = list
        //            //    //.FirstOrDefault(s => s.productWarehouseId == productBillOfDay.Product_Warehouse_Id).Amount;

        //            //productsOfDays.Add(new ProductsOfDays { day = day, productsReport=productQuantities });

        //            //infos.Add(new InfoForSubWarehousesReport { Name = Warehouse.Name, warehouseId = Warehouse.Id, productsReport = productsOfDays });

        //        }

        //    }
        //return Ok(infos);

        //}

    }
}
