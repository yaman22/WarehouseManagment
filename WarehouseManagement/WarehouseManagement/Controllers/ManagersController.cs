using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Auth;
using WarehouseManagement.Models;
using WarehouseManagement.Services;

namespace WarehouseManagement.Controllers
{
    [ApiController]
    [Route("api/managers")]
    public class ManagersController : ControllerBase
    {
        private readonly IWarehouseManagmentRepository warehouseManagmentRepository;
        private readonly IMapper mapper;
        
        public ManagersController(IWarehouseManagmentRepository _warehouseManagmentRepository, IMapper _mapper)
        {
                warehouseManagmentRepository= _warehouseManagmentRepository 
                ?? throw new ArgumentNullException(nameof(_warehouseManagmentRepository));
                 mapper = _mapper
                ?? throw new ArgumentNullException(nameof(_mapper));
            
        }

        
        [HttpGet]
        public ActionResult<IEnumerable<ManagerDto>> GetManagers()
        {
            var managersFromRepo=warehouseManagmentRepository.GetManagers();
            
            
            if(managersFromRepo == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<IEnumerable<Models.ManagerDto>>(managersFromRepo));
            
        }


        [HttpGet("GetById/{managerId}",Name ="GetManager")]
        public IActionResult GetManager([FromRoute] Guid managerId)
        {
            var managerFromRepo = warehouseManagmentRepository.GetManager(managerId);

            if(managerFromRepo == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<Models.ManagerDto>(managerFromRepo));
        }


        [Authorize(Roles = UserRoles.Manager)]
        [HttpPost("AddManager")]
        public ActionResult<ManagerDto> CreateManager([FromBody] ManagerForManipulationDto manager)
        {
            var managerEntity=mapper.Map<Entities.Manager>(manager);
            warehouseManagmentRepository.AddManager(managerEntity);
            warehouseManagmentRepository.Save();

            var managerToReturn = mapper.Map<Models.ManagerDto>(managerEntity);
            return CreatedAtRoute("GetManager", new { managerId = managerToReturn.Id }, managerToReturn);
        }


        [Authorize(Roles = UserRoles.Manager)]
        [HttpPut("UpsertManager/{managerId}")]
        public IActionResult UpsertManager([FromRoute] Guid managerId, [FromBody] ManagerForManipulationDto manager)
        {
            var managerFromRepo = warehouseManagmentRepository.GetManager(managerId);

            if (managerFromRepo == null)
            {
                var managerToAdd = mapper.Map<Entities.Manager>(manager);
                managerToAdd.Id = managerId;

                warehouseManagmentRepository.AddManager(managerToAdd);
                warehouseManagmentRepository.Save();

                var managerToReturn = mapper.Map<Models.ManagerDto>(managerToAdd);

                return CreatedAtRoute("GetManager", new { managerId }, managerToReturn);
            }

            mapper.Map(manager, managerFromRepo);

            warehouseManagmentRepository.UpdateManager(managerFromRepo);
            warehouseManagmentRepository.Save();

            return NoContent();
        }


        [Authorize(Roles = UserRoles.Manager)]
        [HttpPatch("PartiallyUpsertManager/{managerId}")]
        public ActionResult PartiallyUpsertManager([FromRoute] Guid managerId, [FromBody] JsonPatchDocument<ManagerForManipulationDto> patchDocument)
        {
            var managerFromRepo = warehouseManagmentRepository.GetManager(managerId);

            if (managerFromRepo == null)
            {
                var managerDto = new ManagerForManipulationDto();
                patchDocument.ApplyTo(managerDto, ModelState);

                if (!TryValidateModel(managerDto))
                {
                    return ValidationProblem(ModelState);
                }

                var managerToAdd = mapper.Map<Entities.Manager>(managerDto);
                managerToAdd.Id = managerId;

                warehouseManagmentRepository.AddManager(managerToAdd);
                warehouseManagmentRepository.Save();

                var managerToReturn = mapper.Map<Models.ManagerDto>(managerToAdd);

                return CreatedAtRoute("GetManager", new { managerId }, managerToReturn);
            }

            var managerToPatch = mapper.Map<Models.ManagerForManipulationDto>(managerFromRepo);

            patchDocument.ApplyTo(managerToPatch, ModelState);

            if (!TryValidateModel(managerToPatch))
            {
                return ValidationProblem(ModelState);
            }

            mapper.Map(managerToPatch, managerFromRepo);

            warehouseManagmentRepository.UpdateManager(managerFromRepo);
            warehouseManagmentRepository.Save();

            return NoContent();
        }


        [Authorize(Roles = UserRoles.Manager)]
        [HttpDelete("RemoveManager/{managerId}")]
        public ActionResult DeleteManager([FromRoute] Guid managerId)
        {
            var managerFromRepo = warehouseManagmentRepository.GetManager(managerId);

            if(managerFromRepo == null)
            {
                return NotFound();
            }

            warehouseManagmentRepository.RemoveManager(managerFromRepo);
            warehouseManagmentRepository.Save();

            return NoContent();
        }

        
    }
}
