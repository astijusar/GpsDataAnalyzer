using GpsDataAnalyzer.Models;
using GpsDataAnalyzer.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace GpsDataAnalyzer.Utilities
{
    public class BinaryFileReader : IFileReader<GpsData>
    {
        private readonly IBinaryParser<GpsData> _parser;

        public BinaryFileReader(IBinaryParser<GpsData> parser)
        {
            _parser = parser;
        }

        public List<GpsData> ReadFile(string filePath)
        {
            var data = new List<GpsData>();

            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var binaryReader = new BinaryReader(fileStream))
                {
                    while (binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
                    {
                        byte[] bytes = binaryReader.ReadBytes(23);

                        try
                        {
                            GpsData obj = _parser.ParseBytes(bytes);
                            data.Add(obj);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }

            return data;
        }
    }
}
