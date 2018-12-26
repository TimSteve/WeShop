using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WeShop.Domain.Commands.Catalog.ProductBrandCommands;
using WeShop.Domain.Commands.Catalog.ProductCommands;
using WeShop.Domain.Commands.Catalog.ProductTypeCommands;
using WeShop.Infrasture.Common;
using WeShop.Queries;
using WeShop.Queries.Dtos;

namespace WeShop.WebApi.Controllers
{
    /// <summary>
    /// 产品管理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductQueries _productQueries;
        private readonly IMediator _mediator;

        public ProductController(
            IProductQueries productQueries,
            IMediator mediator)
        {
            _productQueries = productQueries;
            _mediator = mediator;
        }

        /// <summary>
        /// 获取产品
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ProductDto), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(List<ProductDto>), (int) HttpStatusCode.OK)]
        public IActionResult Get([FromQuery] int pageIndex, [FromQuery] int pageSize, [FromQuery] long id)
        {
            if (id > 0)
                return GetProduct(id);

            var result = _productQueries.GetProducts(pageIndex, pageSize);
            if (result == null)
                return NoContent();
            return Ok(result);
        }

        /// <summary>
        /// 获取产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:long}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ProductDto), (int) HttpStatusCode.OK)]
        public IActionResult Get(long id)
        {
            return GetProduct(id);
        }

        /// <summary>
        /// 添加产品
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Result<string>), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Result<string>), (int) HttpStatusCode.Created)]
        public async Task<IActionResult> AddProductAsync([FromBody] CreateProductCommand command)
        {
            var commandResult = await _mediator.Send(command);
            return commandResult.Success
                ? (IActionResult) Ok(commandResult)
                : BadRequest(commandResult);
        }

        /// <summary>
        /// 修改产品
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(Result), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Result), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProductAsync([FromBody] UpdateProductCommand command)
        {
            var commandResult = await _mediator.Send(command);
            return commandResult.Success
                ? (IActionResult) Ok(commandResult)
                : BadRequest(commandResult);
        }

        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(Result), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Result), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProductAsync([FromBody] DeleteProductCommand command)
        {
            var commandResult = await _mediator.Send(command);
            return commandResult.Success
                ? (IActionResult) Ok(commandResult)
                : BadRequest(commandResult);
        }

        /// <summary>
        /// 获取产品品牌
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("brands")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(BrandDto), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(List<BrandDto>), (int) HttpStatusCode.OK)]
        public IActionResult GetBrands([FromQuery] int pageIndex, [FromQuery] int pageSize, [FromQuery] long id)
        {
            if (id > 0)
                return GetBrand(id);

            var result = _productQueries.GetBrands(pageIndex, pageSize);
            if (result == null)
                return NoContent();
            return Ok(result);
        }

        /// <summary>
        /// 获取产品品牌
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("brands/{id:long}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(BrandDto), (int) HttpStatusCode.OK)]
        public IActionResult GetBrands(long id)
        {
            return GetBrand(id);
        }

        /// <summary>
        /// 添加产品品牌
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("brands")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(Result<string>), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Result<string>), (int) HttpStatusCode.Created)]
        public async Task<IActionResult> AddBrandAsync([FromBody] CreateBrandCommand command)
        {
            var commandResult = await _mediator.Send(command);
            return commandResult.Success
                ? (IActionResult) Ok(commandResult)
                : BadRequest(commandResult);
        }

        /// <summary>
        /// 修改产品品牌
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("brands")]
        [ProducesResponseType(typeof(Result), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Result), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateBrandAsync([FromBody] UpdateBrandCommand command)
        {
            var commandResult = await _mediator.Send(command);
            return commandResult.Success
                ? (IActionResult) Ok(commandResult)
                : BadRequest(commandResult);
        }

        /// <summary>
        /// 删除产品品牌
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete("brands")]
        [ProducesResponseType(typeof(Result), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Result), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBrandAsync([FromBody] DeleteBrandCommand command)
        {
            var commandResult = await _mediator.Send(command);
            return commandResult.Success
                ? (IActionResult) Ok(commandResult)
                : BadRequest(commandResult);
        }

        /// <summary>
        /// 获取产品分类
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("categories")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(CategoryDto), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(List<CategoryDto>), (int) HttpStatusCode.OK)]
        public IActionResult GetTypes([FromQuery] int pageIndex, [FromQuery] int pageSize, [FromQuery] long id)
        {
            if (id > 0)
                return GetCategory(id);

            var result = _productQueries.GetTypes(pageIndex, pageSize);
            if (result == null)
                return NoContent();
            return Ok(result);
        }

        /// <summary>
        /// 获取产品分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("categories/{id:long}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(CategoryDto), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(List<CategoryDto>), (int) HttpStatusCode.OK)]
        public IActionResult GetTypes(long id)
        {
            return GetCategory(id);
        }

        /// <summary>
        /// 添加产品分类
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("categories")]
        [ProducesResponseType(typeof(Result<string>), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Result<string>), (int) HttpStatusCode.Created)]
        public async Task<IActionResult> AddCategoriesAsync([FromBody] CreateTypeCommand command)
        {
            var commandResult = await _mediator.Send(command);
            return commandResult.Success
                ? (IActionResult) Ok(commandResult)
                : BadRequest(commandResult);
        }

        /// <summary>
        /// 修改产品分类
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("categories")]
        [ProducesResponseType(typeof(Result), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Result), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateCategoriesAsync([FromBody] UpdateTypeCommand command)
        {
            var commandResult = await _mediator.Send(command);
            return commandResult.Success
                ? (IActionResult) Ok(commandResult)
                : BadRequest(commandResult);
        }

        /// <summary>
        /// 删除产品分类
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete("categories")]
        [ProducesResponseType(typeof(Result), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Result), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteCategoriesAsync([FromBody] DeleteTypeCommand command)
        {
            var commandResult = await _mediator.Send(command);
            return commandResult.Success
                ? (IActionResult) Ok(commandResult)
                : BadRequest(commandResult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns></returns>
        [NonAction]
        private IActionResult GetBrand(long brandId)
        {
            var result = _productQueries.GetBrand(brandId);
            if (result == null)
                return NoContent();
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        [NonAction]
        private IActionResult GetCategory(long typeId)
        {
            var result = _productQueries.GetType(typeId);
            if (result == null)
                return NoContent();
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [NonAction]
        private IActionResult GetProduct(long productId)
        {
            var result = _productQueries.GetProduct(productId);
            if (result == null)
                return NoContent();
            return Ok(result);
        }
    }
}