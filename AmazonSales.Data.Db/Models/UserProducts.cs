using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazonSales.Models
{
    //A user bookmarks many products and a product has many users as bookmarkers
    public class UserProducts
    {
        public string ProductId { get; set; }
        public Product Product { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
