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
    [Route("api/categories")]
    public class CategoriesController : Controller
    {
        IDataService _dataService;
        LinkGenerator _linkGenerator;

        public CategoriesController(IDataService dataService, LinkGenerator linkGenerator)
        {
            _dataService = dataService;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _dataService.GetCategories();
            return Ok(categories.Select(x => GetCategoryViewModel(x)));
        }

        [HttpGet("{id}", Name = nameof(GetCategory))]
        public IActionResult GetCategory(int id)
        {
            var category = _dataService.GetCategory(id);

            if (category == null) return NotFound();

            CategoryViewModel model = GetCategoryViewModel(category);

            return Ok(model);
        }

        private CategoryViewModel GetCategoryViewModel(Category category)
        {
            return new CategoryViewModel
            {
                Id = category.Id,
                Url = _linkGenerator.GetUriByName(HttpContext, nameof(GetCategory), new { category.Id }),
                Name = category.Name,
                Description = category.Description
            };
        }

        [HttpPost]
        public IActionResult CreateCategoryViewModel(CreateCategoryViewModel model)
        {
            var category = new Category
            {
                Name = model.Name,
                Description = model.Description
            };

            var returnCategory = _dataService.CreateCategory(category);
            return Created(_linkGenerator.GetUriByName(HttpContext, nameof(GetCategory), new { returnCategory.Id }), GetCategoryViewModel(returnCategory));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, Category update)
        {
            _dataService.UpdateCategory(id, update.Name, update.Description);

            CategoryViewModel model = GetCategoryViewModel(update);

            return Ok(model);
        }
    }
}
