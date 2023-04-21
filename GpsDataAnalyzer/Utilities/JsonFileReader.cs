using GpsDataAnalyzer.Models;
using GpsDataAnalyzer.Utilities.Interfaces;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GpsDataAnalyzer.Utilities
{
    public class JsonFileReader<T> : IFileReader<T>
    {
        // TODO: read file with buffer
        public List<T> ReadFile(string filePath)
        {
            var data = new List<T>();

            using (var sr = new StreamReader(filePath))
            {
                string jsonData = sr.ReadToEnd();

                var jsonOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                try
                {
                    data = JsonSerializer.Deserialize<List<T>>(jsonData, jsonOptions);
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Error deserializing JSON: {ex.Message}");
                }

                return data;
            }
        }
    }
}
