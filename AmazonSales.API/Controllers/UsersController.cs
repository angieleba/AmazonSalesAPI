using AmazonSales.API.Services.User;
using AmazonSales.Data.Db;
using AmazonSales.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace AmazonSales.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SalesContext _context;
        private readonly IUserService userService;

        public UsersController(SalesContext context, IUserService userService)
        {
            _context = context;
            this.userService = userService;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IActionResult Get()
        {
            var users = _context.Users;
            return Ok(new { result = users });
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("{id}")]
        public IActionResult Get(string Id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == Id);
            if (user == null)
            {
                //var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                //{
                //    Content = new StringContent(string.Format("User with id = {0} does not exist", Id), System.Text.Encoding.UTF8, "application/json"),
                //    ReasonPhrase = "User not found."
                //};

                //var msg = new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "User not found." };
                //throw new HttpResponseException(msg);        
                return StatusCode(403, new { ReasonPhrase = "User not found."});
            }

            return Ok(new { result = user });
        }

        [HttpGet]
        [Route("register")]
        public IActionResult Register()
        {
            this.userService.Register(User);

            return Ok(new { result = "" });
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult Post(User user)
        {
            _context.Users.Add(user);         
            _context.SaveChanges();

            return Ok(new { result = user });
        }

        [Microsoft.AspNetCore.Mvc.HttpPut]
        public IActionResult Put(int userId)
        {
            return Ok(new { result = "OK" });
        }

        [Microsoft.AspNetCore.Mvc.HttpDelete]
        public IActionResult Delete(int userId)
        {
            return Ok(new { result = "OK" });
        }
    }
}
