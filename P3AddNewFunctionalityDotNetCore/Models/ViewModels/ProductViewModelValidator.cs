using FluentValidation;

namespace P3AddNewFunctionalityDotNetCore.Models.ViewModels
{
    public class ProductViewModelValidator : AbstractValidator<ProductViewModel>
    {
        public ProductViewModelValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty()
                .WithMessage("MissingName");

            RuleFor(m => m.Price)
                .NotEmpty()
                .WithMessage("MissingPrice");

            RuleFor(m => m.Price)
                .Custom((value, context) =>
                {
                    if (!double.TryParse(value, out double price))
                    {
                        context.AddFailure("PriceNotANumber");
                    }
                    else if (price <= 0)
                    {
                        context.AddFailure("PriceNotGreaterThanZero");
                    }
                });

            RuleFor(m => m.Stock)
                .NotEmpty()
                .WithMessage("MissingStock");

            RuleFor(m => m.Stock)
                .Custom((value, context) =>
                {
                    if (!int.TryParse(value, out int stock))
                    {
                        context.AddFailure("StockNotAnInteger");
                    }
                    else if (stock <= 0)
                    {
                        context.AddFailure("StockNotGreaterThanZero");
                    }
                });
        }
    }
}