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
            var fileReaderManager = new FileReaderManager<GpsData>();

            gpsData.AddRange(fileReaderManager.JsonFile.ReadFile(JSON_FILE));
            gpsData.AddRange(fileReaderManager.CsvFile.ReadFile(CSV_FILE));
            gpsData.AddRange(fileReaderManager.BinaryFile.ReadFile(BINARY_FILE));
        }
    }
}
