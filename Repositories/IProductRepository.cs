using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IProductRepository
    {
        void SaveProduct(Product p);
        Product GetProductById(int id);
        void UpdateProduct(Product p);
        void DeleteProduct(Product p);
        List<Category> GetCategories();
        List<Product> GetProducts();
    }
}
