using FileReaderLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace FileReaderLibrary
{
    public class CsvFileReader<T> : IFileReader<T>
    {
        private readonly ICsvParser<T> _parser;

        public CsvFileReader(ICsvParser<T> parser)
        {
            _parser = parser;
        }

        public List<T> ReadFile(string filePath)
        {
            var data = new List<T>();

            using (var sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();

                    var obj = _parser.ParseLine(line);

                    data.Add(obj);
                }
            }

            return data;
        }
    }
}
