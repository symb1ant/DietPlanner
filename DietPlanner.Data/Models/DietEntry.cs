namespace DietPlanner.Data.Models;
public class DietEntry
{
    public long Id { get; set; }
    public string Food { get; set; }
    public int Calories { get; set; }
    public DateTime Date { get; set; }
    public ApplicationUser User { get; set; }
    public string UserId { get; set; }
    public long MealTypeId { get; set; }
    public MealType MealType { get; set; }
}
