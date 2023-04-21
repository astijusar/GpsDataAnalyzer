using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpsDataAnalyzer.Models
{
    public class GpsData
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime GpsTime { get; set; }
        public int Speed { get; set; }
        public int Angle { get; set; }
        public int Altitude { get; set; }
        public int Satelites { get; set; }

        public GpsData(double latitude, double longitude, DateTime gpsTime, int speed, int angle, int altitude, int satelites)
        {
            Latitude = latitude;
            Longitude = longitude;
            GpsTime = gpsTime;
            Speed = speed;
            Angle = angle;
            Altitude = altitude;
            Satelites = satelites;
        }
    }
}
