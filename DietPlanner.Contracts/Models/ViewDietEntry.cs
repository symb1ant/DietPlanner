namespace DietPlanner.Contracts.Models;
public class ViewDietEntry
{
    public long ID { get; set; }
    public string FoodName { get; set; }
    public string MealName { get; set; }
    public int Calories { get; set; }
    public DateTime Date { get; set; }
}
