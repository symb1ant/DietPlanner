using DietPlanner.Contracts.Models;
using DietPlanner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DietPlanner.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DietController(IDietService dietService) : ControllerBase
{

    [HttpGet("{id}")]
    public async Task<ActionResult<List<ViewDietSummary>>> Get(string id)
    {
        var result = await dietService.GetSummaryByUser(id);
        return Ok(result);
    }
}
