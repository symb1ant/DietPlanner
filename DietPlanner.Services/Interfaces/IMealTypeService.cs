using DietPlanner.Contracts.Models;
using DietPlanner.Data.Models;

namespace DietPlanner.Services.Interfaces;
public interface IMealTypeService
{
    Task<List<MealTypeView>> GetAllAsync();
}
