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






//    [ApiController]
//    [Route("api/profiles")]
//    public class ProfileLocationController : ControllerBase
//    {
//        private readonly AppDbContext _context;
//        private readonly LocationService _locationService;

//        public ProfileLocationController(AppDbContext context, LocationService locationService)
//        {
//            _context = context;
//            _locationService = locationService;
//        }



//        [HttpPost("{id}/location")]
//        public async Task<IActionResult> UpdateLocation(int id, [FromBody] ProfileLocationDTO dto)
//        {
//            var profile = await _context.Profiles.FindAsync(id);
//            if (profile == null) return NotFound();

//            var city = await _locationService.GetCityAsync(dto.Latitude, dto.Longitude);

//            profile.Latitude = Math.Round(dto.Latitude, 2);
//            profile.Longitude = Math.Round(dto.Longitude, 2);
//            profile.City = city;

//            await _context.SaveChangesAsync();

//            return Ok(profile);
//        }

//        [HttpGet("nearby")]
//        public async Task<IActionResult> GetNearbyUsers(double lat, double lon, double radiusKm = 25)
//        {
//            var profile = await _context.Profiles.ToListAsync();

//            var nearbyProfiles = profile
//                .Select(u => new
//                {
//                    User = u,
//                    Distance = DistanceHelper.CalculateDistance(lat, lon, u.Latitude, u.Longitude)
//                })
//                .Where(x => x.Distance <= radiusKm)
//                .Select(x => new
//                {
//                    x.User.Id,
//                    x.User.Name,
//                    x.User.City,
//                    Distance = Math.Round(x.Distance, 1)
//                });

//            return Ok(nearbyProfiles);
//        }
//    }
//}

