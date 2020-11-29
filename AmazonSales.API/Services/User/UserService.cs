

using AmazonSales.API.Services.User;
using AmazonSales.Data.Db;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace AmazonSales.API.Services.User
{
    public class UserService : IUserService
    {
        private readonly SalesContext context;

        public UserService(SalesContext context)
        {
            this.context = context;
        }   
        
        public Models.User GetCurrentUser(string id)
        {
            var dbUser = context.Users.FirstOrDefault(x => x.Id == id);
            return dbUser;     
        }

        public void Register(ClaimsPrincipal user)
        {
            var claims = (user.Identity as ClaimsIdentity).Claims;
            var email = claims.Where(x => x.Type == "emails").FirstOrDefault();
            var name = claims.Where(x => x.Type == ClaimTypes.GivenName).FirstOrDefault();
            var surname = claims.Where(x => x.Type == ClaimTypes.Surname).FirstOrDefault();
            var city = claims.Where(x => x.Type == "city").FirstOrDefault();
            var id = claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault();

            if (id != null && email != null)
            {
                var dbUser = context.Users.FirstOrDefault(x => x.Id == id.Value);
                if (dbUser == null)
                {
                    dbUser = new Models.User()
                    {
                        Id = id?.Value,
                        Email = email?.Value,
                        Name = name?.Value,
                        Surname = surname?.Value,
                        RoleId = 1
                    };
                    context.Users.Add(dbUser);
                    context.SaveChanges();
                }
            }
        }
    }
}
