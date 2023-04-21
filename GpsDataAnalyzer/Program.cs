using GpsDataAnalyzer.Models;
using GpsDataAnalyzer.Utilities;
using GpsDataAnalyzer.Utilities.Interfaces;
using System;
using System.Collections.Generic;

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

            var jsonFileReader = new JsonFileReader<GpsData>();

            var csvParser = new CsvParser<GpsData>();
            var csvFileReader = new CsvFileReader<GpsData>(csvParser);

            var binaryParser = new BinaryParser();
            var binaryFileReader = new BinaryFileReader(binaryParser);

            gpsData.AddRange(jsonFileReader.ReadFile(JSON_FILE));
            gpsData.AddRange(csvFileReader.ReadFile(CSV_FILE));
            gpsData.AddRange(binaryFileReader.ReadFile(BINARY_FILE));
        }
    }
}
