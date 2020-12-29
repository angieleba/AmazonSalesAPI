using AmazonSales.Data.Db;
using AmazonSales.Models;
using AmazonSales.API.Services.Products;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AmazonSales.API.Services
{
    public class ProductService : IProductService
    {
        private readonly SalesContext context;

        public ProductService(SalesContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Returns all existing products 
        /// </summary>
        /// <returns></returns>
        public IList<Product> GetAll()
        {
            return context.Products.ToList();
        }

        /// <summary>
        /// Returns bookmarked products of a given user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<Product> GetBookmarkedProducts(string userId)
        {
            return context.UserProducts.Where(x => x.UserId == userId).Select(x => x.Product).ToList();
        }

        /// <summary>
        /// Creates a new affiliate product link
        /// </summary>
        /// <param name="product"></param>
        public void Create(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }

        /// <summary>
        /// Returns list of published affiliate links of a given user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<Product> GetPublishedProducts(string userId)
        {
            return context.Products.Where(x => x.UserId == userId).Select(x => new Product() { 
                Id = x.Id,
                Link = x.Link,
                UserId = x.UserId
            }).ToList();
        }
    }
}
