namespace DietPlanner.Contracts.Models;
public class UpdateDietEntry
{
    public long ID { get; set; }
    public string FoodName { get; set; }
    public int MealTypeID { get; set; }
    public int Calories { get; set; }
    public DateTime Date { get; set; }
}
