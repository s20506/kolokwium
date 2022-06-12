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
}