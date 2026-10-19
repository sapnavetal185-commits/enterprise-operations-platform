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
           [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
                CancellationToken cancellationToken=default)
    {
        // Validate pagination parameters
        if (pageNumber < 1 || pageSize < 1 || pageSize > 100)
        {
            return BadRequest(new
            {
                message = "Page number must be at least 1, and page size must be between 1 and 100."
            });
        }
        // Count all airport records
        var totalRecords = await _context.Airports
            .CountAsync(cancellationToken);

        // Fetch only the requested page
        var airports = await _context.Airports
            .AsNoTracking()
            .OrderBy(a => a.AirportCode)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var totalPages = (int)Math.Ceiling(
            totalRecords / (double)pageSize);

        return Ok(new
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalRecords = totalRecords,
            TotalPages = totalPages,
            Data = airports
        });
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
