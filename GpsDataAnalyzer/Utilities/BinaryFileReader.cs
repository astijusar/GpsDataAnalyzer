using GpsDataAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace GpsDataAnalyzer.Utilities
{
    public class BinaryFileReader
    {
        public List<GpsData> ReadFile(string filePath)
        {
            var data = new List<GpsData>();

            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var binaryReader = new BinaryReader(fileStream))
                    {
                        while (binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
                        {
                            GpsData gpsData = new GpsData();

                            gpsData.Latitude = BitConverter.ToInt32(ReverseBytes(binaryReader.ReadBytes(4))) / 10000000.0;
                            gpsData.Longitude = BitConverter.ToInt32(ReverseBytes(binaryReader.ReadBytes(4))) / 10000000.0;

                            var milliseconds = BitConverter.ToInt64(ReverseBytes(binaryReader.ReadBytes(8)));
                            gpsData.GpsTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(milliseconds);

                            gpsData.Speed = BitConverter.ToInt16(ReverseBytes(binaryReader.ReadBytes(2)));
                            gpsData.Angle = BitConverter.ToInt16(ReverseBytes(binaryReader.ReadBytes(2)));
                            gpsData.Altitude = BitConverter.ToInt16(ReverseBytes(binaryReader.ReadBytes(2)));
                            gpsData.Satellites = binaryReader.ReadByte();

                            data.Add(gpsData);
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"An error occurred while reading the binary file: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }

            return data;
        }

        private byte[] ReverseBytes(byte[] data)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(data);
            }

            return data;
        }
    }
}
