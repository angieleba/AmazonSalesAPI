using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazonSales.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string Link { get; set; }

        //Affiliate who has published the product
        public string UserId { get; set; }
        public virtual User User { get; set; }

        //Group of users who have bookmarked the product
        public virtual ICollection<UserProducts> BookmarkingUsers { get; set; }

    }
}
