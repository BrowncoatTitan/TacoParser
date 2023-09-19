using System.Runtime;

namespace LoggingKata
{
    public struct Point
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public Point(double lat, double lon)
        {
            Latitude = lat;
            Longitude = lon;
        }

    }
}