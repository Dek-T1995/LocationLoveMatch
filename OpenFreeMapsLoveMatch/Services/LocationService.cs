using Newtonsoft.Json;
using System.Globalization;


namespace OpenFreeMapsLoveMatch.Services
{
    public class LocationService
    {

        private readonly HttpClient _httpClient;

        public LocationService(HttpClient httpClient) // De HttpClient wordt geïnjecteerd via de constructor. Deze client zal worden gebruikt om verzoeken te doen aan de OpenFreeMaps API.
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "DatingApp/1.0"); 
        }

        public async Task<string> GetCityAsync(double lat, double lon) // Ontvangt coördinaten en retourneert de bijbehorende stadnaam
        {
            var url = $"https://nominatim.openstreetmap.org/reverse?lat={lat.ToString(CultureInfo.InvariantCulture)}&lon={lon.ToString(CultureInfo.InvariantCulture)}&format=json"; // URL voor de reverse geocoding API van OpenFreeMaps, latitude en longitude worden meegegeven als parameters.
            Console.WriteLine(url);

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync(); 

            dynamic data = JsonConvert.DeserializeObject(json); 

            return data.address.city
                ?? data.address.town
                ?? data.address.village
                ?? "Unknown";
        }

        public async Task<(double lat, double lon)> GetCoordinatesAsync(string city) // Ontvangt een stadnaam en retourneert de bijbehorende coördinaten
        {
            var url = $"https://nominatim.openstreetmap.org/search?q={city}&format=json&limit=1";
            Console.WriteLine(url);

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return (0, 0);

            var json = await response.Content.ReadAsStringAsync();

            dynamic data = JsonConvert.DeserializeObject(json);

            if (data.Count == 0)
                return (0, 0);

            double lat = double.Parse((string)data[0].lat, CultureInfo.InvariantCulture); // Gebruik InvariantCulture om te zorgen dat de decimale scheidingsteken correct wordt geïnterpreteerd, ongeacht de locale instellingen
            double lon = double.Parse((string)data[0].lon, CultureInfo.InvariantCulture);

            return (lat, lon);
        }
    }
}