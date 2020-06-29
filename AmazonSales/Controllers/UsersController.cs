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

        public UsersController(SalesContext context)
        {
            _context = context;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IActionResult Get()
        {
            var users = _context.Users;
            return Ok(new { result = users });
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("{id}")]
        public IActionResult Get(int Id)
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
