
using AmazonSales.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AmazonSales.Controllers
{
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Authorize] //Requests authorization for API access
        public IActionResult Get()
        {
            IList<Product> products = new List<Product>();

            //Checking that a user has been registered by accessing the emails claim
            var user = User.Claims.Where(x => x.Type == "emails").FirstOrDefault();
            
            if (user != null)
            {
                products = new List<Product>()
                {
                    new Product()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Link = "https://www.amazon.it/"
                    },
                    new Product()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Link = "https://mail.google.com/"
                    }
                };
            } 


            return Ok(products);
        }
        
    }
}
