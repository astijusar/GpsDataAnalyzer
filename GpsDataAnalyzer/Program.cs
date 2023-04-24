using ConsoleChartLibrary;
using FileReaderLibrary;
using GpsDataAnalyzer.Models;
using GpsDataAnalyzer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GpsDataAnalyzer
{
    public class Program
    {
        static void Main(string[] args)
        {
            const string JSON_FILE = "2019-07.json";
            const string CSV_FILE = "2019-08.csv";
            const string BINARY_FILE = "2019-09.bin";

            var gpsData = new List<GpsData>();
            var binFileReader = new BinaryFileReader();
            var fileReaderManager = new FileReaderManager<GpsData>();

            gpsData.AddRange(fileReaderManager.JsonFile.ReadFile(JSON_FILE));
            gpsData.AddRange(fileReaderManager.CsvFile.ReadFile(CSV_FILE));
            gpsData.AddRange(binFileReader.ReadFile(BINARY_FILE));

            Histogram<int, int> sateliteHistogram = new Histogram<int, int>()
            {
                Data = gpsData.Select(x => x.Satellites)
                    .GroupBy(n => n)
                    .ToDictionary(g => g.Key, g => g.Count()),
                Options = new HistogramOptions()
                {
                    Title = "Satelite histogram",
                    XLabel = "Satellites",
                    YLabel = "Hits",
                    BinCount = 20
                }
            };

            Histogram<int, int> speedHistogram = new Histogram<int, int>()
            {
                Data = gpsData.Select(x => x.Speed)
                    .GroupBy(n => n)
                    .ToDictionary(g => g.Key, g => g.Count()),
                Options = new HistogramOptions()
                {
                    Title = "Speed histogram",
                    XLabel = "Hits",
                    YLabel = "Speed"
                }
            };

            Console.WriteLine();

            sateliteHistogram.Render();

            Console.Write(new string('-', Console.WindowWidth));
            Console.WriteLine();
            Console.WriteLine();

            speedHistogram.Render();

            Console.WriteLine();
            Console.WriteLine();

            FindShortestTime.Find(gpsData);
        }
    }
}
