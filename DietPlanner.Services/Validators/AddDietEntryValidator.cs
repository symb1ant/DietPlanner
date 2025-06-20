using DietPlanner.Contracts.Models;
using FluentValidation;

namespace DietPlanner.Services.Validators;
public class AddDietEntryValidator : AbstractValidator<AddDietEntry>
{
    public AddDietEntryValidator()
    {
        RuleFor(x => x.Date).NotEmpty().WithMessage("Date is required");
        RuleFor(x => x.MealTypeID).NotEmpty().WithMessage("Meal Type is required");
        RuleFor(x => x.Calories).NotEmpty().WithMessage("Calories is required");
        RuleFor(x => x.Calories).GreaterThan(0).WithMessage("Calories must be greater than 0");
        RuleFor(x => x.FoodName).NotEmpty().WithMessage("Food Name is required");
        RuleFor(x => x.UserID).NotEmpty().WithMessage("User ID is required");
    }
}
