using AmazonSales.API.Services;
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
    public class PostsController : ControllerBase
    {
        private readonly SalesContext _context;
        private readonly IUserService userService;

        public PostsController(SalesContext context, IUserService userService)
        {
            _context = context;
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var user = userService.GetCurrentUser(User);
            if(user != null)
            {
                //User is authenticated 
            } else
            {
                //not 
            }

            //TODO Implement query for logged in user or not 
            //Apply an algorithm 
            List<Product> products = _context.Products.ToList();
            products.Add(new Product()
            {
                Id = Guid.NewGuid().ToString(),
                Link = "hey"
            });
            return Ok(new { result = products });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post(Product product)
        {
            //TODO Validate product URL first 
            _context.Products.Add(product);
            _context.SaveChanges();

            return Ok(new { result = product });
        }

        [HttpGet]
        [Authorize]
        [Route("/myPosts/{userId}")]
        public IActionResult GetPostedProducts(string userId)
        {
            //TODO Implement query for logged in user or not 
            //Apply an algorithm 
            List<Product> products = _context.Products.Where(x => x.UserId == userId).ToList();
            return Ok(new { result = products });
        }

        [HttpGet]
        [Authorize]
        [Route("/bookmarkedPosts/{userId}")]
        public IActionResult GetBookmarkedProducts(string userId)
        {
            List<Product> products = _context.UserProducts.Where(x => x.UserId == userId).Select(x => x.Product).ToList();
            return Ok(new { result = products });
        }
    }
}
