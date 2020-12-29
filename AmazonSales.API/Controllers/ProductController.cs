using AmazonSales.API.Services.Products;
using AmazonSales.API.Services.User;
using AmazonSales.Data.Db;
using AmazonSales.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AmazonSales.Controllers
{
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IUserService userService;
        private readonly IProductService productService;
        public ProductController(IUserService userService, IProductService productService)
        {
            
            this.userService = userService;
            this.productService = productService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            User user = null;
            IList<Product> products = new List<Product>();

            user = userService.GetCurrentUser(this.User);
            if (user == null) //User not yet saved in database
            {
                user = userService.Register(User);
            }

            //TODO: Implement query for logged in user or not 
            //TODO: Apply an algorithm 
            if (user != null) //check in case of not loggedin users
            {
                products = productService.GetPublishedProducts(user.Id);
            } 
                       
            return Ok(products);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post(Product product)
        {
            //TODO: Validate product URL first
            if(!string.IsNullOrEmpty(product.Link))
            {
                var user = userService.GetCurrentUser(this.User);
                if(user != null)
                {
                    product.User = user;
                    productService.Create(product);
                } else
                {
                    //Error: this scenario should not happen. User must be logged in before calling a product creation
                }
                
            }

            return Ok(new { result = product });
        }

        [HttpGet]
        [Authorize]
        [Route("/myPosts/{userId}")]
        public IActionResult GetPostedProducts(string userId)
        {
            //TODO: Implement query for logged in user or not 
            //TODO: Apply an algorithm 
            var products = productService.GetPublishedProducts(userId);
            return Ok(new { result = products });
        }

        [HttpGet]
        [Authorize]
        [Route("/bookmarkedPosts/{userId}")]
        public IActionResult GetBookmarkedProducts(string userId)
        {
            IList<Product> products = productService.GetBookmarkedProducts(userId);
            return Ok(new { result = products });
        }
    }
}
