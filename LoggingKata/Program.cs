using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;
using System.Reflection.PortableExecutable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";
        const double metersToMiles = 0.00062137;

        static void Main(string[] args)
        {
          
            logger.LogInfo("Log initialized");

            var lines = File.ReadAllLines(csvPath);

            logger.LogInfo($"Lines: {lines[0]}");

            var parser = new TacoParser();

            var locations = lines.Select(parser.Parse).ToArray();

            ITrackable tacoBell1 = null;
            ITrackable tacoBell2 = null;
            double finalDistance = 0;
            double testDistance = 0;
            var geo1 = new GeoCoordinate();
            var geo2 = new GeoCoordinate();

            for (int i = 0; i < locations.Length; i++)
            {
                geo1.Latitude = locations[i].Location.Latitude;
                geo1.Longitude = locations[i].Location.Longitude;

                for (int j = 1; j < locations.Length; j++)
                {
                    geo2.Latitude = locations[j].Location.Latitude;
                    geo2.Longitude = locations[j].Location.Longitude;
            
                testDistance = geo1.GetDistanceTo(geo2);
                    if (finalDistance < testDistance)
                    {
                        finalDistance = testDistance;
                        tacoBell1 = locations[i];
                        tacoBell2 = locations[j];
                    }
                }
            }
            if (tacoBell1 != null && tacoBell2 != null)
            {
                logger.LogInfo($"The furthest apart locations are:");
                logger.LogInfo($"Location 1: {tacoBell1.Name}");
                logger.LogInfo($"Location 1: {tacoBell2.Name}");
                logger.LogInfo($"The distance is {finalDistance * metersToMiles} miles.");
                
            }
            else
            {
                logger.LogInfo("Unable to determine the distance.");
            }
                
        }
    }
}
