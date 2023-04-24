using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpsDataAnalyzer.Models
{
    public class RoadSection
    {
        public Tuple<double, double> StartPos { get; set; }
        public Tuple<double, double> EndPos { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double AverageSpeed { get; set; }
    }
}
