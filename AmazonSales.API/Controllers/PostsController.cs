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
            User user = null;
            List<Product> products = new List<Product>();
            var id = (User.Identity as ClaimsIdentity).Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
            if (id != null)
            {
                user = userService.GetCurrentUser(id.Value);
                if(user == null) //User not yet saved in database
                {
                    user = userService.Register(User);
                }
            }     

            //TODO: Implement query for logged in user or not 
            //TODO: Apply an algorithm 
            if(user != null) //check in case of not loggedin users
            {
                products = _context.Products.Where(x => x.UserId == user.Id).ToList();
            } 
                       
            return Ok(new { result = products });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post(Product product)
        {
            //TODO: Validate product URL first 
            _context.Products.Add(product);
            _context.SaveChanges();

            return Ok(new { result = product });
        }

        [HttpGet]
        [Authorize]
        [Route("/myPosts/{userId}")]
        public IActionResult GetPostedProducts(string userId)
        {
            //TODO: Implement query for logged in user or not 
            //TODO: Apply an algorithm 
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
