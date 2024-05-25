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
    public async Task<ActionResult<bool>> Patch(UpdateDietEntry entry)
    {
        var result = await dietService.UpdateEntry(entry);
        
        if (result == false)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpDelete]
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
    public async Task<ActionResult<List<ViewDietEntry>>> Index(string userid, DateTime date)
    {
        var result = await dietService.GetEntriesByDate(userid, date);
        return Ok(result);
    }

    [HttpGet("index/{userid}")]
    public async Task<ActionResult<List<ViewDietEntry>>> Index(string userid)
    {
        var result = await dietService.GetEntries(userid);
        return Ok(result);
    }

    [HttpGet("summary/{userid}")]
    public async Task<ActionResult<List<ViewDietSummary>>> GetSummary(string userid)
    {
        var result = await dietService.GetSummaryByUser(userid);
        return Ok(result);
    }

    [HttpGet("summary/{userid}/{date}")]
    public async Task<ActionResult<List<ViewDietSummary>>> GetSummary(string userid, DateTime date)
    {
        var result = await dietService.GetSummaryByDate(userid, date);
        return Ok(result);
    }


    [HttpGet("get/{id}")]
    public async Task<ActionResult<ViewDietEntry>> Get(int id)
    {
        var result = await dietService.GetEntry(id);
        return Ok(result);
    }
}
