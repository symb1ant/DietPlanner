using DietPlanner.Data.Models;
using DietPlanner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DietPlanner.API.Controllers;
[Route("api/[controller]")]
[ApiController]
[SwaggerResponse(StatusCodes.Status200OK, "Success")]
[SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data sent to the api ")]
[SwaggerResponse(StatusCodes.Status404NotFound, "Cannot find the data requested")]
[SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error Logged")]
public class MealTypeController(IMealTypeService mealTypeService) : ControllerBase
{
   
    [HttpGet]
    [SwaggerOperation(Summary = "List meal types", Description = "Lists all meal types")]
    public async Task<ActionResult<List<MealType>>> Get()
    {
        var result = await mealTypeService.GetAllAsync();
        return Ok(result);
    }
}
