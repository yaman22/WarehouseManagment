using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using System.Data;
using WarehouseManagement.Auth;
using WarehouseManagement.Models;
using WarehouseManagement.Services;

namespace WarehouseManagement.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IWarehouseManagmentRepository warehouseManagmentRepository;
        private readonly IMapper mapper;

        public ProductsController(IWarehouseManagmentRepository _warehouseManagmentRepository, IMapper _mapper)
        {
            warehouseManagmentRepository = _warehouseManagmentRepository
            ?? throw new ArgumentNullException(nameof(_warehouseManagmentRepository));
            mapper = _mapper
           ?? throw new ArgumentNullException(nameof(_mapper));
        }


        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<ProductDto>> GetProducts()
        {
            var productsFromRepo = warehouseManagmentRepository.GetProducts();

            if (productsFromRepo == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<IEnumerable<Models.ProductDto>>(productsFromRepo));
        }


        [HttpGet("GetById/{productId}", Name = "GetProduct")]
        public IActionResult GetProduct([FromRoute] Guid productId)
        {
            var productFromRepo = warehouseManagmentRepository.GetProducts(productId);

            if (productFromRepo == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<Models.ProductDto>(productFromRepo));
        }


        [Authorize(Roles = UserRoles.Manager)]
        [HttpPost("CreateProduct")]
        public ActionResult<ProductDto> CreatetProduct([FromBody] ProductForManipulationDto product)
        {
            var productEntity = mapper.Map<Entities.Product>(product);
            warehouseManagmentRepository.AddProduct(productEntity);
            warehouseManagmentRepository.Save();

            var productToReturn = mapper.Map<Models.ManagerDto>(productEntity);
            return CreatedAtRoute("GetProduct", new { productId = productToReturn.Id }, productToReturn);
        }


        [Authorize(Roles = UserRoles.Manager)]
        [HttpPut("UpsertProduct/{productId}")]
        public IActionResult UpsertProduct([FromRoute] Guid productId,[FromBody] ProductForManipulationDto product)
        {
            var productFromRepo = warehouseManagmentRepository.GetProduct(productId);

            if(productFromRepo == null)
            {
                var productToAdd=mapper.Map<Entities.Product>(product);
                productToAdd.Id = productId;

                warehouseManagmentRepository.AddProduct(productToAdd);
                warehouseManagmentRepository.Save();

                var productToReturn = mapper.Map<Models.ProductDto>(productToAdd);

                return CreatedAtRoute("GetProduct", new {productId},productToReturn);
            }

            mapper.Map(product, productFromRepo);

            warehouseManagmentRepository.updateProduct(productFromRepo);
            warehouseManagmentRepository.Save();

            return NoContent();
        }


        [Authorize(Roles = UserRoles.Manager)]
        [HttpPatch("PartiallyUpsertProduct/{productId}")]
        public ActionResult PartiallyUpsertProduct([FromRoute] Guid productId,[FromBody] JsonPatchDocument<ProductForManipulationDto> patchDocument)
        {
            var productFromRepo = warehouseManagmentRepository.GetProduct(productId);

            if(productFromRepo == null)
            {
                var productDto = new ProductForManipulationDto();
                patchDocument.ApplyTo(productDto, ModelState);

                if (!TryValidateModel(productDto))
                {
                    return ValidationProblem(ModelState);
                }

                var productToAdd = mapper.Map<Entities.Product>(productDto);
                productToAdd.Id = productId;

                warehouseManagmentRepository.AddProduct(productToAdd);
                warehouseManagmentRepository.Save();

                var productToReturn = mapper.Map<Models.ProductDto>(productToAdd);

                return CreatedAtRoute("GetProduct", new {productId},productToReturn);
            }

            var productToPatch = mapper.Map<Models.ProductForManipulationDto>(productFromRepo);

            patchDocument.ApplyTo(productToPatch, ModelState);

            if (!TryValidateModel(productToPatch))
            {
                return ValidationProblem(ModelState);
            }

            mapper.Map(productToPatch, productFromRepo);

            warehouseManagmentRepository.updateProduct(productFromRepo);
            warehouseManagmentRepository.Save();

            return NoContent();
        }


        [Authorize(Roles = UserRoles.Manager)]
        [HttpDelete("DeleteProduct/{productId}")]
        public ActionResult DeleteProduct([FromRoute] Guid productId)
        {
            var productFromRepo = warehouseManagmentRepository.GetProduct(productId);

            if (productFromRepo == null)
            {
                return NotFound();
            }

            warehouseManagmentRepository.RemoveProduct(productFromRepo);
            warehouseManagmentRepository.Save();

            return NoContent();
        }
        


        [NonAction]
        public override ActionResult ValidationProblem([ActionResultObjectValue] ModelStateDictionary modelStateDictionary)
        {
            var options = HttpContext.RequestServices.GetRequiredService<IOptions<ApiBehaviorOptions>>();
            return (ActionResult)options.Value.InvalidModelStateResponseFactory(ControllerContext);

        }
    }
}
