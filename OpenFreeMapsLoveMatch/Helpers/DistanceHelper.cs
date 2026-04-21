namespace OpenFreeMapsLoveMatch.Helpers
{
    public static class DistanceHelper
    {   // Tim 12-04-2026: Deze helperklasse bevat een methode om de afstand tussen twee geografische coördinaten te berekenen, wat nuttig is voor het matchen van profielen op basis van locatie.
        public static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            var R = 6371; // km

            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);

            var a =
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c;
        }

        private static double ToRadians(double angle)
        {
            return angle * Math.PI / 180;
        }

    }
}