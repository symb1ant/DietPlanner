using DietPlanner.Contracts.Models;
using DietPlanner.Data.Interfaces;
using DietPlanner.Data.Models;
using DietPlanner.Services.Interfaces;
using DietPlanner.Services.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DietPlanner.Services.Implementation;
public class DietService(IRepository<DietEntry> dietEntryRepository, IRepository<ApplicationUser> userRepository, ILogger<DietService> logger) : IDietService
{
    public async Task<bool> AddEntry(AddDietEntry entry)
    {
        try
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
        catch (Exception ex)
        {
            logger.Log(LogLevel.Information, "Failed to add new entry: {message}", ex.Message);
            return false;
        }
    }

    public async Task<bool> DeleteEntry(DeleteEntry entry)
    {
        try
        {
            var user = await userRepository.GetByIDAsync(entry.UserID);

            if (user == null)
            {
                logger.Log(LogLevel.Information, "Failed to delete entry: {entry} - User not found", entry.Id);
                return false;
            }

            var entryToDelete = await dietEntryRepository.GetByIDAsync(entry.Id);

            if (entryToDelete == null)
            {
                logger.Log(LogLevel.Information, "Failed to delete entry: {entry} - Entry not found", entry.Id);
                return false;
            }

            await dietEntryRepository.DeleteAsync(entryToDelete);
            return true;
        }
        catch (Exception ex)
        {
            logger.Log(LogLevel.Information, "Failed to delete entry: {entry} - {message}", entry.Id, ex.Message);
            return false;
        }
    }

    public async Task<List<ViewDietEntry>> GetEntries(string userId)
    {
        var entries = await dietEntryRepository.GetAllAsync(x => x.UserId == userId, include: x => x.Include(e => e.MealType));

        return entries == null ? [] : entries.Select(x => new ViewDietEntry
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

        return entries == null ? [] : entries.OrderBy(x => x.Date).Select(x => new ViewDietEntry
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

        return entry == null ? new ViewDietEntry() : new ViewDietEntry
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


        var summaries = entries == null ? [] : entries.GroupBy(x => x.Date.Date)
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

        var summaries = entries == null ? [] : entries.GroupBy(x => x.Date.Date)
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
        try
        {
            var updateDietEntryValidator = new UpdateDietEntryValidator();
            await updateDietEntryValidator.ValidateAndThrowAsync(entry);

            var entrytoUpdate = await dietEntryRepository.GetByIDAsync(entry.ID);

            if (entrytoUpdate == null)
            {
                logger.Log(LogLevel.Information, "Failed to update entry: {entry id} - Entry not found", entry.ID);
                return false;
            }

            entrytoUpdate.Food = entry.FoodName;
            entrytoUpdate.Calories = entry.Calories;
            entrytoUpdate.Date = entry.Date;
            entrytoUpdate.MealTypeId = entry.MealTypeID;

            await dietEntryRepository.UpdateAsync(entrytoUpdate);

            return true;
        }
        catch (Exception ex)
        {
            logger.Log(LogLevel.Information, "Failed to update entry: {entry id} - {message}", entry.ID, ex.Message);
            return false;
        }
    }
}
