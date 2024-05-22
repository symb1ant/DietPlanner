using DietPlanner.Data.Models;
using DietPlanner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DietPlanner.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class MealTypeController(IMealTypeService mealTypeService) : ControllerBase
{
   
    [HttpGet]
    public async Task<ActionResult<List<MealType>>> Get()
    {
        var result = await mealTypeService.GetAllAsync();
        return Ok(result);
    }
}
