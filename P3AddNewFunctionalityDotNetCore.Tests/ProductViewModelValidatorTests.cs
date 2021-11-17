using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace P3AddNewFunctionalityDotNetCore.Tests
{
    public class ProductViewModelValidatorTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void MissingName_ReturnsError(string name)
        {
            var model = CreateModel(name: name);
            var validator = new ProductViewModelValidator();

            var result = validator.Validate(model);

            Assert.Single(result.Errors);
            Assert.Equal("MissingName", result.Errors.Single().ErrorMessage);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void MissingPrice_ReturnsError(string price)
        {
            var model = CreateModel(price: price);
            var validator = new ProductViewModelValidator();

            var result = validator.Validate(model);

            Assert.NotEmpty(result.Errors);
            Assert.Single(result.Errors.Where(e => e.ErrorMessage.Equals("MissingPrice")));
        }

        [Fact]
        public void PriceNotANumber_ReturnsError()
        {
            var model = CreateModel(price: "not_a_number");
            var validator = new ProductViewModelValidator();

            var result = validator.Validate(model);

            Assert.Single(result.Errors);
            Assert.Equal("PriceNotANumber", result.Errors.Single().ErrorMessage);
        }

        [Theory]
        [InlineData("-1")]
        [InlineData("0")]
        public void PriceNotGreaterThanZero_ReturnsError(string price)
        {
            var model = CreateModel(price: price);
            var validator = new ProductViewModelValidator();

            var result = validator.Validate(model);

            Assert.Single(result.Errors);
            Assert.Equal("PriceNotGreaterThanZero", result.Errors.Single().ErrorMessage);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void MissingStock_ReturnsError(string stock)
        {
            var model = CreateModel(stock: stock);
            var validator = new ProductViewModelValidator();

            var result = validator.Validate(model);

            Assert.NotEmpty(result.Errors);
            Assert.Single(result.Errors.Where(e => e.ErrorMessage.Equals("MissingStock")));
        }

        [Fact]
        public void StockNotAnInteger_ReturnsError()
        {
            var model = CreateModel(stock: "not_an_integer");
            var validator = new ProductViewModelValidator();

            var result = validator.Validate(model);

            Assert.Single(result.Errors);
            Assert.Equal("StockNotAnInteger", result.Errors.Single().ErrorMessage);
        }

        [Theory]
        [InlineData("-1")]
        [InlineData("0")]
        public void StockNotGreaterThanZero_ReturnsError(string stock)
        {
            var model = CreateModel(stock: stock);
            var validator = new ProductViewModelValidator();

            var result = validator.Validate(model);

            Assert.Single(result.Errors);
            Assert.Equal("StockNotGreaterThanZero", result.Errors.Single().ErrorMessage);
        }

        private static ProductViewModel CreateModel(
            int id = 1,
            string name = "Test Product Name",
            string description = "Test product description",
            string details = "Test product details",
            string stock = "1",
            string price = "1.00")
        {
            return new ProductViewModel()
            {
                Id = id,
                Name = name,
                Description = description,
                Details = details,
                Stock = stock,
                Price = price,
            };
        }
    }
}