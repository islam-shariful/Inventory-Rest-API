using Inventory_Rest_API.Models;
using Inventory_Rest_API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Inventory_Rest_API.Controllers
{
    [RoutePrefix("api/categories")]
    public class CategoriesController : ApiController
    {
        CategoryRepository categoryRepository = new CategoryRepository();
        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(categoryRepository.GetAll());
        }
        [Route("{id}", Name="GetCategoryById")]
        public IHttpActionResult GetAll(int id)
        {
            var category = categoryRepository.Get(id);
            if(category == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                return Ok(categoryRepository.Get(id));
            }
        }
        [Route("")]
        public IHttpActionResult Post(Category category)
        {
            categoryRepository.Insert(category);
            string uri = Url.Link("GetCategoryById", new { id = category.CategoryId });
            return Created(uri, category);
        }
        [Route("{id}")]
        public IHttpActionResult Put([FromUri] int id,[FromBody]Category category)
        {
            category.CategoryId = id;
            categoryRepository.Update(category);
            return Ok(category);
        }
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            categoryRepository.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
        [Route("{id}/products")]
        public IHttpActionResult GetProductsByCategoryId(int id)
        {
            ProductRepository productRepository = new ProductRepository();            
            return Ok(productRepository.GetProductsByCategory(id));
        }
    }
}
