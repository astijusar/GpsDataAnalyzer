using Geolocation;
using GpsDataAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpsDataAnalyzer.Utilities
{
    public class FindShortestTime
    {
        public static void Find(List<GpsData> data)
        {
            if (data == null || data.Count < 2)
            {
                Console.WriteLine("No data available.");
                return;
            }

            data.Sort((d1, d2) => DateTime.Compare(d1.GpsTime, d2.GpsTime));

            var startIdx = 0;
            var endIdx = 1;
            var lenght = 0.0;
            var finalLenght = 0.0;
            var shortestTime = double.MaxValue;
            var result = new RoadSection();

            while (endIdx < data.Count && startIdx < data.Count - 1)
            {
                if (lenght < 100)
                {
                    lenght += GeoCalculator.GetDistance(data[endIdx - 1].Latitude, data[endIdx - 1].Longitude,
                        data[endIdx].Latitude, data[endIdx].Longitude, 1, DistanceUnit.Kilometers);

                    endIdx++;
                }
                else
                {
                    TimeSpan timeSpan = data[endIdx].GpsTime - data[startIdx].GpsTime;
                    var currentTime = timeSpan.TotalSeconds;

                    if (currentTime < shortestTime)
                    {
                        shortestTime = currentTime;
                        finalLenght = lenght;
                        result.StartPos = new Tuple<double, double>(data[startIdx].Latitude, data[startIdx].Longitude);
                        result.EndPos = new Tuple<double, double>(data[endIdx].Latitude, data[endIdx].Longitude);
                        result.StartTime = data[startIdx].GpsTime;
                        result.EndTime = data[endIdx].GpsTime;
                        result.AverageSpeed = lenght / (currentTime / 3600);
                    }
                       
                    startIdx++;

                    lenght -= GeoCalculator.GetDistance(data[startIdx - 1].Latitude, data[startIdx - 1].Longitude,
                        data[startIdx].Latitude, data[startIdx].Longitude, 1, DistanceUnit.Kilometers);
                }
            }

            if (shortestTime == double.MaxValue)
            {
                Console.WriteLine("No valid data found.");
                return;
            }

            Console.WriteLine($"Fastest road section of at least 100km was driven over {shortestTime}s and was {string.Format("{0:0.000}", finalLenght)}km long.");
            Console.WriteLine("Start Position: " + result.StartPos.Item1 + ", " + result.StartPos.Item2);
            Console.WriteLine("Start Time: " + result.StartTime);
            Console.WriteLine("End Position: " + result.EndPos.Item1 + ", " + result.EndPos.Item2);
            Console.WriteLine("End Time: " + result.EndTime);
            Console.WriteLine("Average Speed: " + result.AverageSpeed);
        }
    }
}
