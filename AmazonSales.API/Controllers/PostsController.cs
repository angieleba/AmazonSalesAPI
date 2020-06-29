using AmazonSales.Data.Db;
using AmazonSales.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazonSales.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly SalesContext _context;

        public PostsController(SalesContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            //TODO Implement query for logged in user or not 
            //Apply an algorithm 
            List<Product> products = _context.Products.ToList();
            return Ok(new { result = products });
        }

        [HttpPost]
        public IActionResult Post(Product product)
        {
            //TODO Validate product URL first 
            _context.Products.Add(product);
            _context.SaveChanges();

            return Ok(new { result = product });
        }

        [HttpGet]
        [Route("/myPosts/{userId}")]
        public IActionResult GetPostedProducts(int userId)
        {
            //TODO Implement query for logged in user or not 
            //Apply an algorithm 
            List<Product> products = _context.Products.Where(x => x.UserId == userId).ToList();
            return Ok(new { result = products });
        }

        [HttpGet]
        [Route("/bookmarkedPosts/{userId}")]
        public IActionResult GetBookmarkedProducts(int userId)
        {
            List<Product> products = _context.UserProducts.Where(x => x.UserId == userId).Select(x => x.Product).ToList();
            return Ok(new { result = products });
        }

    }
}
