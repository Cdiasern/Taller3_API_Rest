using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taller3_API_Rest.Services;

namespace Taller3_API_Rest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HolidaysController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HolidaysController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("is-holiday")]
        public async Task<IActionResult> IsHoliday([FromQuery] string date)
        {
            if (!DateTime.TryParseExact(date, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
            {
                return BadRequest("Fecha no válida.");
            }

            int year = parsedDate.Year;

            var holidays = await _context.Holidays.ToListAsync();
            foreach (var holiday in holidays)
            {
                DateTime? holidayDate = HolidayCalculator.CalculateHolidayDate(holiday, year);
                if (holidayDate.HasValue && holidayDate.Value.Date == parsedDate.Date)
                {
                    return Ok(new { IsHoliday = true, holiday.Name });
                }
            }

            return Ok(new { IsHoliday = false });
        }

        [HttpGet("get-holidays")]
        public async Task<IActionResult> GetHolidays([FromQuery] int year)
        {
            if (year < 1900 || year > 2100)
            {
                return BadRequest("El año proporcionado no es válido.");
            }

            var holidays = await _context.Holidays.ToListAsync();
            var holidayDates = holidays
                .Select(h => new
                {
                    h.Name,
                    Date = HolidayCalculator.CalculateHolidayDate(h, year)
                })
                .Where(h => h.Date.HasValue)
                .Select(h => new
                {
                    h.Name,
                    Date = h.Date.Value.ToString("dd/MM/yyyy")
                })
                .ToList();

            return Ok(holidayDates);
        }
    }
}