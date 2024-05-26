using DietPlanner.Contracts.Models;
using DietPlanner.Services.Interfaces;

namespace DietPlanner.Fakes.Services;
public class FakeDietService : IDietService
{
    public bool IsAddFail { get; set; } = false;
    public bool IsDeleteFail { get; set; } = false;
    public bool IsUpdateFail { get; set; } = false;
    public bool IsEntryListEmpty { get; set; } = false;

    public async Task<bool> AddEntry(AddDietEntry entry)
    {
        if(IsAddFail)
        {
            return await Task.FromResult(false);
        }

        return await Task.FromResult(true);
    }

    public Task<bool> DeleteEntry(DeleteEntry entry)
    {
        if(IsDeleteFail)
        {
            return Task.FromResult(false);
        }

        return Task.FromResult(true);
    }

    public Task<List<ViewDietEntry>> GetEntries(string userId)
    {
        if (IsEntryListEmpty)
        {
            return Task.FromResult(new List<ViewDietEntry>());
        }

        return Task.FromResult(new List<ViewDietEntry>
        {
            new() { ID = 1, FoodName = "Test Food", Calories = 100, MealTypeId = 1 }
        });
    }

    public Task<List<ViewDietEntry>> GetEntriesByDate(string userId, DateTime date)
    {
        if(IsEntryListEmpty)
        {
            return Task.FromResult(new List<ViewDietEntry>());
        }

        return Task.FromResult(new List<ViewDietEntry>
        {
            new() { ID = 1, FoodName = "Test Food", Calories = 100, MealTypeId = 1 }
        });
    }

    public Task<ViewDietEntry> GetEntry(int id)
    {
        if(IsEntryListEmpty)
        {
            return Task.FromResult(new ViewDietEntry());
        }

        return Task.FromResult(new ViewDietEntry
        {
            ID = 1,
            FoodName = "Test Food",
            Calories = 100,
            MealTypeId = 1
        });
    }

    public async Task<List<ViewDietSummary>> GetSummaryByDate(string userId, DateTime date)
    {
        if(IsEntryListEmpty)
        {
            return await Task.FromResult(new List<ViewDietSummary>());
        }

        return await Task.FromResult(new List<ViewDietSummary>
        {
            new() { Date = DateTime.Now, TotalCalories = 100 }
        });
    }

    public async Task<List<ViewDietSummary>> GetSummaryByUser(string userId)
    {
        if(IsEntryListEmpty)
        {
            return await Task.FromResult(new List<ViewDietSummary>());
        }

        return await Task.FromResult(new List<ViewDietSummary>
        {
            new() { Date = DateTime.Now, TotalCalories = 100 }
        });
    }

    public async Task<bool> UpdateEntry(UpdateDietEntry entry)
    {
        if(IsUpdateFail)
        {
            return await Task.FromResult(false);
        }

        return await Task.FromResult(true);
    }
}
