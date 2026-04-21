using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using OpenFreeMapsLoveMatch.Data;
using OpenFreeMapsLoveMatch.Models;
using OpenFreeMapsLoveMatch.Services;
using OpenFreeMapsLoveMatch.Helpers;
using Microsoft.EntityFrameworkCore;

namespace OpenFreeMapsLoveMatch.Controllers
{
    // Tim 12-04-2026: Deze controller beheert de locatiegegevens van profielen. Het bevat een endpoint om de locatie van een profiel bij te werken en een endpoint om nabijgelegen gebruikers op te halen op basis van hun locatie.



    [ApiController]
    [Route("api/location")]
    public class LocationController : ControllerBase
    {
        private readonly LocationService _locationService; 

        public LocationController(LocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpPost("reverse")] // Ontangt coördinaten en retourneert de bijbehorende stadnaam
        public async Task<IActionResult> ReverseGeocode([FromBody] ProfileLocationDTO dto)
        {
            var city = await _locationService.GetCityAsync(dto.Latitude, dto.Longitude);

            return Ok(new { city });

        }
        [HttpGet("city")] // Ontangt een stadnaam en retourneert de bijbehorende coördinaten
        public async Task<IActionResult> GetCoordinatesFromCity([FromQuery] string city)
        {
            var (lat, lon) = await _locationService.GetCoordinatesAsync(city);

            if (lat == 0 && lon == 0)
                return BadRequest("Location not found");

            return Ok(new
            {
                city = city,
                latitude = Math.Round(lat, 4), // Rond af op 4 decimalen
                longitude = Math.Round(lon, 4)
            });
        }
    }
}
