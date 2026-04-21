using OpenFreeMapsLoveMatch.Services;

namespace OpenFreeMapsLoveMatch.Tests
{
    public class CityLocationTest
    {
        [Fact]
        public async Task TestGetCity_ReturnsCity()
        {
            //Arrange
         
            var locationService = new LocationService(new HttpClient());

            double lat = 50.8514; // Latitude for Maastricht
            double lon = 5.6900; // Longitude for Maastricht
            
            //Act
            var city = await locationService.GetCityAsync(lat, lon);

            //Assert 
            Assert.False(string.IsNullOrEmpty(city));
            Assert.NotEqual("Unknown", city);
        }
    }
}
