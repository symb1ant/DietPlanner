using DietPlanner.Contracts.Models;
using DietPlanner.Data.Models;
using DietPlanner.Repositories.Interfaces;
using DietPlanner.Services.Interfaces;

namespace DietPlanner.Services.Implementation;
public class DietService(IRepository<DietEntry> dietEntryRepository, IRepository<ApplicationUser> userRespository) : IDietService
{
    public async Task<bool> AddEntry(AddDietEntry entry)
    {
        var user = await userRespository.GetByIDAsync(entry.UserID);

        if (user == null)
        {
            return false;
        }

        var dietEntry = new DietEntry
        {
            User = user,
            Date = entry.Date,
            Food = entry.FoodName,
            Calories = entry.Calories,
            MealTypeId = entry.MealTypeID
        };

        await dietEntryRepository.AddAsync(dietEntry);
        return true;
    }

    public async Task<bool> DeleteEntry(DeleteEntry entry)
    {
        var user = await userRespository.GetByIDAsync(entry.UserID);

        if (user == null)
        {
            return false;
        }

        var entryToDelete = await dietEntryRepository.GetByIDAsync(entry.Id);

        if (entryToDelete == null)
        {
            return false;
        }

        await dietEntryRepository.DeleteAsync(entryToDelete);
        return true;

    }

    public async Task<List<ViewDietEntry>> GetEntries(string userId)
    {
        var entries = await dietEntryRepository.GetAllAsync(x => x.UserId == userId);

        return entries.Select(x => new ViewDietEntry
        {
            ID = x.Id,
            FoodName = x.Food,
            Calories = x.Calories,
            Date = x.Date,
            MealName = x.MealType.Name.ToString()

        }).OrderByDescending(x => x.Date).ToList();

    }

    public async Task<List<ViewDietEntry>> GetEntriesByDate(string userId, DateTime date)
    {
        var startDate = date.Date.Date;
        var endDate = date.Date.AddDays(1).AddSeconds(-1);

        var entries = await dietEntryRepository.GetAllAsync(x => x.UserId == userId && x.Date >= startDate && x.Date <= endDate);

        return entries.OrderBy(x => x.Date).Select(x => new ViewDietEntry
        {
            ID = x.Id,
            FoodName = x.Food,
            Calories = x.Calories,
            Date = x.Date,
            MealName = x.MealType.Name.ToString()

        }).ToList();
    }

    public async Task<ViewDietEntry> GetEntry(int id)
    {
        var entry = await dietEntryRepository.GetByIDAsync(id);

        return new ViewDietEntry
        {
            ID = entry.Id,
            FoodName = entry.Food,
            Calories = entry.Calories,
            Date = entry.Date,
            MealName = entry.MealType.Name.ToString()
        };
    }

    public async Task<List<ViewDietSummary>> GetSummaryByDate(string userId, DateTime date)
    {
        var startDate = date.Date;
        var endDate = date.Date.AddDays(1).AddSeconds(-1);

        var entries = await dietEntryRepository.GetAllAsync(x => x.UserId == userId && x.Date >= startDate && x.Date <= endDate);

        var summaries = entries.GroupBy(x => x.Date.Date)
                               .Select(group => new ViewDietSummary
                               {
                                   Date = group.Key,
                                   TotalCalories = group.Sum(x => x.Calories)
                               })
                               .OrderByDescending(x => x.Date).ToList();

        return summaries;
    }

    public async Task<List<ViewDietSummary>> GetSummaryByUser(string userId)
    {
        
        var entries = await dietEntryRepository.GetAllAsync(x => x.UserId == userId);

        var summaries = entries.GroupBy(x => x.Date.Date)
                               .Select(group => new ViewDietSummary
                               {
                                   Date = group.Key,
                                   TotalCalories = group.Sum(x => x.Calories)
                               })
                               .OrderByDescending(x => x.Date).ToList();

        return summaries;
    }


    public async Task<bool> UpdateEntry(UpdateDietEntry entry)
    {
        var entrytoUpdate = await dietEntryRepository.GetByIDAsync(entry.ID);

        if (entrytoUpdate == null)
        {
            return false;
        }

        entrytoUpdate.Food = entry.FoodName;
        entrytoUpdate.Calories = entry.Calories;
        entrytoUpdate.Date = entry.Date;
        entrytoUpdate.MealTypeId = entry.MealTypeID;

        await dietEntryRepository.UpdateAsync(entrytoUpdate);

        return true;
    }
}
