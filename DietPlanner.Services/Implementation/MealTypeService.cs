using DietPlanner.Contracts.Models;
using DietPlanner.Data.Interfaces;
using DietPlanner.Data.Models;
using DietPlanner.Services.Interfaces;

namespace DietPlanner.Services.Implementation;
public class MealTypeService(IRepository<MealType> mealTypeRepository) : IMealTypeService
{
    
    public async Task<List<MealTypeView>> GetAllAsync()
    {
        var result = await mealTypeRepository.GetAllAsync();
        return result.Select(
            x => new MealTypeView
            {
                Id = x.Id,
                Name = x.Name
            }
            ) .OrderBy(x => x.Id).ToList();
    }
}
