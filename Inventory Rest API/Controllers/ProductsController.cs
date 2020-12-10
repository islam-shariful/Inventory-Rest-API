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
    public class ProductsController : ApiController
    {
        ProductRepository productRepository = new ProductRepository();

        public IHttpActionResult GetAll()
        {
            return Ok(productRepository.GetAll());
        }
        public IHttpActionResult Get(int id)
        {
            var product = productRepository.Get(id);
            if (product == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                return Ok(productRepository.Get(id));
            }
        }
        public IHttpActionResult Post(Product product)
        {
            productRepository.Insert(product);
            return Created("api/Products/" + product.ProductId, product);
        }
    }
}
