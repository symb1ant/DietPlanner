using DietPlanner.Contracts.Models;
using DietPlanner.Services.Interfaces;

namespace DietPlanner.Fakes.Services;
public class FakeMealTypeService : IMealTypeService
{
    public bool IsListEmpty { get; set; } = false;

    public FakeMealTypeService()
    {
        IsListEmpty = true;
    }
    public async Task<List<MealTypeView>> GetAllAsync()
    {
        if (IsListEmpty)
        {
            return await Task.FromResult(new List<MealTypeView>());
        }

        return await Task.FromResult(new List<MealTypeView>
        {
            new() { Id = 1, Name = "Breakfast" },
            new() { Id = 2, Name = "Lunch" },
            new() { Id = 3, Name = "Dinner" },
            new() { Id = 4, Name = "Snack" }
        });
    }
}
