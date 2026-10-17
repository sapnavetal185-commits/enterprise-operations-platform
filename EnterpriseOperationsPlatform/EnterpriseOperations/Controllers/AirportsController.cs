using EnterpriseOperations.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseOperations.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AirportsController : ControllerBase
{
    private readonly TravelHubDbContext _context;

    public AirportsController(TravelHubDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAirports(
        CancellationToken cancellationToken)
    {
        var airports = await _context.Airports
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return Ok(airports);
    }
    [HttpGet("{airportCode}")]
    public async Task<IActionResult> GetAirportByCode(
    string airportCode,
    CancellationToken cancellationToken)
    {
        var airport = await _context.Airports
            .AsNoTracking()
            .FirstOrDefaultAsync(
                a => a.AirportCode == airportCode,
                cancellationToken);

        if (airport == null)
        {
            return NotFound(new
            {
                message = $"Airport '{airportCode}' was not found."
            });
        }

        return Ok(airport);
    }
}
