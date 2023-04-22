using GpsDataAnalyzer.Models;
using GpsDataAnalyzer.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace GpsDataAnalyzer.Utilities
{
    // should be a generic class
    public class BinaryParser : IBinaryParser<GpsData>
    {
        public GpsData ParseBytes(byte[] bytes)
        {
            var gpsData = new GpsData();

            using (var stream = new MemoryStream(bytes))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    gpsData.Latitude = convertToCoordinates(reader.ReadBytes(4));
                    gpsData.Longitude = convertToCoordinates(reader.ReadBytes(4));

                    var timestamp = convertToLong(reader.ReadBytes(8));
                    var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    gpsData.GpsTime = epoch.AddMilliseconds(timestamp);

                    gpsData.Speed = reader.ReadUInt16();
                    gpsData.Angle = reader.ReadUInt16();
                    gpsData.Altitude = reader.ReadUInt16();
                    gpsData.Satellites = reader.ReadByte();
                }
            }

            return gpsData;
        }

        private double convertToCoordinates(byte[] bytes)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            int val = BitConverter.ToInt32(bytes, 0);

            return val / 10000000.0;
        }

        private long convertToLong(byte[] bytes)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            long val = BitConverter.ToInt64(bytes, 0);

            return val;
        }
    }
}
