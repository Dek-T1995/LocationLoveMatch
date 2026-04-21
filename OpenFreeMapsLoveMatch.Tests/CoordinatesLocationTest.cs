using OpenFreeMapsLoveMatch.Services;
namespace OpenFreeMapsLoveMatch.Tests
{
    public class CoordinatesLocationTest
    {
        [Fact]
        public async Task TestGetCity_ReturnsCoordinates()
        {
            //Arrange
            var httpClient = new HttpClient();
            var locationService = new LocationService(httpClient);

            //Act
            var (lat, lon) = await locationService.GetCoordinatesAsync("Maastricht"); 

            //Assert 
            Assert.InRange(lat, 50.84, 50.86); // Coördinaten bereik van Maastricht
            Assert.InRange(lon, 5.69, 5.71);

        }
    }
}
