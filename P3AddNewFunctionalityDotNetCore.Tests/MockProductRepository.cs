using P3AddNewFunctionalityDotNetCore.Models.Entities;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P3AddNewFunctionalityDotNetCore.Tests
{
    public class MockProductRepository : IProductRepository
    {
        public List<Product> InMemoryStore = new List<Product>();

        public IEnumerable<Product> GetAllProducts() => this.InMemoryStore;

        public void SaveProduct(Product product)
        {
            if (product.Id == 0)
            {
                product.Id = this.InMemoryStore.Max(p => p.Id) + 1;
            }

            this.InMemoryStore.Add(product);
        }

        #region Unnecessary
        public void DeleteProduct(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Product> GetProduct(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<Product>> GetProduct()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateProductStocks(int productId, int quantityToRemove)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}