using Inventory_Rest_API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Inventory_Rest_API.Controllers
{
    public class CategoriesController : ApiController
    {
        CategoryRepository categoryRepository = new CategoryRepository();
        public IHttpActionResult GetAll()
        {
            return Ok(categoryRepository.GetAll());
        }
        public IHttpActionResult GetAll(int id)
        {
            return Ok(categoryRepository.Get(id));
        }
    }
}
