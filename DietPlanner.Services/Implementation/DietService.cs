using DietPlanner.Contracts.Models;
using DietPlanner.Data.Models;
using DietPlanner.Data.Interfaces;
using DietPlanner.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using DietPlanner.Services.Validators;
using FluentValidation;

namespace DietPlanner.Services.Implementation;
public class DietService(IRepository<DietEntry> dietEntryRepository, IRepository<ApplicationUser> userRepository) : IDietService
{
    public async Task<bool> AddEntry(AddDietEntry entry)
    {
        var addDietEntryValidator = new AddDietEntryValidator();
        await addDietEntryValidator.ValidateAndThrowAsync(entry);

        var user = await userRepository.GetByIDAsync(entry.UserID);

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
        var user = await userRepository.GetByIDAsync(entry.UserID);

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
        var entries = await dietEntryRepository.GetAllAsync(x => x.UserId == userId, include: x => x.Include(e => e.MealType));

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
        var startDate = date.Date;
        var endDate = date.Date.AddDays(1).AddSeconds(-1);

        var entries = await dietEntryRepository.GetAllAsync(x => x.UserId == userId && x.Date >= startDate && x.Date <= endDate, include: x => x.Include(e => e.MealType));

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
        var entry = await dietEntryRepository.GetByIDAsync(id, include: x => x.Include(e => e.MealType));

        return new ViewDietEntry
        {
            ID = entry.Id,
            FoodName = entry.Food,
            Calories = entry.Calories,
            Date = entry.Date,
            MealTypeId = entry.MealTypeId,
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
        var updateDietEntryValidator = new UpdateDietEntryValidator();
        await updateDietEntryValidator.ValidateAndThrowAsync(entry);

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
