using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietPlanner.Data.Models;
public class DietEntry
{
    public long Id { get; set; }
    public string Food { get; set; }
    public int Calories { get; set; }
    public DateTime Date { get; set; }
    public ApplicationUser User { get; set; }
    public MealType MealType { get; set; }
}
