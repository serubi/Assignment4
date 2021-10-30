using Assignment4;

using Assignment4.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.ViewModels;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : Controller
    {
        IDataService _dataService;
        LinkGenerator _linkGenerator;

        public ProductsController(IDataService dataService, LinkGenerator linkGenerator)
        {
            _dataService = dataService;
            _linkGenerator = linkGenerator;
        }

        private ProductViewModel GetProductViewModel(Product product)
        {
            return new ProductViewModel
            {
                Id = product.Id,
                Url = _linkGenerator.GetUriByName(HttpContext, nameof(GetProduct), new { product.Id }),
                Name = product.Name,
                CategoryId = product.CategoryId,
                Category = product.Category
            };
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _dataService.GetProducts();
            return Ok(products.Select(x => GetProductViewModel(x)));
        }

        [HttpGet("{id}", Name = nameof(GetProduct))]
        public IActionResult GetProduct(int id)
        {
            var product = _dataService.GetProduct(id);

            if (product == null) return NotFound();

            ProductViewModel model = GetProductViewModel(product);

            return Ok(model);
        }

        [HttpGet("category/{id}")]
        public IActionResult GetProductsFromCategory(int id)
        {
            var products = _dataService.GetProductByCategory(id);

            IList<ProductViewModel> models = products.Select(x => GetProductViewModel(x)).ToList();

            if (models.Count > 0) return Ok(models);
            return NotFound(models);
        }

        [HttpGet("name/{searchquery}")]
        public IActionResult GetProductsFromNameSearch(string searchquery)
        {
            var products = _dataService.GetProductByName(searchquery);

            IList<ProductViewModel> models = products.Select(x => GetProductViewModel(x)).ToList();

            if (models.Count > 0) return Ok(models);
            return NotFound(models);
        }
    }
}
