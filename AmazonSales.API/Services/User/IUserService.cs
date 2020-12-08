using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AmazonSales.API.Services.User
{
    public interface IUserService
    {
        Models.User GetCurrentUser(string id);
        Models.User Register(ClaimsPrincipal user);

    }
}
