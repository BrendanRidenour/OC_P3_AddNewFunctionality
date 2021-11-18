using FluentValidation;
using Microsoft.Extensions.Localization;
using System;

namespace P3AddNewFunctionalityDotNetCore.Models.ViewModels
{
    public class ProductViewModelValidator : AbstractValidator<ProductViewModel>
    {
        private readonly IStringLocalizer<ProductViewModelValidator> _localizer;

        public ProductViewModelValidator(IStringLocalizer<ProductViewModelValidator> localizer)
        {
            if (localizer is null)
            {
                throw new ArgumentNullException(nameof(localizer));
            }

            this._localizer = localizer;

            RuleFor(m => m.Name)
                .NotEmpty()
                .WithMessage(this._localizer["MissingName"]);

            RuleFor(m => m.Price)
                .NotEmpty()
                .WithMessage(this._localizer["MissingPrice"]);

            RuleFor(m => m.Price)
                .Custom((value, context) =>
                {
                    if (!double.TryParse(value, out double price))
                    {
                        context.AddFailure(this._localizer["PriceNotANumber"]);
                    }
                    else if (price <= 0)
                    {
                        context.AddFailure(this._localizer["PriceNotGreaterThanZero"]);
                    }
                });

            RuleFor(m => m.Stock)
                .NotEmpty()
                .WithMessage(this._localizer["MissingStock"]);

            RuleFor(m => m.Stock)
                .Custom((value, context) =>
                {
                    if (!int.TryParse(value, out int stock))
                    {
                        context.AddFailure(this._localizer["StockNotAnInteger"]);
                    }
                    else if (stock <= 0)
                    {
                        context.AddFailure(this._localizer["StockNotGreaterThanZero"]);
                    }
                });
        }
    }
}