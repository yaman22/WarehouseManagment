using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WarehouseManagement.Auth;
using WarehouseManagement.Models;
using WarehouseManagement.Services;

namespace WarehouseManagement.Controllers
{
    [ApiController]
    [Route("api/warehouses")]
    public class WarehousesController : ControllerBase
    {
        private readonly IWarehouseManagmentRepository warehouseManagmentRepository;
        private readonly IMapper mapper;
        
        public WarehousesController(IWarehouseManagmentRepository _warehouseManagmentRepository, IMapper _mapper)
        {
            warehouseManagmentRepository = _warehouseManagmentRepository
            ?? throw new ArgumentNullException(nameof(_warehouseManagmentRepository));
            mapper = _mapper
           ?? throw new ArgumentNullException(nameof(_mapper));
        }


        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<WarehouseDto>> GetAllWarehouses()
        {
            var warehousesFromRepo = warehouseManagmentRepository.GetWarehouses();

            if (warehousesFromRepo == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<IEnumerable<Models.WarehouseDto>>(warehousesFromRepo));
        }


        [HttpGet("{managerId}/GetAll")]
        public ActionResult<IEnumerable<WarehouseDto>> GetWarehousesForManager(Guid managerId)
        {
            if (!warehouseManagmentRepository.ManagerExists(managerId))
            {
                return NotFound();
            }

            var warehousesForManagerFromRepo = warehouseManagmentRepository.GetWarehouses(managerId);

            return Ok(mapper.Map<IEnumerable<Models.WarehouseDto>>(warehousesForManagerFromRepo));
        }


        [HttpGet("{warehouseId}/GetWarehouse", Name = "GetWarehouse")]
        public IActionResult GetWarehouse(Guid warehouseId)
        {
            var warehouseFromRepo = warehouseManagmentRepository.GetWarehouse(warehouseId);

            if(warehouseFromRepo==null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<Models.WarehouseDto>(warehouseFromRepo));
        } 


        [HttpGet("{mainWarehouseId}/GetSubWarehouses")]
        public ActionResult<IEnumerable<WarehouseDto>> GetSubWarehouses(Guid mainWarehouseId)
        {
            var mainWarehouseFromRepo = warehouseManagmentRepository.GetWarehouses(mainWarehouseId);

            if (mainWarehouseFromRepo == null)
            {
                return NotFound();
            }

            var subWarehousesFromRepo = warehouseManagmentRepository.GetWarehouses().Where(sw => sw.MainWarehouseId == mainWarehouseId);

            return Ok(mapper.Map<IEnumerable<Models.ManagerDto>>(subWarehousesFromRepo));
        }


        [Authorize(Roles = UserRoles.Manager)]
        [HttpPost("{managerId}/CreateMainWarehouse")]
        public ActionResult<WarehouseDto> CreateMainWarehouse(Guid managerId,WarehouseForManipulationDto warehouse)
        {
            if(!warehouseManagmentRepository.ManagerExists(managerId))
            {
                return BadRequest();
            }


            var warehouseEntity = mapper.Map<Entities.Warehouse>(warehouse);

            warehouseEntity.ManagerId= managerId;

            warehouseEntity.MainWarehouseId = Guid.Empty;

            warehouseManagmentRepository.AddMainWarehouse(warehouseEntity, managerId);
            warehouseManagmentRepository.Save();

            var warehouseToReturn = mapper.Map<Models.WarehouseDto>(warehouseEntity);

            return CreatedAtRoute("GetWarehouse", new { warehouseId=warehouseToReturn.Id },warehouseToReturn);
        }


        [Authorize(Roles = UserRoles.Manager)]
        [HttpPost("{mainWarehouseId}/CreateSubWarehouse")]
        public ActionResult<WarehouseDto> CreateSubWarehouse(Guid mainWarehouseId, WarehouseForManipulationDto warehouse)
        {
            var mainWarehouseFromRepo = warehouseManagmentRepository.GetWarehouse(mainWarehouseId);

            if (mainWarehouseFromRepo==null)
            {
                return BadRequest();
            }

            var subWarehouseEntity=mapper.Map<Entities.Warehouse>(warehouse);

            warehouseManagmentRepository.AddSubWarehouse(mainWarehouseFromRepo,subWarehouseEntity);
            warehouseManagmentRepository.Save();

            var warehouseToReturn = mapper.Map<Models.WarehouseDto>(subWarehouseEntity);

            return CreatedAtRoute("GetWarehouse", new { warehouseId = warehouseToReturn.Id }, warehouseToReturn);
        }


        [Authorize(Roles = UserRoles.Manager)]
        [HttpPatch("{warehouseId}/{managerId}")]
        public ActionResult UpserteWarehouse(Guid warehouseId,Guid managerId,JsonPatchDocument<WarehouseForManipulationDto> patchDocument)
        {
            if (!warehouseManagmentRepository.ManagerExists(managerId))
            {
                return NotFound();
            }

            var warehouseFromRepo = warehouseManagmentRepository.GetWarehouse(warehouseId, managerId);

            if(warehouseFromRepo==null)
            {
                var warehouseDto=new WarehouseDto();
                patchDocument.ApplyTo(warehouseDto,ModelState);

                if (!TryValidateModel(warehouseDto))
                {
                    return ValidationProblem(ModelState);
                }

                var warehouseToAdd = mapper.Map<Entities.Warehouse>(warehouseDto);
                warehouseToAdd.Id= warehouseId;

                warehouseManagmentRepository.AddMainWarehouse(warehouseToAdd, managerId);
                warehouseManagmentRepository.Save();

                var warehouseToReturn = mapper.Map<Models.WarehouseDto>(warehouseToAdd);

                return CreatedAtRoute("GetWarehouse", new { warehouseId }, warehouseToReturn);
            }

            var warehouseToPatch=mapper.Map<Models.WarehouseForManipulationDto>(warehouseFromRepo); 
            patchDocument.ApplyTo(warehouseToPatch,ModelState);

            if(!TryValidateModel(warehouseToPatch))
            { 
                return ValidationProblem(ModelState);
            }

            mapper.Map(warehouseToPatch, warehouseFromRepo);

            warehouseManagmentRepository.UpdateWarehouse(warehouseFromRepo);
            warehouseManagmentRepository.Save();

            return NoContent();
        }


        [Authorize(Roles = UserRoles.Manager)]
        [HttpDelete("{warehouseId}/DeleteWarehouse")]
        public ActionResult DeleteWarehouse(Guid warehouseId)
        {
            var warehouseFromRepo = warehouseManagmentRepository.GetWarehouse(warehouseId);
            
            if (warehouseFromRepo == null)
            {
                return NotFound();
            }

            warehouseManagmentRepository.RemoveWarehouse(warehouseFromRepo);
            warehouseManagmentRepository.Save();

            return NoContent();
        }


    }
}
