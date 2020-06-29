using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazonSales.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public virtual ICollection<UserFollowing> Followers { get; set; }
        public virtual ICollection<UserFollowing> Following { get; set; }

        //Navigation property for published products
        public virtual ICollection<Product> Products { get; set; }

        //Navigation property for bookmarked products
        public virtual ICollection<UserProducts> BookmarkedProducts { get; set; }
    }
}
