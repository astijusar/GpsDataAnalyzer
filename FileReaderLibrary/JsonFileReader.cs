using FileReaderLibrary.Interfaces;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace FileReaderLibrary
{
    public class JsonFileReader<T> : IFileReader<T>
    {
        public List<T> ReadFile(string filePath)
        {
            var data = new List<T>();

            try
            {
                using (var fs = File.OpenRead(filePath))
                {
                    using (var json = JsonDocument.Parse(fs))
                    {
                        foreach (var jsonElement in json.RootElement.EnumerateArray())
                        {
                            try
                            {
                                var item = JsonSerializer.Deserialize<T>(jsonElement.GetRawText());
                                data.Add(item);
                            }
                            catch (JsonException ex)
                            {
                                Console.WriteLine($"Error deserializing JSON: {ex.Message}");
                            }
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"An error occurred while reading the JSON file: {ex.Message}");
            }

            return data;
        }
    }
}
