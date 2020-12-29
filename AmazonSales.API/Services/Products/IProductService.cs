using AmazonSales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmazonSales.API.Services.Products
{
    public interface IProductService
    {
        IList<Product> GetAll();
        IList<Product> GetBookmarkedProducts(string userId);
        void Create(Product product);
        IList<Product> GetPublishedProducts(string userId);
    }
}
