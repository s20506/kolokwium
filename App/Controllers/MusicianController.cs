using System.Net;
using APBD_5.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers;

[ApiController]
[Route("api/musicians")]
public class MusicianController : ControllerBase
{
    private readonly IDbService _dbService;

    public MusicianController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet("{idMusician}")]
    public async Task<IActionResult> GetMusician(int idMusician)
    {
        try
        {
            var musician = await _dbService.GetMusician(idMusician);
    
            if (musician == null) return NotFound("Musician not found!");
    
            return Ok(musician);
        }
        catch (Exception exception)
        {
            return Conflict(exception.Message);
        }
    }

    [HttpDelete("{idMusician}")]
    public async Task<IActionResult> DeleteMusician(int idMusician)
    {
        try
        {
            await _dbService.DeleteMusician(idMusician);

            return Ok();
        }
        catch (HttpRequestException httpException)
        {
            if (httpException.StatusCode == HttpStatusCode.BadRequest) return BadRequest(httpException.Message);
            if (httpException.StatusCode == HttpStatusCode.NotFound) return NotFound(httpException.Message);
            
            return Conflict(httpException.Message);
        }
        catch (Exception exception)
        {
            return Conflict(exception.Message);
        }
    }
}