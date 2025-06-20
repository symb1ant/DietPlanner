using DietPlanner.Contracts.Models;
using DietPlanner.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DietPlanner.API.Controllers;
[Route("api/[controller]")]
[SwaggerResponse(StatusCodes.Status200OK, "Success")]
[SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data sent to the api ")]
[SwaggerResponse(StatusCodes.Status404NotFound, "Cannot find the data requested")]
[SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error Logged")]
[ApiController]
public class DietController(IDietService dietService) : ControllerBase
{

    [HttpPost]
    [SwaggerOperation(Summary = "Create a diet entry", Description = "Adds a new entry to the food diary")]
    public async Task<ActionResult<bool>> Post(AddDietEntry entry)
    {
        var result = await dietService.AddEntry(entry);

        if (result == false)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPatch]
    [SwaggerOperation(Summary = "Update a diet entry", Description = "Updates an existing food diary entry")]
    public async Task<ActionResult<bool>> Patch(UpdateDietEntry entry)
    {
        var result = await dietService.UpdateEntry(entry);
        
        if (result == false)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPost("delete")]
    [SwaggerOperation(Summary = "Delete a diet entry", Description = "Deletes an existing food diary entry")]
    public async Task<ActionResult<bool>> Delete(DeleteEntry entry)
    {
        var result = await dietService.DeleteEntry(entry);
        
        if (result == false)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpGet("index/{userid}/{date}")]
    [SwaggerOperation(Summary = "Get diary entry list for date", Description = "Gets a list of food diary entries for a user for specific date")]
    public async Task<ActionResult<List<ViewDietEntry>>> Index(string userid, DateTime date)
    {
        var result = await dietService.GetEntriesByDate(userid, date);
        return Ok(result);
    }

    [HttpGet("index/{userid}")]
    [SwaggerOperation(Summary = "Get entry list for user", Description = "Gets a list of all users food diary entries")]
    public async Task<ActionResult<List<ViewDietEntry>>> Index(string userid)
    {
        var result = await dietService.GetEntries(userid);
        return Ok(result);
    }

    [HttpGet("summary/{userid}")]
    [SwaggerOperation(Summary = "Get summary list for user", Description = "Gets a summary list of diary entries for a specific user")]
    public async Task<ActionResult<List<ViewDietSummary>>> GetSummary(string userid)
    {
        var result = await dietService.GetSummaryByUser(userid);
        return Ok(result);
    }

    [HttpGet("summary/{userid}/{date}")]
    [SwaggerOperation(Summary = "Get summary list for date", Description = "Gets a summary list of diary entries for a specific date and user")]
    public async Task<ActionResult<List<ViewDietSummary>>> GetSummary(string userid, DateTime date)
    {
        var result = await dietService.GetSummaryByDate(userid, date);
        return Ok(result);
    }


    [HttpGet("get/{id}")]
    [SwaggerOperation(Summary = "Get single summary", Description = "Gets a single entry by ID")]
    public async Task<ActionResult<ViewDietEntry>> Get(int id)
    {
        var result = await dietService.GetEntry(id);
        return Ok(result);
    }
}
