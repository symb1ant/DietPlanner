﻿namespace DietPlanner.Contracts.Models;
public class AddDietEntry
{
    public string UserID { get; set; }
    public string FoodName { get; set; }
    public long MealTypeID { get; set; }
    public int Calories { get; set; }
    public DateTime Date { get; set; }
}
