using DietPlanner.Contracts.Models;

namespace DietPlanner.Services.Interfaces;
public interface IDietService
{
    Task<bool> AddEntry(AddDietEntry entry);
    Task<bool> UpdateEntry(UpdateDietEntry entry);
    Task<bool> DeleteEntry(DeleteEntry entry);
    Task<List<ViewDietEntry>> GetEntries(string userId);
    Task<ViewDietEntry> GetEntry(int id);
    Task<List<ViewDietEntry>> GetEntriesByDate(string userId, DateTime date);    
    Task<List<ViewDietSummary>> GetSummaryByDate(string userId, DateTime date);
    Task<List<ViewDietSummary>> GetSummaryByUser(string userId);
}
