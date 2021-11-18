using Microsoft.AspNetCore.Mvc;
using P3AddNewFunctionalityDotNetCore.Controllers;
using P3AddNewFunctionalityDotNetCore.Models.Entities;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace P3AddNewFunctionalityDotNetCore.Tests
{
    public class IntegrationTests
    {
        [Theory]
        [InlineData("Name One")]
        [InlineData("Name Two")]
        public void WhenAdminCreatesNewProduct_NewProductIsVisibleToUser(string name)
        {
            // Arrange
            var repo = new MockProductRepository();
            repo.InMemoryStore.Add(CreateProduct(1));

            var controller = CreateController(repo);

            var adminProduct = CreateProductViewModel(name);

            // Act
            // Simulates an admin creating a new product and saving it to the database
            controller.Create(adminProduct);

            // Simulates a user loading all products
            var result = (ViewResult)controller.Index();
            var products = (IEnumerable<ProductViewModel>)result.Model;

            // Assert
            Assert.Equal(2, products.Count());
            Assert.Single(products.Where(p => p.Name == name));
        }

        private static ProductController CreateController(MockProductRepository repo) =>
            new ProductController(
                new ProductService(
                    productRepository: repo,
                    cart: null,
                    orderRepository: null,
                    localizer: null),
                languageService: null);
        private static Product CreateProduct(int id) =>
            new Product()
            {
                Id = id,
                Name = $"Product {id}",
                Description = $"Product {id}",
                Details = $"Product {id}",
                Price = 100,
                Quantity = 1000,
            };
        private static ProductViewModel CreateProductViewModel(string name) =>
            new ProductViewModel()
            {
                Name = name,
                Description = "Admin product description",
                Details = "Admin product details",
                Price = "10.00",
                Stock = "100",
            };
    }
}