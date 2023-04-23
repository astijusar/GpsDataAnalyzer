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
        // TODO: read file with buffer
        public List<T> ReadFile(string filePath)
        {
            var data = new List<T>();

            try
            {
                using (var sr = new StreamReader(filePath))
                {
                    string jsonData = sr.ReadToEnd();

                    try
                    {
                        data = JsonSerializer.Deserialize<List<T>>(jsonData);
                    }
                    catch (JsonException ex)
                    {
                        Console.WriteLine($"Error deserializing JSON: {ex.Message}");
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
