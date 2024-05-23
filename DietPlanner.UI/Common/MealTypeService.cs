using DietPlanner.Contracts.Models;

namespace DietPlanner.UI.Common;

public class MealTypeService
{
    private readonly HttpClient _httpClient;
    public List<MealTypeView> Data { get; private set; }
    public bool IsInitialized { get; private set; }

    public MealTypeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        Data = [];
    }

    public async Task LoadDataAsync(string apiBaseAddress)
    {
        if (!IsInitialized)
        {
            Data = await _httpClient.GetFromJsonAsync<List<MealTypeView>>($"{apiBaseAddress}/api/mealtype");
            IsInitialized = true;
        }
    }
}
