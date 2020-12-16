using Inventory_Rest_API.Models;
using Inventory_Rest_API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
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
            category.Links.Add(new Link() { Url = HttpContext.Current.Request.Url.AbsoluteUri.ToString(), Method = "Get", Relation = "Self" });
            category.Links.Add(new Link() { Url = "http://localhost:53966/api/Categories/", Method = "Get", Relation = "Get all categories" });
            category.Links.Add(new Link() { Url = "http://localhost:53966/api/Categories/", Method = "Post", Relation = "Create new category resources" });
            category.Links.Add(new Link() { Url = "http://localhost:53966/api/Categories/"+id, Method = "Put", Relation = "Modify existing category resources" });
            category.Links.Add(new Link() { Url = "http://localhost:53966/api/Categories/"+id, Method = "Delete", Relation = "Remove existing category resources" });
            return Ok(categoryRepository.Get(id));
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
